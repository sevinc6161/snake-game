using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace _20Ekim
{
    public partial class Form2 : Form
    {

        Graphics yilanGrafik; //yılanın her birimini grafik işlemi ile çizdirdim
        yilanOyunu yilanoyun = new yilanOyunu();  //class ı nesnesini oluşturdum sınıflara erişim için     
        yemOlustur yemolustur;
        Random rastgele1 = new Random();

        Form2 yeniOyun;         //form2 yi tekrar baslatmak için 
        Form1 frm1 = new Form1(); //form1 e erişim için 

        string sonKisi;


        bool asagi = false;         //oyunu yönelendirmek için yaptım
        bool yukari = false;        // bunu enum sabiti oluşturarak okunmasını kolaylaştırıp
        bool sol = false;           // ve sonrasında bir nesne oluşturarak yapabilirdim
        bool sag = false;           
        bool durdur = false;

        bool oyunBittiMi = false;

        float puan = 0;
        float sayac=0 ;
        float toplamPuan = 0;
        int saniye = 0;
        int salise = 0;
        int dakika = 0;
        string puaniLabeleYaz;






        public Form2()
        {
            InitializeComponent();

            yemolustur = new yemOlustur(rastgele1); //yem class ını kullanırken bu formdan random gönderdiğim için parametre aldığı için burada tanımladım


        }




        private void tmrHareket_Tick(object sender, EventArgs e) //TİMER İLE HAREKET ETTİRME 
        {
            timer1.Start(); //oyun sayacı ile hareket ettirmek için iki farklı timer kullandım
                            // aslında tek timer ile de yapılabilirdi
                               // (buraya sayaç arttırma ekleyip )

            if (sag)        // yukarıda tanımladıgım bool değişkenleri doğru oldugu zaman çalışacak ve yönlendirmeyi yapacak
            {
                yilanoyun.SagaKaydir(); // burada yılanOyunu sınıfında kullanıdıgım fonksiyonu çağırdım
                
            }

            if (sol)
            {
                yilanoyun.SolaKaydir();
            }

            if (yukari)
            {
                yilanoyun.YukariKaydir();
            }

            if (asagi)
            {
                yilanoyun.AsagiKaydir();
            }
            if (durdur)
            {
                timer1.Stop(); //burada oyun hareketi durdurulurken sayacın da artmaması için timer ı durdurdum
            }

            DuvaraCarptiMi();
            KuyrugaCarptiMi();
           // labelZaman.Text = toplamSayac.ToString(); //oyunda toplam geçen süreyi yazdırma sürekli değişiyor 
           
           
            for (int i = 0; i < yilanoyun.yilanGovde.Length; i++) //burada yılan gövdesi boyunca eklendiği kader oyunu döndürerek
            {              //süreklilik sağladım eğer aşağıda belirttiğim DuvaraCarptımı fonksiyonu
                  //oyunu bitirirse döngü bitiyor
                    //bu şekilde kullanma amacım oyunun devamlılığı, yemin dağıtımının devamlılığı
                    //burada bu döngüyü oluşturmak yerine bir bool değişkeni kullanarak bir döngü de oluşturabilirdim
                    // while(bool) if(bool) şeklinde 
          

                if (yilanoyun.yilanGovde[i].IntersectsWith(yemolustur.yem)) //IntersectWith ile yılan ve yemin
                {                   // üst üste gelip gelmediğini kontrol ettim bunu yilanGovde[i].X== yemolustur.yem.Location.X 
                    if (sayac > 6000)    // ve aynı şekilde Y konumlarını if ile kontrol edipte yapılabilirdi
                    {           //sayac 100 den fazla olursa puan eklememesi için şart koydum
                        puan = 0;
                    }
                     if(sayac<=6000)
                    {
                        puan = 6000 / sayac;     //puan ekleme
                        if((yemolustur.yem.Location.X==50 && yemolustur.yem.Location.Y == 50)|| (yemolustur.yem.Location.X == 490 && yemolustur.yem.Location.Y == 50)|| (yemolustur.yem.Location.X == 50 && yemolustur.yem.Location.Y == 290)|| (yemolustur.yem.Location.X == 490 && yemolustur.yem.Location.Y == 290))
                        {
                            puan += 10;     //burada oluşturdugum şart yenilen yem eğer köşelerde mi kontrolu yapıyor
                        }   
                    }
                    toplamPuan += puan;
              
                   // labelPuan.Text = toplamPuan.ToString();
                    puaniLabeleYaz = toplamPuan.ToString();
                    labelPuan.Text = puaniLabeleYaz;
                    sayac = 0; //sayacı sıfırlama sebebim bu sayac ile bir tane yemin yenilme süresini tutuyorum toplam süreyi farklı bir int te tuttum
                    yilanoyun.kuyrukEkleme(); // sınıftan kuyruk ekleme metodunu çağırdım
                    yemolustur.yemiKonumlandır(rastgele1); //yem yerleştirdim 
                   
                }
            }

         
            this.Invalidate();
        }



        private void Form2_KeyDown(object sender, KeyEventArgs e) //HAREKET YÖNLERİ
        {
            if (e.KeyData == Keys.Down && yukari == false) //burada aşağı tuşuna basılma şartını kontrol ettim
            {                       //şart olarak yukari bool değişkenimi kontrol etme sebebim yukari eğer true olursa 
                yukari = false;         //yilanın yönünün değişeceğini başlarken çizilen ana yilan bas karesinin değişeceğini düşündüm
                asagi = true;           //kontrol cok gerekli miydi bilmiyorum 
                sol = false;
                sag = false;
            }
            else if (e.KeyData == Keys.Up && asagi == false)
            {
                asagi = false;
                yukari = true;
                sol = false;
                sag = false;
                durdur = false;
            }
            else if (e.KeyData == Keys.Right && sol == false)
            {
                sag = true;
                sol = false;
                yukari = false;       
                asagi = false;
                durdur = false;

            }
            else if (e.KeyData == Keys.Left && sag == false)
            {
                sol = true;
                sag = false;
                yukari = false;
                asagi = false;
                durdur = false;
               
            }
            else if (e.KeyCode.ToString() == "D")
            {
                sol = false;
                sag = false;
                yukari = false;
                asagi = false;
                durdur = true;
            }
           
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            yilanGrafik = e.Graphics; //paint içinde çizdirme işlemlerini yapmak için gerkli metodları ve sınıfları çağırdım
            yemolustur.YemiCiz(yilanGrafik);
            yilanoyun.YilaniCiz(yilanGrafik);
            Pen panelSekli = new Pen(Color.Black); //panelin sınırlarının görünmesi için 
            e.Graphics.DrawRectangle(panelSekli, 50, 50, 450, 250); //paneli çizdirdim
           


            
            for (int i = 0; i < yilanoyun.yilanBaslangicKaresi.Length; i++) //burada ilk yemin yenme şartını 
            {
               
                if (yilanoyun.yilanBaslangicKaresi[i].IntersectsWith(yemolustur.yem))
                {
                    
                    yemolustur.yemiKonumlandır(rastgele1);
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            sayac++; //yem yeme süresi ve oyun süresi için kullanıdıgım timer
            salise++;
            labelZaman.Text = dakika.ToString() + ":" + saniye.ToString() + ":" + salise.ToString();
            if (salise == 60)
            {
                saniye++;
                salise = 0;
                labelZaman.Text= dakika.ToString() + ":" + saniye.ToString() + ":" + salise.ToString();
                if (saniye == 60)
                {
                    dakika++;
                    saniye = 0;
                   labelZaman.Text=dakika.ToString() + ":" + saniye.ToString() + ":" + salise.ToString();
                }
            }



     
        }

        public void DuvaraCarptiMi()
        {

            if (yilanoyun.yilanBaslangicKaresi[0].X < 50 || yilanoyun.yilanBaslangicKaresi[0].X > 490 || yilanoyun.yilanBaslangicKaresi[0].Y < 50 || yilanoyun.yilanBaslangicKaresi[0].Y > 290 )
            {
                oyunBittiMi = true; //eğer yılan panel sınırlarına değerse şartını yazdım 
                if (oyunBittiMi) 
                {
                    timer1.Enabled = false; //zamanı durdurdum 
                    durdur = true; //durdur değişkenini aktif yapıp yukarıda keydown ve tick ile belirttiğim şekilde hareketi durdurdum
                   
                    this.Close(); //formu kapatmak için 
                
                    DialogResult secim = MessageBox.Show("Yeni oyun", "Kaybettin", MessageBoxButtons.YesNo,MessageBoxIcon.Asterisk);
                    if (secim == DialogResult.Yes) //burada messagebox button ile seçenekli bir mesaj kutusu oluşturdum. ve seçime göre işlemler
                    {

                        //   Form1 frm1 = (Form1)Application.OpenForms["form1"];
                        //  frm1.yeniOyunBaslat();
                        
                        List<string> gonderilenList = Form1.gonderilecekListe; //eğer yeni oyun baslangıcı olursa form1 de girilen son ismi 
                        bool yeniOyunHizi= Form1.gonderilecekHiz; //son hızı buraya çektim 
                        for (int i = 0; i < gonderilenList.Count; i++) //burada listeyi döndürüp listede kayıtlı son ismi aldım
                        {
                            if (i == gonderilenList.Count - 1)
                            {
                                sonKisi = gonderilenList[i];
                            }
                        }
                        
                        frm1.textBox1.Text = sonKisi; //son kişiyi form1 in textbox ına gönderdim 
                        if (yeniOyun == null || yeniOyun.IsDisposed) //ve formu tekrardan çağırdım 
                        {
                            if(yeniOyunHizi) //bu ise son hızı kontrol etmesi için koydugum şart
                            {
                                yeniOyun = new Form2();
                                yeniOyun.tmrHareket.Interval = 1000;
                                yeniOyun.Show();


                            }
                            else
                            {
                                yeniOyun = new Form2();
                               yeniOyun.tmrHareket.Interval = 100;       
                                yeniOyun.Show();

                            }
                            SkorKaydetmek(); 
                      
                          

                         
                        }

                    }
                    else
                    {
                        Application.Restart(); //oyuna devam eilmek istemezse uygulamayı en baştan başlatmak 
                       SkorKaydetmek(); //ödevde oyun baslamadıgı zaman oyunun baslangıca dönmesini söylediğiniz için 
                    }  //bunu form2 yi kapat ve form1 i tekrar aç olarak yapabilirdim ama emin olmadığım için denemedim
                    
                    return;
                }
            }
          
        }

        public void SkorKaydetmek()
        {

            string kisi = Form1.gonderilecekveri; //form1 ken kişiyi çekip dosyaya yazdırmak için 
            string puanyaz = toplamPuan.ToString(); //bütün hepsini string olarak tuttum
             string sayacyaz = labelZaman.Text.ToString();

            string fileName = @"C:\\SkorDosya\\skorgoruntulemek.txt";

            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write); //dosya varsa açan yoksa oluşturup yazma işlemi
            fs.Close();
            File.AppendAllText(fileName, Environment.NewLine + kisi + " " + "Gecen Süre: " + dakika.ToString() + ":" + saniye.ToString() + ":" + salise.ToString() + " Skor:" + puanyaz); //son satıra eklemeye devam etmesi için bunu kullandım
        }


        public void KuyrugaCarptiMi()
        {
        for( int x =3; x<yilanoyun.yilanBaslangicKaresi.Length; x++) { 
            if (yilanoyun.yilanBaslangicKaresi[0].IntersectsWith(yilanoyun.yilanBaslangicKaresi[x]))
            {
                oyunBittiMi = true; //eğer yılan panel sınırlarına değerse şartını yazdım 
                if (oyunBittiMi)
                {
                    timer1.Enabled = false; //zamanı durdurdum 
                    durdur = true; //durdur değişkenini aktif yapıp yukarıda keydown ve tick ile belirttiğim şekilde hareketi durdurdum

                    this.Close(); //formu kapatmak için 

                    DialogResult secim = MessageBox.Show("Yeni oyun", "Kaybettin", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                    if (secim == DialogResult.Yes) //burada messagebox button ile seçenekli bir mesaj kutusu oluşturdum. ve seçime göre işlemler
                    {

                        //   Form1 frm1 = (Form1)Application.OpenForms["form1"];
                        //  frm1.yeniOyunBaslat();

                        List<string> gonderilenList = Form1.gonderilecekListe; //eğer yeni oyun baslangıcı olursa form1 de girilen son ismi 
                        bool yeniOyunHizi = Form1.gonderilecekHiz; //son hızı buraya çektim 
                        for (int i = 0; i < gonderilenList.Count; i++) //burada listeyi döndürüp listede kayıtlı son ismi aldım
                        {
                            if (i == gonderilenList.Count - 1)
                            {
                                sonKisi = gonderilenList[i];
                            }
                        }

                        frm1.textBox1.Text = sonKisi; //son kişiyi form1 in textbox ına gönderdim 
                        if (yeniOyun == null || yeniOyun.IsDisposed) //ve formu tekrardan çağırdım 
                        {
                            if (yeniOyunHizi) //bu ise son hızı kontrol etmesi için koydugum şart
                            {
                                yeniOyun = new Form2();
                                yeniOyun.tmrHareket.Interval = 1000;
                                yeniOyun.Show();


                            }
                            else
                            {
                                yeniOyun = new Form2();
                                yeniOyun.tmrHareket.Interval = 100;
                                yeniOyun.Show();

                            }
                            SkorKaydetmek();




                        }

                    }
                    else
                    {
                        Application.Restart(); //oyuna devam eilmek istemezse uygulamayı en baştan başlatmak 
                        SkorKaydetmek(); //ödevde oyun baslamadıgı zaman oyunun baslangıca dönmesini söylediğiniz için 
                    }  //bunu form2 yi kapat ve form1 i tekrar aç olarak yapabilirdim ama emin olmadığım için denemedim

                    return;
                }
            }
            }
        }






















    }

       
}
