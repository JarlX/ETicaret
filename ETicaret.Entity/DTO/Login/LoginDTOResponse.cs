namespace ETicaret.Entity.DTO.Login;

public class LoginDTOResponse : LoginDTOBase
{
    public string FullName { get; set; }

    public int UserId { get; set; }
    public string Token { get; set; }
}