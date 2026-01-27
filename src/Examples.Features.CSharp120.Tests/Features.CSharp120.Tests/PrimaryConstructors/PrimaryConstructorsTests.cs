using System.Text.Json.Serialization;
using Examples.Features.CSharp120.Tests.PrimaryConstructors.Fixtures;

namespace Examples.Features.CSharp120.Tests.PrimaryConstructors;

/// <summary>
/// Tests for Primary constructors in C# 12.0.
/// </summary>
public partial class PrimaryConstructorsTests
{
    [Fact]
    public void When_ConstructingDistance_Then_ComputesMagnitudeAndDirection()
    {
        // use primary constructor
        var actual1 = new Distance(10.1, 20.2);
        Assert.Equal(22.584286572747875, actual1.Magnitude);
        Assert.Equal(1.1071487177940904, actual1.Direction);

        // use default constructor (NOT DEFINED.)
        var actual2 = new Distance();
        Assert.Equal(0.0, actual2.Magnitude);
        Assert.Equal(0.0, actual2.Direction);

        var actual3 = new Distance();
        Assert.Throws<NotSupportedException>(() => actual3.Translate(3.4, 5.6));
    }

    [Fact]
    public void When_UsingMutableDistance_Then_UpdatesMagnitude()
    {
        // use primary constructor
        var actual1 = new MutableDistance(10.1, 20.2);
        Assert.Equal(22.584286572747875, actual1.Magnitude);
        Assert.Equal(1.1071487177940904, actual1.Direction);

        // use default constructor.
        var actual2 = new MutableDistance();
        Assert.Equal(0.0, actual2.Magnitude);
        Assert.Equal(0.0, actual2.Direction);

        actual2.Translate(3.4, 5.6);

        Assert.Equal(6.55133574166368, actual2.Magnitude);
        Assert.Equal(1.025141272267905, actual2.Direction);
    }

    [Fact]
    public void When_ComparingClassAndRecord_Then_PropertiesAndEqualityMatch()
    {
        // use primary constructor
        var actual1 = new RecordDistance(10.1, 20.2);
        Assert.Equal(22.584286572747875, actual1.Magnitude);
        Assert.Equal(1.1071487177940904, actual1.Direction);

        // properties are generated.
        Assert.Equal(10.1, actual1.Dx);
        Assert.Equal(20.2, actual1.Dy);

        // Equals generated.
        Assert.Equal(new RecordDistance(10.1, 20.2), actual1);

        // use default constructor.
        var actual2 = new RecordDistance();
        Assert.Equal(0.0, actual2.Magnitude);
        Assert.Equal(0.0, actual2.Direction);

        actual2.Translate(3.4, 5.6);

        Assert.Equal(6.55133574166368, actual2.Magnitude);
        Assert.Equal(1.025141272267905, actual2.Direction);

        Assert.Equal(3.4, actual2.Dx);
        Assert.Equal(5.6, actual2.Dy);

        Assert.Equal(new RecordDistance(3.4, 5.6), actual2);
    }

    [Fact]
    public void When_InjectingService_Then_ReturnsCalculatedDistance()
    {
        IService service = Substitute.For<IService>();
        var distance = new Distance(10.1, 20.2);
        service.GetDistance().Returns(distance);

        // Inject service.
        var target = new ExampleController(service);
        var actual = target.Get();

        Assert.Equal(22.584286572747875, actual.Distance.Magnitude);
        Assert.Equal(1.1071487177940904, actual.Distance.Direction);

        service.Received(1).GetDistance();
    }

    [Fact]
    public void When_InitializingAccounts_Then_BalancesAndTuplesAreExpected()
    {
        var accounts = new BankAccount[] {
            new CheckingAccount("9999990001", "Alice"),
            new CheckingAccount("9999990002", "Bob", overdraftLimit: 100_000m),
            new LineOfCreditAccount("9999990003", "Charlie", creditLimit: 200_000m),
            new SavingsAccount("9999990004", "Dave", interestRate: 0.062m),
        };

        accounts.OfType<CheckingAccount>().ToList().ForEach(x => x.Deposit(1000m));
        accounts.OfType<LineOfCreditAccount>().ToList().ForEach(x => x.Withdrawal(500m));
        accounts.OfType<SavingsAccount>().ToList().ForEach(x =>
        {
            x.Deposit(800m);
            x.ApplyInterest();
        });

        var tuples = accounts.Select(x => (x.AccountID, x.Owner));

        Assert.NotEmpty(tuples);
        Assert.Equal(4, tuples.Count());
        Assert.Equal(
            new (string, string)[] {
                ("9999990001", "Alice"),
                ("9999990002", "Bob"),
                ("9999990003", "Charlie"),
                ("9999990004", "Dave")
            },
            tuples);

        var accountList = accounts.ToList();
        Assert.IsType<CheckingAccount>(accountList[0]);
        Assert.Equal(1000m, ((CheckingAccount)accountList[0]).CurrentBalance);

        Assert.IsType<CheckingAccount>(accountList[1]);
        Assert.Equal(1000m, ((CheckingAccount)accountList[1]).CurrentBalance);

        Assert.IsType<LineOfCreditAccount>(accountList[2]);
        Assert.Equal(-500m, ((LineOfCreditAccount)accountList[2]).CurrentBalance);

        Assert.IsType<SavingsAccount>(accountList[3]);
        Assert.Equal(849.600M, ((SavingsAccount)accountList[3]).CurrentBalance);
    }

    [Fact]
    public void When_InspectingPrimaryConstructorAttributes_Then_FindsExpectedAnnotations()
    {
        var type = typeof(Rec);

        var ctorAttrs = type.GetConstructors().SelectMany(ctor => ctor.GetCustomAttributes(inherit: false))
                .ToList();

        var propertyXAttrs = type.GetProperty(nameof(Rec.X))?.GetCustomAttributes(inherit: false)
                .ToList() ?? [];

        var fieldXAttrs = type.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Where(x => x.Name.Contains('X'))
                .SelectMany(x => x.GetCustomAttributes(inherit: false))
                .ToList();

        var propertyYAttrs = type.GetProperty(nameof(Rec.Y))?.GetCustomAttributes(inherit: false)
                .ToList() ?? [];

        var fieldYAttrs = type.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Where(x => x.Name.Contains('Y'))
                .SelectMany(x => x.GetCustomAttributes(inherit: false))
                .ToList();

        Assert.Contains(ctorAttrs, attr => attr is JsonConstructorAttribute);
        Assert.Contains(propertyXAttrs, attr => attr is FooAttribute);
        Assert.DoesNotContain(fieldXAttrs, attr => attr is FooAttribute);
        Assert.DoesNotContain(propertyYAttrs, attr => attr is NonSerializedAttribute);
        Assert.Contains(fieldYAttrs, attr => attr is NonSerializedAttribute);
    }

    [Fact]
    public void When_OverwritingPersonName_Then_OriginalStorageRemains()
    {
        var person = new Person("original name");

        person.Name = "new name";

        Assert.Equal("new name", person.Name);
        Assert.Equal("original name", person.ToString()); // storage remains.
    }

    [Fact]
    public void When_LoggingIllegalAction_Then_ThrowsAndLogsOnce()
    {
        ILogger logger = Substitute.For<ILogger>();

        var target = new LoggingController(logger);

        target.LegalAction();

        Assert.Throws<ApplicationException>(() => target.IllegalAction());

        logger.Received(1).LogInformation(Arg.Any<string>(), Arg.Any<string[]>());
    }

}

