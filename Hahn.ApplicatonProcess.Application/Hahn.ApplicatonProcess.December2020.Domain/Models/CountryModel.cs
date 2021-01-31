using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Models
{
    public class CountryModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("nativeName")]
        public string NativeName { get; set; }
    }
}
