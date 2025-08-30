# DotNetExtras.Security

`DotNetExtras.Security` is a .NET Core library that implements most commonly used security operations.

Use the `DotNetExtras.Security` library to:

- Generate random passwords.
- Encrypt and decrypt data using the AES algorithm.
- Hash data using secure hashing algorithms.
- Generate and validate JSON web tokens (JWT).
- Convert objects to JSON strings with sensitive properties masked.
- Automatically mask sensitive object properties written to the logs.

# Usage

The following examples illustrates various operations implemented by the `DotNetExtras.Security` library.

### DotNetExtras.Security namespace

```cs
using DotNetExtras.Security;
...
// Generate a random password between 12 and 16 characters long.
string randomPassword = Password.Generate(12, 16);
```

```cs
using DotNetExtras.Security;
...
string plainText = "Hello, World!";
string password = "never-hard-code-passwords!";

// Encrypt the plain text using the password to generate the symmetric key.
string encrypted = Crypto.Encrypt(plainText, password)

// Decrypt the cipher text using the same password to get the original plain text.
string decrypted = Crypto.Decrypt(encryptedText, password);
```

```cs
using DotNetExtras.Security;
...
string plainText = "Hello, World!";

HashType hashType = HashType.SHA256;

// Use either option to generate the hash value.
// string hashText = Hash.Generate(hashType, plainText);
// or
string hashText = plainText.ToHash(hashType)

// Given the hash value, validate the plain text.
bool valid = Hash.Validate(hashType, hashText, "Hello, World!")
```

```cs
using System.Security.Claims;
using DotNetExtras.Security;
...
using System.Net.Sockets;

string secret = "never-hard-code-passwords!";
int    tokenExpirationSeconds = 3600; // 1 hour
string email = "joe.doe@sample.com";

Jwt jwt = new(secret, tokenExpirationSeconds);

string token = jwt.Generate(email);

ClaimsPrincipal principal = jwt.Validate(token);

Assert.NotNull(principal);
Assert.Equal(email, principal.FindFirst(ClaimTypes.Email)?.Value);
```
### DotNetExtras.Security.Json namespace

Mask sensitive properties when serializing any objects to JSON via [System.Text.Json](https://learn.microsoft.com/en-us/dotnet/api/system.text.json).
```cs
using DotNetExtras.Security.Json;
...
User user = new()
{   UserName = "joe.doe",
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
// but leaving the first and last 2 characters in plain text.
json = user.ToJson('*', 2, 2, "Password", "PersonalData.Ssn");
```
Mask sensitive properties when serializing your objects to JSON via [System.Text.Json](https://learn.microsoft.com/en-us/dotnet/api/system.text.json).
```cs
using DotNetExtras.Security.Json;
...
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
// and two first and last characters will be left in plain text.
[Mask('*', 2, 2)]
public string? Secret4 { get; set; }

// Value will be replaced with the hex-encoded SHA-256 hash.
[Mask(HashType.SHA256)]
public string? Secret5 { get; set; }
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
