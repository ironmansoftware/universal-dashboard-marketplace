namespace Marketplace.Data.Seeding
{
    public class Version1 : IDataSeeder
    {
        public int Version => 1;

        public void Seed(ApplicationDbContext context)
        {
            var tags = new[] { "azure", "vmware", "help-desk", "monitoring" };

            foreach (var tag in tags)
            {
                var dbTag = new Tag
                {
                    Name = tag
                };
                context.Tags.Add(dbTag);
            }
        }
    }
}
