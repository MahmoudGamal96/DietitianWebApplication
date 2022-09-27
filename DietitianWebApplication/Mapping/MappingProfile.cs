using AutoMapper;
using DietitianWebApplication.Models;
using DietitianWebApplication.ViewModels.CustomerVMs;

namespace DietitianWebApplication.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MedicalProfile, AddEditMedicalProfileVM>().ReverseMap();
            CreateMap<MedicalProfile, CustomerMedicalProfileVM>().ReverseMap();
        }
    }
}
