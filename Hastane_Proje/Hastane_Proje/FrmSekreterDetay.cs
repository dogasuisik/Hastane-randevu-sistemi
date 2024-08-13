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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        public string Tcno;//sekreter giriş formundan  veri almak için
     
        sqlBaglantisi bgl=new sqlBaglantisi();
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            labelTC.Text = Tcno;

            //ad-soyad
            SqlCommand komut=new SqlCommand("select SekreterAdSoyad from Tbl_Sekreterler where SekreterTc=@p1",bgl.baglanti());//where yani tcye göre ad-soyad alıcak
            komut.Parameters.AddWithValue("@p1", Tcno);
            SqlDataReader dr1 = komut.ExecuteReader();
            while (dr1.Read())
            {
                labelAdSoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();

            //branşları datagrideviewe aktarma
            DataTable dt1=new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select BransAd from Tbl_Branslar ", bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //doktorları datagrideviewe aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select DoktorAd,DoktorSoyad,DoktorBrans from Tbl_Doktorlar ", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;//veri tabanından gelen değerler dt2 tablosunu oluşturur.dt2 de datagridviewe aktarılır

            //branşları comboboxa aktarma
            SqlCommand komut2 = new SqlCommand("select BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                comboBoxBrans.Items.Add(dr2[0].ToString());
            }
            bgl.baglanti().Close();


            
           

        }

        private void buttonKaydet_Click(object sender, EventArgs e)
        {
            //randevu ekleme
            SqlCommand komutKaydet = new SqlCommand("Insert into Tbl_Randevular(RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4)", bgl.baglanti());
            komutKaydet.Parameters.AddWithValue("@r1", maskedTextBoxTarih.Text);
            komutKaydet.Parameters.AddWithValue("@r2",maskedTextBoxSaat.Text);
            komutKaydet.Parameters.AddWithValue("@r3", comboBoxBrans.Text);
            komutKaydet.Parameters.AddWithValue("@r4", comboBoxDoktor.Text);
            komutKaydet.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Randevunuz Oluşturuldu");
        }

        private void comboBoxBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //branşa tıklayınca doktorların comboboxa gelmesi

            //yeni bir branş seçtiğinde, önceki branşa ait doktorların listesinin temizlenmesini ve sadece yeni seçilen branşa ait doktorların listelenmesini sağlar.
            comboBoxDoktor.Items.Clear();

            SqlCommand komut = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", comboBoxBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBoxDoktor.Items.Add(dr[0] + " " + dr[1]);
            }
            bgl.baglanti().Close();
        }

        private void buttonDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (duyuru) values (@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", richTextBoxDuyuruOlustur.Text);
            komut.ExecuteNonQuery() ;
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu");
        }

        private void buttonDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli fr= new FrmDoktorPaneli();
            fr.Show();
            
        }

        private void buttonBransPanel_Click(object sender, EventArgs e)
        {
            FrmBransPaneli fr= new FrmBransPaneli();
            fr.Show();
        }

        private void buttonRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi fr=new FrmRandevuListesi();
            fr.Show();

        }

        private void buttonGuncelle_Click(object sender, EventArgs e)
        {

        }

        private void buttonDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr=new FrmDuyurular();
            fr.Show();
        }
    }
}
