// See https://aka.ms/new-console-template for more information
using System.Net.Http;

Console.WriteLine("Enter breed (or breed subbreed):");
var input = Console.ReadLine()?.Trim().Split(' ');
if (input == null || input.Length == 0)
{
    Console.WriteLine("Invalid input");
    return;
}

string url = input.Length == 1
    ? $"http://localhost:5155/dogphoto/{input[0]}"
    : $"http://localhost:5155/dogphoto/{input[0]}/{input[1]}";

using var httpClient = new HttpClient();
try
{
    var imageUrl = await httpClient.GetStringAsync(url);
    Console.WriteLine($"Dog Image URL: {imageUrl}");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
