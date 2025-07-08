using Todo.Core.Requests.Account;
using Todo.Core.Responses;

namespace Todo.Core.Handlers;
//Same as chore handler but for the User, managing login and registration
public interface IAccountHandler
{
    Task<Response<string>> LoginAsync(LoginRequest request);
    Task<Response<string>> RegisterAsync(RegisterRequest request);
    Task LogoutAsync();
}