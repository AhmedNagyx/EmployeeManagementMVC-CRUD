using System.ComponentModel.DataAnnotations;

namespace MVCProject.Models
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Name cannot be empty.");
            }
            else
            {
                string name = value.ToString();
                ITIContext context = new ITIContext();
                Employee emp = context.Employee.FirstOrDefault(e => e.Name == name);
                Employee empForm= (Employee)validationContext.ObjectInstance;
                if (emp!=null)
                {
                    return new ValidationResult("The name is already taken. Please choose a different name.");
                }
                return ValidationResult.Success;
            }
        }
    }
}
