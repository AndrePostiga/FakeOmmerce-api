namespace FakeOmmerce.Errors
{
    using System;

    public class BadRequestException : Exception
    { 
        public BadRequestException(string parameter) : base ($@"BadRequest {parameter} is not valid")
        {
            
        }
    }
}