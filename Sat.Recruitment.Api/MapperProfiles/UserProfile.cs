using AutoMapper;
using Sat.Recruitment.Api.Models.Input;
using Sat.Recruitment.Api.Models.Output;
using Sat.Recruitment.Entities.Models;

namespace Sat.Recruitment.Api.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModelInput, User>();
            CreateMap<User, UserModelOutput>();
        }
    }
}
