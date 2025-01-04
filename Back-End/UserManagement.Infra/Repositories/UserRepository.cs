using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;
using UserManagement.Infra.Data;

namespace UserManagement.Infra.Repositories
{
    public class UserRepository : GenericRepository<User>, IUser
     {
        private readonly Context _context;
       

        public UserRepository(Context context) : base(context)
        {
            _context = context;
        }

       
       
    }

  
}

