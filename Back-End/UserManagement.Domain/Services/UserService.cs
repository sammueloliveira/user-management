using System.Net.Http.Json;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;
using UserManagement.Domain.ServicesInterfaces;

namespace UserManagement.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUser _user;
        private readonly HttpClient _httpClient;

        public UserService(IUser user, HttpClient httpClient)
        {
            _user = user;
            _httpClient = httpClient;
        }

        public async Task<User> AddUser(User user)
        {
            await _user.Add(user);
            return user;
        }

        public async Task UpdateUser(User user)
        {
            var existingUser = await _user.GetEntityById(user.Id);

            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.Gender = user.Gender;
                existingUser.City = user.City;
                existingUser.StreetName = user.StreetName; 
                existingUser.StreetNumber = user.StreetNumber; 
                existingUser.State = user.State;
                existingUser.Country = user.Country;
                existingUser.PostCode = user.PostCode;
                existingUser.Phone = user.Phone ?? string.Empty;
                existingUser.Cell = user.Cell;
                existingUser.ProfilePicture = user.ProfilePicture;

                
                await _user.Update(existingUser);
            }
        }



        public async Task<List<User>> FetchAndSaveRandomUsers(int count)
        {
            var response = await _httpClient.GetFromJsonAsync<RandomUserResponse>($"https://randomuser.me/api/?results={count}");

            if (response == null || response.Results == null)
                throw new Exception("Erro ao consumir a API Random User Generator.");

            var randomUsers = response.Results.Select(r => new User
            {
                FirstName = r.Name.First,
                LastName = r.Name.Last,
                Email = r.Email,
                Gender = r.Gender,
                Phone = r.Phone,
                Cell =  r.Cell,
                City = r.Location.City,
                StreetName = r.Location.Street?.Name,
                StreetNumber = r.Location.Street?.Number ?? 0,
                State = r.Location.State,
                Country = r.Location.Country,
                PostCode =  r.Location.PostCode,
                ProfilePicture = r.Picture.Large
            }).ToList();


            foreach (var user in randomUsers)
            {
                await _user.Add(user);
            }

            return randomUsers;
        }
    }

}

