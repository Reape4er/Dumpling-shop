using System.ComponentModel.DataAnnotations;

namespace Product.DB.Entities
{
    public class DbProduct
    {
        // Уникальный идентификатор товара в базе данных
        [Key]
        public int Id { get; set; }

        // Название товара
        [Required]
        public string Name { get; set; }

        // Описание товара, может включать детали, такие как ингредиенты или вес
        public string Description { get; set; }

        // Цена товара, используется тип decimal для точного представления цен
        [Required]
        public decimal Price { get; set; }

        // Количество товара на складе
        public int StockQuantity { get; set; } = 0;

        public string ImagePath { get; set; }

        // Дата и время создания записи о товаре, по умолчанию устанавливается текущее время
        public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

        // Дата и время удаления записи о товаре из активного доступа (null, если товар не удалён)
        public DateTimeOffset? Deleted { get; set; } = null;
    }
}
