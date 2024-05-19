namespace SnackHub.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Category Category { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
        public List<string> Image { get; private set; }

        public Product(string name, Category category, decimal price, string description, List<string> image)
        {
            Id = Guid.NewGuid();
            Name = name;
            Category = category;
            Price = price;
            Description = description;
            Image = image ?? new List<string>();
        }

        public void Edit(string name, Category categoria, decimal price, string description, List<string> image)
        {
            Name = name;
            Category = categoria;
            Price = price;
            Description = description;
            Image = image ?? new List<string>();
        }
    }
}
