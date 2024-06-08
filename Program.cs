using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        //HttpClient client = new HttpClient();

        //Stopwatch stopwatch = new Stopwatch();  
        //// Thay đổi URL này thành URL của server của bạn
        //string url = "https://localhost:7278/api/Test";

        // Gửi yêu cầu GET
        //stopwatch.Start();

        //HttpResponseMessage responseGet = await client.GetAsync(url);
        //string responseBodyGet = await responseGet.Content.ReadAsStringAsync();

        //stopwatch.Stop();
        //TimeSpan elapsedTime = stopwatch.Elapsed;

        //Console.WriteLine($"GET Response: {responseBodyGet}");
        //Console.WriteLine($"GET Response: {responseBodyGet}");
        //Console.WriteLine($"Time elapsed: {elapsedTime}");


        //test gửi yêu cầu post 
        //HttpClient client = new HttpClient();

        //Stopwatch stopwatch = new Stopwatch();
        //string url = "https://localhost:7278/api/Test";

        //đổi filePath ở đây là được
        //string filePath = @"D:\Downloads\UITHoc\DoAn1LT\Dữ liệu kiểm thử\file_text_15MB.txt";
        //String data;
        //data = await ReadFileAsync(filePath);

        //var postData = new { Value = data };
        //var json = JsonSerializer.Serialize(postData);

        //HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        //stopwatch.Start();
        //HttpResponseMessage responsePost = await client.PostAsync(url, content);
        //string responseBodyPost = await responsePost.Content.ReadAsStringAsync();

        //stopwatch.Stop();
        //TimeSpan elapsedTime = stopwatch.Elapsed;

        //Console.WriteLine($"POST Response: {responseBodyPost}");
        //Console.WriteLine($"Time elapsed: {elapsedTime.TotalSeconds} seconds");


        //gửi 100 cái yêu cầu post
        HttpClient client = new HttpClient();

        Stopwatch stopwatch = new Stopwatch();
        string url = "https://localhost:7278/api/Test";

        string filePath = @"D:\Downloads\UITHoc\DoAn1LT\Dữ liệu kiểm thử\file_text_15MB.txt";
        string data = await ReadFileAsync(filePath);

        var postData = new { Value = data };
        var json = JsonSerializer.Serialize(postData);

        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        int totalRequests = 100;
        int successfulRequests = 0;

        for (int i = 0; i < totalRequests; i++)
        {
            stopwatch.Restart();
            try
            {
                HttpResponseMessage responsePost = await client.PostAsync(url, content);
                string responseBodyPost = await responsePost.Content.ReadAsStringAsync();
                stopwatch.Stop();

                if (responsePost.IsSuccessStatusCode)
                {
                    successfulRequests++;
                    Console.WriteLine($"POST Response: {responseBodyPost}");
                }
                else
                {
                    Console.WriteLine($"POST Failed: {responsePost.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Console.WriteLine($"POST Exception: {ex.Message}");
            }

            TimeSpan elapsedTime = stopwatch.Elapsed;
            Console.WriteLine($"Time elapsed: {elapsedTime.TotalSeconds} seconds");
        }

        Console.WriteLine($"Total Requests: {totalRequests}");
        Console.WriteLine($"Successful Requests: {successfulRequests}");
        Console.WriteLine($"Packet Loss: {(totalRequests - successfulRequests) * 100.0 / totalRequests}%");
    }


    private static async Task<string> ReadFileAsync(string filePath)
    {
        using StreamReader reader = new StreamReader(filePath);
        return await reader.ReadToEndAsync();
    }
}