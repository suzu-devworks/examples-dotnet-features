#if !NET11_0_OR_GREATER

namespace System.Runtime.CompilerServices;

// In C# 15 preview 5, the runtime doesn't yet ship
// `System.Runtime.CompilerServices.ClosedAttribute`. Until it does,
// every project that uses the closed modifier must declare the attribute itself:

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class ClosedAttribute : Attribute { }

#endif
