// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Microsoft.OpenApi.Tests.Services
{
    public class SchemaReferencesCollectorVisitorTests
    {
        [Fact]
        public void VisitorCollectsSchemaReferenceIds()
        {
            // Arrange
            var document = new OpenApiDocument
            {
                Components = new OpenApiComponents
                {
                    Schemas = new Dictionary<string, IOpenApiSchema>
                    {
                        ["Pet"] = new OpenApiSchema
                        {
                            Type = JsonSchemaType.Object,
                            Properties = new Dictionary<string, IOpenApiSchema>
                            {
                                ["id"] = new OpenApiSchema { Type = JsonSchemaType.Integer },
                                ["name"] = new OpenApiSchema { Type = JsonSchemaType.String }
                            }
                        }
                    }
                }
            };

            document.Workspace.RegisterComponents(document);
            var schemaRef = new OpenApiSchemaReference("Pet", document);

            var doc = new OpenApiDocument
            {
                Paths = new OpenApiPaths
                {
                    ["/pets"] = new OpenApiPathItem
                    {
                        Operations = new Dictionary<System.Net.Http.HttpMethod, OpenApiOperation>
                        {
                            [System.Net.Http.HttpMethod.Get] = new OpenApiOperation
                            {
                                Responses = new OpenApiResponses
                                {
                                    ["200"] = new OpenApiResponse
                                    {
                                        Content = new Dictionary<string, OpenApiMediaType>
                                        {
                                            ["application/json"] = new OpenApiMediaType
                                            {
                                                Schema = schemaRef
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var visitor = new SchemaReferencesCollectorVisitor();
            var walker = new OpenApiWalker(visitor);

            // Act
            walker.Walk(doc);
            var references = visitor.GetVisitedSchemasReferences();

            // Assert
            Assert.Single(references);
            // The reference ID should be in the format expected by the visitor
            Assert.All(references, Assert.NotNull);
            Assert.All(references, Assert.NotEmpty);
        }

        [Fact]
        public void VisitorCollectsMultipleSchemaReferences()
        {
            // Arrange
            var document = new OpenApiDocument
            {
                Components = new OpenApiComponents
                {
                    Schemas = new Dictionary<string, IOpenApiSchema>
                    {
                        ["Pet"] = new OpenApiSchema { Type = JsonSchemaType.Object },
                        ["Error"] = new OpenApiSchema { Type = JsonSchemaType.Object },
                        ["User"] = new OpenApiSchema { Type = JsonSchemaType.Object }
                    }
                }
            };

            document.Workspace.RegisterComponents(document);

            var doc = new OpenApiDocument
            {
                Paths = new OpenApiPaths
                {
                    ["/pets"] = new OpenApiPathItem
                    {
                        Operations = new Dictionary<System.Net.Http.HttpMethod, OpenApiOperation>
                        {
                            [System.Net.Http.HttpMethod.Get] = new OpenApiOperation
                            {
                                Responses = new OpenApiResponses
                                {
                                    ["200"] = new OpenApiResponse
                                    {
                                        Content = new Dictionary<string, OpenApiMediaType>
                                        {
                                            ["application/json"] = new OpenApiMediaType
                                            {
                                                Schema = new OpenApiSchemaReference("Pet", document)
                                            }
                                        }
                                    },
                                    ["500"] = new OpenApiResponse
                                    {
                                        Content = new Dictionary<string, OpenApiMediaType>
                                        {
                                            ["application/json"] = new OpenApiMediaType
                                            {
                                                Schema = new OpenApiSchemaReference("Error", document)
                                            }
                                        }
                                    }
                                }
                            },
                            [System.Net.Http.HttpMethod.Post] = new OpenApiOperation
                            {
                                RequestBody = new OpenApiRequestBody
                                {
                                    Content = new Dictionary<string, OpenApiMediaType>
                                    {
                                        ["application/json"] = new OpenApiMediaType
                                        {
                                            Schema = new OpenApiSchemaReference("User", document)
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var visitor = new SchemaReferencesCollectorVisitor();
            var walker = new OpenApiWalker(visitor);

            // Act
            walker.Walk(doc);
            var references = visitor.GetVisitedSchemasReferences();

            // Assert
            Assert.Equal(3, references.Count);
            Assert.All(references, Assert.NotNull);
            Assert.All(references, Assert.NotEmpty);
        }

        [Fact]
        public void VisitorDoesNotCollectNonReferenceSchemas()
        {
            // Arrange
            var doc = new OpenApiDocument
            {
                Paths = new OpenApiPaths
                {
                    ["/pets"] = new OpenApiPathItem
                    {
                        Operations = new Dictionary<System.Net.Http.HttpMethod, OpenApiOperation>
                        {
                            [System.Net.Http.HttpMethod.Get] = new OpenApiOperation
                            {
                                Responses = new OpenApiResponses
                                {
                                    ["200"] = new OpenApiResponse
                                    {
                                        Content = new Dictionary<string, OpenApiMediaType>
                                        {
                                            ["application/json"] = new OpenApiMediaType
                                            {
                                                Schema = new OpenApiSchema
                                                {
                                                    Type = JsonSchemaType.Object,
                                                    Properties = new Dictionary<string, IOpenApiSchema>
                                                    {
                                                        ["id"] = new OpenApiSchema { Type = JsonSchemaType.Integer }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var visitor = new SchemaReferencesCollectorVisitor();
            var walker = new OpenApiWalker(visitor);

            // Act
            walker.Walk(doc);
            var references = visitor.GetVisitedSchemasReferences();

            // Assert
            Assert.Empty(references);
        }

        [Fact]
        public void GetVisitedSchemasReferencesReturnsCopyOfInternalSet()
        {
            // Arrange
            var document = new OpenApiDocument
            {
                Components = new OpenApiComponents
                {
                    Schemas = new Dictionary<string, IOpenApiSchema>
                    {
                        ["Pet"] = new OpenApiSchema { Type = JsonSchemaType.Object }
                    }
                }
            };

            document.Workspace.RegisterComponents(document);

            var doc = new OpenApiDocument
            {
                Paths = new OpenApiPaths
                {
                    ["/pets"] = new OpenApiPathItem
                    {
                        Operations = new Dictionary<System.Net.Http.HttpMethod, OpenApiOperation>
                        {
                            [System.Net.Http.HttpMethod.Get] = new OpenApiOperation
                            {
                                Responses = new OpenApiResponses
                                {
                                    ["200"] = new OpenApiResponse
                                    {
                                        Content = new Dictionary<string, OpenApiMediaType>
                                        {
                                            ["application/json"] = new OpenApiMediaType
                                            {
                                                Schema = new OpenApiSchemaReference("Pet", document)
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var visitor = new SchemaReferencesCollectorVisitor();
            var walker = new OpenApiWalker(visitor);
            walker.Walk(doc);

            // Act
            var references1 = visitor.GetVisitedSchemasReferences();
            var references2 = visitor.GetVisitedSchemasReferences();

            // Modify the first copy
            references1.Add("Modified");

            // Assert
            Assert.Single(references2);
            Assert.DoesNotContain("Modified", references2);
        }

        [Fact]
        public void VisitorHandlesDuplicateSchemaReferences()
        {
            // Arrange
            var document = new OpenApiDocument
            {
                Components = new OpenApiComponents
                {
                    Schemas = new Dictionary<string, IOpenApiSchema>
                    {
                        ["Pet"] = new OpenApiSchema { Type = JsonSchemaType.Object }
                    }
                }
            };

            document.Workspace.RegisterComponents(document);

            var doc = new OpenApiDocument
            {
                Paths = new OpenApiPaths
                {
                    ["/pets"] = new OpenApiPathItem
                    {
                        Operations = new Dictionary<System.Net.Http.HttpMethod, OpenApiOperation>
                        {
                            [System.Net.Http.HttpMethod.Get] = new OpenApiOperation
                            {
                                Responses = new OpenApiResponses
                                {
                                    ["200"] = new OpenApiResponse
                                    {
                                        Content = new Dictionary<string, OpenApiMediaType>
                                        {
                                            ["application/json"] = new OpenApiMediaType
                                            {
                                                Schema = new OpenApiSchemaReference("Pet", document)
                                            }
                                        }
                                    }
                                }
                            },
                            [System.Net.Http.HttpMethod.Post] = new OpenApiOperation
                            {
                                RequestBody = new OpenApiRequestBody
                                {
                                    Content = new Dictionary<string, OpenApiMediaType>
                                    {
                                        ["application/json"] = new OpenApiMediaType
                                        {
                                            Schema = new OpenApiSchemaReference("Pet", document)
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var visitor = new SchemaReferencesCollectorVisitor();
            var walker = new OpenApiWalker(visitor);

            // Act
            walker.Walk(doc);
            var references = visitor.GetVisitedSchemasReferences();

            // Assert - Should only contain one instance even though referenced twice
            Assert.Single(references);
            var referenceId = references.First();
            Assert.NotNull(referenceId);
            Assert.NotEmpty(referenceId);
        }

        [Fact]
        public void VisitorCollectsNestedSchemaReferences()
        {
            // Arrange
            var document = new OpenApiDocument
            {
                Components = new OpenApiComponents
                {
                    Schemas = new Dictionary<string, IOpenApiSchema>
                    {
                        ["Address"] = new OpenApiSchema { Type = JsonSchemaType.Object },
                        ["Person"] = new OpenApiSchema
                        {
                            Type = JsonSchemaType.Object
                        }
                    }
                }
            };

            document.Workspace.RegisterComponents(document);
            
            // Add the reference to Address within Person after registration
            ((OpenApiSchema)document.Components.Schemas["Person"]).Properties = new Dictionary<string, IOpenApiSchema>
            {
                ["address"] = new OpenApiSchemaReference("Address", document)
            };

            document.Paths = new OpenApiPaths
            {
                ["/people"] = new OpenApiPathItem
                {
                    Operations = new Dictionary<System.Net.Http.HttpMethod, OpenApiOperation>
                    {
                        [System.Net.Http.HttpMethod.Get] = new OpenApiOperation
                        {
                            Responses = new OpenApiResponses
                            {
                                ["200"] = new OpenApiResponse
                                {
                                    Content = new Dictionary<string, OpenApiMediaType>
                                    {
                                        ["application/json"] = new OpenApiMediaType
                                        {
                                            Schema = new OpenApiSchemaReference("Person", document)
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var visitor = new SchemaReferencesCollectorVisitor();
            var walker = new OpenApiWalker(visitor);

            // Act
            walker.Walk(document);
            var references = visitor.GetVisitedSchemasReferences();

            // Assert - Should collect both Person and Address references
            Assert.Equal(2, references.Count);
            Assert.All(references, Assert.NotNull);
            Assert.All(references, Assert.NotEmpty);
        }

        [Fact]
        public void VisitorWorksWithEmptyDocument()
        {
            // Arrange
            var doc = new OpenApiDocument();
            var visitor = new SchemaReferencesCollectorVisitor();
            var walker = new OpenApiWalker(visitor);

            // Act
            walker.Walk(doc);
            var references = visitor.GetVisitedSchemasReferences();

            // Assert
            Assert.Empty(references);
        }
    }
}
