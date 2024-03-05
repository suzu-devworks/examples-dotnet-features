using Examples.Features.CS110.FileLocalTypes.Fixtures;

namespace Examples.Features.CS110.FileLocalTypes;

public class FileLocalTypesTests
{
    [Fact]
    public void BasicUsage()
    {
        // OK.
        _ = new MyInternalScopeClass();

        // OK.
        _ = new MyFileScopeClass();
        _ = new MyFileScopeClass.MyInnerPublicClass();

        // OK.
        _ = new OtherFileInternalScopeClasses();
        _ = new OtherFileUnspecifiedScopeClasses();

        // error CS0246: The type or namespace name 'OtherFileFileScopeClass' could not be found (are you missing a using directive or an assembly reference?)
        //_ = new OtherFileFileScopeClass();
        // error CS0246: The type or namespace name 'OtherFileFileScopeClass' could not be found (are you missing a using directive or an assembly reference?)
        // _ = new OtherFileFileScopeClass.OtherFileInnerPublicClass();

        return;
    }
}

internal class MyInternalScopeClass
{
    //error CS9054: File-local type 'MyInnerFileClass' must be defined in a top level type; 'MyInnerFileClass' is a nested type.
    // file class MyInnerFileClass
    // {
    // }
}

file class MyFileScopeClass
{
    public class MyInnerPublicClass
    {
    }

    //error CS9054: File-local type 'MyInnerFileClass' must be defined in a top level type; 'MyInnerFileClass' is a nested type.
    // file class MyInnerFileClass
    // {
    // }

}

