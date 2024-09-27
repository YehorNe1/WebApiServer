using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Paper_Project.Models;

public partial class Paper
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Paper title is required")]
    [StringLength(255, ErrorMessage = "Paper title cannot be more than 255 characters")]
    public string Name { get; set; } = null!;

    public bool Discontinued { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public int Stock { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Price must be greater than 0.0")]
    public double Price { get; set; }

    public virtual ICollection<OrderEntry> OrderEntries { get; set; } = new List<OrderEntry>();

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
