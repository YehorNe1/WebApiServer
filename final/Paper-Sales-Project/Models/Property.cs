using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Paper_Project.Models;

public partial class Property
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Enter Name")]
    [StringLength(255, ErrorMessage = "Name cannot be more than 255 characters")]
    public string PropertyName { get; set; } = null!;

    public virtual ICollection<Paper> Papers { get; set; } = new List<Paper>();
}
