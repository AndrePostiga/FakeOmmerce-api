namespace FakeOmmerce.Errors
{
    using System.Net;

    public class CannotValidateException : HttpError
    { 
        public CannotValidateException(string parameter) : base ($@"Cannot validate {parameter}", HttpStatusCode.BadRequest)
        {
            
        }
    }
}