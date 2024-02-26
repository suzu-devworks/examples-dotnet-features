using System.Text.Json.Serialization;
using Examples.Features.CS120.PrimaryConstructors.Fixtures;
using Examples.Fluency;

namespace Examples.Features.CS120.PrimaryConstructors;

/// <summary>
/// Tests for Primary constructors in C# 12.0.
/// </summary>
public partial class PrimaryConstructorsTests
{
    [Fact]
    public void BasicUsage()
    {
        // use primary constructor
        var actual1 = new Distance(10.1, 20.2);
        using (new AssertionScope())
        {
            actual1.Magnitude.Should().Be(22.584286572747875);
            actual1.Direction.Should().Be(1.1071487177940904);
        }

        // use default constructor (NOT DEFINED.)
        var actual2 = new Distance();
        {
            actual2.Magnitude.Should().Be(0.0);
            actual2.Direction.Should().Be(0.0);
        }

        var actual3 = new Distance();
        actual3.Invoking(x => x.Translate(3.4, 5.6))
                   .Should().Throw<NotSupportedException>()
                   .WithMessage("CS9114");

        return;
    }

    [Fact]
    public void WhenUsingMutableState()
    {
        // use primary constructor
        var actual1 = new MutableDistance(10.1, 20.2);
        using (new AssertionScope())
        {
            actual1.Magnitude.Should().Be(22.584286572747875);
            actual1.Direction.Should().Be(1.1071487177940904);
        }

        // use default constructor.
        var actual2 = new MutableDistance();
        using (new AssertionScope())
        {
            actual2.Magnitude.Should().Be(0.0);
            actual2.Direction.Should().Be(0.0);
        }

        actual2.Translate(3.4, 5.6);

        using (new AssertionScope())
        {
            actual2.Magnitude.Should().Be(6.55133574166368);
            actual2.Direction.Should().Be(1.025141272267905);
        }

        return;
    }

    [Fact]
    public void UnderstandDifferenceBetweenClassesAndRecords()
    {
        // use primary constructor
        var actual1 = new RecordDistance(10.1, 20.2);
        using (new AssertionScope())
        {
            actual1.Magnitude.Should().Be(22.584286572747875);
            actual1.Direction.Should().Be(1.1071487177940904);

            // properties are generated.
            actual1.dx.Should().Be(10.1);
            actual1.dy.Should().Be(20.2);

            // Equals generated.
            actual1.Should().Be(new RecordDistance(10.1, 20.2));
        }

        // use default constructor.
        var actual2 = new RecordDistance();
        using (new AssertionScope())
        {
            actual2.Magnitude.Should().Be(0.0);
            actual2.Direction.Should().Be(0.0);
        }

        actual2.Translate(3.4, 5.6);

        using (new AssertionScope())
        {
            actual2.Magnitude.Should().Be(6.55133574166368);
            actual2.Direction.Should().Be(1.025141272267905);

            actual2.dx.Should().Be(3.4);
            actual2.dy.Should().Be(5.6);

            actual2.Should().Be(new RecordDistance(3.4, 5.6));
        }

        return;

    }

    [Fact]
    public void UseDependencyInjections()
    {
        IService service = Mock.Of<IService>();
        var distance = new Distance(10.1, 20.2);
        Mock.Get(service).Setup(s => s.GetDistance()).Returns(distance);

        // Inject service.
        var target = new ExampleController(service);
        var actual = target.Get();

        using (new AssertionScope())
        {
            actual.Distance.Magnitude.Should().Be(22.584286572747875);
            actual.Distance.Direction.Should().Be(1.1071487177940904);
        }

        Mock.Get(service).VerifyAll();
    }

    [Fact]
    public void InitializesTheBaseClass()
    {
        var accounts = new BankAccount[] {
            new CheckingAccount("9999990001", "Alice"),
            new CheckingAccount("9999990002", "Bob", overdraftLimit: 100_000m),
            new LineOfCreditAccount("9999990003", "Charlie", creditLimit: 200_000m),
            new SavingsAccount("9999990004", "Dave", interestRate: 0.062m),
        };

        accounts.OfType<CheckingAccount>().ForEach(x => x.Deposit(1000m));
        accounts.OfType<LineOfCreditAccount>().ForEach(x => x.Withdrawal(500m));
        accounts.OfType<SavingsAccount>().ForEach(x =>
        {
            x.Deposit(800m);
            x.ApplyInterest();
        });

        var tuples = accounts.Select(x => (x.AccountID, x.Owner));

        tuples.Should().NotBeEmpty()
            .And.HaveCount(4)
            .And.Equal(
                ("9999990001", "Alice"),
                ("9999990002", "Bob"),
                ("9999990003", "Charlie"),
                ("9999990004", "Dave"))
            ;

        accounts.Should().SatisfyRespectively(
            first => first.Should().BeOfType<CheckingAccount>()
                    .Which.CurrentBalance.Should().Be(1000m),
            second => second.Should().BeOfType<CheckingAccount>()
                    .Which.CurrentBalance.Should().Be(1000m),
            third => third.Should().BeOfType<LineOfCreditAccount>()
                    .Which.CurrentBalance.Should().Be(-500m),
            fourth => fourth.Should().BeOfType<SavingsAccount>()
                    .Which.CurrentBalance.Should().Be(849.600M)
            );
    }

    [Fact]
    public void UseAttributesTargetingPrimaryConstructors()
    {
        var type = typeof(Rec);

        var ctorAttrs = type.GetConstructors().SelectMany(ctor => ctor.GetCustomAttributes(inherit: false))
                .ToList();

        var propertyXAttrs = type.GetProperty(nameof(Rec.X))?.GetCustomAttributes(inherit: false)
                .ToList();

        var fieldXAttrs = type.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Where(x => x.Name.Contains('X'))
                .SelectMany(x => x.GetCustomAttributes(inherit: false))
                .ToList();

        var propertyYAttrs = type.GetProperty(nameof(Rec.Y))?.GetCustomAttributes(inherit: false)
                .ToList();

        var fieldYAttrs = type.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Where(x => x.Name.Contains('Y'))
                .SelectMany(x => x.GetCustomAttributes(inherit: false))
                .ToList();

        ctorAttrs.Should().ContainItemsAssignableTo<JsonConstructorAttribute>();
        propertyXAttrs.Should().ContainItemsAssignableTo<FooAttribute>();
        fieldXAttrs.Should().NotContainItemsAssignableTo<FooAttribute>();
        propertyYAttrs.Should().NotContainItemsAssignableTo<NonSerializedAttribute>();
        fieldYAttrs.Should().ContainItemsAssignableTo<NonSerializedAttribute>();

        return;
    }

    [Fact]
    public void AvoidDoubleStorage()
    {
        var person = new Person("original name");

        person.Name = "new name";

        person.Name.Should().Be("new name");
        person.ToString().Should().Be("original name"); // storage remains.

        return;
    }

    [Fact]
    public void AvoidRewritingCaptures()
    {
        ILogger logger = Mock.Of<ILogger>();

        var target = new LoggingController(logger);

        target.LegalAction();

        target.Invoking(x => x.IllegalAction())
            .Should().Throw<ApplicationException>()
            .WithMessage("CS9124");

        Mock.Get(logger).Verify(x => x.LogInformation(It.IsAny<string>(), It.IsAny<string[]>()), times: Times.Once);
        Mock.Get(logger).VerifyAll();
    }

}

