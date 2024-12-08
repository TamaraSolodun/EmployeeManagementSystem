using AutoMapper;
using DAL;
using DAL.Interfaces;
using DAL.Models;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.EmployeeViewModels;
using EmployeeManagementSystem.Models.OfficeViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
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
            var vm = new CreateEmployeeRequest();
            ViewBag.Roles = _mapper.Map<List<RoleViewModel>>(roles);
            ViewBag.Offices = _mapper.Map<List<OfficeViewModel>>(offices);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeRequest employeeRequest)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(employeeRequest);
                await _employeesDAL.CreateEmployeeAsync(employee);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Create)); ;
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
            ViewBag.Roles = _mapper.Map<List<RoleViewModel>>(roles);
            ViewBag.Offices = _mapper.Map<List<OfficeViewModel>>(offices);

            return View(vm);
        }

        // POST: /Employees/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditEmployeeRequest employeeRequest)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(employeeRequest);
                employee.Id = id;
                await _employeesDAL.UpdateEmployeeAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Roles = await _rolesDAL.GetRolesAsync();
            ViewBag.Offices = await _officesDAL.GetOfficesAsync();

            return View(employeeRequest);
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