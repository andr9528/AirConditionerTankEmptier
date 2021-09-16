using Arduino.Windows.Configurator.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Persistence.EntityFramework;
using Wolf.Utility.Core.Startup;
using Wolf.Utility.Core.Startup.Modules;

namespace Arduino.Windows.Configurator
{
    public class Startup : ModularStartup
    {
        private const string CONNECTIONSTRINGNAME = "mainDb";
        public Startup() : base()
        {
            AddModule(new NLogStartupModule());
            AddModule(new EntityFrameworkStartupModule<ArduinoRepositry, ArduinoRepositryHandler>(
                options => { options.UseSqlite(Configuration.GetConnectionString(CONNECTIONSTRINGNAME)); }));

            SetupServices();
            SetupApplication();
        }
    }
}
