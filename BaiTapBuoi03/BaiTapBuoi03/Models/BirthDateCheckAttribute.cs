using System.ComponentModel.DataAnnotations;

namespace BaiTapBuoi03.Models
{
    public class BirthDateCheckAttribute : ValidationAttribute
    {
        public BirthDateCheckAttribute() : base("Ngày sinh chưa hợp lệ") { }

        //public override bool IsValid(object? value)
        //{
        //    return base.IsValid(value);
        //}

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // 1. Ngày sinh bắt buộc
            if (value == null)
            {
                return new ValidationResult("Ngày sinh phải nhập");
            }

            // 2. Tuổi >= 18 (DateTime.Now.Year - BirthDate.Year >= 10)
            var birthDate = (DateTime)value;
            if (DateTime.Now.Year - birthDate.Year < 18)
            {
                return new ValidationResult("Tuổi phải lớn hơn hoặc bằng 18");
            }
            else if(DateTime.Now.Year - birthDate.Year > 62)
            {
                return new ValidationResult("Tuổi phải nhỏ hơn hoặc bằng 62");
            }
            return ValidationResult.Success;
        }
    }
}