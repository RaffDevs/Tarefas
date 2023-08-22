using System;
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
            LbData.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

            if (await Validate(todo))
            {
                var success = await new TodosRepository().Create(todo);



                if (success)
                {
                    MessagingCenter.Send<TodoListPage, ToDo>(new TodoListPage(), "OnToDoCreated", todo);
                    await Navigation.PopModalAsync();
                }
            }

            

        }

        private void DatePickerUnfocus(object sender, FocusEventArgs e)
        {
			DatePicker datePicker = (DatePicker)sender;
            LbData.Text = datePicker.Date.ToString("dd/MM/yyyy");

        }

        private void FocusDatePicker(object sender, EventArgs e)
        {
			Date.Focus();

        }

        private void FocusTimePicker(object sender, EventArgs e)
        {
			StartTime.Focus();
        }

        private void StartTime_Unfocused(object sender, FocusEventArgs e)
        {
			var timePicker = (TimePicker)sender;

            SpStartTime.Text = timePicker.Time.ToString(@"hh\:mm");

            EndTime.Focus();
            
        }

        private void EndTime_Unfocused(object sender, FocusEventArgs e)
        {
            var timePicker = (TimePicker)sender;

            SpEndTime.Text = timePicker.Time.ToString(@"hh\:mm");

        }


        private async Task<bool> Validate(ToDo todo)
        {
            bool validation = true;

            if (todo.StartTime > todo.EndTime)
            {
                await DisplayAlert("Erro", "O horário inicial não pode ser maior que o horário de término!", "OK");
                validation = false;
            }

            if (todo.Name == null)
            {
                await DisplayAlert("Erro", "O nome da tarefa precisa ser preenchido!", "OK");
                validation = false;
            }

            if (todo.Name != null && todo.Name.Length < 5)
            {
                await DisplayAlert("Erro", "O nome da tarefa precisa ter pelo menos 5 caracteres!", "OK");
                validation = false;
            }

            return validation;
        }
    }
}