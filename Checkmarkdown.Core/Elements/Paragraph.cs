using Markdig.Syntax;
using Block = Checkmarkdown.Core.Elements.Meta.Block;

namespace Checkmarkdown.Core.Elements;

public class Paragraph : Block {
    public Paragraph(ParagraphBlock mdo) : base(mdo) { }
}