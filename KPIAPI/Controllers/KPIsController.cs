using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KPIAPI.Data;
using KPIAPI.Data.Models;

namespace KPIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KPIsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KPIsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/KPIs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KPI>>> GetKPIs()
        {
            return await _context.KPIs.ToListAsync();
        }

        // GET: api/KPIs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KPI>> GetKPI(int id)
        {
            var kPI = await _context.KPIs.FindAsync(id);

            if (kPI == null)
            {
                return NotFound();
            }

            return kPI;
        }

        // PUT: api/KPIs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKPI(int id, KPI kPI)
        {
            if (id != kPI.KPIIDNum)
            {
                return BadRequest();
            }

            _context.Entry(kPI).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KPIExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/KPIs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KPI>> PostKPI(KPI kPI)
        {
            _context.KPIs.Add(kPI);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKPI", new { id = kPI.KPIIDNum }, kPI);
        }

        // DELETE: api/KPIs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKPI(int id)
        {
            var kPI = await _context.KPIs.FindAsync(id);
            if (kPI == null)
            {
                return NotFound();
            }

            _context.KPIs.Remove(kPI);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KPIExists(int id)
        {
            return _context.KPIs.Any(e => e.KPIIDNum == id);
        }
    }
}
