using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLP.Data;
using PLP.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PLP.Controllers
{
    public class ReadingCodingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReadingCodingController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ReadingCoding
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReadingCoding.ToListAsync());
        }

        // GET: ReadingCoding/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var readingCoding = await _context.ReadingCoding
                .SingleOrDefaultAsync(m => m.ID == id);
            if (readingCoding == null)
            {
                return NotFound();
            }

            return View(readingCoding);
        }

        // GET: ReadingCoding/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReadingCoding/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ID,Artist,Title,Genre,Play")] ReadingCoding readingCoding)
        {
            if (ModelState.IsValid)
            {
                _context.Add(readingCoding);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(readingCoding);
        }

        // GET: ReadingCoding/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var readingCoding = await _context.ReadingCoding.SingleOrDefaultAsync(m => m.ID == id);
            if (readingCoding == null)
            {
                return NotFound();
            }
            return View(readingCoding);
        }

        // POST: ReadingCoding/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Artist,Title,Genre,Play")] ReadingCoding readingCoding)
        {
            if (id != readingCoding.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(readingCoding);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReadingCodingExists(readingCoding.ID))
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
            return View(readingCoding);
        }

        // GET: ReadingCoding/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var readingCoding = await _context.ReadingCoding
                .SingleOrDefaultAsync(m => m.ID == id);
            if (readingCoding == null)
            {
                return NotFound();
            }

            return View(readingCoding);
        }

        // POST: ReadingCoding/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var readingCoding = await _context.ReadingCoding.SingleOrDefaultAsync(m => m.ID == id);
            _context.ReadingCoding.Remove(readingCoding);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ReadingCodingExists(int id)
        {
            return _context.ReadingCoding.Any(e => e.ID == id);
        }
    }
}
