namespace BikeRental.Tech;

// reference: https://github.com/vkhorikov/CSharpFunctionalExtensions
public abstract class SingleValueObject<T> : ValueObject
{
    public T Value { get; }

    protected SingleValueObject(T value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value?.ToString();
    }

    public static implicit operator T(SingleValueObject<T> valueObject)
    {
        return valueObject == null ? default : valueObject.Value;
    }
}