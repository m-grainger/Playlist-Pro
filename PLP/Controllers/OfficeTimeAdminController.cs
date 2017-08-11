using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLP.Data;
using PLP.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PLP.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OfficeTimeAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OfficeTimeAdminController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: OfficeTimeAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.OfficeTime.ToListAsync());
        }

        // GET: OfficeTimeAdmin/Details/5
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

        // GET: OfficeTimeAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OfficeTimeAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: OfficeTimeAdmin/Edit/5
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

        // POST: OfficeTimeAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: OfficeTimeAdmin/Delete/5
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

        // POST: OfficeTimeAdmin/Delete/5
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
