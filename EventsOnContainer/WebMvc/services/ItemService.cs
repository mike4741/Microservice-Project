
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMvc.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebMvc.Infrastructure;
namespace WebMvc.services
{
    public class ItemService : IItemService
    {
        private readonly string _baseUrl;
        private readonly IHttpClient _client;

        public ItemService (IConfiguration config, IHttpClient client)
        {
            _baseUrl = $"{config["ExternalBaseUrl"]}/api/EventItems/";
            _client = client;
        }
        public async Task<IEnumerable<SelectListItem>> GetCatagoryAsync()
        {
            var CatagoryUri = ApiPaths.Catalog.GetAllCatagorys(_baseUrl);
            var dataString = await _client.GetStringAsync(CatagoryUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value=null,
                    Text="All",
                    Selected = true
                }
            };
            var catagory = JArray.Parse(dataString);
            foreach (var c in catagory)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = c.Value<string>("id"),
                        Text =  c.Value<string>("Catagory")
                    });
            }
            return items;
        }

    

        public async Task<CatalogModel> GetCatalogItemsAsync(int page, int size, int? catagory, int? type)
        {
            var catalogItemsUri = ApiPaths.Catalog.GetAllCatalogItems(_baseUrl, page, size, catagory, type);
            var dataString = await _client.GetStringAsync(catalogItemsUri);
            return JsonConvert.DeserializeObject<CatalogModel>(dataString);
        }

        public async Task<IEnumerable<SelectListItem>> GetTypesAsync()
        {
            var typeUri = ApiPaths.Catalog.GetAllTypes(_baseUrl);
            var dataString = await _client.GetStringAsync(typeUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value=null,
                    Text="All",
                    Selected = true
                }
            };
            var types = JArray.Parse(dataString);
            foreach (var type in types)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = type.Value<string>("id"),
                        Text = type.Value<string>("type")
                    });
            }
            return items;
        }

   
    }
}
