using DisasterAlleviation.Web.Data;
using DisasterAlleviation.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviation.Web.Controllers
{
    [Authorize]
    public class DonationsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public DonationsController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var donations = await _db.Donations.OrderByDescending(d => d.DonatedAt).ToListAsync();
            return View(donations);
        }

        public IActionResult Create()
        {
            return View(new Donation{ Quantity = 1 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Donation donation)
        {
            if (!ModelState.IsValid) return View(donation);
            donation.DonorUserId = _userManager.GetUserId(User);
            _db.Donations.Add(donation);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var donation = await _db.Donations.FindAsync(id);
            if (donation == null) return NotFound();
            return View(donation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Donation donation)
        {
            if (!ModelState.IsValid) return View(donation);
            _db.Update(donation);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var donation = await _db.Donations.FindAsync(id);
            if (donation == null) return NotFound();
            return View(donation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _db.Donations.FindAsync(id);
            if (donation == null) return NotFound();
            _db.Donations.Remove(donation);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleAllocated(int id)
        {
            var donation = await _db.Donations.FindAsync(id);
            if (donation == null) return NotFound();
            donation.Allocated = !donation.Allocated;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
