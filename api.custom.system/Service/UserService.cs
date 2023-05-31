using api.custom.system.Models;
using api.custom.system.Service.Interfaces;
using api__custom_system.Models;
using api__custom_system.Repository;

namespace api.custom.system.Service
{
    public class UserService : IUserService
    {
        private IWebHostEnvironment _environment;
        private readonly DatabaseContext _context;

        public UserService(IWebHostEnvironment environment, DatabaseContext context)
        {
            _environment = environment;
            _context = context;

        }


        public async Task<User> GetUserById(int id)
        {
            return _context.UserInfos.FirstOrDefault(value => value.Id == id);

        }

        public async Task<UserProfile?> GetProfileById(int id)
        {
            UserProfile? userProfile = _context.UserProfile?.FirstOrDefault(profile => profile.Id == id);

            if (userProfile == null)
            {
                return null;
            }

            return userProfile;
        }

        public async Task SaveImageProfile(ICollection<IFormFile> files,int id)
        {
       
            string directory = "images";
            string path = $"{_environment.WebRootPath}\\{directory}\\{id}";
            var fileName = files.First().FileName;
            string file = Path.Combine($"{path}\\{fileName}");
            Directory.CreateDirectory(path);
            File.Create(file).Dispose();


            UserProfile? userProfile = GetProfileById(id).Result;
            userProfile.ImageProfile = file;
            _context.SaveChanges();


            List<byte[]> data = new();

            foreach (var item in files)
            {
                using (var stream = new MemoryStream())
                {
                    await item.CopyToAsync(stream);
                    data.Add(stream.ToArray());
                }
            }
           
            data.ForEach(data =>
            {
                File.WriteAllBytes(file, data);
            });

            await Task.CompletedTask;
            
        }

        public Task CreateUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public async Task<UserProfile> CreateUserProfile(User user)
        {
            UserProfile userProfile = new()
            {
                UserName = user.UserName,
            };
            _context.Add(userProfile);
            _context.SaveChanges();
            return _context.UserProfile.OrderBy(value => value.Id).LastOrDefault();
        }

        public Task UpdateUserProfile(UserProfile userProfile)
        {

            UserProfile? profile = GetProfileById(userProfile.Id).Result;
            if (!string.IsNullOrEmpty(userProfile.UserName)) 
            {
                profile.UserName = userProfile.UserName;
            }
            if(!string.IsNullOrEmpty(userProfile.UserEmail))
            {
                profile.UserEmail = userProfile.UserEmail;
            }
            if(!string.IsNullOrEmpty(userProfile.Address))
            {
                profile.Address = userProfile.Address;
            }
            _context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
