using PPPKZadatak03.Models;
using System.ComponentModel.DataAnnotations;

namespace PPPKZadatak03.ViewModels
{
    public class VMMovie
    {
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(255, ErrorMessage = "Title cannot be longer than 255 characters.")]
        public string? Title { get; set; }

        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [StringLength(50, ErrorMessage = "Genre cannot be longer than 50 characters.")]
        public string? Genre { get; set; }

        public List<string> ActorNames { get; set; } = new();
        public List<string> DirectorNames { get; set; } = new();
        public List<string> PosterLinks { get; set; } = new();

        public List<int> SelectedActorIds { get; set; } = new();
        public List<int> SelectedDirectorIds { get; set; } = new();
        public List<int> SelectedPosterLinks { get; set; } = new();

        // If you need to display a list of all available actors, directors, and posters in your view
        // (e.g., in a dropdown or multi-select list), you can include these properties:
        public IEnumerable<Actor> AvailableActors { get; set; } = new List<Actor>();
        public IEnumerable<Director> AvailableDirectors { get; set; } = new List<Director>();
        public IEnumerable<Poster> AvailablePosters { get; set; } = new List<Poster>();
    }
}
