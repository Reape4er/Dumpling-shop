using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace Product.API.Models
{
    public class DtoProduct
    {
        // Уникальный идентификатор товара в базе данных
        public int Id { get; set; }

        // Название товара
        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        // Описание товара, может включать детали, такие как ингредиенты или вес
        public string Description { get; set; }

        // Цена товара, используется тип decimal для точного представления цен
        [Required]
        [Range(0, float.MaxValue)]
        public decimal Price { get; set; }

        // Количество товара на складе
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        public string Image { get; set; }
    }
}
