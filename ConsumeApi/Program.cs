﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using var httpClient = new HttpClient();
        var baseUrl = "https://localhost:7235/Breakfasts";  // this is hardcoded as of now in actual implementation we could store the base URL of the API in a configuration file
        Console.WriteLine("Enter the Breakfast Id"); 
        string code = Console.ReadLine();

        try
        {
            // Send GET request
            var getResponse = await httpClient.GetAsync($@"{baseUrl}/{code}");
            getResponse.EnsureSuccessStatusCode();

            // Read and save response body to a JSON file
            var getResponseBody = await getResponse.Content.ReadAsStringAsync();
            File.WriteAllText("ConsumeApiresponse.json", getResponseBody);

            Console.WriteLine("Response saved to response.json");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
