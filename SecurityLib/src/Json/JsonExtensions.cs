// Ignore Spelling: Json

using DotNetExtras.Common.Extensions;
using DotNetExtras.Common.Json;
using DotNetExtras.Extended;

namespace DotNetExtras.Security.Json;
/// <summary>
/// Implements extension methods for serializing objects to JSON strings with masked values of the specified properties.
/// </summary>
public static class JsonExtensions
{
    #region Public methods
    /// <inheritdoc cref="ToJson(object?, string?, bool, bool, bool, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a non-indented JSON string 
    /// masking the specified properties 
    /// with the <c>null</c> values.
    /// The resulting JSON string will 
    /// be non-indented, 
    /// use the <c>camelCase</c> notation for property names, and
    /// exclude <c>null</c> values.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The password property value will hold null.
    /// Console.WriteLine(user.ToJson("Password"));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, null, false, false, false, maskedProperties);
    }

    /// <inheritdoc cref="ToJson(object?, string?, bool, bool, bool, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a non-indented JSON string 
    /// masking the specified properties 
    /// with the <c>null</c> values.
    /// The resulting JSON string will 
    /// use the <c>camelCase</c> notation for property names, and
    /// exclude <c>null</c> values.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The password property value will hold null; the JSON string will be indented.
    /// Console.WriteLine(user.ToJson("Password", true));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        bool indented,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, null, indented, false, false, maskedProperties);
    }

    /// <inheritdoc cref="ToJson(object?, string?, bool, bool, bool, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a non-indented JSON string 
    /// masking the specified properties 
    /// with the <c>null</c> values.
    /// The resulting JSON string will 
    /// exclude <c>null</c> values.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The password property value will hold null; the JSON string will be indented.
    /// Console.WriteLine(user.ToJson("Password", true, true));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        bool indented,
        bool useOriginalCase,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, null, indented, useOriginalCase, false, maskedProperties);
    }

    /// <inheritdoc cref="ToJson(object?, string?, bool, bool, bool, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a non-indented JSON string 
    /// masking the specified properties 
    /// with the <c>null</c> values.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The password property value will hold null; the JSON string will be indented.
    /// Console.WriteLine(user.ToJson("Password", true, true, true));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        bool indented,
        bool useOriginalCase,
        bool includeNullValues,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, null, indented, useOriginalCase, includeNullValues, maskedProperties);
    }

    /// <inheritdoc cref="ToJson(object?, string?, bool, bool, bool, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a non-indented JSON string 
    /// masking the specified properties 
    /// with a literal string or the <c>null</c> values.
    /// The resulting JSON string will 
    /// be non-indented,
    /// use the <c>camelCase</c> notation for property names, and
    /// exclude <c>null</c> values.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The password property value will hold "***".
    /// Console.WriteLine(user.ToJson("***", "Password"));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        string? mask,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, mask, false, false, false, maskedProperties);
    }

    /// <inheritdoc cref="ToJson(object?, string?, bool, bool, bool, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a non-indented JSON string 
    /// masking the specified properties 
    /// with a literal string or the <c>null</c> values.
    /// The resulting JSON string will 
    /// use the <c>camelCase</c> notation for property names, and
    /// exclude <c>null</c> values.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The password property value will hold "***".
    /// Console.WriteLine(user.ToJson("***", "Password"));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        string? mask,
        bool indented,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, mask, indented, false, false, maskedProperties);
    }

    /// <inheritdoc cref="ToJson(object?, string?, bool, bool, bool, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a non-indented JSON string 
    /// masking the specified properties 
    /// with a literal string or the <c>null</c> values.
    /// The resulting JSON string will 
    /// exclude <c>null</c> values.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The password property value will hold "***".
    /// Console.WriteLine(user.ToJson("***", "Password"));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        string? mask,
        bool indented,
        bool useOriginalCase,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, mask, indented, useOriginalCase, false, maskedProperties);
    }

    /// <summary>
    /// Serializes any object to a JSON string 
    /// masking the specified properties with 
    /// a literal string <c>null</c> values.
    /// </summary>
    /// <param name="source">
    /// Source object.
    /// </param>
    /// <param name="indented">
    /// If <c>true</c>, serialized JSON elements will be indented.
    /// </param>
    /// <param name="useOriginalCase">
    /// If <c>true</c>, the original property names will be used
    /// (unless they are overwritten by the JSON serialization attributes);
    /// otherwise, the <c>camelCase</c> notation will be used.
    /// </param>
    /// <param name="includeNullValues">
    /// If <c>true</c>, properties with null values will be included;
    /// otherwise, they will be ignored.
    /// </param>
    /// <param name="mask">
    /// String value used for masking the values of the specified properties 
    /// (can use <c>null</c> to set the property values to <c>null</c>).
    /// </param>
    /// <param name="maskedProperties">
    /// List of properties to mask (use full property names for nested properties, e.g. <c>PasswordPolicy.Password</c>).
    /// </param>
    /// <returns>
    /// JSON string with the masked values of the specified properties.
    /// </returns>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The password property value will hold "***".
    /// Console.WriteLine(user.ToJson(true, true, true, "***", "Password"));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        string? mask,
        bool indented,
        bool useOriginalCase,
        bool includeNullValues,
        params string[]? maskedProperties
    )
    {
        object? sanitized;

        if (source != null && maskedProperties != null && maskedProperties.Length > 0)
        {
            sanitized = source?.Clone();

            foreach (string maskedProperty in maskedProperties)
            {
                sanitized?.SetPropertyValue(maskedProperty, mask);
            }
        }
        else
        {
            sanitized = source;
        }

        return Common.Json.JsonExtensions.ToJson(sanitized, indented, useOriginalCase, includeNullValues);
    }

    /// <inheritdoc cref="ToJson(object?, char, bool, bool, bool, int, int, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a non-indented JSON string 
    /// masking the specified properties 
    /// using a mask character.
    /// The resulting JSON string will 
    /// be non-indented,
    /// use the <c>camelCase</c> notation for property names, and
    /// exclude <c>null</c> values.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The JSON password property value will hold "se************".
    /// Console.WriteLine(user.ToJson('*', 2, "Password"));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        char mask,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, mask, false, false, false, 0, 0, maskedProperties);
    }

    /// <inheritdoc cref="ToJson(object?, char, bool, bool, bool, int, int, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a non-indented JSON string 
    /// masking the specified properties 
    /// using a mask character.
    /// The resulting JSON string will 
    /// use the <c>camelCase</c> notation for property names, and
    /// exclude <c>null</c> values.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The JSON password property value will hold "se************".
    /// Console.WriteLine(user.ToJson('*', 2, "Password"));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        char mask,
        bool indented,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, mask, indented, false, false, 0, 0, maskedProperties);
    }

    /// <inheritdoc cref="ToJson(object?, char, bool, bool, bool, int, int, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a non-indented JSON string 
    /// masking the specified properties 
    /// using a mask character.
    /// The resulting JSON string will 
    /// exclude <c>null</c> values.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The JSON password property value will hold "se************".
    /// Console.WriteLine(user.ToJson('*', 2, "Password"));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        char mask,
        bool indented,
        bool useOriginalCase,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, mask, indented, useOriginalCase, false, 0, 0, maskedProperties);
    }

    /// <inheritdoc cref="ToJson(object?, char, bool, bool, bool, int, int, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a non-indented JSON string 
    /// masking the specified properties 
    /// using a mask character.
    /// The resulting JSON string will 
    /// exclude <c>null</c> values.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The JSON password property value will hold "se************".
    /// Console.WriteLine(user.ToJson('*', 2, "Password"));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        char mask,
        bool indented,
        bool useOriginalCase,
        bool includeNullValues,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, mask, indented, useOriginalCase, includeNullValues, 0, 0, maskedProperties);
    }

    /// <inheritdoc cref="ToJson(object?, char, bool, bool, bool, int, int, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a non-indented JSON string 
    /// masking the specified properties 
    /// using a mask character and keeping the specified number of characters
    /// in the beginning of the string in plain text.
    /// The resulting JSON string will 
    /// be non-indented,
    /// use the <c>camelCase</c> notation for property names, and
    /// exclude <c>null</c> values.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The JSON password property value will hold "se************".
    /// Console.WriteLine(user.ToJson('*', 2, "Password"));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        char mask,
        int unmaskedCharsStart,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, mask, false, false, false, unmaskedCharsStart, 0, maskedProperties);
    }

    /// <inheritdoc cref="ToJson(object?, char, bool, bool, bool, int, int, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a JSON string 
    /// masking the specified properties 
    /// using a mask character and keeping the specified number of characters
    /// in the beginning of the string in plain text.
    /// The resulting JSON string will 
    /// use the <c>camelCase</c> notation for property names, and
    /// exclude <c>null</c> values.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The JSON password property value will hold "se************".
    /// Console.WriteLine(user.ToJson(false, '*', 2, "Password"));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        char mask,
        bool indented,
        int unmaskedCharsStart,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, mask, indented, false, false, unmaskedCharsStart, 0, maskedProperties);
    }

    /// <inheritdoc cref="ToJson(object?, char, bool, bool, bool, int, int, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a JSON string 
    /// masking the specified properties 
    /// using a mask character and keeping the specified number of characters
    /// in the beginning of the string in plain text.
    /// The resulting JSON string will 
    /// exclude <c>null</c> values.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The JSON password property value will hold "se************".
    /// Console.WriteLine(user.ToJson(false, '*', 2, "Password"));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        char mask,
        bool indented,
        bool useOriginalCase,
        int unmaskedCharsStart,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, mask, indented, useOriginalCase, false, unmaskedCharsStart, 0, maskedProperties);
    }

    /// <inheritdoc cref="ToJson(object?, char, bool, bool, bool, int, int, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a JSON string 
    /// masking the specified properties 
    /// using a mask character and keeping the specified number of characters
    /// in the beginning of the string in plain text.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The JSON password property value will hold "se************".
    /// Console.WriteLine(user.ToJson(false, '*', 2, "Password"));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        char mask,
        bool indented,
        bool useOriginalCase,
        bool includeNullValues,
        int unmaskedCharsStart,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, mask, indented, useOriginalCase, includeNullValues, unmaskedCharsStart, 0, maskedProperties);
    }

    /// <inheritdoc cref="ToJson(object?, char, bool, bool, bool, int, int, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a non-indented JSON string 
    /// masking the specified properties 
    /// using a mask character and keeping the specified number of characters
    /// in the beginning and/or at the end of the string in plain text.
    /// The resulting JSON string will 
    /// be non-indented,
    /// use the <c>camelCase</c> notation for property names, and
    /// exclude <c>null</c> values.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The JSON password property value will hold "sen*********ue".
    /// Console.WriteLine(user.ToJson('*', 3, 2, "Password"));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        char mask,
        int unmaskedCharsStart,
        int unmaskedCharsEnd,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, mask, false, false, false, unmaskedCharsStart, unmaskedCharsEnd, maskedProperties);
    }

    /// <inheritdoc cref="ToJson(object?, char, bool, bool, bool, int, int, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a non-indented JSON string 
    /// masking the specified properties 
    /// using a mask character and keeping the specified number of characters
    /// in the beginning and/or at the end of the string in plain text.
    /// The resulting JSON string will 
    /// use the <c>camelCase</c> notation for property names, and
    /// exclude <c>null</c> values.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The JSON password property value will hold "sen*********ue".
    /// Console.WriteLine(user.ToJson('*', 3, 2, "Password"));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        char mask,
        bool indented,
        int unmaskedCharsStart,
        int unmaskedCharsEnd,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, mask, indented, false, false, unmaskedCharsStart, unmaskedCharsEnd, maskedProperties);
    }

    /// <inheritdoc cref="ToJson(object?, char, bool, bool, bool, int, int, string[])" path="param|returns"/>
    /// <summary>
    /// Serializes any object to a non-indented JSON string 
    /// masking the specified properties 
    /// using a mask character and keeping the specified number of characters
    /// in the beginning and/or at the end of the string in plain text.
    /// The resulting JSON string will 
    /// exclude <c>null</c> values.
    /// </summary>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The JSON password property value will hold "sen*********ue".
    /// Console.WriteLine(user.ToJson('*', 3, 2, "Password"));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        char mask,
        bool indented,
        bool useOriginalCase,
        int unmaskedCharsStart,
        int unmaskedCharsEnd,
        params string[]? maskedProperties
    )
    {
        return ToJson(source, mask, indented, useOriginalCase, false, unmaskedCharsStart, unmaskedCharsEnd, maskedProperties);
    }

    /// <summary>
    /// Serializes any object to a JSON string 
    /// masking the specified properties 
    /// using a mask character and keeping the specified number of characters
    /// in the beginning and/or at the end of the string in plain text.
    /// </summary>
    /// <param name="source">
    /// Source object.
    /// </param>
    /// <param name="mask">
    /// Character used for masking the values of the specified properties.
    /// </param>
    /// <param name="indented">
    /// If <c>true</c>, serialized JSON elements will be indented.
    /// </param>
    /// <param name="useOriginalCase">
    /// If <c>true</c>, the original property names will be used
    /// (unless they are overwritten by the JSON serialization attributes);
    /// otherwise, the <c>camelCase</c> notation will be used.
    /// </param>
    /// <param name="includeNullValues">
    /// If <c>true</c>, properties with null values will be included;
    /// otherwise, they will be ignored.
    /// </param>
    /// <param name="unmaskedCharsStart">
    /// Number characters to be left unmasked at the start of the string.
    /// </param>
    /// <param name="unmaskedCharsEnd">
    /// Number characters to be left unmasked at the end of the string.
    /// </param>
    /// <param name="maskedProperties">
    /// List of properties to mask (use full property names for nested properties, e.g. <c>PasswordPolicy.Password</c>).
    /// </param>
    /// <returns>
    /// JSON string with the masked values of the specified properties.
    /// </returns>
    /// <example>
    /// <code>
    /// User user = new User()
    /// { 
    ///     Name = "John", 
    ///     Age = 30, 
    ///     Password = "sensitiveValue" 
    /// };
    /// 
    /// // The JSON password property value will hold "sen*********ue".
    /// Console.WriteLine(user.ToJson(false, '*', 3, 2, "Password"));
    /// </code>
    /// </example>
    public static string ToJson
    (
        this object? source,
        char mask,
        bool indented,
        bool useOriginalCase,
        bool includeNullValues,
        int unmaskedCharsStart,
        int unmaskedCharsEnd,
        params string[]? maskedProperties
    )
    {
        object? sanitized;

        if (source != null && maskedProperties != null && maskedProperties.Length > 0)
        {
            sanitized = source?.Clone();

            foreach (string maskedProperty in maskedProperties)
            {
                string? value = sanitized?.GetPropertyValue(maskedProperty)?.ToString();

                if (value != null)
                {
                    value = MaskValue(value, mask, unmaskedCharsStart, unmaskedCharsEnd);
                }

                sanitized?.SetPropertyValue(maskedProperty, value);
            }
        }
        else
        {
            sanitized = source;
        }

        return Common.Json.JsonExtensions.ToJson(sanitized, indented, useOriginalCase, includeNullValues);
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Use mask settings to mask the specified value.
    /// </summary>
    /// <param name="value">
    /// Value to be masked.
    /// </param>
    /// <param name="mask">
    /// Mask character.
    /// </param>
    /// <param name="unmaskedCharsStart">
    /// Number characters to be left unmasked at the start of the string.
    /// </param>
    /// <param name="unmaskedCharsEnd">
    /// Number characters to be left unmasked at the end of the string.
    /// </param>
    /// <returns>
    /// Masked value.
    /// </returns>
    private static string MaskValue
    (
        string value,
        char mask,
        int unmaskedCharsStart,
        int unmaskedCharsEnd
    )
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        if (unmaskedCharsStart + unmaskedCharsEnd < value.Length)
        {
            if (unmaskedCharsStart == 0 && unmaskedCharsEnd == 0)
            {
                value = new string(mask, value.Length);
            }
            else
            {
                string start = unmaskedCharsStart == 0 ? "" : value[..unmaskedCharsStart];
                string end   = unmaskedCharsEnd == 0 ? "" : value[^unmaskedCharsEnd..];
                string middle= new(mask, value.Length - unmaskedCharsStart - unmaskedCharsEnd);

                value = start + middle + end;
            }
        }

        return value;
    }
    #endregion
}
