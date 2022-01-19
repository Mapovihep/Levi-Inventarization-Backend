using System.ComponentModel.DataAnnotations;
namespace ReactASPCore.Models
{
    public class Inventory
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
        [MaxLength(30)]
        public string? RoomName { get; set; }
        public Guid? OwnerId { get; set; }
        [MaxLength(500)]
        public string? Img { get; set; }
        [MaxLength(500)]
        public string? Defects { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime UpdatedAt 
        {
            get { return _newDate; }
            set { _newDate = value; }
        }
    }
}
