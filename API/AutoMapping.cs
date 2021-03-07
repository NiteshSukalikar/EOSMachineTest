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
            CreateMap<Approval, ApprovalVM>().ReverseMap();
        }
    }
}
