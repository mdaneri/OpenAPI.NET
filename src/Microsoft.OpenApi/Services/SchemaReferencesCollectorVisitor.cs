using System;
using System.Collections.Generic;

namespace Microsoft.OpenApi;
internal class SchemaReferencesCollectorVisitor : OpenApiVisitorBase
{
    private readonly HashSet<string> _visitedSchemasReferences = new(StringComparer.Ordinal);
    public override void Visit(IOpenApiReferenceHolder referenceableHolder)
    {
        base.Visit(referenceableHolder);
        if (referenceableHolder is OpenApiSchemaReference schemaReference && !string.IsNullOrEmpty(schemaReference.Reference?.Id) && schemaReference.Reference?.Id is not null)
        {
            _visitedSchemasReferences.Add(schemaReference.Reference.Id);
        }
    }
    public HashSet<string> GetVisitedSchemasReferences()
    {
        // creating a copy to avoid external modification of the internal set
        return new (_visitedSchemasReferences, StringComparer.Ordinal);
    }
}
