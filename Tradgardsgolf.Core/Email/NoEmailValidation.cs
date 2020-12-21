namespace Tradgardsgolf.Core.Email
{
    public class NoEmailValidation : IEmailValidator
    {
        public bool IsValid(string input)
        {
            return true;
        }
    }
}
