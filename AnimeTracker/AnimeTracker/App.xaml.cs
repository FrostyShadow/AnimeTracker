using System;
using System.IO;
using AnimeTracker.Interfaces;
using AnimeTracker.Models;
using Prism;
using Prism.Ioc;
using AnimeTracker.ViewModels;
using AnimeTracker.Views;
using JikanDotNet;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AnimeTracker
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            
            await NavigationService.NavigateAsync("RootTabbedPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterInstance<IDatabaseHelper>(new DatabaseHelper(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "AnimeTracker.db")));
            containerRegistry.RegisterInstance<IJikan>(new Jikan(true));

            containerRegistry.RegisterForNavigation<RootTabbedPage, RootTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<AnimeListPage, AnimeListPageViewModel>();
            containerRegistry.RegisterForNavigation<SearchPage, SearchPageViewModel>();
            containerRegistry.RegisterForNavigation<AnimeMoreInfoPage, AnimeMoreInfoPageViewModel>();
            containerRegistry.RegisterForNavigation<MyAnimeListPage, MyAnimeListPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
        }
    }
}
