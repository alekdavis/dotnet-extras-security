// Ignore Spelling: Json

using DotNetExtras.Common.Json;
using DotNetExtras.Security;
using DotNetExtras.Security.Json;

namespace SecurityTests;
public partial class MaskAttributeTests
{
    public class LiteralSample1
    {
        public string? Value { get; set; }

        [Mask("~~~~~~")]
        public string? SecretA { get; set; }
    }

    public class LiteralSample2
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        [Mask()]
        public string? Secret1 { get; set; }

        [Mask("")]
        public string? Secret2 { get; set; }

        [Mask("***masked3***")]
        public string? Secret3 { get; set; }

        [Mask("***masked4***")]
        public string? Secret4 { get; set; }

        [Mask("***masked5***")]
        public string? Secret5 { get; set; }

        public LiteralSample1? Inner { get; set; }
    }

    [Fact]
    public void ToJson_Literal()
    {
        int    id      = 123;
        string value   = "something";
        string name    = "whatever";
        string secret  = "secret";
        string secret1 = "secret1";
        string secret2 = "secret2";
        string secret3 = "secret3";

        LiteralSample2 sample = new()
        {
            Id = id,
            Name = name,
            Secret1 = secret1,
            Secret2 = secret2,
            Secret3 = secret3,
            Secret4 = null,
            Secret5 = "",

            Inner = new LiteralSample1()
            {
                Value   = value,
                SecretA = secret
            }
        };

        string json = sample.ToJson();

        LiteralSample2? sampleClone = json.FromJson<LiteralSample2>();

        Assert.NotNull(sampleClone);

        Assert.Equal(id, sampleClone?.Id);
        Assert.Equal(name, sampleClone?.Name);

        Assert.Null(sampleClone?.Secret1);
        Assert.Equal("", sampleClone?.Secret2);
        Assert.Equal("***masked3***", sampleClone?.Secret3);
        Assert.Null(sampleClone?.Secret4);
        Assert.Equal("***masked5***", sampleClone?.Secret5);

        Assert.NotNull(sampleClone?.Inner);

        Assert.Equal(value, sampleClone?.Inner?.Value);
        Assert.Equal("~~~~~~", sampleClone?.Inner?.SecretA);
    }
}