using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FInDashboardWASM.Server;
using FInDashboardWASM.Shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FInDashboardWASM.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyDataController : ControllerBase
    {
        private readonly MssqDBContext _context;
        private readonly IConfiguration _configuration;

        public CompanyDataController(MssqDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCompanyData(string name)
        {

            string key = _configuration["api_key"];
            HttpClient cl = new HttpClient();

            var response = await cl.GetAsync($"https://api.polygon.io/v3/reference/tickers/{name}?apiKey={key}");

            var jObject = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync() );

             var res =  jObject["results"];


            CompanyData dat =  res.ToObject<CompanyData>();

            dat.LogoLink = jObject["results"]["branding"]["logo_url"].ToString() + $"?apiKey={key}";





            return Ok(dat);

        }



        [HttpGet]
        public async Task<IActionResult> GetCompanyData()
        {

            List<string> ticker = new List<string>();

            await _context.companyData.AsNoTracking().ForEachAsync(c => ticker.Add(c.Name + " - " + c.Ticker));

           return Ok( ticker);




        }





        // POST: api/CompanyDatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompanyData>> PostCompanyData(CompanyData companyData)
        {
          if (_context.companyData == null)
          {
              return Problem("Entity set 'MssqDBContext.companyDatas'  is null.");
          }
            _context.companyData.Add(companyData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompanyData", new { id = companyData.Id }, companyData);
        }

        // DELETE: api/CompanyDatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyData(int id)
        {
            if (_context.companyData == null)
            {
                return NotFound();
            }
            var companyData = await _context.companyData.FindAsync(id);
            if (companyData == null)
            {
                return NotFound();
            }

            _context.companyData.Remove(companyData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyDataExists(int id)
        {
            return (_context.companyData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
