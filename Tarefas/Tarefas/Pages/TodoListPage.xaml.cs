using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarefas.Repositories;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tarefas.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoListPage : ContentPage
    {
        public TodoListPage()
        {
            InitializeComponent();

            new TodosRepository().Search(DateTime.Now);
        }

        private void NavigateToRegisterTask(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new TodoRegisterPage());
        }

        private void OpenTaskDetails(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TodoDetailPage());
        }
    }
}