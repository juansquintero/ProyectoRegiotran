﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Regiotran.Droid.Perisistence;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(ISQLiteDB))]

namespace Regiotran.Droid.Perisistence
{
    interface ISQLiteDB
    {

    }    
}