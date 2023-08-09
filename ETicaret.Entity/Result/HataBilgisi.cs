namespace ETicaret.Entity.Result;

public class HataBilgisi
{
    public string HataAciklama { get; set; }

    public object Hata { get; set; }

    public static HataBilgisi NotFound(object? hata = null, string hataAciklama = "Sonuç Bulunamadı")
    {
        return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };
    }
    public static HataBilgisi Error(object? hata = null, string hataAciklama = "Genel Bir Hata Oluştu")
    {
        return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };
    }

    public static HataBilgisi AuthenticationError(object? hata = null, string hataAciklama = "Kullanıcı Bulunamadı Veya Yanlış Şifre")
    {
        return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };
    }
    public static HataBilgisi TokenNotFoundError(object? hata = null, string hataAciklama = "Token Bulunamadı")
    {
        return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };
    }
    
    public static HataBilgisi TokenError(object? hata = null, string hataAciklama = "Token Hatası")
    {
        return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };
    }

    public static HataBilgisi FieldValidationError(object? hata = null, string hataAciklama = "Zorunlu Alanlarda Eksiklikler Var")
    {
        return new HataBilgisi { Hata = hata, HataAciklama = hataAciklama };
    }
}