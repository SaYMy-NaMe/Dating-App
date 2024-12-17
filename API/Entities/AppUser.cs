namespace API.Entities;

//Entity framework's access modifier should always be public 

public class AppUser
{
    public int Id { get; set; }
    public required string UserName { get; set; }

    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
}
