namespace SharedKernel.Primitives;

/// <summary>
/// Immutable type with no identity; two instances are equal when their components are equal.
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public bool Equals(ValueObject? other) => other is not null && ValuesAreEqual(other);

    public override bool Equals(object? obj) => obj is ValueObject valueObject && ValuesAreEqual(valueObject);

    public override int GetHashCode()
    {
        var hashCode = default(HashCode);
        foreach (var component in GetEqualityComponents())
        {
            hashCode.Add(component);
        }

        return hashCode.ToHashCode();
    }

    public static bool operator ==(ValueObject? left, ValueObject? right) => Equals(left, right);

    public static bool operator !=(ValueObject? left, ValueObject? right) => !Equals(left, right);

    private bool ValuesAreEqual(ValueObject other) =>
        GetType() == other.GetType() &&
        GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
}
