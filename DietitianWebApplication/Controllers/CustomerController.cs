using AutoMapper;
using DietitianWebApplication.Models;
using DietitianWebApplication.Services;
using DietitianWebApplication.ViewModels.CustomerVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DietitianWebApplication.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerRepository customerRepository, 
            UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> MedicalProfile()
        {
            try
            {
                var medicalProfile = _mapper.Map<CustomerMedicalProfileVM>(await _customerRepository.GetMedicalProfileAsync(_userManager.GetUserAsync(User).Result.Id));

                if (medicalProfile != null)
                    return View(medicalProfile);

                return RedirectToAction(nameof(CreateMedicalProfile));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> CreateMedicalProfile()
        {
            try
            {
                var medicalProfile = await _customerRepository.GetMedicalProfileAsync(_userManager.GetUserAsync(User).Result.Id);

                if (medicalProfile == null)
                    return View(new AddEditMedicalProfileVM());

                return View(_mapper.Map<AddEditMedicalProfileVM>(medicalProfile));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMedicalProfile(AddEditMedicalProfileVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(new AddEditMedicalProfileVM());

                var medicalProfile = _mapper.Map<MedicalProfile>(model);
                medicalProfile.UserId = _userManager.GetUserAsync(User).Result.Id;

                await _customerRepository.AddMedicalProfileAsync(medicalProfile);

                return RedirectToAction(nameof(medicalProfile));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}