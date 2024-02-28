using System.ComponentModel.DataAnnotations;

namespace SocialMediaPlatform.Models
{
    public class Post
    {
        public Post() { }
        public Post(string image, string description, string sender, string receiver, DateTime date, string displayName, int userID)
        {
            this.Image = image;
            this.Description = description;
            this.SenderID = sender;
            this.ReceiverID = receiver;
            this.PostDate = date;
            senderUsername = displayName;
            MatchingID = userID;
        }
        [Key] public int Id { get; set; }
        [Required] public string Image { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string SenderID { get; set; }
        [Required] public string ReceiverID { get; set; }
        [Required] public DateTime PostDate  { get; set; }
		[Required] public string senderUsername { get; set; }
		[Required] public int MatchingID { get; set; }
    }
}
