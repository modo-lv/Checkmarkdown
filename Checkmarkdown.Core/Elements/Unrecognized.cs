using Markdig.Syntax;
using Checkmarkdown.Core.Elements.Meta;

namespace Checkmarkdown.Core.Elements;

/// <summary>
/// Fallback element for Markdig's AST nodes that haven't been converted to any actual <see cref="Element"/>.  
/// </summary>
/// <remarks>
/// In practice there shouldn't be such elements (once all the Checkmarkdown elements have been implemented), but
/// during development and if something unexpected shows up, this allows the rest of the tree to be processed
/// normally.
/// </remarks>
public class Unrecognized(IMarkdownObject mdo) : Inline {
    public readonly String Name = mdo.GetType().Name;
}