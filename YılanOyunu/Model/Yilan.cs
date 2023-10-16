using System.Collections.Generic;

namespace YılanOyunu
{
    class Yilan
    {
        bool yonDegistirebilir = true; //Yılan yönü ve klavyenin girişi taramak için bool sınıfından bir değişken
        Yonlar yilanYonu; //Yonlar sınıfından bir değişken tanımla ve yilanYonu olarak adlandır

        
        public List<Nokta> YilanCismi;

        public Yilan(int x, int y) //Yapıcı
        {
            YilanCismi = new List<Nokta>(); //Nokta sınfından yılan kuyruğu için bir liste dizisi oluştur
            
            YilanCismi.Add(new Nokta(x, y)); //Yılana bir parça ekle
            YilanCismi.Add(new Nokta(x - 10, y)); //yılana 2. parçası ekle

            yilanYonu = Yonlar.Right; //yılan yönü sağa doğru olarak ayarla
        }

        public void YilanHareketEttir()
        {
            //Yılanı ve parçalarını hareket ettir
            for (int i = YilanCismi.Count - 1; i > 0; --i)
                YilanCismi[i].Set(YilanCismi[i - 1]);

            //başın hangi yöne hareket ettiğine göre diğer parçalarını hareket ettir
            if (yilanYonu == Yonlar.Left)
                YilanCismi[0].X -= 10;

            if (yilanYonu == Yonlar.Right)
                YilanCismi[0].X += 10;

            if (yilanYonu == Yonlar.Up)
                YilanCismi[0].Y -= 10;

            if (yilanYonu == Yonlar.Down)
                YilanCismi[0].Y += 10;

            yonDegistirebilir = true; //Yılan yönünü değiştirmeye izin ver 
        }

        public void YonuAyarla(Yonlar klavyeGirisYonu)
        {
            if (yonDegistirebilir)
            {
                //Yılan yönü, giriş yönünün tersi ise hiçbir şey yapmadan çık (yasaklı durumlar)
                if (yilanYonu == Yonlar.Left && klavyeGirisYonu == Yonlar.Right)
                    return;
                if (yilanYonu == Yonlar.Right && klavyeGirisYonu == Yonlar.Left)
                    return;
                if (yilanYonu == Yonlar.Up && klavyeGirisYonu == Yonlar.Down)
                    return;
                if (yilanYonu == Yonlar.Down && klavyeGirisYonu == Yonlar.Up)
                    return;
                
                //Burası ise giriş yönü kabull et (yasaklı bir durumla rastlamamış demektir)
                yilanYonu = klavyeGirisYonu; //Yılan yönü giriş yönüne eşit yap
                yonDegistirebilir = false; //Bu metottan çıkması için
            }
        }
    }
}
