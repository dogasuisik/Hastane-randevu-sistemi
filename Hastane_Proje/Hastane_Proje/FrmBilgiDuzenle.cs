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
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }

        public string TCno;

        sqlBaglantisi bgl=new sqlBaglantisi();
        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            //
            maskedTextBoxTc.Text = TCno;

            SqlCommand komut = new SqlCommand("select * from Tbl_Hastalar where HastaTc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TCno);
            SqlDataReader dr= komut.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                maskedTextBoxTel.Text = dr[4].ToString();
                TxtSifre.Text = dr[5].ToString();
                comboBoxSifre.Text = dr[6].ToString();
            }
            bgl.baglanti().Close();
        }

        private void buttonBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update Tbl_Hastalar set HastaAd=@p1,HastaSoyad=@p2,HastaTelefon=@p3,HastaSifre=@p4,HastaCinsiyet=@p5 where HastaTc=@p6 ", bgl.baglanti());//set:güncellenmesi gereken kısımları belirtir.update sorgusuna where yazmayı unutma
            komut2.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut2.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3", maskedTextBoxTel.Text);
            komut2.Parameters.AddWithValue("@p4", TxtSifre.Text);
            komut2.Parameters.AddWithValue("@p5", comboBoxSifre.Text);
            komut2.Parameters.AddWithValue("@p6", maskedTextBoxTc.Text);
            komut2.ExecuteNonQuery();//işlemleri kaydeder
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi", "Bilgi: ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
