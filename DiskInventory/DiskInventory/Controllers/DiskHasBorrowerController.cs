using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace DiskInventory.Controllers

{
    public class DiskHasBorrowerController : Controller
    {
        private diskInventoryv2bmContext context { get; set; }
        public DiskHasBorrowerController(diskInventoryv2bmContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            var diskHasBorrowers = context.DiskHasBorrowers.
                Include(d => d.Disk).OrderBy(d => d.Disk.DiskName).
                Include(b => b.Borrower).ToList();
            return View(diskHasBorrowers);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Disks = context.Disks.OrderBy(d => d.DiskName).ToList();
            ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
            DiskHasBorrower newdiskhasborrower = new DiskHasBorrower();
            newdiskhasborrower.BorrowedDate = DateTime.Today;
            newdiskhasborrower.DueDate = DateTime.Today;
            return View("Edit", newdiskhasborrower);

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Disks = context.Disks.OrderBy(d => d.DiskName).ToList();
            ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
            var diskHasBorrower = context.DiskHasBorrowers.Find(id);
            return View(diskHasBorrower);
        }

        [HttpPost]
        public IActionResult Edit(DiskHasBorrower diskHasBorrower)
        {
            if (ModelState.IsValid)
            {
                if (diskHasBorrower.DiskHasBorrowerId == 0)
                {
                    context.DiskHasBorrowers.Add(diskHasBorrower);
                }
                else
                {
                    context.DiskHasBorrowers.Update(diskHasBorrower);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "DiskHasBorrower");
            }
            else
            {
                ViewBag.Action = (diskHasBorrower.DiskHasBorrowerId == 0) ? "Add" : "Edit";
                ViewBag.Disks = context.Disks.OrderBy(d => d.DiskName).ToList();
                ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
                return View(diskHasBorrower);
            }
        }
    }
}
