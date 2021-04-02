using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.services
{
    public interface IItemService
    {
        Task<CatalogModel> GetCatalogItemsAsync(int page, int size, int? Catagory, int? type);
        Task<IEnumerable<SelectListItem>> GetCatagoryAsync();
        Task<IEnumerable<SelectListItem>> GetTypesAsync();
    }
}
