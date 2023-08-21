using System;
using Tarefas.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tarefas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var mainPage = new NavigationPage(new TodoListPage());
            MainPage = mainPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
