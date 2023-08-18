//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Exercise03ex01.Context;
//using Exercise03ex01.Models;

//namespace Exercise03ex01.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PrintermakesController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public PrintermakesController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Printermakes
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Printermakes>>> GetPrinterMakes()
//        {
//            return await _context.PrinterMakes.ToListAsync();
//        }

//        // GET: api/Printermakes/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Printermakes>> GetPrintermakes(int id)
//        {
//            var printermakes = await _context.PrinterMakes.FindAsync(id);

//            if (printermakes == null)
//            {
//                return NotFound();
//            }

//            return printermakes;
//        }

//        // PUT: api/Printermakes/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutPrintermakes(int id, Printermakes printermakes)
//        {
//            if (id != printermakes.printerMakeID)
//            {
//                return BadRequest();
//            }

//            _context.Entry(printermakes).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!PrintermakesExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Printermakes
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Printermakes>> PostPrintermakes(Printermakes printermakes)
//        {
//            _context.PrinterMakes.Add(printermakes);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetPrintermakes", new { id = printermakes.printerMakeID }, printermakes);
//        }

//        // DELETE: api/Printermakes/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeletePrintermakes(int id)
//        {
//            var printermakes = await _context.PrinterMakes.FindAsync(id);
//            if (printermakes == null)
//            {
//                return NotFound();
//            }

//            _context.PrinterMakes.Remove(printermakes);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool PrintermakesExists(int id)
//        {
//            return _context.PrinterMakes.Any(e => e.printerMakeID == id);
//        }
//    }
//}
