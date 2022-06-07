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
    public class InboxController : ControllerBase
    {
        private readonly SaythanksContext _context;

        public InboxController(SaythanksContext context)
        {
            _context = context;
        }

        // GET: api/Inbox
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inbox>>> GetInboxes()
        {
            return await _context.Inboxes.ToListAsync();
        }

        // GET: api/Inbox/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inbox>> GetInbox(string id)
        {
            var inbox = await _context.Inboxes.FindAsync(id);

            if (inbox == null)
            {
                return NotFound();
            }

            return inbox;
        }

        // PUT: api/Inbox/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInbox(string id, Inbox inbox)
        {
            if (id != inbox.AuthId)
            {
                return BadRequest();
            }

            _context.Entry(inbox).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InboxExists(id))
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

        // POST: api/Inbox
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inbox>> PostInbox(Inbox inbox)
        {
            _context.Inboxes.Add(inbox);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InboxExists(inbox.AuthId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInbox", new { id = inbox.AuthId }, inbox);
        }

        // DELETE: api/Inbox/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInbox(string id)
        {
            var inbox = await _context.Inboxes.FindAsync(id);
            if (inbox == null)
            {
                return NotFound();
            }

            _context.Inboxes.Remove(inbox);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InboxExists(string id)
        {
            return _context.Inboxes.Any(e => e.AuthId == id);
        }
    }
}
