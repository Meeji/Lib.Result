namespace Result;

public interface ISuccess<TSuccess>
{
    public TSuccess Value { get; }

    public void Deconstruct(out TSuccess value)
    {
        value = Value;
    }
}