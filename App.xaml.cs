using PostSharp.Patterns.Diagnostics;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.IO;
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

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(lls)
                .WriteTo.Console()
                .WriteTo.File(LoggingPath("wpfLog.txt"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            // setting up PostSharp logging for Serilog
            // additionally, "GlobalAspects.cs" file must be added
            // this statement is typically done in the main or StartUp.cs 
            LoggingServices.DefaultBackend = new PostSharp.Patterns.Diagnostics.Backends.Serilog.SerilogLoggingBackend();

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

        private ICommand mUpdate1;
        public ICommand UpdateCommand1
        {
            get
            {
                if (mUpdate1 == null)
                    mUpdate1 = new Updater();
                return mUpdate1;
            }
            set
            {
                mUpdate1 = value;
            }
        }

        public class Updater : ICommand
        {

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {

            }
        }
    }
}