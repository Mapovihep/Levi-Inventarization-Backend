using Inventarization.Models;
using System.ComponentModel.DataAnnotations;

namespace ReactASPCore.Models
{
    public class Employee
    {
        DateTime _new = DateTime.Now;
        [Required]
        public Guid Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Email { get; set; }
        [MaxLength(50)]
        [MinLength(8)]
        public string Password { get; set; }
        [MaxLength(30)]
        public string? FirstName { get; set; }
        [MaxLength(30)]
        public string? LastName { get; set; }
        [MaxLength(30)]
        public string? Phone { get; set; }
        public string? token { get; set; }
        public bool IsAdmin { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedAt 
        {
            get { return _new; }
            set { _new = value; } 
        } 
        public List<Inventory>? InventoryLots { get; set; } = new List<Inventory>();
        public List<InventorySetup>? InventorySetups { get; set; } = new List<InventorySetup>();

    }
}
