// Ignore Spelling: Json

using DotNetExtras.Common.Json;
using DotNetExtras.Security.Json;

namespace SecurityTests;
public partial class MaskAttributeTests
{
    public class CharSample1
    {
        public string? Value { get; set; }

        [Mask('~')]
        public string? SecretA { get; set; }
    }

    public class CharSample2
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        [Mask('*')]
        public string? Secret1 { get; set; }

        [Mask('#')]
        public string? Secret2 { get; set; }

        [Mask('*', 3, 0)]
        public string? Secret3 { get; set; }

        [Mask('*', 0, 3)]
        public string? Secret4 { get; set; }

        [Mask('*', 2, 2)]
        public string? Secret5 { get; set; }

        public CharSample1? Inner { get; set; }
    }

    [Fact]
    public void ToJson_Char()
    {
        int    id    = 123;
        string value = "something";
        string name  = "whatever";
        string secret= "secret";

        CharSample2 sample = new()
        {
            Id = id,
            Name = name,
            Secret1 = secret,
            Secret2 = secret,
            Secret3 = secret,
            Secret4 = secret,
            Secret5 = secret,

            Inner = new CharSample1()
            {
                Value   = value,
                SecretA = secret
            }
        };

        string json = sample.ToJson();

        CharSample2? sampleClone = json.FromJson<CharSample2>();

        Assert.NotNull(sampleClone);

        Assert.Equal(id, sampleClone?.Id);
        Assert.Equal(name, sampleClone?.Name);

        Assert.Equal("******", sampleClone?.Secret1);
        Assert.Equal("######", sampleClone?.Secret2);
        Assert.Equal("sec***", sampleClone?.Secret3);
        Assert.Equal("***ret", sampleClone?.Secret4);
        Assert.Equal("se**et", sampleClone?.Secret5);

        Assert.NotNull(sampleClone?.Inner);

        Assert.Equal(value, sampleClone?.Inner?.Value);
        Assert.Equal("~~~~~~", sampleClone?.Inner?.SecretA);
    }
}
