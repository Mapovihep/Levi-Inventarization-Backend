using ReactASPCore.Models;
using System.ComponentModel.DataAnnotations;

namespace Inventarization.Models
{
    public class InventorySetup
    {
        private Guid _id = Guid.NewGuid();
        private DateTime _newDate = DateTime.Now;
        [Required]
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [MaxLength(100)]
        public string? Category { get; set; }
        [MaxLength(100)]
        public string? RoomName { get; set; }
        public string? OwnerId { get; set; }
        public string? Img { get; set; }
        [MaxLength(500)]
        public string? Defects { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime UpdatedAt
        {
            get { return _newDate; }
            set { _newDate = value; }
        }
        public List<Inventory>? Setup = new List<Inventory>();

    }
}
