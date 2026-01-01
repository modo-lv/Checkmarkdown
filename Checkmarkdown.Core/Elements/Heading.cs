using Checkmarkdown.Core.Elements.Meta;
using MarkdigHeading = Markdig.Syntax.HeadingBlock;

namespace Checkmarkdown.Core.Elements; 

public class Heading(MarkdigHeading mHeading) : Block(mHeading) {
  public readonly Int32 Level = mHeading.Level;
}