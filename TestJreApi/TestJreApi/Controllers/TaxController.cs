using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taxjar;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace TestJreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly AppSettings _mySettings;

        public TaxController(IOptions<AppSettings> settings)
        {
            //This is always null
            _mySettings = settings.Value;
        }
        // GET: api/Tax
        [HttpGet]
        public IEnumerable<string> Get()
        {
            
            return new string[] { "Provide Zip Code", "Provide Order Amount" };
        }

        // GET: api/Tax/5
        [HttpGet("{id}/{Amt}", Name = "Get")]
        public ActionResult<rate> Get(string id,string Amt)
        {
            var AppName = _mySettings.TaxjarApiKey;//new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["TaxjarApiKey"];
            var client = new TaxjarApi(AppName);
            var rates = client.RatesForLocation(id);
            rate r = new rate();
            r.city = rates.City;
            r.city_rate = rates.CityRate;
            r.combined_district_rate = rates.CombinedDistrictRate;
            r.combined_rate = rates.CombinedRate;
            r.county = rates.Country;
            r.county_rate = rates.CountryRate;
            r.freight_taxable = rates.FreightTaxable;
            r.state = rates.State;
            r.state_rate = rates.StateRate;
            r.zip = rates.Zip;
            r.RAmt = Math.Round(Convert.ToDecimal(Amt) + r.city_rate + r.combined_rate + r.state_rate,2, MidpointRounding.ToEven);
            return r;
        }

        // POST: api/Tax
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Tax/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        public class rate {
            public string zip { get; set; }
            public string state { get; set; }
            public decimal state_rate { get; set; }
            public string county { get; set; }
            public decimal county_rate { get; set; }
            public string city { get; set; }
            public decimal city_rate { get; set; }
            public decimal combined_district_rate { get; set; }
            public decimal combined_rate { get; set; }
            public bool freight_taxable { get; set; }
            public decimal RAmt { get; set; }
        }
    }
}
