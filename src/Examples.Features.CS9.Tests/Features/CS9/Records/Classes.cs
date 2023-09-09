using System;

namespace Examples.Features.CS9.Records
{
    /// <summary>
    /// Define Type-1 (Positional parameter provided in the record declaration).
    /// </summary>
    public record Type1Person(string FirstName, string LastName);

    /// <summary>
    /// Define Type-2 (standard init-only property).
    /// </summary>
    public record Type2Person
    {
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public DateTime? DateOfBirth { get; init; }
    };

}
