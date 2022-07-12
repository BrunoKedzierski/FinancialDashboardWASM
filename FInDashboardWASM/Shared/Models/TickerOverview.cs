using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInDashboardWASM.Shared.Models
{
    public class TickerOverview
    {

     
            public string Ticker { get; set; }
            public string Name { get; set; }
            public string Locale { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is TickerOverview overview &&
                   Ticker == overview.Ticker &&
                   Name == overview.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Ticker, Name);
        }
    }
}
