namespace ETicaret.Helper.CustomException;

public class TokenNotFoundException : Exception
{
    public TokenNotFoundException(string message = "Token Bilgisi Gelmedi") : base(message)
    {

    }
}