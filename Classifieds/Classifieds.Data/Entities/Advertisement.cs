using System.ComponentModel;

namespace Classifieds.Data.Entities;

public class Advertisement
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}