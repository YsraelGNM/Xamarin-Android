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

namespace Phoneword
{
    class CusotmListAdapter : BaseAdapter<Persona>
    {
        Activity context;
        List<Persona> list;

        public CusotmListAdapter(Activity _context, List<Persona> _list)
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

        public override Persona this[int index]
        {
            get { return list[index]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.rLista, parent, false);

            Persona item = this[position];
            view.FindViewById<TextView>(Resource.Id.Nombre).Text = item.Nombre;
            view.FindViewById<TextView>(Resource.Id.Cliente).Text = item.Cliente;
            view.FindViewById<TextView>(Resource.Id.Codigo).Text = item.Codigo;
           
            return view;
        }
    }
}