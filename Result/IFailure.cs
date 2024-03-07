namespace Result;

public interface IFailure<TFailure>
{
    public TFailure Value { get; }

    public void Deconstruct(out TFailure value)
    {
        value = Value;
    }
}