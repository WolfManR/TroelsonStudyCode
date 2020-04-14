using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
            _baseUrl = configuration.GetSection("ServicesAddress").Value;
        }

        public async Task<IActionResult> Index()
        {
            return View("IndexWithViewComponent");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();
            var inventory = await GetInventoryRecord(id.Value);
            return inventory != null ? (IActionResult)View(inventory) : NotFound();
        }

        // GET: Inventory/Create
        public IActionResult Create() => View();

        // POST: Inventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Make,Color,PetName")] Inventory inventory)
        {
            if (!ModelState.IsValid) return View(inventory);
            try
            {
                string json = JsonConvert.SerializeObject(inventory);
                var response = await client.PostAsync(_baseUrl, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Создать запись невозможно
                ModelState.AddModelError(string.Empty, $"Unable to create record: {ex.Message}");
            }
            return View(inventory);
        }

        // GET: Inventory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            var inventory = await GetInventoryRecord(id.Value);
            return inventory != null ? (IActionResult)View(inventory) : NotFound();
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Make,Color,PetName,Id,Timestamp")] Inventory inventory)
        {
            if (id != inventory.Id) return BadRequest();
            if (!ModelState.IsValid) return View(inventory);

            string json = JsonConvert.SerializeObject(inventory);
            var response = await client.PutAsync($"{_baseUrl}/{inventory.Id}", new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));

            return View(inventory);
        }

        // GET: Inventory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var inventory = await GetInventoryRecord(id.Value);
            return inventory != null ? (IActionResult)View(inventory) : NotFound();
        }

        // POST: Inventory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([Bind("Id,Timestamp")] Inventory inventory)
        {
            var timeStampString = JsonConvert.SerializeObject(inventory.Timestamp);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/{inventory.Id}/{timeStampString}");
            await client.SendAsync(request);
            return RedirectToAction(nameof(Index));
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