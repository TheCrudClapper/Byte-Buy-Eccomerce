namespace ByteBuy.Core.Domain.Exceptions;

public class DomainInvariantException : Exception
{
    public DomainInvariantException() { }

    public DomainInvariantException(string message) 
        : base(message) { }

    public DomainInvariantException(string message, Exception innerException) 
        : base(message, innerException) { }
}
