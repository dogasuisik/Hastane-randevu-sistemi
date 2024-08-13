using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_Proje
{
    public partial class FrmGirisler : Form
    {
        public FrmGirisler()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //butona basınca hastagiriş sayfası açılıyor
            FrmHastaGiris fr=new FrmHastaGiris();
            fr.Show();
            this.Hide();//butona basınca açık olan formu kapatıp yeni formu açar
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            //doktor butonuna basınca doktorgiriş sayfası açılıyor
            FrmDoktorGiris fr=new FrmDoktorGiris(); 
            fr.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmSekreterGiris fr = new FrmSekreterGiris();
            fr.Show();
            this.Hide();
        }

        
    }
}
