﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AKAN.Data;
using AKAN.Models;

namespace AKAN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProposalsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProposalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Proposals
        [HttpGet]
        public async Task<ActionResult<Response>> GetProposals()
        {
            return new Response(true, new { Proposals = await _context.Proposals.ToListAsync() }, null);
        }

        // GET: api/Proposals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetProposal(int id)
        {
            var proposal = await _context.Proposals.FindAsync(id);

            if (proposal == null)
            {
                return new Response(false, "", "Id'ye ait Proposal bulunamadı.");
            }

            return new Response(true, new { Proposal = proposal }, null);
        }

        [HttpGet("MyProposals/{id}")]
        public async Task<ActionResult<Response>> GetMyProposal(int id)
        {
            var proposals = await _context.Proposals.Where(x => x.TransmitterId == id).ToListAsync();

            if (!proposals.Any())
            {
                return new Response(false, "", "Id'ye ait Proposal bulunamadı.");
            }

            return new Response(true, new { MyProposals = proposals }, null);
        }

        // PUT: api/Proposals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProposal(int id, Proposal proposal)
        {
            if (id != proposal.Id)
            {
                return BadRequest();
            }

            _context.Entry(proposal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProposalExists(id))
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

        [HttpPut("AcceptProposal/{id}")]
        public async Task<ActionResult<Response>> PutAcceptProposal(int id)
        {
            var proposal = await _context.Proposals.FindAsync(id);
            proposal.isAccepted = true;

            _context.Entry(proposal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new Response(true, "Teklif kabul edildi", "");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProposalExists(id))
                {
                    return new Response(false, "", "Proposal doesn't exist");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Proposals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proposal>> PostProposal(Proposal proposal)
        {
            _context.Proposals.Add(proposal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProposal", new { id = proposal.Id }, proposal);
        }

        [HttpPost("MakeProposal/{transmitterId}/{advertId}")]
        public async Task<ActionResult<Response>> PostProposal(int transmitterId, int advertId)
        {
            var proposals = await _context.Proposals.Where(x => (x.TransmitterId == transmitterId && x.AdvertId == advertId)).ToListAsync();

            if (proposals.Any())
            {
                return new Response(false, "", "Bu ilana zaten teklif gönderdiniz.");
            }
            else
            {
                Proposal proposal = new Proposal();
                proposal.AdvertId = advertId;
                proposal.TransmitterId = transmitterId;
                _context.Proposals.Add(proposal);
                await _context.SaveChangesAsync();

                return new Response(true, new { Proposal = CreatedAtAction("GetProposal", new { id = proposal.Id }, proposal).Value }, null);
            }
        }

        // DELETE: api/Proposals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProposal(int id)
        {
            var proposal = await _context.Proposals.FindAsync(id);
            if (proposal == null)
            {
                return NotFound();
            }

            _context.Proposals.Remove(proposal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProposalExists(int id)
        {
            return _context.Proposals.Any(e => e.Id == id);
        }
    }
}
