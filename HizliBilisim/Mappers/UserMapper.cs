using HizliBilisim.DTOs;
using HizliBilisim.models;

namespace HizliBilisim.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                // Map other properties as needed
            };
        }

        public static User ToEntity(this UserDto userDto)
        {
            return new User
            {
                UserId = userDto.UserId,
                UserName = userDto.UserName,
                // Map other properties if needed
            };
        }

        public static User ToEntity(this UserForCreationDto dto)
        {
            return new User
            {
                UserName = dto.UserName,
                Password = dto.Password
                // Map other fields if needed
            };
        }
    }
}
