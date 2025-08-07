using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;

[ApiController]
[Route("dogphoto")]
public class DogPhotoController : ControllerBase
{
    private readonly DogPhotoContext _context;
    private readonly HttpClient _httpClient;

    public DogPhotoController(DogPhotoContext context, IHttpClientFactory httpClientFactory)
    {
        _context = context;
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpGet("{breed}")]
    [HttpGet("{breed}/{subBreed}")]
    public async Task<IActionResult> Get(string breed, string? subBreed = null)
    {
        var photo = await _context.DogPhotos.FirstOrDefaultAsync(p => 
            p.Breed.ToLower() == breed.ToLower() && 
            (subBreed == null || p.SubBreed.ToLower() == subBreed.ToLower()));

        if (photo != null) return Ok(photo.ImageUrl);

        string apiUrl = subBreed == null
            ? $"https://dog.ceo/api/breed/{breed}/images/random"
            : $"https://dog.ceo/api/breed/{breed}/{subBreed}/images/random";

        var response = await _httpClient.GetStringAsync(apiUrl);
        var result = JsonSerializer.Deserialize<DogApiResponse>(response);
        Console.WriteLine(result.status);

        if (result?.status != "success" || string.IsNullOrEmpty(result.message))
            return NotFound("Image not found for the specified breed.");

        var newPhoto = new DogPhoto
        {
            Breed = breed,
            SubBreed = subBreed,
            ImageUrl = result.message
        };

        _context.DogPhotos.Add(newPhoto);
        await _context.SaveChangesAsync();

        return Ok(newPhoto.ImageUrl);
    }

    private class DogApiResponse
    {
        public string message { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
    }
}