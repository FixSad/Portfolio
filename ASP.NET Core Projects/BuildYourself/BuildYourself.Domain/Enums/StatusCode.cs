namespace BuildYourself.Domain.Enums
{
    public enum StatusCode
    {
        CategoryIsHadAlready = 1,
        ItemIsHadAlready = 2,
        NoIncompleteFiles = 3,
        FileWasNotFound = 4,

        Success = 200,
        InternalServerError = 500,
    }
}
