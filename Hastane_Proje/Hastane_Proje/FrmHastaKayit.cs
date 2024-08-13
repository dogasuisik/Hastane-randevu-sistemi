using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Hastane_Proje
{
    public partial class FrmHastaKayit : Form
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
        }

        //sınıfı çağırıyoruz
        sqlBaglantisi bgl=new sqlBaglantisi();
        private void buttonKayıtYap_Click(object sender, EventArgs e)
        {
            //kayıtyap butonuna tıklayınca yeni üye oluşturur
            //SqlCommand sınıfından cmd nesnesi oluşturur.
            //insert into,Tbl_Hastalar tablosuna yeni veriler ekler.()içinde hangi sutünlara veri eklemek istediğimizi belirtiriz.values ile sutünlara eklenecek değerler belirlenir ve @pler değerleri tutan parametreler
            SqlCommand cmd = new SqlCommand("insert into Tbl_Hastalar (HastaAd,HastaSoyad,HastaTc,HastaTelefon,HastaSifre,HastaCinsiyet) values(@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
            //cmd.Parameters.AddWithValue() ile değerleri nerden alacağımızı belirler
            cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
            cmd.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            cmd.Parameters.AddWithValue("@p3", maskedTextBoxTc.Text);
            cmd.Parameters.AddWithValue("@p4", maskedTextBoxTel.Text);
            cmd.Parameters.AddWithValue("@p5", TxtSifre.Text);
            cmd.Parameters.AddWithValue("@p6", comboBoxSifre.Text);
            cmd.ExecuteNonQuery();//sql sorgusunu çalıştırır-veri tabanına ekleme yapar
            bgl.baglanti().Close();//ekleme yapıldıktan sonra veri tabanı kapatılır
            MessageBox.Show("Kaydınız Gerçekleşmiştir Şifreniz :" + TxtSifre.Text, "Bilgi:", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
