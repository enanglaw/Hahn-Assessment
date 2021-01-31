using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services
{
    public class CountryValidatorService
    {
        private readonly string countriesApiBase = "https://restcountries.eu/rest/v2";
        public CountryValidatorService()
        {
        }

        public async Task<(bool isValid, string countryName)> ValidateCountryName(string name)
        {
            try
            {
                var nameUrl = $"{countriesApiBase}/name/{name}?fullText=true";
                using (var http = new HttpClient())
                {
                    http.BaseAddress = new Uri(nameUrl);
                    http.DefaultRequestHeaders.Accept.Clear();
                    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await http.GetAsync(nameUrl);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                        return (false, string.Empty);
                    var countries = JsonConvert.DeserializeObject<List<CountryModel>>(responseContent);
                    if (countries?.Count > 0)
                        return (true, countries.FirstOrDefault()?.Name);
                    return (false, string.Empty);
                }
            }
            catch (Exception exception)
            {
                return (false, string.Empty);
            }
        }
    }
}
