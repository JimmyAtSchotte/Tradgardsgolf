using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Tradgardsgolf.Contracts;
using Tradgardsgolf.Infrastructure.Files;

namespace Tradgardsgolf.Api.JsonConverters;

public class ImageReferenceConverter : JsonConverter<ImageReference>
{
    private readonly AzureStorageOptions _options;

    public ImageReferenceConverter(AzureStorageOptions options)
    {
        _options = options;
    }

    public override ImageReference Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ImageReference value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Url", _options.BaseUrl);
        writer.WriteString("Path", value.Path);
        writer.WriteEndObject();
    }
}