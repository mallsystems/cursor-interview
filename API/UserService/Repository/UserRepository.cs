using API.DbContexts;
using API.Entities;
using API.UserService.Model;
using Microsoft.EntityFrameworkCore;

namespace API.UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly InterviewDBContext _context;

        public UserRepository(InterviewDBContext interviewContext)
        {
            _context = interviewContext ?? throw new ArgumentNullException(nameof(interviewContext));
        }

        public async Task<User> GetUserDetails(string IDCARD)
        {
            try
            {
                return await _context.Users
                    .Where(x => x.IDCARD == IDCARD)
                    .SingleAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> Login(UserLoginDto userLoginDto)
        {
            try
            {
                return await _context.Users
                    .Where(x => x.EMAIL == userLoginDto.EMAIL && x.PASSWORD == userLoginDto.PASSWORD)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Register(User userRegisterDto)
        {
            try
            {
                _context.Users.Add(userRegisterDto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool SaveChanges()
        {
            try
            {
                return _context.SaveChanges() >= 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() >= 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
