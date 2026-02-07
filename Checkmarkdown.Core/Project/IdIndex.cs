using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Wiring.Errors;

namespace Checkmarkdown.Core.Project;

/// <summary>A reference of which IDs are located in which documents.</summary>
/// <remarks>Also enforces global ID uniqueness.</remarks>
public class IdIndex : Dictionary<String, Document> {
    
    public new Document this[String key] {
        get => base[key];
        set => Add(key, value);
    }

    /// <inheritdoc cref="IDictionary{TKey,TValue}.Add(TKey, TValue)"/>
    public new void Add(String key, Document value) {
        if (this.ContainsKey(key))
            throw new DuplicateIdException(id: key, firstDoc: base[key], secondDoc: value);
        base[key] = value;
    }
    
}