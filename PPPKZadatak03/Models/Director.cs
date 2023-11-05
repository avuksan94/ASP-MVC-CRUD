using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PPPKZadatak03.Models;

public partial class Director
{
    public int DirectorId { get; set; }
    [Required(ErrorMessage = "Name is required!!.")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
