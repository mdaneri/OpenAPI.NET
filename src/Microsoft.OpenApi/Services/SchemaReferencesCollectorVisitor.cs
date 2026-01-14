using System;
using System.Collections.Generic;

namespace Microsoft.OpenApi;
internal class SchemaReferencesCollectorVisitor : OpenApiVisitorBase
{
    private readonly HashSet<string> _visitedSchemasReferences = new(StringComparer.Ordinal);
    public override void Visit(IOpenApiReferenceHolder referenceableHolder)
    {
        base.Visit(referenceableHolder);
        if (referenceableHolder is OpenApiSchemaReference schemaReference &&
            !string.IsNullOrEmpty(schemaReference.Reference?.Id) &&
            schemaReference.Reference?.Id is not null &&
            _visitedSchemasReferences.Add(schemaReference.Reference.Id) &&
            schemaReference.RecursiveTarget is {} referencedSchema)
        {
            Visit((IOpenApiSchema)referencedSchema);
        }
    }
    public override void Visit(IOpenApiSchema schema)
    {
        base.Visit(schema);
        if (schema is IOpenApiReferenceHolder referenceHolder) Visit(referenceHolder);
        if (schema is not OpenApiSchema openApiSchema) return;
        VisitCollection(openApiSchema.AllOf);
        VisitCollection(openApiSchema.AnyOf);
        VisitCollection(openApiSchema.OneOf);
        VisitCollection(openApiSchema.Properties?.Values);
        if (openApiSchema.AdditionalProperties is not null)
            Visit(openApiSchema.AdditionalProperties);
        if (openApiSchema.Items is not null)
            Visit(openApiSchema.Items);
        if (openApiSchema.Not is not null)
            Visit(openApiSchema.Not);
        if (openApiSchema.Discriminator?.Mapping is not null)
            VisitCollection(openApiSchema.Discriminator.Mapping.Values);
        //TODO add default mapping in V3
    }
    private void VisitCollection(IEnumerable<IOpenApiSchema>? schemas)
    {
        if (schemas is null) return;
        foreach (var schema in schemas)
        {
            Visit(schema);
        }
    }
    public HashSet<string> GetVisitedSchemasReferences()
    {
        // creating a copy to avoid external modification of the internal set
        return new (_visitedSchemasReferences, StringComparer.Ordinal);
    }
}
