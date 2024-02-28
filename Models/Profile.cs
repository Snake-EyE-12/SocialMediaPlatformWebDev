using System.ComponentModel.DataAnnotations;

namespace SocialMediaPlatform.Models
{
    public class Profile
    {
        public Profile() { }
        public Profile(string name, string pPicture, int age, string color, string hobby)
        {
            Name = name;
            ProfilePicture = pPicture;
            Age = age;
            FavoriteColor = color;
            FavoriteHobby = hobby;

        }
        [Key] public int Id { get; set; }
        [Required(ErrorMessage = "Must have a name")] public string Name { get; set; }
        [Required(ErrorMessage = "Must have an image")] public string ProfilePicture { get; set; }
        [Required][Range(0, 200)] public int Age { get; set; }
        [Required(ErrorMessage = "Must have a color")] public string FavoriteColor { get; set; }
        [Required(ErrorMessage = "Must have a hobby")] public string FavoriteHobby { get; set; }
        [Required] public string Username { get; set; }
    }
}
