using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLP.Data;
using PLP.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PLP.Controllers
{
    public class OfficeTimeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OfficeTimeController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: OfficeTime
        public async Task<IActionResult> Index()
        {
            return View(await _context.OfficeTime.ToListAsync());
        }

        // GET: OfficeTime/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officeTime = await _context.OfficeTime
                .SingleOrDefaultAsync(m => m.ID == id);
            if (officeTime == null)
            {
                return NotFound();
            }

            return View(officeTime);
        }

        // GET: OfficeTime/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: OfficeTime/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ID,Artist,Title,Genre,Play")] OfficeTime officeTime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(officeTime);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(officeTime);
        }

        // GET: OfficeTime/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officeTime = await _context.OfficeTime.SingleOrDefaultAsync(m => m.ID == id);
            if (officeTime == null)
            {
                return NotFound();
            }
            return View(officeTime);
        }

        // POST: OfficeTime/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Artist,Title,Genre,Play")] OfficeTime officeTime)
        {
            if (id != officeTime.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(officeTime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficeTimeExists(officeTime.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(officeTime);
        }

        // GET: OfficeTime/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officeTime = await _context.OfficeTime
                .SingleOrDefaultAsync(m => m.ID == id);
            if (officeTime == null)
            {
                return NotFound();
            }

            return View(officeTime);
        }

        // POST: OfficeTime/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var officeTime = await _context.OfficeTime.SingleOrDefaultAsync(m => m.ID == id);
            _context.OfficeTime.Remove(officeTime);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OfficeTimeExists(int id)
        {
            return _context.OfficeTime.Any(e => e.ID == id);
        }
    }
}
