using System.Data.Entity;

namespace AspNetSampleMusicStore.Models
{
    public class MusicStoreContext : DbContext
    {
        public MusicStoreContext() : base("name=MusicStoreContext")
        {
        }

        public System.Data.Entity.DbSet<AspNetSampleMusicStore.Models.Album> Albums { get; set; }

        public System.Data.Entity.DbSet<AspNetSampleMusicStore.Models.Artist> Artists { get; set; }

        public System.Data.Entity.DbSet<AspNetSampleMusicStore.Models.Genre> Genres { get; set; }

        public System.Data.Entity.DbSet<AspNetSampleMusicStore.Models.Order> Orders { get; set; }
    
    }
}
