namespace ETicaretAPI.Entity.Result;

public class Sonuc<T>
{
    public Sonuc(T _data,string _mesaj,int _statusCode,HataBilgisi _hataBilgisi)
    {
        Data = _data;
        Mesaj = _mesaj;
        StatusCode = _statusCode;
        Hatabilgisi = _hataBilgisi;

    }
    public T Data { get; set; }

    public string Mesaj { get; set; }

    public int StatusCode { get; set; }

    public HataBilgisi Hatabilgisi { get; set; }
}
