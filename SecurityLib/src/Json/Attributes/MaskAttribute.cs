using System.Text.Json.Serialization;

namespace DotNetExtras.Security.Json;
/// <summary>
/// Masks a sensitive string property value during an object-to-JSON-string conversion via
/// the <see href="https://learn.microsoft.com/en-us/dotnet/api/system.text.json">System.Text.Json</see> (STJ)
/// serialization.
/// </summary>
/// <remarks>
/// <para>
/// Depending on the constructor used, one of these mask option can be applied
/// to convert a string property value to:
/// </para>
/// <list type="bullet">
/// <item>
///     <para>A literal string or <c>null</c>.</para>
/// </item>
/// <item>
///     <para>A string masked with the specified character (with the optional leading and/or trailing characters left in plain text).</para>
/// </item>
/// <item>
///     <para>A secure SHA hash value (with an optional salt applied to the original string).</para>
/// </item>
/// </list>
/// </remarks>
/// <example>
/// <code language="csharp">
/// <![CDATA[
/// // Sets the value to null.
/// [Mask]
/// public string? Secret1 { get; set; }
/// 
/// // Sets the value to "*redacted*".
/// [Mask("*redacted*")]
/// public string? Secret2 { get; set; }
/// 
/// // Masks every character with '*'.
/// [Mask('*')]
/// public string? Secret3 { get; set; }
/// 
/// // Masks every character with '*'; 
/// // leaves the leading 2 characters unmasked.
/// [Mask('*', 2)]
/// public string? Secret4 { get; set; }
/// 
/// // Masks every character with '*'; 
/// // leaves the leading 2 and trailing 3 characters unmasked.
/// [Mask('*', 2, 3)]
/// public string? Secret5 { get; set; }
/// 
/// // Sets the value to its SHA256 hash.
/// [Mask(HashType.SHA256)]
/// public string? Secret6 { get; set; }
/// 
/// // Masks the value with its SHA256 hash.
/// [Mask(HashType.SHA256)]
/// public string? Secret1 { get; set; }
/// 
/// // Adds a random 4-character salt to the original string and hashes the result with SHA-512.
/// [Mask(HashType.512, 4)]
/// public string? Secret2 { get; set; }
/// 
/// // Adds a random 4-character salt to the original string and hashes the result with SHA-256
/// // (includes the salt value as a prfix of the hash string).
/// [Mask(HashType.256, 4, true)]
/// public string? Secret3 { get; set; } 
/// ]]>
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Property)]
public class MaskAttribute: JsonConverterAttribute
{
    #region Private properties
    // Character mask settings.
    private readonly char? _maskChar;
    private readonly int? _unmaskedCharsStart;
    private readonly int? _unmaskedCharsEnd;

    // Literal mask settings.
    private readonly string? _maskLiteral;

    // Hash mask settings.
    private readonly HashType? _hashType;
    private readonly int? _saltLength;
    private readonly bool? _saveSalt;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="MaskAttribute"/> class
    /// for applying a literal string mask to a string property value during JSON serialization
    /// via <see href="https://learn.microsoft.com/en-us/dotnet/api/system.text.json">System.Text.Json</see> (STJ).
    /// </summary>
    /// <param name="maskLiteral">
    /// Literal string value that will replace the string property value (can be `null`).
    /// </param>
    /// <example>
    /// <code language="csharp">
    /// <![CDATA[
    /// // Sets the value to null.
    /// [Mask]
    /// public string? Secret1 { get; set; }
    /// 
    /// // Sets the value to "*redacted*".
    /// [Mask("*redacted*")]
    /// public string? Secret2 { get; set; }
    /// ]]>
    /// </code>
    /// </example>
    public MaskAttribute
    (
        string? maskLiteral = null
    )
    {
        _maskLiteral        = maskLiteral;
        _maskChar           = null;
        _unmaskedCharsStart = null;
        _unmaskedCharsEnd   = null;
        _hashType           = null;
        _saltLength         = null;
        _saveSalt           = null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MaskAttribute"/> class
    /// for applying a character mask to a string property value during JSON serialization
    /// via <see href="https://learn.microsoft.com/en-us/dotnet/api/system.text.json">System.Text.Json</see> (STJ).
    /// </summary>
    /// <param name="maskChar">
    /// Mask character.
    /// </param>
    /// <param name="unmaskedCharsStart">
    /// Number characters to be left unmasked at the start of the string.
    /// </param>
    /// <param name="unmaskedCharsEnd">
    /// Number characters to be left unmasked at the end of the string.
    /// </param>
    /// <example>
    /// <code language="csharp">
    /// <![CDATA[
    /// // Masks every character with '*'.
    /// [Mask('*')]
    /// public string? Secret2 { get; set; }
    /// 
    /// // Masks every character with '*'
    /// // leaving the leading 2 characters unmasked.
    /// [Mask('*', 2)]
    /// public string? Secret2 { get; set; }
    /// 
    /// // Masks every character with '*'
    /// // leaving the leading 2 and trailing 3 characters unmasked.
    /// [Mask('*', 2, 3)]
    /// public string? Secret2 { get; set; }
    /// ]]>
    /// </code>
    /// </example>
    public MaskAttribute
    (
        char maskChar,
        int unmaskedCharsStart = 0,
        int unmaskedCharsEnd = 0
    )
    {
        _maskChar           = maskChar;
        _unmaskedCharsStart = unmaskedCharsStart < 0 ? 0 : unmaskedCharsStart;
        _unmaskedCharsEnd   = unmaskedCharsEnd   < 0 ? 0 : unmaskedCharsEnd;
        _maskLiteral        = null;
        _hashType           = null;
        _saltLength         = null;
        _saveSalt           = null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MaskAttribute"/> class
    /// for applying an SHA hash mask to a string property value during JSON serialization
    /// via <see href="https://learn.microsoft.com/en-us/dotnet/api/system.text.json">System.Text.Json</see> (STJ).
    /// </summary>
    /// <param name="hashType">
    /// Hashing algorithm.
    /// </param>
    /// <param name="saltLength">
    /// Number of characters of the optional random salt to be generated and added before plain text.
    /// </param>
    /// <param name="saveSalt">
    /// If <c>true</c>, the salt will be added to the beginning of the hash string.
    /// </param>
    /// <example>
    /// <code language="csharp">
    /// <![CDATA[
    /// // Masks the value with its SHA256 hash.
    /// [Mask(HashType.SHA256)]
    /// public string? Secret1 { get; set; }
    /// 
    /// // Adds a random 4-character salt to the original string and hashes the result with SHA-512.
    /// [Mask(HashType.512, 4)]
    /// public string? Secret2 { get; set; }
    /// 
    /// // Adds a random 4-character salt to the original string and hashes the result with SHA-256
    /// // (includes the salt value as a prfix of the hash string).
    /// [Mask(HashType.256, 4, true)]
    /// public string? Secret3 { get; set; } 
    /// ]]>
    /// </code>
    /// </example>
    public MaskAttribute
    (
        HashType hashType,
        int saltLength = 0,
        bool saveSalt = false
    )
    {
        _hashType           = hashType;
        _saltLength         = saltLength < 0 ? 0 : saltLength;
        _saveSalt           = saveSalt;
        _maskLiteral        = null;
        _maskChar           = null;
        _unmaskedCharsStart = null;
        _unmaskedCharsEnd   = null;
    }
    #endregion

    #region Public methods
    /// <summary>
    /// Sets an appropriate converter that will apply a mask to a decorated string property
    /// based on the settings specified in the constructor.
    /// </summary>
    /// <param name="type">
    /// Data type to convert (<see cref="string"/> only).
    /// </param>
    /// <returns>
    /// One of these converters:
    /// <see cref="CharMaskJsonConverter"/>,
    /// <see cref="LiteralMaskJsonConverter"/>, or
    /// <see cref="HashMaskJsonConverter"/>.
    /// </returns>
    public override JsonConverter CreateConverter
    (
        Type type
    )
    {
        if (type != typeof(string))
        {
            throw new ArgumentException($"The '{nameof(MaskAttribute)}' attribute can only be applied to 'string' properties, but it was applied to a '{type.Name}' property.");
        }

        if (_hashType.HasValue)
        {
            return (JsonConverter)new HashMaskJsonConverter(_hashType.Value, _saltLength ?? 0, _saveSalt ?? false);
        }
        else if (_maskChar.HasValue)
        {
        
            return (JsonConverter)new CharMaskJsonConverter(_maskChar.Value, _unmaskedCharsStart ?? 0, _unmaskedCharsEnd ?? 0);
        }
        else
        {
            return (JsonConverter)new LiteralMaskJsonConverter(_maskLiteral);
        }
    }
    #endregion
}
