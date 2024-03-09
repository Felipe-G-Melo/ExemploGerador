using ApiExemplo.Dto;
using ApiExemplo.Entities;
using ApiExemplo.Interfaces;

namespace ApiExemplo.Repositories;

public class UserRepository : IUserRepository
{
    public List<UserEntity> Users { get; private set; } = new List<UserEntity>();
    public Task<UserDto> Add(UserDto user)
    {
        var entity = new UserEntity(user.Username, user.Email, user.Password);
        Users.Add(entity);
        return Task.FromResult(user);
    }

    public Task<bool> Delete(Guid id)
    {
        var entity = Users.FirstOrDefault(u => u.Id == id);
        Users.Remove(entity!);
        return Task.FromResult(true);
    }

    public Task<UserDto> Update(UserDto user, Guid id)
    {
        var entity = Users.FirstOrDefault(u => u.Id == id);
        entity!.Update(user.Username, user.Email, user.Password);
        return Task.FromResult(user);
    }

    public Task<List<UserEntity>> GetAll()
    {
        return Task.FromResult(Users);
    }
}
