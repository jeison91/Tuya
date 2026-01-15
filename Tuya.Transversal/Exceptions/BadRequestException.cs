namespace Tuya.Transversal
{
    public class BadRequestException : Exception
    {
        public BadRequestException() { }
        public BadRequestException(string Message) : base(Message) { }
        public BadRequestException(string Message, Exception inner) : base(Message, inner) { }
    }
}
