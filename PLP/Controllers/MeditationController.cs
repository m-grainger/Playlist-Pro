using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLP.Data;
using PLP.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PLP.Controllers
{
    public class MeditationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeditationController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Meditation
        public async Task<IActionResult> Index()
        {
            return View(await _context.Meditation.ToListAsync());
        }

        // GET: Meditation/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meditation = await _context.Meditation
                .SingleOrDefaultAsync(m => m.ID == id);
            if (meditation == null)
            {
                return NotFound();
            }

            return View(meditation);
        }

        // GET: Meditation/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meditation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ID,Artist,Title,Genre,Play")] Meditation meditation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meditation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(meditation);
        }

        // GET: Meditation/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meditation = await _context.Meditation.SingleOrDefaultAsync(m => m.ID == id);
            if (meditation == null)
            {
                return NotFound();
            }
            return View(meditation);
        }

        // POST: Meditation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Artist,Title,Genre,Play")] Meditation meditation)
        {
            if (id != meditation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meditation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeditationExists(meditation.ID))
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
            return View(meditation);
        }

        // GET: Meditation/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meditation = await _context.Meditation
                .SingleOrDefaultAsync(m => m.ID == id);
            if (meditation == null)
            {
                return NotFound();
            }

            return View(meditation);
        }

        // POST: Meditation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meditation = await _context.Meditation.SingleOrDefaultAsync(m => m.ID == id);
            _context.Meditation.Remove(meditation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MeditationExists(int id)
        {
            return _context.Meditation.Any(e => e.ID == id);
        }
    }
}
