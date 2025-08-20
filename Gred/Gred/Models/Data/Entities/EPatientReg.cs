namespace Gred.Data.Entities
{
    public class EPatientReg
    {
        public string Flag { get; set; }
        public int PatientID { get; set; }
       public int doctorID { get; set; }
    public int? Stage { get; set; }

    public string Initial { get; set; }
        public string SubjectNo { get; set; }
        public DateTime Date { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Education { get; set; }
        public string Occupation { get; set; }
        public int State { get; set; }
        public int City { get; set; }
        public int Pincode { get; set; }
        public string PlaceType { get; set; }
        public string SocioeconomicStatus { get; set; }
        public string FamilyIncome { get; set; }
        public string PastHistory { get; set; }
        public string Diet { get; set; }
        public int CreatedBy { get; set; }
    }
}
