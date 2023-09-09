namespace Examples.Features.CS9.InitOnlySetters
{
    /// <summary>
    /// Define standard private-setter property.
    /// </summary>
    public class PrivateSetterPerson
    {
        public string? Id { get; private set; }
        public string? Name { get; private set; }
    }

    /// <summary>
    /// Define Standard internal-setter property.
    /// </summary>
    public class InternalSetterPerson
    {
        public string? Id { get; internal set; }
        public string? Name { get; internal set; }
    }

    /// <summary>
    /// Define Standard init-only property.
    /// </summary>
    public class InitOnlyPerson
    {
        public string? Id { get; init; }
        public string? Name { get; init; }
    }
}
