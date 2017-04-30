namespace AspNetSampleMusicStore.Models
{
    public class MusicStoreDbInitializer
       : System.Data.Entity.DropCreateDatabaseAlways<MusicStoreContext>
    {
        protected override void Seed(MusicStoreContext context)
        {
            context.Artists.Add(new Artist { Name = "Jay" });
            context.Genres.Add(new Genre { Name = "Pop" });
            context.Albums.Add(new Album
            {
                Artist = new Artist { Name = "USA" },
                Genre = new Genre { Name = "POP" },
                Price = 0.01m,
                Title = "See you!"
            });
            base.Seed(context);
        }
    }
}