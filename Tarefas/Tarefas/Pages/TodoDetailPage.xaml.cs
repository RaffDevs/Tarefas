using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarefas.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tarefas.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TodoDetailPage : ContentPage
	{
		public TodoDetailPage ()
		{
			InitializeComponent ();
		}

		public TodoDetailPage(ToDo todo)
		{
			InitializeComponent ();

			BindingContext = todo;
		}

        private void Back(object sender, EventArgs e)
        {
			Navigation.PopAsync();
        }
    }
}