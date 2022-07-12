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
    public class OHLCController : ControllerBase
    {
        private readonly MssqDBContext _context;
        private readonly IConfiguration _configuration;

        public OHLCController(MssqDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }



        // GET: api/OHLCs/5
        [HttpGet("{name}")]
        public async Task<IActionResult> GetOHLC(string name)
        {
            string key = _configuration["api_key"];
            DateTime today = DateTime.Now;

            today = today.DayOfWeek == DayOfWeek.Saturday ? today.AddDays(-1) : today;
            today = today.DayOfWeek == DayOfWeek.Sunday ? today.AddDays(-2) : today;

            string request = $"https://api.polygon.io/v1/open-close/{name}/{today.Date.ToString("yyyy-MM-dd")}?adjusted=true&apiKey={key}";
            HttpClient client = new HttpClient();
            var res = await client.GetAsync(request);

            OHLC ohlc =  JsonConvert.DeserializeObject<OHLC>(await res.Content.ReadAsStringAsync());
            ohlc.DateOn = today;

            return Ok(ohlc);
        }

      
        [HttpGet("{name}/{from}/{to}")]
        public async Task<IActionResult> PutOHLC(string name, string from, string to)
        {
            string key = _configuration["api_key"];


            HttpClient cl = new HttpClient();

            var response = await cl.GetAsync($"https://api.polygon.io/v2/aggs/ticker/{name}/range/1/day/{from}/{to}?adjusted=true&sort=asc&limit=120&apiKey={key}");

            var jObject = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            var res = jObject["results"];

            List<OHLC> ohlcs = new List<OHLC>();

            res.ToList().ForEach(x => ohlcs.Add(x.ToObject<OHLC>()));
            return Ok(ohlcs);

        }

        // POST: api/OHLCs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OHLC>> PostOHLC(OHLC oHLC)
        {
          if (_context.ohlc == null)
          {
              return Problem("Entity set 'MssqDBContext.ohlcs'  is null.");
          }
            _context.ohlc.Add(oHLC);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOHLC", new { id = oHLC.Id }, oHLC);
        }

        // DELETE: api/OHLCs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOHLC(int id)
        {
            if (_context.ohlc == null)
            {
                return NotFound();
            }
            var oHLC = await _context.ohlc.FindAsync(id);
            if (oHLC == null)
            {
                return NotFound();
            }

            _context.ohlc.Remove(oHLC);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OHLCExists(int id)
        {
            return (_context.ohlc?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
