using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PPPKZadatak03.Models;

public partial class Poster
{
    public int PosterId { get; set; }
    [Url(ErrorMessage = "Invalid URL")]
    public string Content { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
