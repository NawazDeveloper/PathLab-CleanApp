using ClarmindsApp.DTOs;

namespace ClarmindsApp.Interfaces
{
    public interface IUserAuth
    {
        // user regiter 
        Task<RegistrationResponse> RegisterAsync(RegisterationRequest request, string roleName);

        // user login
        Task<string> LoginAsync(string email, string password);

        // get user profile by id
        Task<UserProfileDto> GetProfileByIdAsync(string userId);

        // get all user profiles (only Admin)
        Task<IEnumerable<UserProfileDto>> GetAllProfilesAsync();
    }
}
