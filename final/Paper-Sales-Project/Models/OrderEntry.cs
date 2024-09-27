using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Paper_Project.Models;

public partial class OrderEntry
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter the order number")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Please enter the product id")]
    public int? ProductId { get; set; }

    [Required(ErrorMessage = "Please enter the order id")]
    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Paper? Product { get; set; }
}
