using MyLibrary.Api.Tests;

namespace MyLibrary.Api.Services
{
    public class UserService: IUserService
    {
        private Dictionary<int, User> _userStore = new();
        public int Create(User newUser)
        {

            var id = _userStore.Count() + 1;
            newUser.Id = id;
            _userStore[id] = newUser;
            return id;
        }

        public bool Delete(int id)
        {
            return _userStore.Remove(id);
        }

        public IReadOnlyList<User> GetAll()
        {
            return _userStore.Values.ToList();
        }

        public User? GetById(int id)
        {
            return _userStore.GetValueOrDefault(id);
        }

        public bool Update(User updatedUser)
        {
            var user = GetById(updatedUser.Id);
            if (user is null)
                return false;
            _userStore[updatedUser.Id] = updatedUser;
            return true;
        }
    }
}
