using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleConnectFour
{
    public struct UserPayloadObj
    {
        public UserObj user { get; set; }
        public string message { get; set; }
        public string reqid { get; set; }
    }

    public struct UserObj
    {
        public string id { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

    public class LeaderboardPayloadObj
    {
        public LeaderboardObj[] scores { get; set; }
        public string message { get; set; }
        public string reqid { get; set; }
    }

    public class LeaderboardObj
    {
        public int total_points { get; set; }
        public UserObj user { get; set; }
    }

    public class ScoreObj
    {
        public int points { get; set; }
    }


    class Client
    {
        public static bool LoggedIn;
        public static UserObj User;
        private static CookieContainer cookieContainer;
        private static HttpClientHandler clienthandler;
        private static HttpClient apiClient;


        public static void Setup()
        {
            cookieContainer = new CookieContainer();
            clienthandler = new HttpClientHandler { AllowAutoRedirect = true, UseCookies = true, CookieContainer = cookieContainer };

            apiClient = new HttpClient(clienthandler);
            apiClient.BaseAddress = new Uri("http://localhost:4000");

            LoggedIn = false;
        }

        public static async Task Login()
        {
            Console.Clear();
            
            Console.WriteLine("Login System \n");

            Console.WriteLine("Enter Email \n");
            string email = Console.ReadLine();

            Console.WriteLine("Enter Password \n");
            string password = Console.ReadLine();

            var postUser = new UserObj { email = email, password = password };

            try
            {
                HttpResponseMessage postResponse = await apiClient.PostAsJsonAsync("/session", postUser);
                postResponse.EnsureSuccessStatusCode();

                string responseBody = await postResponse.Content.ReadAsStringAsync();
                var userPayload = JsonConvert.DeserializeObject<UserPayloadObj>(responseBody);

                LoggedIn = true;
                User = userPayload.user;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            catch (Newtonsoft.Json.JsonException) // Invalid JSON
            {
                Console.WriteLine("Invalid JSON.");
                Console.ReadKey();
            }
        }

        public static async Task Register()
        {
            Console.Clear();

            Console.WriteLine("Register System \n");

            Console.WriteLine("Enter Email:");
            string email = Console.ReadLine();

            Console.WriteLine("Enter Username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter Password");
            string password = Console.ReadLine();

            var postUser = new UserObj { email = email, username = username, password = password };

            try
            {
                HttpResponseMessage postResponse = await apiClient.PostAsJsonAsync("/session/register", postUser);
                postResponse.EnsureSuccessStatusCode();

                string responseBody = await postResponse.Content.ReadAsStringAsync();
                var userPayload = JsonConvert.DeserializeObject<UserPayloadObj>(responseBody);

                LoggedIn = true;
                User = userPayload.user;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            catch (Newtonsoft.Json.JsonException e) // Invalid JSON
            {
                Console.WriteLine("Invalid JSON.");
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        public static async Task Info()
        {
            Console.WriteLine("Info System \n");

            try
            {
                var userPayload = await apiClient.GetFromJsonAsync<UserPayloadObj>("/session");

                Console.WriteLine(userPayload.user.id);
                Console.WriteLine(userPayload.message);

            }
            catch (HttpRequestException) // Non success
            {
                Console.WriteLine("An error occurred.");
            }
            catch (NotSupportedException) // When content type is not valid
            {
                Console.WriteLine("The content type is not supported.");
            }
            catch (System.Text.Json.JsonException) // Invalid JSON
            {
                Console.WriteLine("Invalid JSON.");
            }

            Console.ReadKey();
        }

        public static async Task<LeaderboardPayloadObj> Leaderboard()
        {
            try
            {
                var leaderboardPayload = await apiClient.GetFromJsonAsync<LeaderboardPayloadObj>("/leaderboard/4a16036e-a969-4211-9a14-32702eee265f");

                return leaderboardPayload;
            }
            catch (HttpRequestException) // Non success
            {
                Console.WriteLine("An error occurred.");
            }
            catch (NotSupportedException) // When content type is not valid
            {
                Console.WriteLine("The content type is not supported.");
            }
            catch (System.Text.Json.JsonException e) // Invalid JSON
            {
                Console.WriteLine("Invalid JSON.");
                Console.WriteLine(e.Message);
            }

            return null;
        }

        public static async Task AddPoints(int points)
        {
            var postUser = new ScoreObj { points = points };

            try
            {
                HttpResponseMessage postResponse = await apiClient.PostAsJsonAsync("/leaderboard/4a16036e-a969-4211-9a14-32702eee265f", postUser);
                postResponse.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
