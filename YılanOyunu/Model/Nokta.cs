namespace YılanOyunu
{
    //Bu, Yonlar adlı bir enum sınıfıdır
    //bu oyun için, yönleri sınıflandırmak daha kolay olması için enum kullanıyoruz 
    public enum Yonlar
    {
        Up,
        Down,
        Right,
        Left
    }

    class Nokta
    {
        
        public int X { get; set; } //noktanın X'yi
        public int Y { get; set; } //noktanın Y'yi

        public Nokta(int x, int y) //Yapıcı
        {
            X = x;
            Y = y;
        }

        public void Set(Nokta nokta) //her hangi noktanın X'i ve Y'i belirle
        {
            X = nokta.X;
            Y = nokta.Y;
        }

    }
}
