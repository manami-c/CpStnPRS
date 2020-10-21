using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CpStnPRS.Data;
using CpStnPRS.Models;

namespace CpStnPRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly CpStnPRSContext _context;

        public RequestsController(CpStnPRSContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequest()
        {
            return await _context.Requests.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Request>> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return request;
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }

        // REVIEW: api/Requests/review
        [HttpPut("review/{id}")]
        public async Task<IActionResult> PutRequestInReview(int id, Request request)
        {
            request.Status = request.Total <= 50 ? "APPROVED" : "REVIEW";
            return await PutRequest(id, request);
        }
        // APPROVE: api/Requests/approve
        [HttpPut("APPROVED/{id}")]
        public async Task<IActionResult> RequestSetToApproved(int id, Request request)
        {
            request.Status = "APPROVED";
            return await PutRequest(id, request);
        }

        // REJECT/api/Requests/reject
        [HttpPut("REJECTED/{id}")]
        public async Task<IActionResult> RequestSetToRejected(int id, Request request)
        {
            request.Status = "REJECTED";
            return await PutRequest(id, request);
        }
        //GetReviews/api/Requests/
        [HttpGet("review/{userId}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestInReview(int userId)
        {
            return await _context.Requests.Where(r => r.Status == "REVIEW" && r.UserId != userId).ToListAsync();
        }

    }
}
