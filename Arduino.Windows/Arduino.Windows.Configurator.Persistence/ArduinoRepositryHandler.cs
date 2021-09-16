using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolf.Utility.Core.Persistence.EntityFramework;

namespace Arduino.Windows.Configurator.Persistence
{
    public class ArduinoRepositryHandler : BaseHandler<ArduinoRepositry>
    {
        public ArduinoRepositryHandler(ArduinoRepositry context) : base(context)
        {
        }

        protected override Task<IQueryable<T>> AbstractFind<T>(T predicate)
        {
            throw new NotImplementedException();
        }
    }
}
