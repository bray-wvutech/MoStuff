namespace Domain.Constants;
public static class AuthConstants
{
    // this is used for secure api

    // and your secret key should not of course be stored in a constant
    // for anything other than testing purposes
    public const string JWT_SECRET_KEY = "SuperSecretAuthKeyThatMustBe256Bits";
}
