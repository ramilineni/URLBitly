﻿namespace UserRegistration.Models.RequestModels
{
    public class PatientReq
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public DateTime DateOfBirth { get; set; }
    }
}
