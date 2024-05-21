using SnackHub.Domain.Entities;

namespace SnackHub.Application.Models
{
    public class GetProductResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public List<string>? Images { get; set; }

        public GetProductResponse() { }

        public GetProductResponse(Guid id, string name, Category category, decimal price, string description, List<string> images)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
            Description = description;
            Images = images;
        }
    }
}
