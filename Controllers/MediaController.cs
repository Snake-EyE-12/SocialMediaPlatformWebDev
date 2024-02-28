using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using SocialMediaPlatform.Interfaces;
using SocialMediaPlatform.Models;
using System.Security.Claims;

namespace SocialMediaPlatform.Controllers
{
	public class MediaController : Controller
	{
		private DataAccessLayer<Profile> DALProfile;
		private DataAccessLayer<Post> DALPost;
		public MediaController(DataAccessLayer<Profile> _DALProfile, DataAccessLayer<Post> _DALPost) 
		{
			DALProfile = _DALProfile;
			DALPost = _DALPost;
		}
		private Profile GetMatchingProfile()
		{
			string currentPlayer = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (currentPlayer == null)
			{
				Console.WriteLine("Not Signed In");
				return null;
			}
			Profile data = DALProfile.GetAll().FirstOrDefault(x => x.Username.Equals(currentPlayer));
			if (data != default(Profile))
			{
				return data;
			}

			Profile pd = new Profile("Unnamed", "/Images/profilePic.png", 0, "None", "None");
			pd.Username = currentPlayer;
			DALProfile.Add(pd);
			return pd;
		}
		public IActionResult MyPage(int id)
		{
			Profile profile = DALProfile.Get(id);
			
			if (profile == null || profile.Username == null)
			{
				Profile matching = GetMatchingProfile();
				if(matching == null) return RedirectToPage("/Account/Login", new { area = "Identity" });
				else return View(GetBigModel(GetMatchingProfile()));
			}
			return View(GetBigModel(profile));
		}
		
		private PostsWithProfile GetBigModel(Profile profile)
		{
			return new PostsWithProfile(profile, DALPost.GetAll().Where(x => x.ReceiverID == profile.Username));
		}
		private PostsWithProfile GetBigModelPersonal(Profile profile)
		{
			return new PostsWithProfile(profile, DALPost.GetAll().Where(x => x.SenderID == profile.Username));
		}

		[HttpGet]
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				ViewData["Error"] = "No ID given";
				return View();
			}
			else
			{
				Profile? m = DALProfile.Get(id.Value);
				if (m == null)
				{
					ViewData["Error"] = "Unknown ID";
					return View();
				}
				return View(m);
			}
		}
		[HttpPost]
		public IActionResult Edit(Profile m)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			DALProfile.Update(m);
			TempData["success"] = "Profile Updated";
			return RedirectToAction("MyPage", "Media");
		}
		public IActionResult SearchPage(string filter)
		{
			return View(DALProfile.Search(filter).ToList());
		}
		public IActionResult MakePost(string receiver)
		{
			Profile recProfile = DALProfile.GetAll().First(x => x.Username == receiver);
			Profile userP = DALProfile.GetAll().First(x => x.Username == User.FindFirstValue(ClaimTypes.NameIdentifier));
			return View(new Post("", "", User.FindFirstValue(ClaimTypes.NameIdentifier), receiver, DateTime.Now, User.FindFirstValue(ClaimTypes.Email), userP.Id));
		}
		public IActionResult SeePosts()
		{
			Profile p = GetMatchingProfile();
			return View("MyPage", GetBigModelPersonal(p));
		}
		public IActionResult PostPost(Post post)
		{

			if (!ModelState.IsValid)
			{
				return RedirectToAction("MakePost", "Media", new { receiver = post.ReceiverID });
			}

			post.PostDate = DateTime.Now;
			DALPost.Add(post);
			return RedirectToAction("MyPage", "Media");
			
		}
	}
}
