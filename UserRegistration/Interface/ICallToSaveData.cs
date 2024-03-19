using UserRegistration.Models;

namespace UserRegistration.Interface
{
    public interface ICallToSaveData

    {
        Task<bool> Save(Patient patient);
    }
}
