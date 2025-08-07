public class DogPhoto
{
    public int Id { get; set; }
    public string Breed { get; set; } = string.Empty;
    public string? SubBreed { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
