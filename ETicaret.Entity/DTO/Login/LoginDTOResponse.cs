namespace ETicaret.Entity.DTO.Login;

public class LoginDTOResponse : LoginDTOBase
{
    public string FullName { get; set; }

    public int UserId { get; set; }

    public string Email { get; set; }
    public string Token { get; set; }

    public string Address { get; set; }
}