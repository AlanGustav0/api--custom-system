using System.Text.RegularExpressions;
using api.custom.system.Models;
using api.custom.system.Repository.Dto;
using api.custom.system.Service.Interfaces;
using api__custom_system.Models;
using api__custom_system.Repository;
using api__custom_system.Repository.Dto;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace api.custom.system.Service
{
    public class UserService : IUserService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IWebHostEnvironment environment, DatabaseContext context,IMapper mapper, IConfiguration configuration)
        {
            _environment = environment;
            _context = context;
            _mapper = mapper;
            _configuration = configuration;

        }


        public User? GetUserById(int id)
        {
            User? user = _context.UserInfos?.FirstOrDefault(value => value.Id == id);
            if(user != null)
            {
                return user;
            }
            return null;

        }


        public void SaveImageProfile(ProfileData profileData)
        {

            User? user = GetUserById(profileData.Id);

            if (!string.IsNullOrEmpty(user.ImageProfile))
            {
                string imageProfile = $"{_environment.WebRootPath}/{user.ImageProfile}";
                string imagePath = Regex.Split(imageProfile,"(\\/\\w+\\.jpg)")[0];
                Directory.Delete(imagePath, true);
            }
 
            string directory = "images";
            var guid = Guid.NewGuid(); 
            string path = $"{_environment.WebRootPath}//{directory}//{guid}";
            var fileName = profileData?.File?.FileName;
            string file = Path.Combine($"{path}//{fileName}");

            
            

            Directory.CreateDirectory(path);
            File.Create(file).Dispose();

            string hostFile = $"/{directory}/{guid}/{fileName}";
            
            if(user != null)
            {
                user.ImageProfile = hostFile;
            }
            
            _context.SaveChanges();


            List<byte[]> data = new();

         
                using (var stream = new MemoryStream())
                {
                    profileData?.File?.CopyToAsync(stream);
                    data.Add(stream.ToArray());
                }
           
            data.ForEach(data =>
            {
                File.WriteAllBytes(file, data);
            });

            
            
        }

        public User CreateUser(UserRequestDto userDto)
        {
            User user = _mapper.Map<User>(userDto);

            UserProfile? profile = CreateUserProfile(user.UserName).Result;
            user.UserName = userDto.NickName;
            user.UserProfile = profile;

            _context.Add(user);
            _context.SaveChanges();

            return user;
        }

        public UserProfileResponseDto UpdateUserProfile(UserProfileRequestDto userProfileDto)
        {
            UserProfile? userProfile = _mapper.Map<UserProfile>(userProfileDto);

            User? user = GetUserById(userProfileDto.Id);
            
            if(user != null)
            {
                if (!string.IsNullOrEmpty(userProfileDto.UserName))
                {
                    user.UserProfile.UserName = userProfile.UserName;
                }
                if (!string.IsNullOrEmpty(userProfileDto.Email))
                {
                    user.UserProfile.Email = userProfile.Email;
                }
                if (!string.IsNullOrEmpty(userProfileDto.Endereco))
                {
                    user.UserProfile.Endereco = userProfile.Endereco;
                }
                if (!string.IsNullOrEmpty(userProfile.Numero))
                {
                    user.UserProfile.Numero = userProfile.Numero;
                }
                if (!string.IsNullOrEmpty(userProfile.Numero))
                {
                    user.UserProfile.Numero = userProfile.Numero;
                }
                if (!string.IsNullOrEmpty(userProfile.Cep))
                {
                    user.UserProfile.Cep = userProfile.Cep;
                }
                if (!string.IsNullOrEmpty(userProfile.Cidade))
                {
                    user.UserProfile.Cidade = userProfile.Cidade;
                }
                if (userProfile.Estado != null)
                {
                    user.UserProfile.Estado = userProfile.Estado;
                }
            }
            
            _context.SaveChanges();
            var userProfileResponse = _mapper.Map<UserProfileResponseDto>(user?.UserProfile);

            return userProfileResponse;
        }

        public UserProfile GetUserProfile(int userProfileId)
        {
            UserProfile userProfile = _context.UserProfile.FirstOrDefault(profileId => profileId.Id == userProfileId);
            return userProfile;
        }

        private Task<UserProfile?> CreateUserProfile(string name)
        {
            UserProfile userProfile = new()
            {
                UserName = name
            };
            _context.Add(userProfile);
            _context.SaveChanges();

            UserProfile? profile = _context.UserProfile?.OrderBy(orderId => orderId.Id).LastOrDefault();

            return Task.FromResult(profile);
        }

        
    }
}
