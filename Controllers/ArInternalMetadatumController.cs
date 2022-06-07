using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saythanks;

namespace Saythanks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArInternalMetadatumController : ControllerBase
    {
        private readonly SaythanksContext _context;

        public ArInternalMetadatumController(SaythanksContext context)
        {
            _context = context;
        }

        // GET: api/ArInternalMetadatum
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArInternalMetadatum>>> GetArInternalMetadata()
        {
            return await _context.ArInternalMetadata.ToListAsync();
        }

        // GET: api/ArInternalMetadatum/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArInternalMetadatum>> GetArInternalMetadatum(string id)
        {
            var arInternalMetadatum = await _context.ArInternalMetadata.FindAsync(id);

            if (arInternalMetadatum == null)
            {
                return NotFound();
            }

            return arInternalMetadatum;
        }

        // PUT: api/ArInternalMetadatum/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArInternalMetadatum(string id, ArInternalMetadatum arInternalMetadatum)
        {
            if (id != arInternalMetadatum.Key)
            {
                return BadRequest();
            }

            _context.Entry(arInternalMetadatum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArInternalMetadatumExists(id))
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

        // POST: api/ArInternalMetadatum
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArInternalMetadatum>> PostArInternalMetadatum(ArInternalMetadatum arInternalMetadatum)
        {
            _context.ArInternalMetadata.Add(arInternalMetadatum);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ArInternalMetadatumExists(arInternalMetadatum.Key))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetArInternalMetadatum", new { id = arInternalMetadatum.Key }, arInternalMetadatum);
        }

        // DELETE: api/ArInternalMetadatum/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArInternalMetadatum(string id)
        {
            var arInternalMetadatum = await _context.ArInternalMetadata.FindAsync(id);
            if (arInternalMetadatum == null)
            {
                return NotFound();
            }

            _context.ArInternalMetadata.Remove(arInternalMetadatum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArInternalMetadatumExists(string id)
        {
            return _context.ArInternalMetadata.Any(e => e.Key == id);
        }
    }
}
