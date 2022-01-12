using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleConnectFour
{
    public class User
    {
        public string id { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

    public class Response
    {
        public string payload { get; set; }
    }

    class Client
    {
        private static CookieContainer cookieContainer;
        private static HttpClientHandler clienthandler;
        private HttpClient apiClient;

        public Client()
        {
            cookieContainer = new CookieContainer();
            clienthandler = new HttpClientHandler { AllowAutoRedirect = true, UseCookies = true, CookieContainer = cookieContainer };

            apiClient = new HttpClient(clienthandler);

            apiClient.BaseAddress = new Uri("http://localhost:4000");

        }

        public async Task Login()
        {
            Console.WriteLine("Login System \n");

            Console.WriteLine("Enter Email \n");
            string email = Console.ReadLine();

            Console.WriteLine("Enter Password \n");
            string password = Console.ReadLine();

            var postUser = new User { email = email, password = password };

            try
            {
                HttpResponseMessage postResponse = await apiClient.PostAsJsonAsync("/session", postUser);
                postResponse.EnsureSuccessStatusCode();

                string responseBody = await postResponse.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);

                Console.ReadKey();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);

                Console.ReadKey();
            }
        }

        public async Task Register()
        {
            Console.WriteLine("Register System \n");

            Console.WriteLine("Enter Email:");
            string email = Console.ReadLine();

            Console.WriteLine("Enter Username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter Password");
            string password = Console.ReadLine();

            var postUser = new User { email = email, username = username, password = password };

            try
            {
                HttpResponseMessage postResponse = await apiClient.PostAsJsonAsync("/session/register", postUser);
                postResponse.EnsureSuccessStatusCode();

                string responseBody = await postResponse.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);

                Console.ReadKey();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);

                Console.ReadKey();
            }
        }

        public async Task Info()
        {
            Console.WriteLine("Info System \n");

            try
            {
                var user = await apiClient.GetFromJsonAsync<User>("/session");

                Console.WriteLine(user.id);

            }
            catch (HttpRequestException) // Non success
            {
                Console.WriteLine("An error occurred.");
            }
            catch (NotSupportedException) // When content type is not valid
            {
                Console.WriteLine("The content type is not supported.");
            }
            catch (JsonException) // Invalid JSON
            {
                Console.WriteLine("Invalid JSON.");
            }

            Console.ReadKey();
        }
    }
}
