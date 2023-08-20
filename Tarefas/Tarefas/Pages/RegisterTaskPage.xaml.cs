using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tarefas.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterTaskPage : ContentPage
	{
		public RegisterTaskPage ()
		{
			InitializeComponent ();
		}

        private void CloseModal(object sender, EventArgs e)
        {
			Navigation.PopModalAsync();
        }
    }
}