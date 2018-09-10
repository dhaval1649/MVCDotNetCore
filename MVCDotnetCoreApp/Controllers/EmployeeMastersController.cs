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
    public class EmployeeMastersController : Controller
    {
        private readonly BloggingContext _context;

        public EmployeeMastersController(BloggingContext context)
        {
            _context = context;
        }

        // GET: EmployeeMasters
        public async Task<IActionResult> Index()
        {
            var employeeMaster = await _context.EmployeeMaster.OrderBy(n => n.Id)
                  .Select(n =>
                  new EmployeeMaster
                  {
                      Name = n.Name,
                      Surname = n.Surname,
                      Address = n.Address,
                      Qualification = n.Qualification,
                      Contact_Number = n.Contact_Number,
                      DepartmentName = n.DepartmentMaster.DepartmentName,
                      Id = n.Id
                  }).ToListAsync();


            return View(employeeMaster);
        }

        // GET: EmployeeMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeMaster = await _context.EmployeeMaster
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeMaster == null)
            {
                return NotFound();
            }

            return View(employeeMaster);
        }

        // GET: EmployeeMasters/Create
        public IActionResult Create()
        {

            ViewBag.ListofDepartments = GetDepartments();
            return View();
        }

        // POST: EmployeeMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Address,Qualification,Contact_Number,DepartmentId")] EmployeeMaster employeeMaster)
        {
            if (ModelState.IsValid)
            {
                // without type parameter
                var emp = new EmployeeMaster
                {
                    Name = employeeMaster.Name,
                    Surname = employeeMaster.Surname,
                    Address = employeeMaster.Address,
                    Qualification = employeeMaster.Qualification,
                    Contact_Number = employeeMaster.Contact_Number,
                    DepartmentId = employeeMaster.DepartmentId
                };
                //context.Add(author);
                //context.SaveChanges();
                _context.Add(emp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeMaster);
        }

        // GET: EmployeeMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeMaster = await _context.EmployeeMaster.FindAsync(id);
            if (employeeMaster == null)
            {
                return NotFound();
            }
            ViewBag.ListofDepartments = GetDepartments();
            return View(employeeMaster);
        }

        // POST: EmployeeMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Address,Qualification,Contact_Number,DepartmentId")] EmployeeMaster employeeMaster)
        {
            if (id != employeeMaster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeMasterExists(employeeMaster.Id))
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
            return View(employeeMaster);
        }

        // GET: EmployeeMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeMaster = await _context.EmployeeMaster
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeMaster == null)
            {
                return NotFound();
            }

            return View(employeeMaster);
        }

        // POST: EmployeeMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeMaster = await _context.EmployeeMaster.FindAsync(id);
            _context.EmployeeMaster.Remove(employeeMaster);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeMasterExists(int id)
        {
            return _context.EmployeeMaster.Any(e => e.Id == id);
        }

        public IEnumerable<SelectListItem> GetDepartments()
        {
            try
            {


                List<SelectListItem> departments = _context.DepartmentMaster.AsNoTracking()
                    .OrderBy(n => n.DepartmentName)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.DepartmentId.ToString(),
                            Text = n.DepartmentName
                        }).ToList();
                var departmenttip = new SelectListItem()
                {
                    Value = null,
                    Text = "Select Department"
                };
                departments.Insert(0, departmenttip);
                return new SelectList(departments, "Value", "Text");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }

}
