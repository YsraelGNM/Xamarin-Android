using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Data.SqlClient;
using System.Data;

namespace Phoneword
{
    [Activity(Label = "Aprobar Precio: Ingresa Descuento y Aprueba")]
    public class ListClient : Activity
    {
        List<clientListaLadrillo> items;
        string Codigo;
        string USU_ID;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ListClient);
            // Create your application here
            ListView Lista = FindViewById<ListView>(Resource.Id.Lista);
            Button btnAprobar = FindViewById<Button>(Resource.Id.Aprobar);

            items = new List<clientListaLadrillo>();

            Codigo = Intent.GetStringExtra("Codigo") ?? "Data not available";
            USU_ID = Intent.GetStringExtra("USU_ID") ?? "Data not available";

            SqlConnection sqlconn;
            string connsqlstring = string.Format("Server=190.116.179.42,1433;Database=SpringLED;User Id=sa;Password=Serverdbl2013;Integrated Security=False");
            sqlconn = new SqlConnection(connsqlstring);
            sqlconn.Open();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("select Articulo, APD_PRECIO_NORMAL, APD_PRECIO_APROBADO, APD_ID from dbo.vwSolicitudAjustePrecio WHERE SAP_ID = " + Codigo, sqlconn);
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            foreach (DataRow mFila in ds.Tables[0].Rows)
            {
                clientListaLadrillo clLadrillo = new clientListaLadrillo();
                clLadrillo.Ladrillo = mFila["Articulo"].ToString();
                clLadrillo.PrecioN = mFila["APD_PRECIO_NORMAL"].ToString();
                clLadrillo.PrecioA = mFila["APD_PRECIO_APROBADO"].ToString();
                clLadrillo.APD_ID = int.Parse(mFila["APD_ID"].ToString());
                items.Add(clLadrillo);
            }

            clientCustomListAdapter ListAdapter = new clientCustomListAdapter(this, items);
            Lista.Adapter = ListAdapter;

            btnAprobar.Click += BtnAprobar_Click;
        }

        private void BtnAprobar_Click(object sender, EventArgs e)

        {
            SqlConnection sqlconn;
            string connsqlstring = string.Format("Server=190.116.179.42,1433;Database=SpringLED;User Id=sa;Password=Serverdbl2013;Integrated Security=False");
            sqlconn = new SqlConnection(connsqlstring);
            sqlconn.Open();
            SqlCommand cmd;

            foreach (clientListaLadrillo mFila in items)
            {
                if (decimal.Parse(mFila.PrecioA) != 0)
                {
                    cmd = new SqlCommand("update CO_Promocion set Estado = 'A', UltimoUsuario = '" + USU_ID + "', UltimaFechaModif = '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "' where Promocion = " + Codigo);
                    cmd.Connection = sqlconn;
                    cmd.ExecuteNonQuery();
                }
            }

            //cmd = new SqlCommand("exec dbo.Crear_LPR " + Codigo);
            //cmd.Connection = sqlconn;
            //cmd.ExecuteNonQuery();

            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle("Mensaje");
            builder.SetMessage("Operacion completada.");
            //builder.SetCancelable(false);
            //builder.SetPositiveButton("OK", (senderAlert, args) => {
            //    Intent ListActivity = new Intent(this, typeof(ListActivity));
            //    StartActivity(ListActivity);
            //});
            builder.SetPositiveButton("OK", (senderAlert, args) => {
                //base.OnBackPressed();
                onBackPressed();
            });
            //builder.SetPositiveButton("OK", delegate { Finish(); });
            //builder.SetNegativeButton("Cancel", delegate { Finish(); });
            builder.Show();


           
        }

        public void onBackPressed()
        {
            //StartActivity(new Intent(this, typeof(ListActivity)));
            Intent Listactivity = new Intent(this, typeof(ListActivity));
            Listactivity.PutExtra("USU_ID", USU_ID.ToUpper());
            StartActivity(Listactivity);
        }

    }
}