using Moq;
using YourNotes.Domain.Interfaces.Repositories;

namespace CommonTestUtilities.Builders
{
    public class UnitOfWorkBuilder
    {
        public Mock<IUnitOfWork> uof = new();
       
    }
}
