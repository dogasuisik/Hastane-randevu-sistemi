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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

        sqlBaglantisi bgl=new sqlBaglantisi();

        public string Tc;
        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            labelTC.Text = Tc;

            //ad-soyadı doktor bilgi kısmına çekme
            SqlCommand komut = new SqlCommand("select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorTc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Tc);
            SqlDataReader dr= komut.ExecuteReader();
            while (dr.Read())//if sadece girişlerde yani parametrelerin eşit olup olmadığı kontrolünde kullanılır
            {
                labelAdSoyad.Text = dr[0]+" "+dr[1];

            }
            bgl.baglanti().Close();

            //randevulistesi
            DataTable dt = new DataTable();
            SqlDataAdapter da=new SqlDataAdapter("select * from Tbl_Randevular where RandevuDoktor=@p1  ",bgl.baglanti());
            da.SelectCommand.Parameters.AddWithValue("@p1", labelAdSoyad.Text);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void buttonBilgiDuzenle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle fr = new FrmDoktorBilgiDuzenle();
            fr.TcNo = labelTC.Text;
            fr.Show();
        }

        private void buttonDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr= new FrmDuyurular();
            fr.Show();
        }

        private void buttonCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();//programı kapatır
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //datagridde bir hücreye basınca şikayet randevu detay kısmına gelir
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            richTextBoxSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }
    }
}
