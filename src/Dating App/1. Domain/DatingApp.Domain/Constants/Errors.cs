namespace DatingApp.Domain.Constants
{
    public class Errors
    {
        public const string AppUserExists = "The user with such username exists";
        public const string AppUserNotFound = "The user has not been found";
        public const string AppUserNotUpdated = "The user has not been updated";
        public const string AppUsersNotFound = "Users has not been found";
        public const string AppUserAddingError = "Error while adding user";
        public const string AppUserPasswordInvalid = "The password is invalid";
        public const string AppUserUsernameInvalid = "The username is invalid";
        public const string AppUserIncorrectCredentials = "Incorrect login or password";

        public const string PhotoAddingError = "Error while adding photo";
        public const string PhotoDoesNotExists = "The photo does not exists";
    }
}
