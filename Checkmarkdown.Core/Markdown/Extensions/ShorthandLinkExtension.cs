using Checkmarkdown.Core.Utils;
using Markdig;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Parsers.Inlines;
using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Checkmarkdown.Core.Markdown.Extensions;

/// <summary>
/// Makes Markdig register plain `[abc]` (without any explicit URL) as a link.
/// </summary>
public class ShorthandLinkExtension : IMarkdownExtension {
    public void Setup(MarkdownPipelineBuilder pipeline) {
        if (!pipeline.InlineParsers.Contains<ShorthandLinkParser>())
            pipeline.InlineParsers.InsertBefore<LinkInlineParser>(new ShorthandLinkParser());
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer) { }

    private class ShorthandLinkParser : InlineParser {
        public ShorthandLinkParser() {
            this.OpeningCharacters = ['['];
        }

        public override Boolean Match(InlineProcessor processor, ref StringSlice slice) {
            var startPosition = processor.GetSourcePosition(slice.Start, out var line, out var column);
            var last = slice.PeekCharExtra(-1);
            var current = slice.CurrentChar;

            if (current != '[' || last == '\\')
                return false;

            // Link reference
            if (LinkHelper.TryParseLabel(ref slice, out var label, out var labelSpan))
                if (processor.Document.ContainsLinkReferenceDefinition(label))
                    return false;

            // Explicit link
            if (slice.CurrentChar.IsIn('(', ':'))
                return false;

            // Shorthand
            processor.Inline = new LinkInline(label!, "") {
                LabelSpan = processor.GetSourcePositionFromLocalSpan(labelSpan),
                IsImage = false,
                Span = new SourceSpan(
                    startPosition,
                    processor.GetSourcePosition(slice.Start - 1)
                ),
                Line = line,
                Column = column,
                IsClosed = true,
            }.AppendChild(new LiteralInline(label!));

            return true;
        }
    }
}