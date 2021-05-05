using DiskInventory.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DiskInventory.Controllers
{
    public class DiskController : Controller
    {
        private diskInventoryv2bmContext context { get; set; }

        public DiskController(diskInventoryv2bmContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            //var disks = context.Disks.ToList();
            var disks = context.Disks.OrderBy(d => d.DiskName).ThenBy(s => s.StatusId).ToList();
            return View(disks);
        }
        [HttpGet]
        public IActionResult Add()      //
        {
            ViewBag.Action = "Add";
            ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
            ViewBag.DiskTypes = context.DiskTypes.OrderBy(dt => dt.Description).ToList();
            Disk newdisk = new Disk();
            newdisk.ReleaseDate = DateTime.Today;
            return View("Edit", newdisk);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
            ViewBag.DiskTypes = context.DiskTypes.OrderBy(dt => dt.Description).ToList();
            var disk = context.Disks.Find(id);
            return View(disk);
        }
        [HttpPost]
        public IActionResult Edit(Disk disk)
        {
            if (ModelState.IsValid)
            {
                if (disk.DiskId == 0)
                {
                    //context.Disks.Add(disk);
                    context.Database.ExecuteSqlRaw("execute sp_ins_disk @p0, @p1, @p2, @p3, @p4", 
                        parameters: new[] {disk.DiskName, disk.ReleaseDate.ToString(), disk.GenreId.ToString(), disk.StatusId.ToString(), disk.DiskTypeId.ToString()});
                }
                else
                {
                    //context.Disks.Update(disk);
                    context.Database.ExecuteSqlRaw("execute sp_upd_disk @p0, @p1, @p2, @p3, @p4, @p5", parameters: new[] { disk.DiskId.ToString(), disk.DiskName, disk.ReleaseDate.ToString(), disk.GenreId.ToString(), disk.StatusId.ToString(), disk.DiskTypeId.ToString() });
                }
                //context.SaveChanges();
                return RedirectToAction("Index", "Disk");
            }
            else
            {
                ViewBag.Action = (disk.DiskId == 0) ? "Add" : "Edit";
                //ViewBag.DiskTypes = context.DiskTypes.OrderBy(t => t.Description).ToList();
                ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
                ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
                ViewBag.DiskTypes = context.DiskTypes.OrderBy(dt => dt.Description).ToList();
                return View(disk);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var disk = context.Disks.Find(id);
            return View(disk);
        }

        [HttpPost]
        public IActionResult Delete(Disk disk)
        {
            //context.Disks.Remove(disk);
            //context.SaveChanges();
            context.Database.ExecuteSqlRaw("execute sp_del_disk @p0",
                parameters: new[] { disk.DiskId.ToString() });
            return RedirectToAction("Index", "Disk");
        }
    }
}
