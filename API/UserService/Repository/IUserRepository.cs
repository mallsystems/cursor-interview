using API.Entities;
using API.UserService.Model;

namespace API.UserService.Repository
{
    public interface IUserRepository
    {
        Task<User> Login(UserLoginDto userLoginDto);
        void Register(User userRegisterDto);
        Task<User> GetUserDetails(string IDCARD);
        bool SaveChanges();
        Task<bool> SaveChangesAsync();
    }
}
