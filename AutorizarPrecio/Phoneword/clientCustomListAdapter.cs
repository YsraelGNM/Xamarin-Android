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
using Android.Text;

namespace Phoneword
{
    class clientCustomListAdapter : BaseAdapter<clientListaLadrillo>
    {
        Activity context;
        List<clientListaLadrillo> list;

        public clientCustomListAdapter(Activity _context, List<clientListaLadrillo> _list)
            : base()
        {
            this.context = _context;
            this.list = _list;
        }

        public override int Count
        {
            get { return list.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override clientListaLadrillo this[int index]
        {
            get { return list[index]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.clientLista, parent, false);


            EditText numberPicker = view.FindViewById<EditText>(Resource.Id.PrecioA);
            //numberPicker.Text = this[position].PrecioA;
            numberPicker.TextChanged += (object sender, TextChangedEventArgs e) => {
                this[position].PrecioA = e.Text.ToString();
            };
          



            clientListaLadrillo item = this[position];



            view.FindViewById<TextView>(Resource.Id.Ladrillo).Text = item.Ladrillo;
            view.FindViewById<TextView>(Resource.Id.PrecioN).Text = item.PrecioN;
            view.FindViewById<EditText>(Resource.Id.PrecioA).Text = item.PrecioA;

            return view;
        }

       
}
}