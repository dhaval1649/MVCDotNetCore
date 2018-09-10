using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCDotnetCoreApp.Models;

namespace MVCDotnetCoreApp.Controllers
{
    public class DepartmentMastersController : Controller
    {
        private readonly BloggingContext _context;

        public DepartmentMastersController(BloggingContext context)
        {
            _context = context;
        }

        // GET: DepartmentMasters
        public async Task<IActionResult> Index()
        {
            return View(await _context.DepartmentMaster.ToListAsync());
        }

        // GET: DepartmentMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentMaster = await _context.DepartmentMaster
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (departmentMaster == null)
            {
                return NotFound();
            }

            return View(departmentMaster);
        }

        // GET: DepartmentMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DepartmentMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartmentName")] DepartmentMaster departmentMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departmentMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departmentMaster);
        }

        // GET: DepartmentMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentMaster = await _context.DepartmentMaster.FindAsync(id);
            if (departmentMaster == null)
            {
                return NotFound();
            }
            return View(departmentMaster);
        }

        // POST: DepartmentMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentName")] DepartmentMaster departmentMaster)
        {
            if (id != departmentMaster.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departmentMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentMasterExists(departmentMaster.DepartmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departmentMaster);
        }

        // GET: DepartmentMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentMaster = await _context.DepartmentMaster
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (departmentMaster == null)
            {
                return NotFound();
            }

            return View(departmentMaster);
        }

        // POST: DepartmentMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departmentMaster = await _context.DepartmentMaster.FindAsync(id);
            _context.DepartmentMaster.Remove(departmentMaster);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentMasterExists(int id)
        {
            return _context.DepartmentMaster.Any(e => e.DepartmentId == id);
        }
    }
}
