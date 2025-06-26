using Todo.Core.Models;
using Todo.Core.Requests.Chores;
using Todo.Core.Responses;

namespace Todo.Core.Handlers;

public interface IChoreHandler
{
    //the CRUD operations are set here to abstract only what the API needs to configure
    Task<Response<Chore?>> CreateAsync(CreateChoreRequest request);
    Task<Response<Chore?>> GetByIdAsync(GetChoreByIdRequest request);
    Task<Response<Chore?>> UpdateAsync(UpdateChoreRequest request);
    Task<Response<Chore?>> DeleteAsync(DeleteChoreRequest request);
    Task<PagedResponse<List<Chore>>> GetAllAsync(GetAllChoresRequest request);
}