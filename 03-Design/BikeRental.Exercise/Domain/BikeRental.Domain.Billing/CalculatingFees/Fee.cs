using BikeRental.Domain.Shared.Billing;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees;

public class FeeId:SingleValueObject<Guid>
{
    public FeeId(Guid value) : base(value)
    {
    }
}

public class Fee : Entity<FeeId>, IAggregateRoot
{
    public static Fee StartFee(FeeId feeId, ClientId clientId, FeeSourceType sourceType, FeeSourceId sourceId, DateTimeOffset applyDate)
    {
        return new Fee(feeId, clientId, sourceType, sourceId, applyDate);
    }

    public void ApplyFeesAndFinishCalculation(IEnumerable<FeeItem> feeItems, DateTimeOffset finishDate)
    {
        ApplyFees(feeItems);
        FinishCalculation(finishDate);
    }

    public void ApplyFees(IEnumerable<FeeItem> feeItems)
    {
        if (CannotBeModified) throw new DomainError();

        foreach (var feeItem in feeItems)
        {
            _feeItems.Add(feeItem);
            AddEvent(new FeeApplied(Id, _clientId, feeItem, feeItem.ApplyDate));
        }
    }

    public void FinishCalculation(DateTimeOffset finishDate)
    {
        if (IsEmpty) throw new DomainError();
        if (CannotBeModified) throw new DomainError();
        
        _finishDate = finishDate;
        AddEvent(new FeeCalculationFinished(Id, _clientId, finishDate));
    }

    public void Cancel(FeeCancellation feeCancellation)
    {
        if (CannotBeModified) throw new DomainError();
        
        _feeCancellation = feeCancellation;
        
        AddEvent(new FeeCancelled(Id, _clientId, feeCancellation));
    }

    private bool CannotBeModified => IsCancelled || IsFinished;
    private bool IsFinished => _finishDate.HasValue;
    private bool IsCancelled => _feeCancellation != null;

    private bool IsEmpty => !_feeItems.Any();
    
    private Fee(FeeId feeId, ClientId clientId, FeeSourceType sourceType, FeeSourceId sourceId,
        DateTimeOffset applyDate)
    {
        Id = feeId;
        SourceId = sourceId;
        _sourceType = sourceType;
        _startDate = applyDate;
        _clientId = clientId;
        _feeItems = new List<FeeItem>();
        
        AddEvent(new FeeStarted(Id, _clientId, SourceId, _sourceType, _startDate));
    }

    private List<FeeItem> _feeItems;
    public FeeSourceId SourceId;
    private FeeSourceType _sourceType;
    private DateTimeOffset _startDate;
    private ClientId _clientId;
    private DateTimeOffset? _finishDate;
    private FeeCancellation _feeCancellation;
}

public class FeeItem: ValueObject
{
    public FeeItem(Money price, string description,  DateTimeOffset applyDate)
    {
        Price = price;
        Description = description;
        ApplyDate = applyDate;
    }

    public Money Price { get;  }
    public string Description { get;  }
    public DateTimeOffset ApplyDate { get;  }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ApplyDate;
        yield return Description;
        yield return Price;
    }
}


public record FeeApplied(FeeId FeeId, ClientId ClientId, FeeItem FeeItem,
    DateTimeOffset ApplyDate);
public record FeeCalculationFinished(FeeId FeeId, ClientId ClientId, DateTimeOffset FinishDate);
public record FeeCancelled(FeeId FeeId, ClientId ClientId, FeeCancellation FeeCancellation);

public record FeeStarted(FeeId FeeId, ClientId ClientId, FeeSourceId FeeSourceId, FeeSourceType FeeSourceType,
    DateTimeOffset StartDate);


public class FeeCancellation: ValueObject
{
    public FeeCancellation(string description, DateTimeOffset cancellationDate)
    {
        Description = description;
        CancellationDate = cancellationDate;
    }
    public string Description { get;  }
    public DateTimeOffset CancellationDate { get;  }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Description;
        yield return CancellationDate;
    }
}


public enum FeeSourceType
{
    Rental, Reservation
}
