using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
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
    public partial class TodoListPage : ContentPage, INotifyPropertyChanged
    {
        private TodosRepository _repository = new TodosRepository();

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<ToDo> _todoList = new ObservableCollection<ToDo>();
        public ObservableCollection<ToDo> TodoList
        {
            get
            {
                return _todoList;
            }

            set
            {
                _todoList = value;
                RaisePropertyChanged(nameof(TodoList));

            }
        }

        public TodoListPage()
        {
            InitializeComponent();
            UpdateCalendarDate(DateTime.Now);
            RegisterSubscriber();
            
        }

        private void RegisterSubscriber()
        {
            MessagingCenter.Subscribe<TodoListPage, ToDo>(this, "OnToDoCreated", (sender, todo) =>
            {
                if (
                    todo.Date.Value.Month == DateTime.Now.Month && 
                    todo.Date.Value.Day == DateTime.Now.Day
                )
                {
                    TodoList.Add(todo);
                }
            });
        }

        private void NavigateToRegisterTask(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new TodoRegisterPage());
        }

        private void OpenTaskDetails(object sender, EventArgs e)
        {
            var myEvent = (TappedEventArgs)e;
            var todo = (ToDo)myEvent.Parameter;
            var todoDetailPage = new TodoDetailPage(todo);

            Navigation.PushAsync(todoDetailPage);
        }

        private async void SwipeRemoveItem(object sender, EventArgs e)
        {
            try
            {
                var swipeItem = (SwipeItem)sender;
                ToDo todo = (ToDo)swipeItem.CommandParameter;

                bool confirm = await DisplayAlert("Excluir", "Deseja remover essa tarefa?", "Sim", "Não");

                if (confirm)
                {
                    var deleted = await _repository.Delete(todo.Id);

                    if (deleted)
                    {
                        TodoList.Remove(todo);
                    }
                }
            } catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            

        }

        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = (CheckBox)sender;

            if (checkBox.BindingContext != null)
            {
                var todo = (ToDo)checkBox.BindingContext;
                var label = checkBox.Parent.FindByName<Label>("LbTodoName");

                await _repository.Update(todo);
            }
        }

        private void OpenCalendar(object sender, EventArgs e)
        {
            DPCalendar.Focus();
        }

        private void DPCalendar_DateSelected(object sender, DateChangedEventArgs e)
        {
            UpdateCalendarDate(e.NewDate);
        }

        private void UpdateCalendarDate(DateTime date)
        {
            Task.Run(async () =>
            {
                var todos = await _repository.Search(date);


                Device.BeginInvokeOnMainThread(() =>
                {
                    TodoList.Clear();

                    foreach(var todo in todos)
                    {
                        TodoList.Add(todo);
                    }
                    TaskCount.Text = $"{todos.Count.ToString()} tarefas";
                    CvListTodo.ItemsSource = TodoList;
                    DayName.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(date.DayOfWeek).ToString();
                    DayMonth.Text = date.Day.ToString();
                    MonthName.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
                });
            });
        }

        private void Strikethrough(Label label, bool done)
        {
            if (done)
            {
                label.TextDecorations = TextDecorations.Strikethrough;
            } else
            {
                label.TextDecorations = TextDecorations.None;
            }
        }
    }
}