// Ignore Spelling: Json

using DotNetExtras.Security.Json;

namespace SecurityTests;
public partial class MaskJsonConverterTests
{
    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("password", "********")]
    [InlineData("password", "******rd", '*', 0, 2)]
    [InlineData("password", "pa******", '*', 2, 0)]
    [InlineData("password", "pa****rd", '*', 2, 2)]
    [InlineData("password", "########", '#')]
    [InlineData("password", "pas$$$$$", '$', 3)]
    [InlineData("password", "@@@@@ord", '@', 0, 3)]
    [InlineData("password", "password", '@', 0, 20)]
    [InlineData("password", "password", '@', 20, 0)]
    [InlineData("password", "password", '@', 20, 20)]
    public void CharMask_Write
    (
        string? input,
        string? expected,
        char maskChar = '*',
        int unmaskedCharsStart = 0,
        int unmaskedCharsEnd = 0
    )
    {
        CharMaskJsonConverter converter = new(maskChar, unmaskedCharsStart, unmaskedCharsEnd);

#pragma warning disable CS8604 // Possible null reference argument.
        string? result = SerializeValue(converter, input);
#pragma warning restore CS8604 // Possible null reference argument.

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("null", null)]
    [InlineData("\"\"", "")]
    [InlineData("\"abc\"", "abc")]
    public void CharMask_Read
    (
        string? input,
        string? expected
    )
    {
        CharMaskJsonConverter converter = new();

#pragma warning disable CS8604 // Possible null reference argument.
        string? result = DeserializeValue(converter, input);
#pragma warning restore CS8604 // Possible null reference argument.

        Assert.Equal(expected, result);
    }
}
