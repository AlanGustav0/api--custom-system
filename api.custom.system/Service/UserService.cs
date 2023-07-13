using System.Text.RegularExpressions;
using api.custom.system.Models;
using api.custom.system.Repository.Dto;
using api.custom.system.Repository.Interfaces;
using api.custom.system.Service.Interfaces;
using api__custom_system.Models;
using api__custom_system.Repository;
using api__custom_system.Repository.Dto;
using AutoMapper;

namespace api.custom.system.Service
{
    public class UserService : IUserService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IWebHostEnvironment environment, DatabaseContext context,IMapper mapper, IUserRepository userRepository)
        {
            _environment = environment;
            _mapper = mapper;
            _userRepository = userRepository;

        }


        public async Task<User?> GetUserById(int id)
        {
            User? user = await _userRepository.GetUserById(id);
            if(user != null)
            {
                return user;
            }
            return null;

        }


        public async Task SaveImageProfile(ProfileData profileData)
        {

            User? user = await GetUserById(profileData.Id);

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

             await _userRepository.SaveImageProfile();


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

        public async Task<User> CreateUser(UserRequestDto userDto)
        {
            User user = _mapper.Map<User>(userDto);

            UserProfile? profile = CreateUserProfile(user.UserName).Result;
            user.UserName = userDto.NickName;
            user.UserProfile = profile;

            await _userRepository.CreateUser(user);

            return user;

            
        }

        public async Task<UserProfileResponseDto> UpdateUserProfile(UserProfileRequestDto userProfileDto)
        {
            UserProfile? userProfile = _mapper.Map<UserProfile>(userProfileDto);

            User? user = await GetUserById(userProfileDto.Id);
            
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

            await _userRepository.UpdateUserProfile();
            var userProfileResponse = _mapper.Map<UserProfileResponseDto>(user?.UserProfile);

            return userProfileResponse;
        }

        public async Task<UserProfile> GetUserProfile(int userProfileId)
        {
            UserProfile? userProfile = await _userRepository.GetUserProfile(userProfileId);

            return userProfile;
        }

        private async Task<UserProfile?> CreateUserProfile(string name)
        {
            UserProfile userProfile = new()
            {
                UserName = name
            };
            UserProfile? profile = await _userRepository.CreateUserProfile(userProfile);

            return profile;
        }

        
    }
}
