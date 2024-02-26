using System;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS72.RefStructTypes
{
    /// <summary>
    /// Tests for Declare <c>ref struct</c> types in for C# 7.2.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/builtin-types/ref-struct"/>
    public class RefStructTypesTests
    {
        [Fact]
        public void BasicUsage()
        {
            // C# 7.1 :error CS8302: Feature 'ref structs' is not available in C# 7.1. Please use language version 7.2 or greater.
            Span<int> local = stackalloc int[1];

            // normal struct.
            {
                var @struct = new NormalStruct(0, local);

                ModifyStruct(ref @struct);

                @struct.Id.Is(1);

                void ModifyStruct(ref NormalStruct param)
                {
                    // C# 7.1 :error CS8302: Feature 'ref structs' is not available in C# 7.1. Please use language version 7.2 or greater.
                    Span<int> newStack = stackalloc int[2];
                    param.Modify(1, newStack);      // OK.
                }
            }

            // ref struct.
            {
                var @struct = new RefStruct(0, local);

                ModifyStruct(ref @struct);

                @struct.Id.Is(0);

                void ModifyStruct(ref RefStruct param)
                {
                    // C# 7.1 :error CS8302: Feature 'ref structs' is not available in C# 7.1. Please use language version 7.2 or greater.
                    Span<int> newStack = stackalloc int[2];
                    // error CS8350: This combination of arguments to 'xxxx' is disallowed because it may expose variables referenced by parameter 'span' outside of their declaration scope
                    //param.Modify(2, newStack);
                    _ = newStack;
                }
            }

            // read only struct.
            {
                var @struct = new ReadonlyRefStruct(0, local);

                ModifyStruct(ref @struct);

                @struct.Id.Is(0);

                void ModifyStruct(ref ReadonlyRefStruct param)
                {
                    // C# 7.1 :error CS8302: Feature 'ref structs' is not available in C# 7.1. Please use language version 7.2 or greater.
                    Span<int> newStack = stackalloc int[2];
                    param.Modify(3, newStack);
                }
            }

            return;
        }


        private struct NormalStruct
        {
            public NormalStruct(int id, Span<int> span)
            {
                Id = id;
                _ = span;
            }

            public int Id { get; }

            // error CS8345: Field or auto-implemented property cannot be of type 'Span<int>' unless it is an instance member of a ref struct.
            //public Span<int> Span { get; }

            public void Modify(int id, Span<int> span)
            {
                // update readonly fields.
                this = new NormalStruct(id, span);
            }
        }

        // C# 7.1 :error CS8302: Feature 'ref structs' is not available in C# 7.1. Please use language version 7.2 or greater.
        private ref struct RefStruct
        {
            public RefStruct(int id, Span<int> span)
            {
                Id = id;
                Span = span;
            }

            public int Id { get; }

            public Span<int> Span { get; }

            public void Modify(int id, Span<int> span)
            {
                // update readonly fields !!
                this = new RefStruct(id, span);
            }
        }

        // C# 7.1 : error CS8302: Feature 'readonly structs' is not available in C# 7.1. Please use language version 7.2 or greater.
        private readonly ref struct ReadonlyRefStruct
        {
            public ReadonlyRefStruct(int id, Span<int> span)
            {
                Id = id;
                Span = span;
            }

            public int Id { get; }

            public Span<int> Span { get; }

            public void Modify(int id, Span<int> span)
            {
                // not update.
                // error CS1604: Cannot assign to 'this' because it is read-only
                //this = new ReadonlyRefStruct(id, span);
                _ = id;
                _ = span;
            }
        }

    }
}
