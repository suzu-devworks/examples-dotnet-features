using System;
using System.Linq;
using System.Reflection;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS73.AttachAttributesToBackingFields
{
    /// <summary>
    /// Tests for Attach attributes to the backing field of auto-implemented properties in C# 7.3.
    /// </summary>
    public class AttachAttributesToBackingFieldsTests
    {
        [Fact]
        public void BasicUsage()
        {
            // It appears that `NonSerializedAttribute` is only used in `SoapFormatter`(system.runtime.serialization.formatters.soap.dll).
            // `BinaryFormatter` is deprecated.

            // C# 7.2
            {
                var fields = typeof(OldFigure).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(x => x.GetCustomAttributes(inherit: false)
                                    .Any(a => a is NonSerializedAttribute));

                fields.Count().Is(2);
            }

            // C# 7.3 or later
            {
                var fields = typeof(NewFigure).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(x => x.GetCustomAttributes(inherit: false)
                                    .Any(a => a is NonSerializedAttribute));

                fields.Count().Is(2);
            }

            return;
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
