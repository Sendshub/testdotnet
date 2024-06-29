using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestDotNet.Models;

namespace TestDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataItemsController : ControllerBase
    {
        private readonly DataContext _context;

        public DataItemsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/DataItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataItem>>> GetDataItems()
        {
            var dataItems = await _context.DataItems.OrderBy(item => item.Code).ToListAsync();
            return Ok(dataItems);
        }

        // POST: api/DataItems
        [HttpPost]
        public async Task<ActionResult<IEnumerable<DataItem>>> PostDataItems([FromBody] IEnumerable<DataItem> dataItems)
        {
            if (dataItems == null || !dataItems.Any())
            {
                return BadRequest("Invalid data.");
            }

            _context.DataItems.RemoveRange(_context.DataItems);
            await _context.SaveChangesAsync();

            //как вариант, можно использовать подход без очищения порядкового номера
            // var newDataItems = dataItems.Select(item => new DataItem { Code = item.Code, Value = item.Value })
            //                             .OrderBy(item => item.Code)
            //                             .ToList();

            int order = 1;
            var newDataItems = dataItems.OrderBy(item => item.Code).Select(item => new DataItem 
            { 
                Id = order++,
                Code = item.Code, 
                Value = item.Value 
            }).ToList();

            _context.DataItems.AddRange(newDataItems);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDataItems), new { }, newDataItems);
        }
    }
}
