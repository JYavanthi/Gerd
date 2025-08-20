namespace Gred.Data.Entities
{
    public class EFamilyHistory
    {
        public string Flag { get; set; }
        public int FamilyHistoryID { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
    public int? Stage { get; set; }

    public string FH_GRED { get; set; }
        public string FH_Remark { get; set; }
        public string FH_EGC { get; set; }
        public string FH_EGCRemark { get; set; }
    public string? gH_PPI { get; set; }
    public string Medication_Name { get; set; }
        public string Dose { get; set; }
        public string Frequency { get; set; }
        public int CreatedBy { get; set; }
    }
}
