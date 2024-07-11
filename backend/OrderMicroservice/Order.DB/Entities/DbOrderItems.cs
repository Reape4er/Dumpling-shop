using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.DB.Entities
{
    public class DbOrderItems
    {
        [Key]
        public int id { get; set; }

        public int quantity { get; set; }
        public int ItemID { get; set; }
        
        [ForeignKey("DbOrder")]
        public int OrderID { get; set; }

        public virtual DbOrder DbOrder { get; set; }

        // Дата и время создания записи о товаре, по умолчанию устанавливается текущее время
        public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

        // Дата и время удаления записи о товаре из активного доступа (null, если товар не удалён)
        public DateTimeOffset? Deleted { get; set; } = null;
    }
}
