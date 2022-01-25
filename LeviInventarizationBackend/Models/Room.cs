using Inventarization.Models;
using System.ComponentModel.DataAnnotations;
namespace ReactASPCore.Models
{
    public class Room
    {
        private Guid _id = Guid.NewGuid();
        [Required]
        public Guid Id
        {
            get { return _id; }
            set { _id = Guid.NewGuid(); }
            // set {_id = value; }
        }
        [Required]
        public string Name { get; set; } = null!;
        public string CreatedAt { get; set; } = null;

        public List<Inventory>? InventoryLots { get; set; } = new List<Inventory>();
        public List<InventorySetup>? InventorySetups { get; set; } = new List<InventorySetup>();
    }
}
