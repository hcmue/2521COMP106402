using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BaiTapBuoi03.Models
{
    public class Employee
    {
        public int Id { get; set; } = 0;

        [StringLength(20, MinimumLength = 5, ErrorMessage = "EmployeeNo must be between 5 and 20 characters.")]
        [Remote(action: "CheckEmployeeNoExists", controller: "Employee", ErrorMessage = "EmployeeNo đã bị lấy.")]
        public string EmployeeNo { get; set; }

        [MaxLength(100, ErrorMessage = "FullName cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password must match Password.")]
        public string ConfirmPassword { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Url]
        public string? Website { get; set; }

        [DataType(DataType.Date)]
        [BirthDateCheck]
        public DateTime BirthDate { get; set; }

        public bool Gender { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public double Salary { get; set; }

        public string? Address { get; set; }

        [RegularExpression("^0[35789]\\d{8}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string Phone { get; set; }

        [CreditCard]
        public string? CreditCard { get; set; }

        [MaxLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
