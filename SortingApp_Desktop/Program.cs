using AutoUpdaterDotNET;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace SortingApp_Desktop
{
    internal static class Program
    {
        public static IConfiguration Configuration { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            ApplicationConfiguration.Initialize();
            string currentVersion = Application.ProductVersion;

            AutoUpdater.Start("https://drive.google.com/uc?export=download&id=1ph6NmDirXxvG39BbvZnxkOsLwIT9W_fL");
            AutoUpdater.Mandatory = true;
            Application.Run(new ConfigProcessId());
        }
    }
}