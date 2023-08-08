namespace ETicaret.Entity.Result;

public class HataBilgisi
{
    public string HataAciklama { get; set; }

    public object Hata { get; set; }

    public static HataBilgisi NotFound(object? hata = null, string hataAciklama = "Sonuç Bulunamadı")
    {
        return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };
    }

    public static HataBilgisi AuthenticationError(object? hata = null, string hataAciklama = "Kullanıcı Bulunamadı")
    {
        return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };
    }

    public static HataBilgisi FieldValidationError(object? hata = null, string hataAciklama = "Zorunlu Alanlarda Eksiklikler Var")
    {
        return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };
    }
}