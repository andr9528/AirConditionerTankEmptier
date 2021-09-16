using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Arduino.Windows.Configurator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Startup ModularStartup { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            ModularStartup = new Startup();

            base.OnStartup(e);
        }
    }
}
