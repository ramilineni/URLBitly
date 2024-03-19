using Newtonsoft.Json;
using UserRegistration.Interface;
using UserRegistration.Models;
using UserRegistration.Models.RequestModels;

namespace UserRegistration.Service
{
    public class CallToSaveDataInAmelia : ICallToSaveData
    {
        public async Task<bool> Save(Patient patient)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = "http://localhost:7054/api/SavePatientData";
                    PatientRequestAmelia pReq = new()
                    {
                        Patient = new()
                        {
                            FirstName = patient.FirstName,
                            LastName = patient.LastName,
                            PhoneNumber = patient.PhoneNumber,
                            DateOfBirth = patient.DateOfBirth
                        }
                    };
                    string callData = JsonConvert.SerializeObject(pReq);

                    StringContent sContent = new StringContent(callData, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage res = await client.PostAsync(url, sContent);

                    if (res.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;

                }catch(Exception ex)
                {
                    //log error
                    return false;
                }
            }
        }

        
    }
}
