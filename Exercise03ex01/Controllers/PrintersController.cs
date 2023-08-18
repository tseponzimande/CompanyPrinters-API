using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exercise03ex01.Context;
using Exercise03ex01.Models;
using Microsoft.Data.SqlClient;
using System.Reflection;
using NuGet.Packaging.Signing;
using System.Data;
using System.Xml.Linq;

namespace Exercise03ex01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly AppDbContext _context;

        public PrintersController(AppDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Printers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Printer>>> GetPrinters()
        {
            return await _context.Printers.ToListAsync();
        }

        // GET: api/Printers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Printer>> GetPrinter(int id)
        {
            var printer = await _context.Printers.FindAsync(id);

            if (printer == null)
            {
                return NotFound();
            }

            return printer;
        }

        // PUT: api/Printers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrinter(int id, Printer printer)
        {
            if (id != printer.engenPrintersID)
            {
                return BadRequest();
            }

            _context.Entry(printer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrinterExists(id))
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

        // POST: api/Printers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("ADD")]
        [HttpPost]
        public async Task<ActionResult<Printer>> PostPrinter(Printer printer)
        {
            _context.Printers.Add(printer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PrinterExists(printer.engenPrintersID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPrinter", new { id = printer.engenPrintersID }, printer);
        }

        // DELETE: api/Printers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrinter(int id)
        {
            var printer = await _context.Printers.FindAsync(id);
            if (printer == null)
            {
                return NotFound();
            }

            _context.Printers.Remove(printer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrinterExists(int id)
        {
            return _context.Printers.Any(e => e.engenPrintersID == id);
        }

        [Route("search")]
        [HttpPost]
        public object search(searchdata sd)
        {
            //DateTime startdate;
            //DateTime enddate;
            //string printerMake;
            //string query = @"SELECT Printers.engenPrintersID, Printers.printerName, Printers.printerMake, Printers.folderToMonitor, Printers.outputType, Printers.fileOutput, Printers.active, Printers.createTimeStamp
            //                FROM [dbo].[Printers]                            
            //                WHERE (Printers.createTimeStamp BETWEEN @StartDate AND @EndDate) AND (Printers.printerMake = Printers.printerMake)";

            /*string query = @"SELECT * FROM  [dbo].[Printers]
                            WHERE (createTimeStamp BETWEEN @StartDate AND @EndDate) AND (printerMake = @printerMake)";*/

            //string query = @"SELECT engenPrintersID, printerName, printerMake, folderToMonitor, outputType, fileOutput, active, createTimeStamp
            //                FROM Printers
            //                WHERE (CreateTimeStamp BETWEEN @startdate AND @enddate)";

            string connectionString = _configuration.GetConnectionString("SQLServerConnStr");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("filterDates", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@startDate", sd.startdate);
                command.Parameters.AddWithValue("@endDate", sd.enddate);
                command.Parameters.AddWithValue("@printerMake", "Canon");
                //command.ExecuteNonQuery();


                SqlDataReader reader = command.ExecuteReader();

                var resultList = new List<Dictionary<string, object>>();
                while (reader.Read())
                {
                    var resultDict = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        object columnValue = reader.GetValue(i);
                        resultDict.Add(columnName, columnValue);
                    }
                    resultList.Add(resultDict);
                }

                reader.Close();
                connection.Close();

                return resultList;
            }

        }

        

        

        [HttpDelete("delete-multiple")]
        public ActionResult Delete(int[] id)
        {
            var records = _context.Printers.Where(r => id.Contains(r.engenPrintersID)).ToList();

            // Delete the records.
            _context.Printers.RemoveRange(records);
            _context.SaveChanges();

            // Return a success response.
            return Ok();


        }
    }
}
