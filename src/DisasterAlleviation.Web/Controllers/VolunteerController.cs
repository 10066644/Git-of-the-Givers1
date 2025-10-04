using DisasterAlleviation.Web.Data;
using DisasterAlleviation.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviation.Web.Controllers
{
    [Authorize]
    public class VolunteerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public VolunteerController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var tasks = await _db.VolunteerTasks
                .OrderBy(t => t.StartAt)
                .ToListAsync();
            return View(tasks);
        }

        public IActionResult Create()
        {
            return View(new VolunteerTask());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VolunteerTask task)
        {
            if (!ModelState.IsValid) return View(task);
            _db.VolunteerTasks.Add(task);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var task = await _db.VolunteerTasks.FindAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VolunteerTask task)
        {
            if (!ModelState.IsValid) return View(task);
            _db.Update(task);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _db.VolunteerTasks.FindAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _db.VolunteerTasks.FindAsync(id);
            if (task == null) return NotFound();
            _db.VolunteerTasks.Remove(task);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(int id)
        {
            var task = await _db.VolunteerTasks.FindAsync(id);
            if (task == null) return NotFound();

            var userId = _userManager.GetUserId(User);
            var count = await _db.VolunteerAssignments.CountAsync(a => a.TaskId == id);
            var already = await _db.VolunteerAssignments.AnyAsync(a => a.TaskId == id && a.UserId == userId);
            if (already || count >= task.Capacity)
            {
                return RedirectToAction(nameof(Index));
            }
            _db.VolunteerAssignments.Add(new VolunteerAssignment { TaskId = id, UserId = userId! });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(MyAssignments));
        }

        public async Task<IActionResult> MyAssignments()
        {
            var userId = _userManager.GetUserId(User);
            var taskIds = await _db.VolunteerAssignments
                .Where(a => a.UserId == userId)
                .Select(a => a.TaskId)
                .ToListAsync();
            var tasks = await _db.VolunteerTasks.Where(t => taskIds.Contains(t.Id)).ToListAsync();
            return View(tasks);
        }
    }
}
