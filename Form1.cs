using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Diagnostics;
namespace _20Ekim
{
    public partial class Form1 : Form
    {

        List<string> kisiler = new List<string>(); // Oyunu oynayan kisileri listede tuttum yeni oyun basladıgı zaman 
        Form2 oyun;                                 // son kişi kaydına erişmek için       
      
       
        public Form1()

        {
            InitializeComponent();
            
        }

        public static string gonderilecekveri;              // form2 ye gönderilecek verileri burda static olarak tanımladım
        public static List<string> gonderilecekListe;       // oyun bitince yeeni oyun oynamayı tercih edenler için  zaman kullanmak için 
        public static bool gonderilecekHiz;                

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            
            if (e.KeyCode.ToString() == "B")  //oyuna baslamayı B tuşu ile aktif ettim
            {
                int a;
                string b = (String)textBox1.Text.ToString();
              
                a = b.Length;

                if (a == 0) //burada isim girişi boş bırakıldığı zaman oyunun baslamaması için
                {
                    MessageBox.Show("Oyuna başlamak için isim girişi yapınız");
                }
              
                else
                {
                  //  kisiler.Add(textBox1.Text); // kişi listesine 
                    
                    if (oyun == null || oyun.IsDisposed)        //burada formun iki kere açılmasını engellemek için 
                    {                                           //disposed sınıfını kullanım
                                                                //direk Form2=new şeklinde çağırdıgım zaman form2 kere açılıyordu 
                        oyun = new Form2();                     // çözüm olarak böyle denedim

                        if (radioButton1.Checked)           //radio button kontrolüne göre yılanın hareket hızını ayarladım
                        {
                            oyun.tmrHareket.Interval = 1000;
                            gonderilecekHiz = true;         //oyun yenidne basladıgı zaman son seçilen oyun hızını göndermek için


                        }
                        if (radioButton2.Checked)


                        {
                            oyun.tmrHareket.Interval = 100;
                            gonderilecekHiz =false;

                        }
                        oyun.Show();
                    }
                    

                   
                 
                    this.Visible = false;
                    
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Rüveyda Havva Sevinç tarafından geliştirildi.Oyunu yön tuşları ile oynayın.Oyunun amacı rastgele yerleştirilen yemi yemektir.iyi oyunlar");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            
           
            int c;          
            string d = (String)textBox1.Text.ToString();  //textbox a kişiyi ekledim
            gonderilecekveri = textBox1.Text;  // son kisiyi aktarmak için 

            c = d.Length;
            if (c==0)
            {
                MessageBox.Show("Bos giriş denediniz adınızı giriniz"); //boş isim girişi için 
            }
           

            else {
                kisiler.Add(d); //kisiler listesine yazılan ismi ekledim 
                gonderilecekListe = kisiler; //listeyi farklı bir listeye kopyaladım
                MessageBox.Show(textBox1.Text + " "+"kaydedildi");
            }
            
          //  textBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            string file = @"C:\\SkorDosya\\skorgoruntulemek.txt"; //işlem sınıfını using ile ekleyip dosyayı görüntülemeyi sağladım
            Process.Start(file);
        }


  



    }
}