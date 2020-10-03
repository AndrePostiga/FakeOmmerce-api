namespace FakeOmmerce.Errors
{   
    using System.Net;

    class ConflictException : HttpError
    {
        public ConflictException(string productName) : base ($@"Conflict error {productName} is already on database", HttpStatusCode.Conflict)
        {
            
        }
    }
}