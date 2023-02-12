using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _20Ekim
{
    class yemOlustur
    {
        yilanOyunu yilanoyun = new yilanOyunu();

        public Rectangle yem;
        private SolidBrush fircaYem;
        

        public int xYem;
        public int yYem;

        private int genislik;
        private int yukseklik;

        public yemOlustur(Random rastgele1 )
        {
            
            fircaYem = new SolidBrush(Color.Red);

            xYem = rastgele1.Next(5, 50) * 10;
            yYem = rastgele1.Next(5, 30) * 10;

            genislik = 10;
            yukseklik = 10;

            yem = new Rectangle(xYem, yYem, genislik, yukseklik);
                          
        }
        public void yemiKonumlandır(Random rastgele1)
        {
            xYem = rastgele1.Next(5, 50) * 10;
            yYem = rastgele1.Next(5, 30) * 10;
        }

        public void YemiCiz(Graphics yilanGrafik) //yemi grafikle çizdirme
        {
            yem.X = xYem;
            yem.Y = yYem;

            yilanGrafik.FillRectangle(fircaYem, yem);

        }



    }

}
