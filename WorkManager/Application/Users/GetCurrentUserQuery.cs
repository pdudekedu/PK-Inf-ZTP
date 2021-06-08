using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WorkManager.Infrastructure.Authorization;
using WorkManager.Persistence.Entities;

namespace WorkManager.Application.Users
{
    public class GetCurrentUserQuery : IRequest<User>
    {

    }

    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, User>
    {
        private readonly IUserContext _userContext;

        public GetCurrentUserQueryHandler(IUserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<User> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            return _userContext.User;
        }
    }
}
