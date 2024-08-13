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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl=new sqlBaglantisi();
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //üye ola tıklayınca hasta kayıt formuna gider
            FrmHastaKayit fr=new FrmHastaKayit();
            fr.Show();
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            //sqlcommand sql sorgusunu veri tabanını göndermek için kullanılır
            //giriş yap butonuna basınca select ile hastalar tablosundan değerler çekilecek(hastatc ve hastaadsoyad).Bunlar hastadetay formunda kişi bilgi kısmına gelecek
            //bu değerleri @pler ile atayacak
            SqlCommand cmd = new SqlCommand("select * from Tbl_Hastalar where HastaTc=@p1 and HastaSifre=@p2", bgl.baglanti());
            cmd.Parameters.AddWithValue("p1", maskedTextBoxTc.Text);
            cmd.Parameters.AddWithValue("p2", TxtSifre.Text);

            //cmd.ExecuteReader() metodu, SQL sorgusunu çalıştırır ve sorgu sonucunda dönen verileri okumak için bir SqlDataReader nesnesi olan dr oluşturur.
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read()) //if(dr.Read()) ifadesi, eğer SqlDataReader ile bir satır okunabilirse (yani, girilen TC ve şifreye sahip bir kayıt varsa) yani kayıt varsa hastadetay sayfasını açar
            {
                FrmHastaDetay fr=new FrmHastaDetay();
                fr.tc=maskedTextBoxTc.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC veya Şifre");
            }
            bgl.baglanti().Close();
        }

        
    }
}
