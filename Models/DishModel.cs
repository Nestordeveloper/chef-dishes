#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace chef_dishes.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required(ErrorMessage = "El nombre del platillo es requerido.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El sabor es requerido.")]
        [Range(1, 5, ErrorMessage = "El sabor debe estar entre 1 y 5.")]
        public int Tastiness { get; set; }
        [Required(ErrorMessage = "Las calorías son requeridas.")]
        [Range(0, int.MaxValue, ErrorMessage = "Las calorías deben ser un número positivo.")]
        public int Calories { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int ChefId { get; set; }
        public Chef? Creator { get; set; }
    }
}
