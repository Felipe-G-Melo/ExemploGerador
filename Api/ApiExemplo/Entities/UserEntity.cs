namespace ApiExemplo.Entities;

public class UserEntity
{
    public Guid Id{ get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    public UserEntity(string username, string email, string password)
    {
        Id = Guid.NewGuid();
        Username = username;
        Email = email;
        Password = password;
    }

    public void Update(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
    }
}