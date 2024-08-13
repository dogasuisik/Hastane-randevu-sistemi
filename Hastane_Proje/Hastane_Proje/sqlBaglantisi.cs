using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Hastane_Proje
{
    internal class sqlBaglantisi
    {
        public SqlConnection baglanti()//method
        {
            //SqlConnection nesnesi oluşturur ve bu nesneyi baglan adında bir değişkene atar.
            //baglan nesnesi adresi tutar
            //veri tabanına bağlanmayı sağlar
            //()içinde adres yazar
            SqlConnection baglan =new SqlConnection("Data Source=DESKTOP-AT015RL\\SQLEXPRESS;Initial Catalog=HastaneProje;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
