using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Utils;

namespace Checkmarkdown.Core.Tests.Utils;

public static class AstProcessorUtils
{
    extension(AstProcessorPipeline self)
    {
        public Document RunFromMarkdown(String markdown) =>
            FromMarkdown.ToCheckmarkdown(markdown, null).Let(self.Run);
    }
}