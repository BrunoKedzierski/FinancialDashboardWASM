using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInDashboardWASM.Shared.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class OHLC
    {

        [Key]
        public int Id { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime DateOn { get; set; }

        [JsonProperty("open")]
        [Required]
        public double Open { get; set; }

        [JsonProperty("o")]
        private double open2 { set { Open = value; } }


        [JsonProperty("low")]
        [Required]
        public double Low { get; set; }

        [JsonProperty("l")]
        private double low2 { set { Low = value; } }


        [JsonProperty("high")]
        [Required]
        public double High { get; set; }

        [JsonProperty("h")]
        private double high2 { set { High = value; } }

        [JsonProperty("close")]
        [Required]
        public double Close { get; set; }

        [JsonProperty("c")]
        private double close2 { set { Close = value; } }


        public virtual CompanyData CompanyData { get; set; }
    }
}
