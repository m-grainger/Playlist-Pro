using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLP.Data;
using PLP.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PLP.Controllers
{
    public class WorkingOutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkingOutController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: WorkingOut
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkingOut.ToListAsync());
        }

        // GET: WorkingOut/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workingOut = await _context.WorkingOut
                .SingleOrDefaultAsync(m => m.ID == id);
            if (workingOut == null)
            {
                return NotFound();
            }

            return View(workingOut);
        }

        // GET: WorkingOut/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkingOut/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ID,Artist,Title,Genre,Play")] WorkingOut workingOut)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workingOut);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(workingOut);
        }

        // GET: WorkingOut/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workingOut = await _context.WorkingOut.SingleOrDefaultAsync(m => m.ID == id);
            if (workingOut == null)
            {
                return NotFound();
            }
            return View(workingOut);
        }

        // POST: WorkingOut/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Artist,Title,Genre,Play")] WorkingOut workingOut)
        {
            if (id != workingOut.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workingOut);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkingOutExists(workingOut.ID))
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
            return View(workingOut);
        }

        // GET: WorkingOut/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workingOut = await _context.WorkingOut
                .SingleOrDefaultAsync(m => m.ID == id);
            if (workingOut == null)
            {
                return NotFound();
            }

            return View(workingOut);
        }

        // POST: WorkingOut/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workingOut = await _context.WorkingOut.SingleOrDefaultAsync(m => m.ID == id);
            _context.WorkingOut.Remove(workingOut);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool WorkingOutExists(int id)
        {
            return _context.WorkingOut.Any(e => e.ID == id);
        }
    }
}
