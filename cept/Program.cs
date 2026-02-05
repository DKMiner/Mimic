using System;

namespace Cept
{
    class Program
    {
        public static string APP_NAME = "Cept! Auto Accept";
        public static string VERSION = "2.1.0";

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
