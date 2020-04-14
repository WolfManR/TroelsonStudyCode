using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoLotDAL_Core2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AutoLotMVC_Core2.Controllers
{
    public class InventoryController : Controller
    {
        private readonly string _baseUrl;
        private readonly HttpClient client = new HttpClient();
        public InventoryController(IConfiguration configuration)
        {
            _baseUrl = configuration.GetSection("ServiceAddress").Value;
        }
        public async Task<IActionResult> Index()
        {
            var response = await client.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                return View(JsonConvert.DeserializeObject<List<Inventory>>(await response.Content.ReadAsStringAsync()));
            }
            return NotFound();
        }

        private async Task<Inventory> GetInventoryRecord(int id)
        {
            var response = await client.GetAsync($"{_baseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Inventory>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }
    }
}