using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PPPKZadatak03.Models;

public partial class Movie
{
    public int MovieId { get; set; }
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(255, ErrorMessage = "Title cannot be longer than 300 characters.")]
    public string Title { get; set; } = null!;

    public DateTime? ReleaseDate { get; set; }
    [Required(ErrorMessage = "Gerne is required.")]
    [StringLength(255, ErrorMessage = "Genre cannot be longer than 300 characters.")]
    public string? Genre { get; set; }

    public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();

    public virtual ICollection<Director> Directors { get; set; } = new List<Director>();

    public virtual ICollection<Poster> Posters { get; set; } = new List<Poster>();
}
