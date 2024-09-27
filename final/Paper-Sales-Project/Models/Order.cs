using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Paper_Project.Models;

public partial class Order
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Oder message is required")]
    public DateTime OrderDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    [Required(ErrorMessage = "Status is required")]
    [StringLength(50, ErrorMessage = "Status cannot be more than 50 characters")]
    public string Status { get; set; } = null!;

    [Required(ErrorMessage = "Total amount is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Total amount cannot be negative")]
    public double TotalAmount { get; set; }

    [Required(ErrorMessage = "Customer id is required")]
    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderEntry> OrderEntries { get; set; } = new List<OrderEntry>();
}
