using MiniEMR.Enums;

namespace MiniEMR.Entities
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public int VisitId { get; set; }
        public int DrugId { get; set; }
        public string Dosage { get; set; } = null!;
        public Frequency Frequency { get; set; }
        public string Duration { get; set; } = null!;
        public string? Instructions { get; set; }

        public Visit Visit { get; set; } = null!;

        public Drug Drug { get; set; } = null!;
    }
}
