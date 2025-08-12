using YourNotes.Domain.Interfaces.Repositories;
using YourNotes.Domain.Interfaces.Services;
using YourNotes.Domain.Interfaces.UseCases;

namespace YourNotes.Application.User.DeleteUser
{
    public class DeleteUserUseCase(ILoggedUser loggedUser, IUnitOfWork uof) : IDeleteUserUseCase
    {
        private readonly ILoggedUser _loggedUser = loggedUser;
        private readonly IUnitOfWork _uof = uof;

        public async Task Execute()
        {
            var user = await _loggedUser.User();

            user.Active = false;

            await _uof.Commit();
        }
    }
}
