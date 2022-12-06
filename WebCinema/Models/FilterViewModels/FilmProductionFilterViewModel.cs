namespace WebCinema.Models.FilterViewModels
{
    public class FilmProductionFilterViewModel
    {
        public string? SelectedCountry { get; }
        public string? SelectedName { get; }

        public FilmProductionFilterViewModel(string? filmProductionName, string? filmProductionCountry)
        {
            SelectedName = filmProductionName;
            SelectedCountry = filmProductionCountry;
        }
    }
}
