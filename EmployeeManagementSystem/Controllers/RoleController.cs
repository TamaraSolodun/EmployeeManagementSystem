using DAL;
using DAL.Interfaces;
using DAL.Models;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("[controller]/[action]")]
    public class RoleController : Controller
    {
        private readonly IRolesDAL _rolesDAL;

        public RoleController(IRolesDAL rolesDAL)
        {
            _rolesDAL = rolesDAL;
        }

        // GET: /Role/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = (await _rolesDAL.GetRolesAsync()).Select(role => new RoleViewModel
            {
                Id = role.Id,
                Title = role.Title
            });

            return View(roles);
        }

        // GET: /Role/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleVM)
        {
            if (ModelState.IsValid)
            {
                var role = new Role
                {
                    Title = roleVM.Title
                };

                await _rolesDAL.CreateRoleAsync(role);
                return RedirectToAction(nameof(Index));
            }

            return View(roleVM);
        }

        // GET: /Role/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var role = await _rolesDAL.GetRoleAsync(id);
            if (role == null) return NotFound();

            var roleViewModel = new RoleViewModel
            {
                Id = role.Id,
                Title = role.Title,
            };

            return View(roleViewModel);
        }

        // POST: /Role/Edit/5
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoleViewModel roleVM)
        {
            if (ModelState.IsValid)
            {
                var role = new Role
                {
                    Id = roleVM.Id,
                    Title = roleVM.Title,
                };

                await _rolesDAL.UpdateRoleAsync(role);
                return RedirectToAction(nameof(Index));
            }

            return View(roleVM);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _rolesDAL.GetRoleAsync(id);
            if (role == null) return NotFound();

            var roleViewModel = new RoleViewModel
            {
                Id = role.Id,
                Title = role.Title,
            };

            return View(roleViewModel);
        }

        // POST: /Role/Delete/5
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _rolesDAL.GetRoleAsync(id);
            if (role == null) return NotFound();

            await _rolesDAL.DeleteRoleAsync(role);
            return RedirectToAction(nameof(Index));
        }
    }
}