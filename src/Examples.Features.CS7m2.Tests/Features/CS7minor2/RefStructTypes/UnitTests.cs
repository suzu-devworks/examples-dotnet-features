using System;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS7.RefStructTypes
{
    /// <summary>
    /// Tests for C# 7.2, Declare ref struct types.
    /// </summary>
    /// <seealso href="https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/builtin-types/struct#ref-struct"/>
    /// <seealso href="https://ufcpp.net/study/csharp/resource/refstruct/"/>
    public class UnitTests
    {
        [Fact]
        public void WhenUsingStructs()
        {
            Span<int> local = stackalloc int[1];

            var struct1 = new NormalStruct(10, local);
            LocalToStrict(ref struct1);
            struct1.Id.Is(1);

            var struct2 = new RefStruct(20, local);
            LocalToRef(ref struct2);
            struct2.Id.Is(20);

            var struct3 = new ReadonlyRefStruct(30, local);
            LocalToReadonlyRef(ref struct3);
            struct3.Id.Is(30);

            return;
        }


        private static void LocalToStrict(ref NormalStruct strict)
        {
            Span<int> local = stackalloc int[1];
            strict.Modify(1, local);    //OK.
            strict.Id.Is(1);
        }

        private static void LocalToRef(ref RefStruct strict)
        {
            Span<int> local = stackalloc int[1];
            //strict.Modify(2, local);  //NG CS8350
            //strict.Id.Is(2);
        }

        private static void LocalToReadonlyRef(ref ReadonlyRefStruct strict)
        {
            Span<int> local = stackalloc int[1];
            strict.Modify(3, local);    //OK (no-update)
                                        //strict.Id.Is(3);
        }

        private struct NormalStruct
        {
            //private readonly Span<int> _span;   //NG CS8345

            public NormalStruct(int id, Span<int> span)
            {
                Id = id;
                _ = span;
            }

            public int Id { get; }
            public Span<int> Span => throw new NotSupportedException();

            public void Modify(int id, Span<int> span)
            {
                // update readonly fields.
                this = new NormalStruct(id, span);
            }
        }

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
                //this = new ReadonlyRefStruct(id, span); //NG CS1604
                _ = new ReadonlyRefStruct(id, span);
            }
        }

    }
}
