using MyLibrary.Api.Tests;

namespace MyLibrary.Api.Dtos
{
    public static class UserDtoMapper
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Email = user.Email,
                Name = user.Name,
                Id = user.Id
            };
        }

        public static User ToUser(this UserDto user)
        {
            return new User
            {
                Email = user.Email,
                Name = user.Name,
                Id = user.Id
            };
        }
    }
}
