namespace ETicaret.API.Validation.FluentValidation;

using Entity.DTO.Category;
using global::FluentValidation;

public class CategoryValidator : AbstractValidator<CategoryDTORequest>
{
    public CategoryValidator()
    {
        RuleFor(q => q.CategoryName).NotEmpty().WithMessage("Kategori Adı Boş Olamaz");
    }
}