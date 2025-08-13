using Bogus.DataSets;

namespace CommonTestUtilities.Services
{
    public static class GeneratePasswordService
    {
        public static string GeneratePassword(this Internet internet)
        {
            var password = internet.Password(20);


            var passwordSplit = password.ToCharArray();

            passwordSplit[0] = Char.Parse(passwordSplit[0].ToString().ToUpper());
            passwordSplit[1] = Char.Parse(passwordSplit[1].ToString().ToLower());
            passwordSplit[3] = '1';
            passwordSplit[4] = '@';

            return string.Concat(passwordSplit);

        }
    }
}
