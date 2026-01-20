using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Examples.Features.CSharp73.Tests.AttachAttributesToBackingFields
{
    /// <summary>
    /// Tests for Attach attributes to the backing field of auto-implemented properties in C# 7.3.
    /// </summary>
    public class AttachAttributesToBackingFieldsTests
    {
        [Fact]
        public void When_AttachingAttributesToBackingFields_Then_AttributesAreRecognized()
        {
            // It appears that `NonSerializedAttribute` is only used in `SoapFormatter`(system.runtime.serialization.formatters.soap.dll).
            // `BinaryFormatter` is deprecated.

            // C# 7.2
            {
                var fields = typeof(OldFigure).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(x => x.GetCustomAttributes(inherit: false)
                                    .Any(a => a is NonSerializedAttribute));

                Assert.Equal(2, fields.Count());
            }

            // C# 7.3 or later
            {
                var fields = typeof(NewFigure).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(x => x.GetCustomAttributes(inherit: false)
                                    .Any(a => a is NonSerializedAttribute));

                Assert.Equal(2, fields.Count());
            }
        }

        public class OldFigure
        {
            public int Id { get; set; }

            // error CS0592: Attribute 'NonSerialized' is not valid on this declaration type. It is only valid on 'field' declarations.
            //[NonSerialized]
            //public double X { get; set; }

            public double X { get => _x; set => _x = value; }
            [NonSerialized]
            private double _x;

            // error CS0592: Attribute 'NonSerialized' is not valid on this declaration type. It is only valid on 'field' declarations.
            //[NonSerialized]
            //public double Y { get; set; }

            public double Y { get => _y; set => _y = value; }
            [NonSerialized]
            private double _y;
        }

        public class NewFigure
        {
            public int Id { get; set; }

            // C# 7.2 : CS8371: Field-targeted attributes on auto-properties are not supported in language version 7.2. Please use language version 7.3 or greater.
            [field: NonSerialized]
            public double X { get; set; }

            // C# 7.2 : CS8371: Field-targeted attributes on auto-properties are not supported in language version 7.2. Please use language version 7.3 or greater.
            [field: NonSerialized]
            public double Y { get; set; }
        }


    }
}
