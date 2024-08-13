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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;//formlar arası geçiş

        sqlBaglantisi bgl=new sqlBaglantisi();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {

            //formdan ad soyad tc çekme
            labelTc.Text=tc;

            //hastatc sine göre veri tabanından hastalar tablosundan hastanmın ad-soyadını çeker
            SqlCommand komut = new SqlCommand("Select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", tc);
            SqlDataReader dr= komut.ExecuteReader();//SqlDataReader, verileri satır satır okur.komut.ExecuteReader,sorguyu başlatır
            while (dr.Read())
            {
                //labeladsoyad etiketine ad-soyadı yazar 
                labelAdSoyad.Text = dr[0] + " " + dr[1];
                
            }
            bgl.baglanti().Close();


            //formdan randevu geçmişi çekme
            DataTable dt = new DataTable();//datatable verileri tablo şeklinde tutar
            //SqlDataAdapter,veritabanı ile DataTable arasında veri alışverişini sağlar.ve tabloyu doldurur
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular where HastaTc=" + tc, bgl.baglanti());//veritabanından veri almak için kullanılır.dataadapter
            da.Fill(dt);//veritabanından alınan tüm randevu kayıtları dt içinde saklanır.
            dataGridView1.DataSource = dt;


            //branş çekme
            SqlCommand komut2=new SqlCommand("select BransAd from Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr2= komut2.ExecuteReader();
            while (dr2.Read())
            {
                comboBoxBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void comboBoxBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1",bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", comboBoxBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())

            {
                comboBoxDoktor.Items.Add( dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();

        }

        private void comboBoxDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            //RandevuDurum=0-->müsait randevular
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Tbl_Randevular where RandevuBrans=@p1 and RandevuDoktor=@p2 and RandevuDurum=0", bgl.baglanti()); //tırnak ' tırnak içine yazılır.sadece seçtiğimiz doktorun ve(and) branşın randevuları görüntülenir.Syrıca randevudurumu 0 yani boş olan randevuları görüntülesin
            da1.SelectCommand.Parameters.AddWithValue("@p1", comboBoxBrans.Text);
            da1.SelectCommand.Parameters.AddWithValue("@p2", comboBoxDoktor.Text);
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;
        }


        private void linkLabelBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fr=new FrmBilgiDuzenle();
            fr.TCno=labelTc.Text; 
            fr.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //datagridden bir hücreye tıklayınca bilgileri sol tarafta gözükür
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            textBoxID.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
            
        }

        private void buttonRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut=new SqlCommand("update Tbl_Randevular set RandevuDurum=1,HastaTC=@p1,HastaSikayet=@p2 where RandevuId=@p3",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",labelTc.Text);
            komut.Parameters.AddWithValue("@p2", richTextBoxSikayet.Text);
            komut.Parameters.AddWithValue("@p3",textBoxID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
