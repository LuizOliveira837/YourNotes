using YourNotes.Persistence.Autentication.Tokens.Access.Generator;

namespace CommonTestUtilities.Builders
{
    public static class JwtTokenGeneratorBuilder
    {
        private static string secretKey = "KVX2dfFqwjSfEO5u2lGhvIDMFBpxmJsmyS8cKdW+GbaXDRVT4CWhsZMnv8DC0Eq1KGk/5rBaXGCybcwO5NfMk2/dgFI2BJPEopUYH9AQwts2AU59/EGixd4Y0hS3Qu9lL0Ck+/ExDj6Bbhm0POWXP3vrOLBukjRGfj1Nw+uK73CFRKMZ0Qj8zrsCFWNxWFNY";
        private static int expirationTimeInMinutes = 30;
        public static JwtTokenGenerator Build()
        {

            return new JwtTokenGenerator(secretKey, expirationTimeInMinutes);
        }
    }
}
