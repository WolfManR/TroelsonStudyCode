﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoLotDAL_Core2.Models;
using AutoLotDAL_Core2.Repos;

namespace AutoLotMVC_Core2.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryRepo _repo;

        public InventoryController(IInventoryRepo repo)
        {
            _repo = repo;
        }

        // GET: Inventory
        public IActionResult Index()
        {
            return View("IndexWithViewComponent");
        }

        // GET: Inventory/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var inventory = _repo.GetOne(id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Make,Color,PetName")] Inventory inventory)
        {
            if (!ModelState.IsValid) return View(inventory);
            try
            {
                _repo.Add(inventory);
            }
            catch (Exception ex)
            {
                // Создать запись невозможно.
                ModelState.AddModelError(string.Empty, $@"Unable to create record: {ex.Message}");
                return View(inventory);
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Inventory/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var inventory = _repo.GetOne(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Make,Color,PetName,Id,Timestamp")] Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) return View(inventory);
            try
            {
                _repo.Update(inventory);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Сохранить запись невозможно из-за её обновления другим пользователем.
                ModelState.AddModelError(string.Empty, $@"Unable to save record. Another user has updated it. {ex.Message}");
                return View(inventory);
            }
            catch (Exception ex)
            {
                // Сохранить запись невозможно.
                ModelState.AddModelError(string.Empty, $@"Unable to save record. {ex.Message}");
                return View(inventory);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Inventory/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var inventory = _repo.GetOne(id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([Bind("Id,Timestamp")] Inventory inventory)
        {
            try
            {
                _repo.Delete(inventory);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Удалить запись невозможно из-за её обновления другим пользователем.
                ModelState.AddModelError(string.Empty, $@"Unable to delete record. Another user has updated it. {ex.Message}");
            }
            catch (Exception ex)
            {
                // Удалить запись невозможно.
                ModelState.AddModelError(string.Empty, $@"Unable to delete record. {ex.Message}");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
