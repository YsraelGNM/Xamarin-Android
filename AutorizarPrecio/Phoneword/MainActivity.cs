using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoneword
{
    [Activity(Label = "Aprobar Precio: Login", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            EditText UsuarioText = FindViewById<EditText>(Resource.Id.Usuario);
            EditText ContrasenaText = FindViewById<EditText>(Resource.Id.Clave);

            translateButton.Click += delegate {
               
                SqlConnection sqlconn;
                string connsqlstring = string.Format("Server=190.116.179.42,1433;Database=SpringLED;User Id=sa;Password=Serverdbl2013;Integrated Security=false");
                sqlconn = new SqlConnection(connsqlstring);
                sqlconn.Open();


                DataSet ds1 = new DataSet();
                SqlCommand cmd = new SqlCommand("select * from Usuario where usuario = '" + UsuarioText.Text + "'", sqlconn);
                var adapter1 = new SqlDataAdapter(cmd);
                adapter1.Fill(ds1);
                string mClave;

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    mClave = ds1.Tables[0].Rows[0]["Clave"].ToString().Replace(" ","");
                    int lClave;
                    string tClave;
                    tClave = "";
                    lClave = ContrasenaText.Text.Length;
                    for (int i = 0; i <= lClave-1; i++)
                    {
                        tClave = tClave + char.ConvertFromUtf32((int)Char.Parse(ContrasenaText.Text.Substring(i,1))+(i+1));
                    }

                    if (mClave == tClave)
                    {
                        UsuarioText.Enabled = false;
                        Intent Listactivity = new Intent(this, typeof(ListActivity));
                        Listactivity.PutExtra("USU_ID", UsuarioText.Text.ToUpper());
                        StartActivity(Listactivity);
                    }
                    else
                    {
                        AlertDialog.Builder builder = new AlertDialog.Builder(this);
                        builder.SetTitle("Error de Logueo");
                        builder.SetMessage("La Contraseña es incorrecta.");
                        //builder.SetCancelable(false);
                        builder.SetPositiveButton("OK", (senderAlert, args) => { });
                        //builder.SetNegativeButton("Cancel", delegate { Finish(); });
                        builder.Show();
                    }
                }
                else
                {
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetTitle("Error de Logueo");
                    builder.SetMessage("El usuario es incorrecto.");
                    //builder.SetCancelable(false);
                    builder.SetPositiveButton("OK", (senderAlert, args) => { });
                    //builder.SetNegativeButton("Cancel", delegate { Finish(); });
                    builder.Show();
                }
  
            };
        }

    }
}

