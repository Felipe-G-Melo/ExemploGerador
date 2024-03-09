using ApiExemplo.Dto;
using ApiExemplo.Entities;

namespace ApiExemplo.Interfaces;

public interface IUserRepository
{
    Task<UserDto> Add(UserDto user);
    Task<UserDto> Update(UserDto user, Guid id);
    Task<bool> Delete(Guid id);
    Task<List<UserEntity>> GetAll();
}
