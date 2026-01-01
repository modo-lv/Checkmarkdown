using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Elements.Meta;

namespace Checkmarkdown.Core.Wiring.Errors;

public class DuplicateIdException(String id, Document firstDoc, Document secondDoc) : Exception(
    $"ID [{id}] exists in both [{firstDoc.ProjectFileId}] and [{secondDoc.ProjectFileId}]."
);