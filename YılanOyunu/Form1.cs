using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO; //Dosyalar oluşturmak ve açmak için

namespace YılanOyunu
{
    public partial class Form1 : Form
    {
        private const string Path = (@"D:\Skorlar.txt"); //Açık yol yazılmalı ex: @("C:\Users\Ex\source\repos\YılanOyunu\Skorlar.txt")
        int saniye = 0; //Oyun saniyeler süresi
        int dakika = 0; //Oyun dakikalar süresi
        int puanSaniyesi = 0; //yılan kaç saniye sonra nesneyi yediği hesaplamak için bir değişken
        double puan = 0; //Puan için bir değişken
        int sayac = 0; //Klavyenin (D) tuşunu Kontrol etmek için bir sayaç
        int sayac1 = 0; //Klavyenin (B) tuşunu Kontrol etmek için bir sayaç
        int xSet = 250; //Yılanın başlangıç noktasının X'i
        int ySet = 150; //Yılanın başlangıç noktasının Y'yi
        int x1; //nesnenin X'i
        int y1; //nesnenin Y'yi

        PictureBox nesne = new PictureBox(); //nesne için bir picture box belirle

        Yilan yilan1; //Yılan sınıfınden bir nesne belirleyip yilan1 olarak adlandır

        

        public Form1()
        {
            InitializeComponent();          
        }

        private void button1_Click(object sender, EventArgs e) //Yardım buttonu tanımlama
        {
            MessageBox.Show("Bu Proje SEYMA ALKAYM Tarafından Yazılmış.\nİstediğiniz Seviye Seçimi İle Oyunu Oyunabilirsiniz.\n" +
                "Oyunu Durdurmak İçin [D Tuşunu]\nOyunu Başlatmak İçin [B Tuşunu] Kullanınız.\n" +
                "Oyunu Başlatmadan Önce İsminizi Kaydediniz(Kişi Kaydet'e Basınız) " +
                "Sonra B Tuşuna Basınız.\nBAŞARILAR ...\n" + "  ---OYUN KURALLARI--- \n" + "---------------------------------------\n" +
                "1. Yılan, panel'in ortasından sağa doğru hareket etmeye başlar.\n" + "2. Yılan, her iki seviyede de sabit bir hızda hareket eder.\n"+
                "3. Yılan yalnızca uykarıya, aşağıya, sağa veya sola hareket eder. Yılanın hareketi yön tuşları ile kontrol edilebilir.\n"
                + "4. Nesne panel içinde rastgele yerlerde beliriyor ve nesnesi 100 saniye geçmeden yemenizi gerekiyor, aksi takdirde üzerine puan almayacaksınız.\n"
                + "5. Oyun yılan _ölünceye_ kadar devam eder.\n" + "6. Yılan ya panel'in kenarına koşarak ya da kendi kuyruğuna çarparak ölür.\n"
                + "7. Sonuç, yılanın kaç saniyede nesne yediğine bağlıdır.\n");
        }

        private void button3_Click(object sender, EventArgs e) //Skoları Görüntüle buttonu tanımlama
        {
            //Belirleten dosyayı aç
            System.Diagnostics.Process.Start(Path); 
        }

        private void button2_Click(object sender, EventArgs e) //Kişi Kaydet buttonu tanımlama
        {
            
            if (textBox1.Text == "") //textBox'ta hiçbir şey yazılmadığı durumda
            {
                MessageBox.Show("Önce Kiyinin Adını Yazınız!. Kişinin Adı Sadece Harflerden Oluşmalıdır..");
            }
            else 
            {
                if (HarflerdenOluşurMu()) //textBox'taki yazılar tümünü harfdir 
                {
                    MessageBox.Show("Kişi Kaydedildi.");
                    this.KeyPreview = true; //Klavye tuşları kullanmak için  
                }

                else //bir tanesi harf değil
                {
                    MessageBox.Show("Önce Kiyinin Adını Yazınız!. Kişinin Adı Sadece Harflerden Oluşmalıdır..");
                }
            }
        }

        //Adın sadece harflerden oluşup oluşmadığını kontrol etmek için bir metod
        private bool HarflerdenOluşurMu() 
        {
            bool sonuc = true;

            for(int i=0;i<textBox1.Text.Length;i++)
            {
                if (!char.IsLetter(textBox1.Text[i])) //en az birisi harf değilse false döndür
                {
                    sonuc = false;
                    return sonuc;
                }
            }

            return sonuc; //Tümünü harf
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            nesne.Visible = false; //Nesne görünmez şeklinde yap
            nesne.Size = new System.Drawing.Size(10, 10); //nesne boyutunu belire
            nesne.BackColor = Color.Red; // nesne rengini kırmızı olarak belirle 
            panel1.Controls.Add(nesne); //nesne panele ekle
        }

        private void NesneOlustur() //nesne yedildikten sonra tekrar oluştur
        {
            Random rndm = new Random(); //rastgele sayılar oluşturmak için
            x1 = rndm.Next(0, panel1.Width - 10); 
            y1 = rndm.Next(0, panel1.Height - 10);
            //Yılan ve nesne üst üste oturmak için (Çünkü yılan on on mektarında hareket eder)
            while (x1 % 10 != 0 || y1 % 10 != 0) 
            {
                //Bu iki sayı 10'a bölünebilir olmalı 
                if (x1 % 10 == 0 && y1 % 10 != 0)
                {
                    y1 = rndm.Next(0, panel1.Height);
                }
                else if (x1 % 10 != 0 && y1 % 10 == 0)
                {
                    x1 = rndm.Next(0, panel1.Width);
                }
                else if (x1 % 10 != 0 && y1 % 10 != 0)
                {
                    x1 = rndm.Next(0, panel1.Width - 10);
                    y1 = rndm.Next(0, panel1.Height - 10);
                }
                else
                    break;

            }
            nesne.Location = new System.Drawing.Point(x1, y1);//nesne panel içine rasgele olarak koy 
            nesne.Visible = true; //nesne görünür şeklinde yap
        }

        private void timer1_Tick_1(object sender, EventArgs e) //Oyun süresi için bir timer
        {
            puanSaniyesi++; //puan saniyeleri artır

            saniye++;
            //saniyeyi label'e yazdır
            if (saniye < 10)
            {
                label5.Text = ":0" + saniye.ToString(); 
            }
            else
                label5.Text = ":" + saniye.ToString();

            if (saniye == 59)            
            {
                //dakikalar artır ve label'e yazdır
                dakika++;
                if (dakika < 10)
                {
                    label6.Text = "0" + dakika.ToString();
                    saniye = 0;
                }
                else
                {
                    label6.Text = dakika.ToString();
                    saniye = 0;
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e) //yılan hareketi başlatmak için bir timer
        {
            yilan1.YilanHareketEttir(); //YiLan sınıfının bir metodu (yılan hareket ettirmek için)
            YilanCiz(yilan1); //Panel'de yılan çiz 
            NesneYedi(); //Yılan nesneye ulaştığında birkaç şey yapması için bir metod

            for (int i = 1; i < yilan1.YilanCismi.Count; ++i)
            {
                //yılan kuyruğuna çarptığında oyunu bitir 
                if (yilan1.YilanCismi[0].X == yilan1.YilanCismi[i].X
                    && yilan1.YilanCismi[0].Y == yilan1.YilanCismi[i].Y) 
                {
                    OyunuBitti();
                }
            }
            //yılan panel'e çarıptığında oyunu bitir
            if (yilan1.YilanCismi[0].X < 0 || yilan1.YilanCismi[0].Y < 0 
                || yilan1.YilanCismi[0].Y > (panel1.Height - 10) || 
                yilan1.YilanCismi[0].X > (panel1.Width - 10))
            {
                OyunuBitti();
            }          
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; //Klavyenin girişini kabul etmek için

            if ((e.KeyChar == (char)Keys.B || e.KeyChar == 'b') && (sayac1 == 0) && (sayac == 0)
                && (radioButton1.Checked || radioButton2.Checked))
            {
                label5.Text = ":00";
                label6.Text = "00";
                label4.Text = "0.0";
                OyunuBaslat(); //Oyun başlatmak için bir metod 
            }

            if (e.KeyChar == (char)Keys.D || e.KeyChar == 'd')
                OyunuDurdur(); //Oyun Durdurmak için bir metod             
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Yılan yönü ve giriş yönü arasında karşlaştırmak için Yilan sınıfından bir metodu kullan
            if (e.KeyCode == Keys.Up)
                yilan1.YonuAyarla(Yonlar.Up);
            else if (e.KeyCode == Keys.Down)
                yilan1.YonuAyarla(Yonlar.Down);
            else if (e.KeyCode == Keys.Right)
                yilan1.YonuAyarla(Yonlar.Right);
            else if (e.KeyCode == Keys.Left)
                yilan1.YonuAyarla(Yonlar.Left);
        }

        private void NesneYedi()
        {
            if (yilan1.YilanCismi[0].X == x1 && yilan1.YilanCismi[0].Y == y1) //Yılan nesneye ulaştığında
            {
                //puan artır ve nesneyi panelden sil
                puan += Puanlama();
                label4.Text = puan.ToString("0.0");
                panel1.Controls.Remove(nesne);

                var count = yilan1.YilanCismi.Count; //yılan uzunluğu
                //Yılan kuyruğuna 3 parça ekle (kuyruğunu uzatmak için)
                var son = yilan1.YilanCismi[count - 1];
                yilan1.YilanCismi.Add(new Nokta(son.X, son.Y));

                var son1 = yilan1.YilanCismi[count - 1];
                yilan1.YilanCismi.Add(new Nokta(son1.X, son1.Y));

                var son2 = yilan1.YilanCismi[count - 1];
                yilan1.YilanCismi.Add(new Nokta(son2.X, son2.Y));

                NesneOlustur(); //yeni nesne oluştur
                
                panel1.Controls.Add(nesne); //nesneyi panele ekle
            }
        }

        private void OyunuBaslat()
        {
            //Oyunu başlatmak için
            timer1.Enabled = true;
            timer2.Enabled = true;
            //Form içindeki (Buttonlar, radioButton ve değerlei) görünmez şeklinde yap
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            panel2.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            textBox1.Visible = false;
            sayac1++; //B tuşunu bir kere kullandı demektir
            yilan1 = new Yilan(xSet, ySet); //Yilan konumu belirle
            if (radioButton1.Checked) //Kolay seviye
            {
                timer2.Interval = 350; //Hareket hızı
                NesneOlustur(); //Yeni nesne olustur
            }
            else if (radioButton2.Checked) //Zor seviye
            {
                timer2.Interval = 200; //Hareket hızı (Kolay seviyden daha hızlı)
                NesneOlustur(); //Yeni nesne olustur
            }
        }

        private void OyunuDurdur()
        {
            if((sayac1 != 0) && (sayac % 2 == 0))
            {
                //Oyunu durdurmak için tüm sürelar durdur
                timer1.Enabled = false;
                timer2.Enabled = false;
                sayac++; //D tuşunu 1 kere kullandı demektir
            }
            else if((sayac1 != 0) && (sayac % 2 == 1))
            {
                //Oyunu durdurduktan sonra yeniden başlatmak için tüm süreler yeniden başlat
                timer1.Enabled = true;
                timer2.Enabled = true;
                sayac++; //D tuşunu 1 kere daha kullandı demektir
            }
        }

        private void OyunuBitti()
        {
            //Oyun bittiğinde tüm süreler durdur
            timer1.Enabled = false;
            timer2.Enabled = false;
            //Dosya oluştur ve içine skoru yazdır
            using (StreamWriter sw = new StreamWriter(Path, true)) 
            {
                sw.WriteLine(textBox1.Text + " " + label6.Text + label5.Text + " Skor: " + label4.Text);
            }
            //Ouynu yeniden onynamak isteyip istemediği kullancıya sor
            DialogResult dialogResult = MessageBox.Show("Yeni Oyun", "Kaybettiniz", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //Varsayılan değerler 
                xSet = 250;
                ySet = 150;
                sayac = 0;
                sayac1 = 0;
                puan = 0;
                puanSaniyesi = 0;
                saniye = 0;
                dakika = 0;
                label4.Text = "0.0";
                label5.Text = ":00";
                label6.Text = "00";
                panel1.Refresh(); //Eski yılan panelden silmek için
                OyunuBaslat(); //Oyunu Başlat metodu kullan
            }
            else if (dialogResult == DialogResult.No)
            {
               Application.Restart(); //Formu varsayılan hale getir
            }
        }

       private void YilanCiz(Yilan Sn)
        {
            Graphics g = panel1.CreateGraphics(); //Graphics sınıfından bir nesne tanımla
            var count = Sn.YilanCismi.Count; //Yılanın uzunluğu
            SolidBrush yilanrengi = new SolidBrush(Color.Green); //yılan rengi yeşil olarak belirle
            panel1.Refresh(); //yılan hareket ettiği zaman kalan parçaları panelden sil   
            for (int i = 0; i < count; ++i)
            {                
                g.FillRectangle(yilanrengi, Sn.YilanCismi[i].X, Sn.YilanCismi[i].Y, 10, 10); 
                //yılanın her parçasının boyutu 10 olarak çiz                
            }
        }

        private double Puanlama() 
        {
            double puan1 = 0; //hesaplanacak puan değeri atamak için bir değişken
            if (puanSaniyesi <= 100) //Saniye sayısı 100 den küçük ya da 100 e eşit olması durumunda
            {
                if ((x1 == 0 && y1 == 0) || (x1 == 0 && y1 == (panel1.Height - 10)) ||
                    (x1 == (panel1.Width - 10) && y1 == 0) ||
                    (x1 == (panel1.Width - 10) && y1 == (panel1.Height - 10))) //köşe noktalar için sonuca 10 puan ekle
                    puan1 = 100.0 / puanSaniyesi + 10;

                else if (puanSaniyesi == 0) //Eğer nesneyi hemen yediyse (1 saniye geçmeden nesneyi buldu ise) 
                    //puanSaniyesi 0 olacak (0'a bölünme işlemi sonsuz verecek), bu durumda 1 olarak kabul edecek
                    puan1 = 100.0; 

                else //Köşede olmayan nesneler
                    puan1 = 100.0 / puanSaniyesi;
            }

                puanSaniyesi = 0; //Puan saniyeleri sıfırla

                return puan1; //Puan değeri döndür
        }
    }
}