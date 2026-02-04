using System;

namespace Conduit
{
    class Program
    {
        public static string APP_NAME = "Cept! Auto Accept";
        public static string VERSION = "2.2.0";

        private static App _instance;

        [STAThread]
        public static void Main()
        {
            // Start the application.
            _instance = new App();
            _instance.InitializeComponent();
            _instance.Run();
        }
    }
}
