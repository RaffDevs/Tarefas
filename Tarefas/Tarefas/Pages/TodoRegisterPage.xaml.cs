using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarefas.Entities;
using Tarefas.Repositories;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tarefas.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TodoRegisterPage : ContentPage
	{
		public TodoRegisterPage ()
		{
			InitializeComponent ();
		}

        private void CloseModal(object sender, EventArgs e)
        {
			Navigation.PopModalAsync();
        }

        private async void SaveTodo(object sender, EventArgs e)
        {
			ToDo todo = new ToDo();

			todo.Name = Name.Text;
			todo.Description = Description.Text;
			todo.Date = Date.Date;
			todo.StartTime = StartTime.Time;
			todo.EndTime = EndTime.Time;
			todo.Done = false;

			await new TodosRepository().Create(todo);

        }
    }
}