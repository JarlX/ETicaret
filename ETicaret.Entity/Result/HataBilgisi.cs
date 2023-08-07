namespace ETicaret.Entity.Result;

public class HataBilgisi
{
    public string HataAciklama { get; set; }

    public object Hata { get; set; }

    public static HataBilgisi NotFound(string hataAciklama = "Sonuç Bulunamadı", object? hata = null)
    {
        return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };
    }

    public static HataBilgisi AuthenticationError(string hataAciklama = "Kullanıcı Bulunamadı", object? hata = null)
    {
        return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };
    }

    public static HataBilgisi FieldValidationError(string hataAciklama = "Zorunlu Alanlarda Eksiklikler Var", object? hata = null)
    {
        return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };
    }
}