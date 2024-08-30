using Caronas.Application.Contratos;
using Caronas.Domain;
using Caronas.Persistence.Contratos;

namespace Caronas.Application
{
    public class UserService : IUserService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IUserPersist _userPersist;

        public UserService(IGeralPersist geralPersist, IUserPersist userPersist)
        {
            _geralPersist = geralPersist;
            _userPersist = userPersist;   
        }

        public async Task<User> AddUser(User model)
        {
            try
            {
                _geralPersist.Add<User>(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _userPersist.GetUserByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> UpdateUser(string userId, User model)
        {
            try
            {
                var user = await _userPersist.GetUserByIdAsync(userId);
                if (user == null) return null;

                model.Id = user.Id;
                _geralPersist.Update(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _userPersist.GetUserByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteUser(string userId)
        {
            try
            {
                var user = await _userPersist.GetUserByIdAsync(userId);
                if (user == null) throw new Exception("Usuário para delete não encontrado");

                _geralPersist.Delete<User>(user);
                return await _geralPersist.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User[]> GetAllUsersAsync()
        {
            try
            {
                var users = await _userPersist.GetAllUsersAsync();
                if (users == null) return null;

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            try
            {
                return await _userPersist.GetUserByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}