namespace FakeOmmerce.Errors
{
    using System.Net;

    public class NotFoundException : HttpError
    { 
        public NotFoundException(string id) : base ($@"Cannot found {id} on database", HttpStatusCode.NotFound)
        {
            
        }
    }
}