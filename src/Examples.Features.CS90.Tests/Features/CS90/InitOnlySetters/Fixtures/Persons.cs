namespace Examples.Features.CS90.InitOnlySetters.Fixtures
{
    /// <summary>
    /// Define standard private-setter property.
    /// </summary>
    public class PersonOfPrivateSetter
    {
        public string? Id { get; private set; }
        public string? Name { get; private set; }
    }

    /// <summary>
    /// Define Standard internal-setter property.
    /// </summary>
    public class PersonOfInternalSetter
    {
        public string? Id { get; internal set; }
        public string? Name { get; internal set; }
    }

    /// <summary>
    /// Define Standard init-only property.
    /// </summary>
    public class PersonOfInitSetter
    {
        public string? Id { get; init; }
        public string? Name { get; init; }
    }
}
