using YourNotes.Application.Services.Crypt;

namespace CommonTestUtilities.Builders
{
    public static class PasswordEncrypterBuilder
    {

        public static PasswordEncrypter Build()
        {

            return new PasswordEncrypter("ISSONAOEHUMTESTE");


        }
    }
}
