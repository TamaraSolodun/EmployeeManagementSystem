﻿using System.ComponentModel.DataAnnotations;
using API.Models.EmployeeViewModels;

namespace API.Models.OfficeViewModels
{
    public class OfficeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public List<EmployeeViewModel>? Employees { get; set; } = new List<EmployeeViewModel>();
    }
}
