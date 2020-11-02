namespace CareerTrack.Common
{
    public interface IConfiguration
    {
        string DisplayGenericUserErrorMessage { get; }
        string DisplayObjectNotFoundErrorMessage { get; }
        string DisplayExistentUserExceptionMessage { get; }
        string DisplayPasswordsAreNotTheSameExceptionMessage { get; }
        string NoRolesAssignedExceptionMessage { get; }
        string UserEmailNotConfirmedExceptionMessage { get; }

        JWTConfiguration JWTConfiguration { get; }

        EmailServiceConfiguration EmailServiceConfiguration { get; }
    }
}
