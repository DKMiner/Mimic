using System.Windows.Forms;

namespace Conduit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private NotifyIcon icon;
        private MenuItem autoAcceptMenuItem;
        private AutoAcceptManager autoAcceptManager;

        public App()
        {
            autoAcceptMenuItem = new MenuItem("Auto Accept")
            {
                Checked = false
            };

            icon = new NotifyIcon
            {
                Text = "Cept! Auto Accept",
                Icon = Conduit.Properties.Resources.cept,
                Visible = true,
                ContextMenu = new ContextMenu(new []
                {
                    new MenuItem(Program.APP_NAME + " " + Program.VERSION)
                    {
                        Enabled = false
                    },
                    autoAcceptMenuItem,
                    new MenuItem("Quit", (a, b) => Shutdown())
                })
            };

            autoAcceptManager = new AutoAcceptManager();
            autoAcceptManager.AutoAcceptChanged += UpdateAutoAcceptMenuItem;
            UpdateAutoAcceptMenuItem(autoAcceptManager.AutoAcceptEnabled);

            autoAcceptMenuItem.Click += (sender, args) =>
            {
                autoAcceptManager.SetAutoAccept(!autoAcceptManager.AutoAcceptEnabled);
            };
        }

        /**
         * Updates the menu item based on the current auto-accept state.
         */
        private void UpdateAutoAcceptMenuItem(bool enabled)
        {
            autoAcceptMenuItem.Checked = enabled;
        }

        /**
         * Shows a simple notification with the specified text for 5 seconds.
         */
        public void ShowNotification(string text)
        {
            icon.BalloonTipTitle = "Cept! Auto Accept";
            icon.BalloonTipText = text;
            icon.ShowBalloonTip(5000);
        }
    }
}
