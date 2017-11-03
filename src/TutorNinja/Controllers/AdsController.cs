using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TutorNinja.Data;
using TutorNinja.Models;
using Microsoft.AspNetCore.Identity;


namespace TutorNinja.Controllers
{
    public class AdsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public AdsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Ads
        public async Task<IActionResult> Index(int? id, string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentCategory"] = "All Categories";
            ViewData["CurrentFilter"] = searchString;

            var ads = from a in _context.Ads.Include(a => a.Category)
                       select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                ads = ads.Where(s => s.Title.Contains(searchString) || s.Description.Contains(searchString));
            }

            if (id != null)
            {
                ads = ads.Where(s => s.CategoryID.Equals(id));
                ViewData["CurrentCategory"] = _context.Categories.Where(i => i.CategoryID == id.Value).Single().CategoryName;
            }

            switch (sortOrder)
            {
                case "Price":
                    ads = ads.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    ads = ads.OrderByDescending(s => s.Price);
                    break;
                default:
                    ads = ads.OrderBy(s => s.CreateDate);
                    break;
            }

            int pageSize = 5;
            ViewBag.Categories = _context.Categories.AsNoTracking().ToList();
            return View(await PaginatedList<Ad>.CreateAsync(ads.AsNoTracking(), page ?? 1, pageSize));

            //var adContext = _context.Ads
            //    .AsNoTracking()
            //    .Include(a => a.Category); ;
            //if (id != null)
            //{
            //     adContext = _context.Ads
            //        .AsNoTracking()
            //        .Where(s => s.CategoryID.Equals(id))
            //        .Include(a => a.Category);
            //    ViewData["CurrentCategory"] = _context.Categories.Where(i => i.CategoryID == id.Value).Single().CategoryName;
            //}

            //ViewBag.Categories = _context.Categories.AsNoTracking().ToList();
            //return View(await adContext.ToListAsync());
        }

        // GET: Ads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads
                .Include(a => a.Category)
                .Include(a => a.User)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.AdID == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // GET: Ads/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories.AsNoTracking(), "CategoryID", "CategoryName");
            return View();
        }

        // POST: Ads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryID,Description,Price,Title")] Ad ad)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                ad.User = user;
                ad.CreateDate = DateTime.Now;
                _context.Add(ad);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories.AsNoTracking(), "CategoryID", "CategoryName", ad.CategoryID);
            return View(ad);
        }

        // GET: Ads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads.SingleOrDefaultAsync(m => m.AdID == id);
            if (ad == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories.AsNoTracking(), "CategoryID", "CategoryName", ad.CategoryID);
            return View(ad);
        }

        // POST: Ads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdID,CategoryID,Description,CreateDate,Price,Title")] Ad ad)
        {
            if (id != ad.AdID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdExists(ad.AdID))
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
            ViewData["CategoryID"] = new SelectList(_context.Categories.AsNoTracking(), "CategoryID", "CategoryName", ad.CategoryID);
            return View(ad);
        }

        // GET: Ads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads.SingleOrDefaultAsync(m => m.AdID == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ad = await _context.Ads.SingleOrDefaultAsync(m => m.AdID == id);
            _context.Ads.Remove(ad);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AdExists(int id)
        {
            return _context.Ads.Any(e => e.AdID == id);
        }
    }
}
