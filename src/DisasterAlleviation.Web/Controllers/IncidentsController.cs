using DisasterAlleviation.Web.Data;
using DisasterAlleviation.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviation.Web.Controllers
{
    [Authorize]
    public class IncidentsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public IncidentsController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var incidents = await _db.Incidents
                .OrderByDescending(i => i.Date)
                .ToListAsync();
            return View(incidents);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var incident = await _db.Incidents.FindAsync(id);
            if (incident == null) return NotFound();
            return View(incident);
        }

        public IActionResult Create()
        {
            return View(new Incident{ Date = DateTime.UtcNow });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Incident incident)
        {
            if (!ModelState.IsValid) return View(incident);
            incident.ReporterUserId = _userManager.GetUserId(User);
            _db.Incidents.Add(incident);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var incident = await _db.Incidents.FindAsync(id);
            if (incident == null) return NotFound();
            return View(incident);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Incident incident)
        {
            if (!ModelState.IsValid) return View(incident);
            _db.Update(incident);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var incident = await _db.Incidents.FindAsync(id);
            if (incident == null) return NotFound();
            return View(incident);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incident = await _db.Incidents.FindAsync(id);
            if (incident == null) return NotFound();
            _db.Incidents.Remove(incident);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
