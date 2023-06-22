using api.custom.system.Models;
using api.custom.system.Repository.Dto;
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
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IWebHostEnvironment environment, DatabaseContext context,IMapper mapper, IConfiguration configuration)
        {
            _environment = environment;
            _context = context;
            _mapper = mapper;
            _configuration= configuration;

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
 
            string directory = "images";
            var guid = Guid.NewGuid(); 
            string path = $"{_environment.WebRootPath}//{directory}//{guid}";
            var fileName = profileData?.File?.FileName;
            string file = Path.Combine($"{path}//{fileName}");

            User? user = GetUserById(profileData.Id);
            
            if (!string.IsNullOrEmpty(user.ImageProfile))
            {
                string imageProfile = $"{_environment.WebRootPath}/{user.ImageProfile}";
                File.Delete(imageProfile);
            }

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

            var profile = CreateUserProfile(user.UserName).Result;
            user.UserProfile = profile;

            _context.Add(user);
            _context.SaveChanges();

            return user;
        }

        public UserProfileResponseDto UpdateUserProfile(UserProfileRequestDto userProfileDto)
        {
            var userProfile = _mapper.Map<UserProfile>(userProfileDto);

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
            var userAddressResponse = _mapper.Map<UserProfileResponseDto>(user?.UserProfile);

            return userAddressResponse;
        }

        public UserProfile GetUserProfile(int userAdressId)
        {
            UserProfile userAdress = _context.UserProfile.FirstOrDefault(addressId => addressId.Id == userAdressId);
            return userAdress;
        }

        private Task<UserProfile?> CreateUserProfile(string name)
        {
            UserProfile userProfile = new();
            userProfile.UserName= name;
            _context.Add(userProfile);
            _context.SaveChanges();

            UserProfile? profile = _context.UserProfile?.OrderBy(orderId => orderId.Id).LastOrDefault();

            return Task.FromResult(profile);
        }

        
    }
}
