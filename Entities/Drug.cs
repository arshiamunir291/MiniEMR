using MiniEMR.Enums;

namespace MiniEMR.Entities
{
    public class Drug
    {
        public int DrugId { get; set; }
        public string DrugName { get; set; } = null!;
        public string? GenericName { get; set; }
        public string? Strength { get; set; }
        public DrugForm DrugForm { get; set; } 
        public string? Manufacturer { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    }
}
