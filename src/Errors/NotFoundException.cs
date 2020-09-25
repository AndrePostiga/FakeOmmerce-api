namespace FakeOmmerce.Errors
{
    using System;

    public class NotFoundException : Exception
    { 
        public NotFoundException(string id) : base ($@"Cannot found {id} on database")
        {
            
        }
    }
}