using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Checkmarkdown.Web.Project.Config;

public class ShortlinkRewriteRuleConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, Object? value, JsonSerializer serializer) {
        throw new NotImplementedException();
    }
    public override Object ReadJson(
        JsonReader reader,
        Type objectType,
        Object? existingValue,
        JsonSerializer serializer) {

        if (reader.TokenType == JsonToken.StartArray) {
            var array = JArray.Load(reader);
            if (array.Count != 2) {
                throw new JsonSerializationException(
                    "Shortlink rewrite rule array syntax requires exactly two elements."
                );
            }
            return new ShortlinkRewriteRule(
                Pattern: array[0].ToObject<String>()!,
                Replacement: array[1].ToObject<String>()!
            );
        }

        return serializer.Deserialize<ShortlinkRewriteRule>(reader)!;
    }

    public override Boolean CanConvert(Type objectType) =>
        objectType == typeof(ShortlinkRewriteRule);
}