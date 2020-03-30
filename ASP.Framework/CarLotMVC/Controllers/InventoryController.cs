using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoLotDAL.EF;
using AutoLotDAL.Models;
using AutoLotDAL.Repos;

namespace CarLotMVC.Controllers
{
    public class InventoryController : Controller
    {
        private readonly InventoryRepo _repo = new InventoryRepo();

        // GET: Inventory
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        // GET: Inventory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = _repo.GetOne(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // GET: Inventory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Make,Color,PetName")] Inventory inventory)
        {
            if (!ModelState.IsValid) return View(inventory);

            try
            {
                _repo.Add(inventory);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to create record: {ex.Message}"); // Не удаётся создать запись.
            }

            return RedirectToAction("Index");
        }

        // GET: Inventory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = _repo.GetOne(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventory/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Make,Color,PetName,Timestamp")] Inventory inventory)
        {
            if (!ModelState.IsValid) return View(inventory);
            try
            {
                _repo.Save(inventory);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to save the record. Another user has updated it. {ex.Message}"); // Не удаётся сохранить запись. Другой пользователь обновил её.
                return View(inventory);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to save the record. {ex.Message}"); // Не удаётся сохранить запись.
                return View(inventory);
            }
            return RedirectToAction("Index");
        }

        // GET: Inventory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Cars.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inventory inventory = db.Cars.Find(id);
            db.Cars.Remove(inventory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
