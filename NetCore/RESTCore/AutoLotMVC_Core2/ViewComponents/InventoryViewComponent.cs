using AutoLotDAL_Core2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutoLotMVC_Core2.ViewComponents
{
    public class InventoryViewComponent : ViewComponent
    {
        private readonly string _baseUrl;
        public InventoryViewComponent(IConfiguration configuration)
        {
            _baseUrl = configuration.GetSection("ServicesAddress").Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                return View("InventoryPartialView", JsonConvert.DeserializeObject<List<Inventory>>(await response.Content.ReadAsStringAsync()));
            }
            return new ContentViewComponentResult("Unable to locate records.");
        }
    }
}
