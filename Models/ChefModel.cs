#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace chef_dishes.Models
{
    public class Chef
    {
        [Key]
        public int ChefId { get; set; }
        [Required(ErrorMessage = "El Nombre es requerido.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "El Apellido es requerido.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "La fecha de nacimiento es requerida.")]
        [MinimumAge(18, ErrorMessage = "El chef debe ser mayor de 18 años.")]
        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<Dish> AllDishes { get; set; } = new List<Dish>();
    }
}

public class MinimumAgeAttribute : ValidationAttribute
{
    private readonly int _minimumAge;

    public MinimumAgeAttribute(int minimumAge)
    {
        _minimumAge = minimumAge;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;

            if (dateOfBirth.Date > today.AddYears(-age)) age--;

            if (age >= _minimumAge)
            {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult($"El chef debe tener al menos {_minimumAge} años.");
    }
}