namespace YourNotes.Domain.Interfaces.UseCases
{
    public interface IDeleteTopicUseCase
    {
        public Task Execute(Guid id);
    }
}
