using EventCatalogAPI.Data;
using EventCatalogAPI.Domain;
using EventCatalogAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventItemsController : ControllerBase
    {
        private readonly EventContext _context;
        private readonly IConfiguration _config;
        public EventItemsController(EventContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> ItemsByCatagoryandType (
                [FromQuery] int? catagoryId,
                [FromQuery] int? typeId,
                [FromQuery] int pageIndx = 0,
                [FromQuery] int pagesize = 6)
        {

            var query = (IQueryable<EventItem>)_context.EventItems;
            if (catagoryId.HasValue)
            {
                query = query.Where(i => i.CatagoryId == catagoryId);
            }
            if (typeId.HasValue)
            {
                query = query.Where(i => i.TypeId == typeId);
            }
            var itemCount = query.LongCountAsync();
            var result =  await query
                              .OrderBy(s => s.EventName)
                              .Skip(pageIndx * pagesize)
                              .Take(pagesize)
                              .ToListAsync();
            result = ChangePicUrl(result);
            var model = new PaginatedItemViewModel<EventItem>
            {
                PageIndex = pageIndx,
                PageSize = result.Count,
                ItemCount = (int)itemCount.Result,
                Data = result
            };
           

            return Ok(model);


        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Item(
                [FromQuery] int pageIndx = 0,
                [FromQuery] int pagesize = 6)
        {
            var itemCount = _context.EventItems.LongCountAsync();
            var query = await _context.EventItems
                              .OrderBy(s => s.EventName)
                              .Skip(pageIndx * pagesize)
                              .Take(pagesize)
                              .ToListAsync();
            query = ChangePicUrl(query);

            var model = new PaginatedItemViewModel<EventItem>
            {
                PageIndex = pageIndx,
                PageSize  = query.Count,
                ItemCount = (int)itemCount.Result,
                Data= query
            };

            return Ok(model);
        }

        private List<EventItem> ChangePicUrl(List<EventItem> query)
        {
            query.ForEach(item =>
             item.EventImageUrl = item.EventImageUrl.
             Replace("http://externaleventbaseurltoberplaced/", 
            _config["ExternalBaseUrl"]));
            return query;
        }


         [HttpGet("[action]")]
        public async Task<IActionResult> CatalogCatagory()
        {
            var query = await _context.EventCategories.ToListAsync();
            return Ok(query);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CatalogType()
        {
            var query = await _context.EventTypes.ToListAsync();
            return Ok(query);
        }

    }
}
