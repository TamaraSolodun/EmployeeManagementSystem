using API.Models.OfficeViewModels;
using AutoMapper;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]/[action]")]
    public class OfficeController : Controller
    {
        private readonly IOfficesDAL _officesDAL;
        private readonly IEmployeesDAL _employeesDAL;
        private readonly IMapper _mapper;

        public OfficeController(IOfficesDAL officesDAL, IEmployeesDAL employeesDAL, IMapper mapper)
        {
            _officesDAL = officesDAL;
            _employeesDAL = employeesDAL;
            _mapper = mapper;
        }

        // GET: /Office/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var offices = await _officesDAL.GetOfficesAsync();
            var officesVMs = _mapper.Map<IEnumerable<OfficeViewModel>>(offices);

            return View(officesVMs);
        }

        // GET: /Office/{id}
        [HttpGet("{officeId}")]
        public async Task<IActionResult> View(int officeId)
        {
            var employees = await _officesDAL.GetOfficeAsync(officeId, true);

            return View(_mapper.Map<OfficeViewModel>(employees));
        }

        // GET: /Office/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Office/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfficeViewModel officeVM)
        {
            if (ModelState.IsValid)
            {
                var office = _mapper.Map<Office>(officeVM);

                await _officesDAL.CreateOfficeAsync(office);
                return RedirectToAction(nameof(Index));
            }

            return View(officeVM);
        }

        // GET: /Office/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var office = await _officesDAL.GetOfficeAsync(id);
            if (office == null) return NotFound();

            var officeViewModel = _mapper.Map<OfficeViewModel>(office);

            return View(officeViewModel);
        }

        // POST: /Office/Edit/5
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OfficeViewModel officeVM)
        {
            if (ModelState.IsValid)
            {
                var office = _mapper.Map<Office>(officeVM);

                await _officesDAL.UpdateOfficeAsync(office);
                return RedirectToAction(nameof(Index));
            }

            return View(officeVM);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var office = await _officesDAL.GetOfficeAsync(id);
            if (office == null) return NotFound();

            var officeViewModel = _mapper.Map<OfficeViewModel>(office);

            return View(officeViewModel);
        }

        // POST: /Office/Delete/5
        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var office = await _officesDAL.GetOfficeAsync(id);
            if (office == null) return NotFound();

            await _officesDAL.DeleteOfficeAsync(office);
            return RedirectToAction(nameof(Index));
        }
    }
}