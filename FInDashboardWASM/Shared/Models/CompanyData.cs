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
    public class CompanyData
    {

        [Key]
        public int Id { get; set; }

        
        public string? LogoLink { get; set; }
        public string? QueryDate { get; set; }


        [Required]
        [JsonProperty("name")] 
        public string Name { get; set; }
        [Required]
        [JsonProperty("primary_exchange")]
        public string Exchange { get; set; }

        [Required]
        [JsonProperty("ticker")]
        public string Ticker { get; set; }

        [Required]
        [JsonProperty("locale")]
        public string Locale { get; set; }




        [Required]
        [JsonProperty("currency_name")]
        public string Currency { get; set; }


        public virtual ICollection<OHLC> OHLCs { get; set; }
        public virtual ICollection<User> Users { get; set; }



    }
}
