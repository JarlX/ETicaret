namespace ETicaretAPI.Entity.DTO.Category;

public class CategoryDTOBase : BaseDTO
{
    public Guid? Guid { get; set; }

    public string CategoryName  { get; set; }
}