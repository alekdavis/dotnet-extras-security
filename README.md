# DotNetExtras.Security

`DotNetExtras.Security` is a .NET Core library that implements frequently used security operations.

Use the `DotNetExtras.Security` library to:

- Generate random passwords.
- Encrypt and decrypt data using the AES/Rijndael algorithm with 256-bit key and random salt.
- Hash data and validate data hashes using secure SHA hashing algorithms and random salt.
- Generate and validate JSON web tokens (JWT).
- Convert objects to JSON strings with sensitive properties masked (using the `System.Text.Json` serialization).
- Automatically mask sensitive object properties written to the logs (using the `System.Text.Json` serialization).

## Usage

The following examples illustrates various operations implemented by the `DotNetExtras.Security` library.

### DotNetExtras.Security.Password class

Generate a random password between 12 and 16 characters long.

```cs
string randomPassword = Password.Generate(12, 16);
```

### DotNetExtras.Security.Crypto class

Encrypt and decrypt data using the AES/Rijndael algorithm with 256-bit key and random salt.

```cs
string plainText = "Hello, world!";
string password = "never-hard-code-passwords!";

// Encrypt the plain text using the password to generate the symmetric key and a random salt value.
string encrypted = Crypto.Encrypt(plainText, password)

// Decrypt the cipher text using the same password to get the original plain text.
string decrypted = Crypto.Decrypt(encryptedText, password);
```

### DotNetExtras.Security.Hash class

Generate and validate hash values using the SHA-256 hashing algorithms and random salt.

```cs
string plainText = "Hello, world!";

// Use either option to generate the hash value.
// string hashText = Hash.Generate(HashType.SHA256, plainText);
// or
string hashText = plainText.ToHash(HashType.SHA256)

// Given the hash value, validate the plain text.
bool valid = Hash.Validate(HashType.SHA256, hashText, "Hello, World!")
```

### DotNetExtras.Security.Jwt class

Generate and validate JSON web tokens (JWT).

```cs
string secret = "never-hard-code-passwords!";
string email = "joe.doe@sample.com";

int    tokenExpirationSeconds = 3600; // 1 hour

Jwt jwt = new(secret, tokenExpirationSeconds);

string token = jwt.Generate(email);

System.Security.Claims.ClaimsPrincipal principal = jwt.Validate(token);

// Extract the email claim from the validated principal.
string email2 = principal.FindFirst(ClaimTypes.Email)?.Value;
```

### DotNetExtras.Security.Json.JsonExtensions class

Mask sensitive properties when serializing any objects to JSON via [System.Text.Json](https://learn.microsoft.com/en-us/dotnet/api/system.text.json).

```cs
User user = new()
{   
    UserName = "joe.doe",
    Password = "never-hard-code-passwords!",
    Email = "joe.doe@sample.com",
    PersonalData = new()
    {
        Ssn = "123-45-6789"
    }
};

string? json;

// Serialize the user object to JSON, 
// replacing the Password property value with null.
json = user.ToJson("Password");

// Serialize the user object to JSON, 
// replacing the Password and PersonalData.Ssn property values with "###".
json = user.ToJson("###", "Password", "PersonalData.Ssn");

// Serialize the user object to JSON, 
// masking the Password and PersonalData.Ssn property values
// with the asterisk characters,
// but leaving the first 2 and last 1 characters in plain text.
json = user.ToJson('*', 2, 1, "Password", "PersonalData.Ssn");
```

Mask sensitive properties when serializing your objects to JSON via [System.Text.Json](https://learn.microsoft.com/en-us/dotnet/api/system.text.json).

```cs
public class Demo
{
    // Value will be replaced with null.
    [Mask(null)]
    public string? Secret1 { get; set; }

    // Value will be replaced with an empty string.
    [Mask("")]
    public string? Secret2 { get; set; }

    // Value will be replaced with the literal string "***masked***".
    [Mask("***masked***")]
    public string? Secret3 { get; set; }

    // Value will be replaced with the asterisks
    // and two first and one last characters will be left in plain text.
    [Mask('*', 2, 1)]
    public string? Secret4 { get; set; }

    // Value will be replaced with the hex-encoded SHA-256 hash.
    [Mask(HashType.SHA256)]
    public string? Secret5 { get; set; }
}
```

## Documentation

For complete documentation, usage details, and code samples, see:

- [Documentation](https://alekdavis.github.io/dotnet-extras-security)
- [Unit tests](https://github.com/alekdavis/dotnet-extras-mail/tree/main/SecurityTests)

## Package

Install the latest version of the `DotNetExtras.Security` NuGet package from:

- [https://www.nuget.org/packages/DotNetExtras.Security](https://www.nuget.org/packages/DotNetExtras.Security)

## See also

Check out other `DotNetExtras` libraries at:

- [https://github.com/alekdavis/dotnet-extras](https://github.com/alekdavis/dotnet-extras)
