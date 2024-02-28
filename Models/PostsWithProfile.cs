namespace SocialMediaPlatform.Models
{
	public class PostsWithProfile
	{
		public PostsWithProfile(Profile _pro, IEnumerable<Post> _posts)
		{
			profile = _pro;
			posts = _posts;
		}
		public Profile profile { get; set; }
		public IEnumerable<Post> posts { get; set; }
	}
}
