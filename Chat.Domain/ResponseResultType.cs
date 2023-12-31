namespace Chat.Domain;
public enum ResponseResultType
{
    Success,
    NotFound,
    AlreadyExists,
    NoChanges,
    ValidationError
}