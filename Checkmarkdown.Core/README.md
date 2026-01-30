# Checkmarkdown.Core

Parses the source Markdown files into an AST (abstract syntax tree) that other modules can use to generate their outputs (text, HTML, etc.).

The parsing happens in two main steps:
* First, the standard Markdown parsing is run, and the resulting Markdig AST is converted into Checkmarkdown's (by replacing Markdig's nodes with ours).
* Then, Checkmarkdown workers run over the tree, processing all the Checkmarkdown-specific features, producing the final AST meant for output.

# Misc.

IDs must be valid according to [HTML4 spec](https://www.w3.org/TR/html401/types.html#type-name):
> ID and NAME tokens must begin with a letter ([A-Za-z]) and may be followed by any number of letters, digits ([0-9]), hyphens ("-"), underscores ("_"), colons (":"), and periods (".").