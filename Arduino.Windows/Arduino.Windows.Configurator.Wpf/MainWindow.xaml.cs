using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wolf.Utility.Core.Startup.Assist;
using Wolf.Utility.Core.Extensions.Methods;
using Microsoft.Extensions.DependencyInjection;

namespace Arduino.Windows.Configurator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ILoggerManager Logger { get; }
        public MainWindow()
        {
            Logger = App.ModularStartup.ServiceProvider.GetService<ILoggerManager>();

            InitializeComponent();
                        
            Logger.SetCaller(GetType().FullName);

            Logger.LogInfo($"MainWindow Initialized");
        }

        
    }
}
