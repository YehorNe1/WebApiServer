using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Paper_Project.Models;

public partial class Customer
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Please Enter Name")]
    [StringLength(255, ErrorMessage = "Name cannot be more than 255 characters")]
    public string Name { get; set; } = null!;

    [StringLength(255, ErrorMessage = "Email cannot be more than 255 characters")]
    public string? Address { get; set; }

    [Phone(ErrorMessage = "Please Enter Phone Number")]
    [StringLength(50, ErrorMessage = "Phone number cannot be more than 50 characters")]
    public string? Phone { get; set; }

    [EmailAddress(ErrorMessage = "Please Enter Email Address")]
    [StringLength(255, ErrorMessage = "Email cannot be more than 255 characters")]
    public string? Email { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
