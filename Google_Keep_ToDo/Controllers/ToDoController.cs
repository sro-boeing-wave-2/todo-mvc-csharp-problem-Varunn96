﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Google_Keep_ToDo.Models;

namespace Google_Keep_ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        public readonly Google_Keep_Context _context;

        public ToDoController(Google_Keep_Context context)
        {
            _context = context;
        }

        // GET: api/ToDo
        [HttpGet]
        public IEnumerable<Note> GetMyNote()
        {
            return _context.Note.Include(p => p.CheckList).Include(p => p.Labels);
        }

        // GET: api/ToDo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var myNote = await _context.Note.Include(p => p.CheckList).Include(p => p.Labels).SingleOrDefaultAsync(p => p.Id == id);

            if (myNote == null)
            {
                return NotFound();
            }

            return Ok(myNote);
        }


        [HttpGet("title")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var myNote = await _context.Note.Include(p => p.CheckList).Include(p => p.Labels).SingleOrDefaultAsync(s => s.Name == title);

            if (myNote == null)
            {
                return NotFound();
            }
            return Ok(myNote);
        }

        [HttpGet("pinstatus")]
        public async Task<IActionResult> GetByPinStatus([FromQuery] bool pinstatus)
        {
            return Ok(await _context.Note.Include(p => p.CheckList).Include(p => p.Labels).Where(p => p.PinStatus == pinstatus).ToListAsync());
        }

        [HttpGet("label")]
        public async Task<IActionResult> GetByLabel([FromQuery] string label)
        {
            var NonNull = _context.Note.Include(p => p.CheckList).Include(p => p.Labels).Where(p => p.Labels != null);
            return Ok(await NonNull.Where(p => p.Labels.Any(q => q.LabelName == label)).ToListAsync());
        }

        // PUT: api/ToDo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMyNote([FromRoute] int id, [FromBody] Note myNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != myNote.Id)
            {
                return BadRequest();
            }

            _context.Note.Update(myNote);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyNoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(myNote);
        }

        // POST: api/ToDo
        [HttpPost]
        public async Task<IActionResult> PostMyNote([FromBody] Note myNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Note.Add(myNote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMyNote", new { id = myNote.Id }, myNote);
        }

        // DELETE: api/ToDo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyNote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var myNote = await _context.Note.Include(p => p.CheckList).Include(p => p.Labels).SingleOrDefaultAsync(p => p.Id == id);
            if (myNote == null)
            {
                return NotFound();
            }

            _context.Note.Remove(myNote);
            await _context.SaveChangesAsync();

            return Ok(myNote);
        }

        [HttpDelete("title")]
        public async Task<IActionResult> DeleteByTitle([FromQuery] string title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var myNote = await _context.Note.Include(p => p.CheckList).Include(p => p.Labels).SingleOrDefaultAsync(p => p.Name == title);
            if (myNote == null)
            {
                return NotFound();
            }

            _context.Note.Remove(myNote);
            await _context.SaveChangesAsync();

            return Ok(myNote);
        }

        [HttpDelete("label")]
        public async Task<IActionResult> DeleteByLabel([FromQuery] string label)
        {
            var NonNull = _context.Note.Include(p => p.CheckList).Include(p => p.Labels).Where(p => p.Labels != null);
            var deletenote = NonNull.Where(p => p.Labels.Any(q => q.LabelName == label));
            _context.Note.RemoveRange(deletenote);

            await _context.SaveChangesAsync();

            return Ok(deletenote);
        }


        private bool MyNoteExists(int id)
        {
            return _context.Note.Any(e => e.Id == id);
        }
    }
}