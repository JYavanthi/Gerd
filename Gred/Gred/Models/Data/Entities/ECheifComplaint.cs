namespace Gred.Data.Entities
{
    public class ECheifComplaint
    {
        public string Flag { get; set; }
        public int CheifCompliantID { get; set; }
        public int PatientID { get; set; }
    public int Stage { get; set; }

    public int DoctorID { get; set; }
        public int HB_Duration { get; set; }
        public int HB_Frequency { get; set; }
        public string HB_Postural { get; set; }
        public string HB_Nocturnal { get; set; }
        public int R_Duration { get; set; }
        public int R_Frequency { get; set; }
        public string R_Postural { get; set; }
        public string R_Nocturnal { get; set; }
        public int RP_Duration { get; set; }
        public int RP_Frequency { get; set; }
        public string RP_Postural { get; set; }
        public string RP_Nocturnal { get; set; }
        public int AT_Duration { get; set; }
        public int AT_Frequency { get; set; }
        public string AT_Postural { get; set; }
        public string AT_Nocturnal { get; set; }
        public int CreatedBy { get; set; }
    }
}
