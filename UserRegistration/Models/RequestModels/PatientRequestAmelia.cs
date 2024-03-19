namespace UserRegistration.Models.RequestModels
{
    public class PatientRequestAmelia
    {
        public PatientReq Patient { get; set; }
        public bool OptIn { get; set; } = true;
        public string Intents { get; set; } = string.Empty;
        
    }
}
