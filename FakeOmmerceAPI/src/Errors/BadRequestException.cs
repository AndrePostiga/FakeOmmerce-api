namespace FakeOmmerce.Errors
{
    using System.Net;

    public class BadRequestException : HttpError
    { 
        public BadRequestException(string parameter) : base ($@"BadRequest {parameter} is not valid", HttpStatusCode.BadRequest)
        {
            
        }
    }
}