using PostSharp.Patterns.Diagnostics;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.IO;
using System.Xml.Linq;
using System.Windows;
using System.Windows.Input;
using WpfApp1.ViewModel;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public LoggingLevelSwitch lls = new LoggingLevelSwitch(LogEventLevel.Debug);

        protected override void OnStartup(StartupEventArgs e)
        {
            // below are hard coded serilog specific logging settings.
            // modify to use specific logging package (i.e., serilog, nlog, log4net)
            // modify to use different file name (not hardcoded)
            // modify to rotate based on size or time duration
            // modify to load settings based on an xml or json configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(lls)
                .WriteTo.Console()
                .WriteTo.File(LoggingPath("wpfLog.txt"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            // setting up PostSharp logging for Serilog
            // additionally, "GlobalAspects.cs" file must be added
            // this statement is typically done in the main or StartUp.cs 
            LoggingServices.DefaultBackend = new PostSharp.Patterns.Diagnostics.Backends.Serilog.SerilogLoggingBackend();

            string configFileName = "PostSharpLoggingConfig.xml";
            var configDocument = XDocument.Load(configFileName);

            // additional logging configuration file to give more granular control to the logging settings
            // modify to use "ConfigureFromXmlWithAutoReloadAsync" to adjust logging settings dynamically
            LoggingConfigurationManager.ConfigureFromXml(LoggingServices.DefaultBackend, configDocument);

            base.OnStartup(e);
            MainWindow window = new MainWindow();
            UserViewModel VM = new UserViewModel();
            window.DataContext = VM;
            window.Show();
        }

        public void changeLoggingLevel()
        {
            lls.MinimumLevel = LogEventLevel.Debug;
        }

        //WpfApp1.App.UpdateCommand1
        public static string LoggingPath(string logFileName)
        {
            string pathStartup = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            string pathToLoggedFileName = Path.Combine(pathStartup, $"assets/logs/{logFileName}");

            return pathToLoggedFileName;
        }
    }
}