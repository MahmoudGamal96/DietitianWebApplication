using DietitianWebApplication.Models;

namespace DietitianWebApplication.Services
{
    public interface ICustomerRepository
    {
        public Task AddMedicalProfileAsync(MedicalProfile medicalProfile);
        public Task<MedicalProfile> GetMedicalProfileAsync(string id);
    }
}
