using API.Models;
using API.Models.EmployeeViewModels;
using API.Models.OfficeViewModels;
using AutoMapper;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeesDAL _employeesDAL;
        private readonly IRolesDAL _rolesDAL;
        private readonly IOfficesDAL _officesDAL;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeesDAL employeesDAL,
            IRolesDAL rolesDAL,
            IOfficesDAL officesDAL,
            IMapper mapper)
        {
            _employeesDAL = employeesDAL;
            _rolesDAL = rolesDAL;
            _officesDAL = officesDAL;
            _mapper = mapper;

        }

        // GET: /Employees/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _employeesDAL.GetEmployeesAsync();
            var employeesVM = _mapper.Map<List<EmployeeViewModel>>(employees);
            return View(employeesVM);
        }

        // GET: /Employees/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = await _rolesDAL.GetRolesAsync();
            var offices = await _officesDAL.GetOfficesAsync();
            var vm = new CreateEmployeeRequest
            {
                RoleIds = new List<int>()
            };

            ViewBag.Roles = _mapper.Map<List<RoleViewModel>>(roles);
            ViewBag.Offices = _mapper.Map<List<OfficeViewModel>>(offices);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeRequest employeeRequest)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));
            }

            var employee = _mapper.Map<Employee>(employeeRequest);
            if (employeeRequest.RoleIds != null && employeeRequest.RoleIds.Any())
            {
                employee.EmployeeRoles = employeeRequest.RoleIds
                    .Select(roleId => new EmployeeRole { RoleId = roleId })
                    .ToList();
            }
            await _employeesDAL.CreateEmployeeAsync(employee);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Employees/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeesDAL.GetEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var roles = await _rolesDAL.GetRolesAsync();
            var offices = await _officesDAL.GetOfficesAsync();
            var vm = _mapper.Map<EditEmployeeRequest>(employee);
            vm.RoleIds = employee.EmployeeRoles?.Select(er => er.RoleId).ToList() ?? new List<int>();
            ViewBag.Roles = _mapper.Map<List<RoleViewModel>>(roles);
            ViewBag.Offices = _mapper.Map<List<OfficeViewModel>>(offices);

            return View(vm);
        }

        // POST: /Employees/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditEmployeeRequest employeeRequest)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit), new { id });
            }

            var employee = await _employeesDAL.GetEmployeeAsync(id);
            employee.Name = employeeRequest.Name;
            employee.Position = employeeRequest.Position;
            employee.OfficeId = employeeRequest.OfficeId;
            if (employeeRequest.RoleIds != null)
            {
                employee.EmployeeRoles = employeeRequest.RoleIds
                    .Select(roleId => new EmployeeRole() { RoleId = roleId, EmployeeId = id })
                    .ToList();
            }
            await _employeesDAL.UpdateEmployeeAsync(employee);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Employees/Delete/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeesDAL.GetEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);

            return View(employeeViewModel);
        }

        // POST: /Employees/Delete/5
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _employeesDAL.GetEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _employeesDAL.DeleteEmployeeAsync(employee);

            return RedirectToAction(nameof(Index));
        }
    }
}