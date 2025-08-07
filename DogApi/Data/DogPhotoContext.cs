using Microsoft.EntityFrameworkCore;


public class DogPhotoContext : DbContext
{
    public DogPhotoContext(DbContextOptions<DogPhotoContext> options) : base(options) { }

    public DbSet<DogPhoto> DogPhotos => Set<DogPhoto>();
}