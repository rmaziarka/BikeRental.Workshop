namespace BikeRental.Tech;

// reference: https://github.com/vkhorikov/CSharpFunctionalExtensions
public abstract class Entity<TId>
{
    public virtual TId Id { get; protected set; }
    
    private List<object> _events = new List<object>();
    

    public IReadOnlyCollection<object> Events => _events?.AsReadOnly();

    protected Entity()
    {
    }

    protected Entity(TId id)
    {
        Id = id;
    }

    protected void AddEvent(object @event)
    {
        _events.Add(@event);
    }
    
    public void ClearEvents()
    {
        _events.Clear();
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Entity<TId> other))
            return false;

        if (ReferenceEquals(this, other))
            return true;
            
        if (ValueObject.GetUnproxiedType(this) != ValueObject.GetUnproxiedType(other))
            return false;

        if (IsTransient() || other.IsTransient())
            return false;

        return Id.Equals(other.Id);
    }

    private bool IsTransient()
    {
        return Id is null || Id.Equals(default(TId));
    }

    public static bool operator ==(Entity<TId> a, Entity<TId> b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity<TId> a, Entity<TId> b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (ValueObject.GetUnproxiedType(this).ToString() + Id).GetHashCode();
    }
}