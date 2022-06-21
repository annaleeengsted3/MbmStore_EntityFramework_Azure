using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MbmStore.Data;
using MbmStore.Models;
using System.Data;

namespace MbmStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MusicController : Controller
    {
        private readonly MbmStoreContext _context;

        public MusicController(MbmStoreContext context)
        {
            _context = context;
        }

        // GET: Admin/Music
        public async Task<IActionResult> Index()
        {
            return View(await _context.MusicCDs.ToListAsync());
        }

        // GET: Admin/Music/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicCD = await _context.MusicCDs
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (musicCD == null)
            {
                return NotFound();
            }

            return View(musicCD);
        }

        // GET: Admin/Music/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Music/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Artist,Label,Released,Title,Price,ImageUrl,Category")] MusicCD musicCD)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    musicCD.CreatedDate = DateTime.Now;
                    _context.Add(musicCD);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(musicCD);
        }


        // GET: Admin/Music/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicCD = await _context.MusicCDs.FindAsync(id);
            if (musicCD == null)
            {
                return NotFound();
            }
            return View(musicCD);
        }

        // POST: Admin/Music/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Artist,Label,Released,ProductId,Title,Price,ImageUrl,Category")] MusicCD musicCD)
        {
            if (id != musicCD.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musicCD);
                    _context.Entry<MusicCD>(musicCD).Property(x => x.CreatedDate).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicCDExists(musicCD.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(musicCD);
        }



        // GET: Admin/Music/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }


            var musicCD = await _context.MusicCDs.FirstOrDefaultAsync(m => m.ProductId == id);
            if (musicCD == null)
            {
                return NotFound();
            }

            return View(musicCD);
        }


        // POST: Admin/Music/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int ProductId)
        {
            try
            {
                var musicCD = await _context.MusicCDs.FindAsync(ProductId);
                _context.MusicCDs.Remove(musicCD);

                await _context.SaveChangesAsync();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = ProductId, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }



        private bool MusicCDExists(int id)
        {
            return _context.MusicCDs.Any(e => e.ProductId == id);
        }
    }
}
