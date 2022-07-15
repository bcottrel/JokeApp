using System.ComponentModel.DataAnnotations;

namespace JokeApp.Model
{
    public class Joke
    {
        public int Id { get; set; }

        [Display(Name = "Joke Question")]
        [Required(ErrorMessage = "The set-up of the joke is needed for the joke to make sense.")]
        public string? JokeQuestion { get; set; }

        [Display(Name = "Joke Answer")]
        [Required(ErrorMessage = "The answer of the joke is needed.")]
        public string? JokeAnswer { get; set; }
    }
}
