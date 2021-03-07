using AutoMapper;
using PorabayData.Models;
using PorabayData.ViewModels;

namespace Porabay
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Login, LoginVM>().ReverseMap();
            CreateMap<Registration, RegistrationVM>().ReverseMap();
            CreateMap<Domain, DomainVM>().ReverseMap();
            CreateMap<User, UserVM>().ReverseMap();
            CreateMap<Approval, ApprovalVM>().ReverseMap();
        }
    }
}
