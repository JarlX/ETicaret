namespace ETicaretAPI.Entity.DTO.Login;

public class LoginDTORequest : LoginDTOBase
{
    public string UserName { get; set; }
    
    public string Password { get; set; }
}