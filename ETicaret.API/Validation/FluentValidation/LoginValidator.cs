namespace ETicaret.API.Validation.FluentValidation;

using Entity.DTO.Login;
using global::FluentValidation;

public class LoginValidator : AbstractValidator<LoginDTORequest>
{
    public LoginValidator()
    {
        RuleFor(q => q.UserName).NotEmpty().WithMessage("Kullanıcı Adı Boş Olamaz");
        RuleFor(q => q.Password).NotEmpty().WithMessage("Şifre Adı Boş Olamaz");
    }
}