using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.ApplicationModel;

namespace Community.Uwp
{
    public class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel()
        {
            if (DesignMode.DesignModeEnabled)
                GenerateDummyData();
            else
                GetUsers();
        }

        private void GenerateDummyData()
        {
            this.Users = new ObservableCollection<User> {
            new User { Name="Facundo", LastName="Primero", Email="facundo@gmail.com", Id=1},
            new User { Name="Ramiro", LastName="Segundo", Email="facundo@gmail.com", Id=2},
            new User { Name="Daniel", LastName="Tercero", Email="facundo@gmail.com", Id=3},
            new User { Name="Juan", LastName="Cuarto", Email="facundo@gmail.com", Id=4}};
        }

        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        private async Task GetUsers()
        {


            var client = CustomHttpClient.GetClient();


            HttpResponseMessage response =
                await client.GetAsync("http://localhost:32162/api/users?sort=name&page=1&pagesize=50");
            string content = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<IEnumerable<User>>(content);
            Users.Clear();
            if (list != null)
            {
                foreach (var user in list)
                {
                    Users.Add(user);
                }
            }
        }
    }
}