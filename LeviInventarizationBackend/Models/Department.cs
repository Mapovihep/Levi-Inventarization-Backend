using ReactASPCore.Models;
using System.ComponentModel.DataAnnotations;

namespace Inventarization.Models
{
    public class Department
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public List<Inventory>? InventoryLots { get; set; } = new List<Inventory>();
        public List<InventorySetup>? InventorySetups { get; set; } = new List<InventorySetup>();
    }
}
