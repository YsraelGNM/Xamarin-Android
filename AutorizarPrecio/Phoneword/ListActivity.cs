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
    [Activity(Label = "Aprobar Precio: Escoge Vendedor y Cliente")]
    public class ListActivity : Activity
    {
        //string[] items;
        List<Persona> items;
        string Usu_Id;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ListActivity);
            // Create your application here
            ListView Lista = FindViewById<ListView>(Resource.Id.Lista);

            //items = new string[] { "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers" };
            items = new List<Persona>();

            Usu_Id = Intent.GetStringExtra("USU_ID") ?? "Data not available";

            SqlConnection sqlconn;
            string connsqlstring = string.Format("Server=190.116.179.42;Database=SpringLED;User Id=sa;Password=Serverdbl2013;Integrated Security=False");
            sqlconn = new SqlConnection(connsqlstring);
            sqlconn.Open();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("select distinct Nombre, Cliente, SAP_ID from dbo.vwSolicitudAjustePrecio", sqlconn);
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);

            foreach (DataRow mFila in ds.Tables[0].Rows)
            {
                Persona Per = new Persona();
                Per.Nombre = "Vend.: " + mFila["Nombre"].ToString();
                Per.Cliente = "Cli.: " + mFila["Cliente"].ToString();
                Per.Codigo = mFila["SAP_ID"].ToString(); ;
                items.Add(Per);
            }

            CusotmListAdapter ListAdapter = new CusotmListAdapter(this, items);
            Lista.Adapter = ListAdapter;

            Lista.ItemClick += Lista_ItemClick;
            
        }

        private void Lista_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
           //Console.WriteLine(items[e.Position].Codigo);
            Intent ListClient = new Intent(this, typeof(ListClient));
            ListClient.PutExtra("Codigo", items[e.Position].Codigo);
            ListClient.PutExtra("USU_ID", Usu_Id);
            StartActivity(ListClient);

        }
    }
}