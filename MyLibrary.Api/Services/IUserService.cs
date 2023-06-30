using MyLibrary.Api.Tests;

namespace MyLibrary.Api.Services
{
    public interface IUserService
    {
        int Create(User newUser);
        IReadOnlyList<User> GetAll();
        User? GetById(int id);
        bool Update(User updatedUser);
        bool Delete(int id);
    }
}
