using System;
using System.Data.Entity;

namespace SqLiteEF
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var db = new SqLiteDb();
            int lastId = 0;

            foreach (var entity in db.Entities)
            {
                Console.WriteLine(entity.Name);
                lastId = entity.Id;
            }

            lastId++;
            db.Entities.Add(new Entity { Name = string.Format("enity {0}", lastId) });
            db.SaveChanges();
        }
    }

    public class SqLiteDb : DbContext
    {
        public SqLiteDb()
            : base("DatabaseContext")
        {
        }

        public DbSet<Entity> Entities { get; set; }
    }

    public class Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}