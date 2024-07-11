using System.ComponentModel.DataAnnotations;

namespace Order.DB.Entities
{
    public class DbOrder
    {
        // Уникальный идентификатор товара в базе данных
        [Key]
        public int Id { get; set; }

        //public string PaymentMethod { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<DbOrderItems> OrderItems { get; set; }

        // Дата и время создания записи о товаре, по умолчанию устанавливается текущее время
        public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

        // Дата и время удаления записи о товаре из активного доступа (null, если товар не удалён)
        public DateTimeOffset? Deleted { get; set; } = null;
    }
}
