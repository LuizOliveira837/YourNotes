using YourNotes.Persistence.Autentication.Tokens.Access.Generator;

namespace CommonTestUtilities.Builders
{
    public static class JwtTokenGeneratorBuilder
    {
        private static string secretKey = "KVX2dfFqwjSfEO5u2lGhvIDMFBpxmJsmyS8cKdW+GbaXDRVT4CWhsZMnv8";
        private static int expirationTimeInMinutes = 30;
        public static JwtTokenGenerator Build()
        {

            return new JwtTokenGenerator(secretKey, expirationTimeInMinutes);
        }
    }
}
