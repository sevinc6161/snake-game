using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace _20Ekim
{
    class yilanOyunu
    {
        public Rectangle [] yilanBaslangicKaresi;
        private SolidBrush firca;
        Random rastgele = new Random();

        public int xKonum;
        public int yKonum;

        private int genislik;
        private int yukseklik;

        int[,] Tsekli = {
                  { 1, 1, 1, 0} ,
                  { 0, 1, 0, 0} ,
                  { 0, 1, 0, 0},
                  { 0, 0, 0, 0},
                };



        public Rectangle[] yilanGovde
        {
            get { return yilanBaslangicKaresi;  }
        }

       

        public yilanOyunu()
        {
            yilanBaslangicKaresi = new Rectangle[2];
            firca = new SolidBrush(Color.Green);

            xKonum = rastgele.Next(5,50)*10;
            yKonum = rastgele.Next(5,30)*10;
            genislik = 10;
            yukseklik = 10;

            for (int i = 0; i < yilanBaslangicKaresi.Length; i++)
            {
                yilanBaslangicKaresi[i] = new Rectangle(xKonum, yKonum, genislik, yukseklik);
                xKonum -= 10;
            }
        }

        public void YilaniCiz(Graphics yilanGrafik) //kareleri grafik ile çizdirme
        {
            foreach (Rectangle rec in yilanBaslangicKaresi)
            {
                yilanGrafik.FillRectangle(firca, rec);
            }

        }


        public void YilaniCiz() //hareketten sonra yılanı çizmek için 
        {
            for (int i = yilanBaslangicKaresi.Length - 1; i > 0; i--)
            {
                yilanBaslangicKaresi[i] = yilanBaslangicKaresi[i - 1];
            }
        }


        public void SolaKaydir() //yönlere göre hareketin nasıl olacağı
        {
            YilaniCiz();
            yilanBaslangicKaresi[0].X -= 10;
          


        }
        public void SagaKaydir()
        {
            YilaniCiz();
            yilanBaslangicKaresi[0].X += 10;
            

        }
        public void YukariKaydir()
        {
            YilaniCiz();
            yilanBaslangicKaresi[0].Y -= 10;

        }

        public void AsagiKaydir()
        {
            YilaniCiz();
            yilanBaslangicKaresi[0].Y += 10;

        }
      

    
     

        public void kuyrukEkleme()
        {
            List<Rectangle> kare = yilanBaslangicKaresi.ToList(); //eklenen yeni yılanları bir nesne listesinde tuttum
            kare.Add(new Rectangle(yilanBaslangicKaresi[yilanBaslangicKaresi.Length - 1].X, yilanBaslangicKaresi[yilanBaslangicKaresi.Length - 1].Y, genislik, yukseklik));
            yilanBaslangicKaresi = kare.ToArray();
        }

       
        

        
    }
}

