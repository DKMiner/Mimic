using System;
using System.Threading;

namespace Conduit
{
    class AutoAcceptManager
    {
        private readonly LeagueConnection leagueConnection;
        private readonly object timerLock = new object();
        private Timer autoAcceptTimer;
        private bool autoAcceptEnabled;

        public event Action<bool> AutoAcceptChanged;

        public bool AutoAcceptEnabled => autoAcceptEnabled;

        public AutoAcceptManager()
        {
            leagueConnection = new LeagueConnection();
            leagueConnection.OnDisconnected += () => ClearAutoAcceptTimer();

            leagueConnection.Observe("/lol-matchmaking/v1/ready-check", HandleReadyCheckUpdate);
            leagueConnection.Observe("/lol-gameflow/v1/session", HandleGameflowUpdate);
        }

        public void SetAutoAccept(bool enabled)
        {
            autoAcceptEnabled = enabled;
            if (!enabled)
            {
                ClearAutoAcceptTimer();
            }
            else
            {
                RefreshReadyCheckState();
            }

            AutoAcceptChanged?.Invoke(autoAcceptEnabled);
        }

        private void HandleReadyCheckUpdate(dynamic data)
        {
            if (!autoAcceptEnabled || data == null)
            {
                ClearAutoAcceptTimer();
                return;
            }

            string state = data["state"];
            string playerResponse = data["playerResponse"];

            if (state != "InProgress")
            {
                ClearAutoAcceptTimer();
                return;
            }

            if (playerResponse == "Accepted" || playerResponse == "Declined")
            {
                ClearAutoAcceptTimer();
                return;
            }

            ScheduleAutoAccept();
        }

        private void HandleGameflowUpdate(dynamic data)
        {
            if (data == null)
            {
                return;
            }

            string phase = data["phase"];
            if (phase == "InProgress" && autoAcceptEnabled)
            {
                SetAutoAccept(false);
            }
        }

        private void ScheduleAutoAccept()
        {
            lock (timerLock)
            {
                if (autoAcceptTimer != null)
                {
                    return;
                }

                autoAcceptTimer = new Timer(_ => AcceptReadyCheck(), null, TimeSpan.FromSeconds(8), Timeout.InfiniteTimeSpan);
            }
        }

        private void AcceptReadyCheck()
        {
            try
            {
                if (!autoAcceptEnabled)
                {
                    return;
                }

                leagueConnection.Request("POST", "/lol-matchmaking/v1/ready-check/accept", null)
                    .GetAwaiter()
                    .GetResult();
            }
            catch (Exception e)
            {
                DebugLogger.Global.WriteError($"Failed to auto-accept ready check: {e}");
            }
            finally
            {
                ClearAutoAcceptTimer();
            }
        }

        private void ClearAutoAcceptTimer()
        {
            lock (timerLock)
            {
                if (autoAcceptTimer == null)
                {
                    return;
                }

                autoAcceptTimer.Dispose();
                autoAcceptTimer = null;
            }
        }

        private void RefreshReadyCheckState()
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    var data = leagueConnection.Get("/lol-matchmaking/v1/ready-check")
                        .GetAwaiter()
                        .GetResult();
                    HandleReadyCheckUpdate(data);
                }
                catch (Exception e)
                {
                    DebugLogger.Global.WriteError($"Failed to refresh ready check state: {e}");
                }
            });
        }
    }
}
