using DietitianWebApplication.Data;
using DietitianWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DietitianWebApplication.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddMedicalProfileAsync(MedicalProfile medicalProfile)
        {
            _context.Update(medicalProfile);
            await _context.SaveChangesAsync();
        }

        public async Task<MedicalProfile> GetMedicalProfileAsync(string id)
        {
            if (id != null)
                return _context.MedicalProfiles.ToListAsync().Result.FirstOrDefault(mp => mp.UserId == id);

            return new MedicalProfile();
        }

    }
}
