namespace ETicaretAPI.Entity.Base;

public class BaseEntity
{
    public int ID { get; set; }

    public Guid GUID { get; set; }

    public bool? IsActive { get; set; }  // Null olması şart değil ama yapsan iyi
        
    public bool? IsDeleted { get; set; } // Gerçekten silindi mi? Yoksa sadece Pasif mi yapıldı.
}