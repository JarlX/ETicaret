namespace ETicaret.Entity.DTO.Category;

public class CategoryUpdateDTORequest
{
    public string CategoryName  { get; set; }

    public Guid? Guid { get; set; }

    public bool? IsActive { get; set; }
}