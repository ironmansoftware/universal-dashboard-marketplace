namespace Marketplace.Data.Seeding
{
    public interface IDataSeeder
    {
        int Version { get; }

        void Seed(ApplicationDbContext context);
    }
}
