using API.Entities;
using API.UserService.Model;
using AutoMapper;

namespace API.UserService.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<User, UserDto>();

            CreateMap<UserRegisterDto, User>();
        }
    }
}
