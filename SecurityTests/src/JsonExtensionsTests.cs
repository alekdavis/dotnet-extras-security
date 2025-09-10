// Ignore Spelling: Json

using DotNetExtras.Common.Json;
using DotNetExtras.Extended;
using DotNetExtras.Security.Json;
using SecurityLibTests.Models;

namespace SecurityTests;
public class JsonExtensionsTests
{
    [Theory]
    [InlineData(null, "sensitiveValue", null, "sensitiveValue", "sensitiveValue")]
    [InlineData(null, "sensitiveValue", "", "sensitiveValue", "sensitiveValue")]
    [InlineData(null, "sensitiveValue", "Password", null, "sensitiveValue")]
    [InlineData(null, "sensitiveValue", "Sponsor.Password", "sensitiveValue", null)]
    [InlineData(null, "sensitiveValue", "Password|Sponsor.Password", null, null)]
    [InlineData("***", "sensitiveValue", "Password", "***", "sensitiveValue")]
    [InlineData("redacted", "sensitiveValue", "Sponsor.Password", "sensitiveValue", "redacted")]
    [InlineData("######", "sensitiveValue", "Password|Sponsor.Password", "######", "######")]
    public void Object_ToJson_MaskString
    (
        string? mask,
        string  sensitiveValue,
        string? maskedPropertiesString = null,
        string? expectedPassword = null,
        string? expectedSponsorPassword = null
    )
    {
        User original;
        User? secure;
        string json;

        string[]? maskedProperties = maskedPropertiesString == null
            ? null
            : maskedPropertiesString == string.Empty
                ? []
                : maskedPropertiesString.ToArray<string>();

        original = new()
        {
            Id = "12345",
            Name = new()
            {
                GivenName = "Joe",
                Surname = "Doe"
            },
            Age = 31,

            PasswordExpirationDate = new (2031, 11, 30, 19, 15, 33),
            LastLoginDateOffset = new(new DateTime(2021, 10, 12, 20, 33, 41), new TimeSpan(3, 30, 0)),

            Sponsor = new()
            {
                Password = sensitiveValue
            },
            Password = sensitiveValue
        };

        json = original.ToJson(mask, maskedProperties);

        secure = json.FromJson<User>();

        Assert.Equal("Joe", secure?.Name?.GivenName);
        Assert.Equal("Doe", secure?.Name?.Surname);
        Assert.Equal(31, secure?.Age);
        Assert.Equal(expectedPassword, secure?.Password);
        Assert.Equal(expectedSponsorPassword, secure?.Sponsor?.Password);
    }

    [Theory]
    [InlineData('*', 0, 0, "sensitiveValue", null, "sensitiveValue", "sensitiveValue")]
    [InlineData('*', 0, 0, "sensitiveValue", "", "sensitiveValue", "sensitiveValue")]
    [InlineData('*', 0, 0, "sensitiveValue", "Password", "**************", "sensitiveValue")]
    [InlineData('#', 0, 0, "sensitiveValue", "Sponsor.Password", "sensitiveValue", "##############")]
    [InlineData('*', 2, 0, "sensitiveValue", "Password|Sponsor.Password", "se************", "se************")]
    [InlineData('*', 0, 2, "sensitiveValue", "Password|Sponsor.Password", "************ue", "************ue")]
    [InlineData('*', 3, 2, "sensitiveValue", "Password|Sponsor.Password", "sen*********ue", "sen*********ue")]
    public void Object_ToJson_MaskChar
    (
        char mask,
        int unmaskedCharsStart,
        int unmaskedCharsEnd,
        string  sensitiveValue,
        string? maskedPropertiesString = null,
        string? expectedPassword = null,
        string? expectedSponsorPassword = null
    )
    {
        User original;
        User? secure;
        string json;

        string[]? maskedProperties = maskedPropertiesString == null
            ? null
            : maskedPropertiesString == string.Empty
                ? []
                : maskedPropertiesString.ToArray<string>();

        original = new()
        {
            Id = "12345",
            Name = new()
            {
                GivenName = "Joe",
                Surname = "Doe"
            },
            Age = 31,

            PasswordExpirationDate = new (2031, 11, 30, 19, 15, 33),
            LastLoginDateOffset = new(new DateTime(2021, 10, 12, 20, 33, 41), new TimeSpan(3, 30, 0)),

            Sponsor = new()
            {
                Password = sensitiveValue
            },
            Password = sensitiveValue
        };

        json = unmaskedCharsEnd == 0
            ? original.ToJson(mask, unmaskedCharsStart, maskedProperties)
            : original.ToJson(mask, unmaskedCharsStart, unmaskedCharsEnd, maskedProperties);

        secure = json.FromJson<User>();

        Assert.Equal("Joe", secure?.Name?.GivenName);
        Assert.Equal("Doe", secure?.Name?.Surname);
        Assert.Equal(31, secure?.Age);
        Assert.Equal(expectedPassword, secure?.Password);
        Assert.Equal(expectedSponsorPassword, secure?.Sponsor?.Password);
    }
}
