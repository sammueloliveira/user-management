using UserManagement.Domain.Entities;

namespace UserManagement.Domain.ServicesInterfaces
{
     public interface IUserService
     {

        Task<User> AddUser(User user);
        Task UpdateUser(User user);
        Task<List<User>> FetchAndSaveRandomUsers(int count);

     }
}
