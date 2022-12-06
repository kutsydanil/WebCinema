using CinemaCore.Models;

namespace WebCinema.Models.IndexViewModels
{
    public class ActorsViewModel
    {
        public IEnumerable<Actors> ActorsList { get; set; } = new List<Actors>();

        public string? ActorName { get; set; }

        public string? ActorMiddleName { get; set; }

        public string? ActorSurName { get; set; }
    }
}
