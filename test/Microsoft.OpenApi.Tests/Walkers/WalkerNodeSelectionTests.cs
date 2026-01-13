// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Microsoft.OpenApi.Tests.Walkers
{
    public class WalkerNodeSelectionTests
    {
        [Fact]
        public void WalkerWithNullNodesVisitsAllNodes()
        {
            // Arrange
            var doc = new OpenApiDocument
            {
                Info = new OpenApiInfo { Title = "Test" },
                Paths = new OpenApiPaths
                {
                    ["/test"] = new OpenApiPathItem()
                },
                Components = new OpenApiComponents()
            };

            var visitor = new CountingVisitor();
            var walker = new OpenApiWalker(visitor, nodesToVisit: null);

            // Act
            walker.Walk(doc);

            // Assert - should visit all major nodes
            Assert.True(visitor.VisitedInfo);
            Assert.True(visitor.VisitedPaths);
            Assert.True(visitor.VisitedComponents);
        }

        [Fact]
        public void WalkerWithEmptyNodesVisitsAllNodes()
        {
            // Arrange
            var doc = new OpenApiDocument
            {
                Info = new OpenApiInfo { Title = "Test" },
                Paths = new OpenApiPaths
                {
                    ["/test"] = new OpenApiPathItem()
                },
                Components = new OpenApiComponents()
            };

            var visitor = new CountingVisitor();
            var walker = new OpenApiWalker(visitor, nodesToVisit: []);

            // Act
            walker.Walk(doc);

            // Assert - empty array should visit all
            Assert.True(visitor.VisitedInfo);
            Assert.True(visitor.VisitedPaths);
            Assert.True(visitor.VisitedComponents);
        }

        [Fact]
        public void WalkerSelectsOnlyInfoNode()
        {
            // Arrange
            var doc = new OpenApiDocument
            {
                Info = new OpenApiInfo { Title = "Test" },
                Paths = new OpenApiPaths
                {
                    ["/test"] = new OpenApiPathItem()
                },
                Components = new OpenApiComponents(),
                Tags = new System.Collections.Generic.HashSet<OpenApiTag>
                {
                    new OpenApiTag { Name = "Test" }
                }
            };

            var visitor = new CountingVisitor();
            var walker = new OpenApiWalker(visitor, 
                [d => d.Info]);

            // Act
            walker.Walk(doc);

            // Assert
            Assert.True(visitor.VisitedInfo);
            Assert.False(visitor.VisitedPaths);
            Assert.False(visitor.VisitedComponents);
            Assert.False(visitor.VisitedTags);
        }

        [Fact]
        public void WalkerSelectsMultipleNodes()
        {
            // Arrange
            var doc = new OpenApiDocument
            {
                Info = new OpenApiInfo { Title = "Test" },
                Paths = new OpenApiPaths
                {
                    ["/test"] = new OpenApiPathItem()
                },
                Components = new OpenApiComponents(),
                Tags = new System.Collections.Generic.HashSet<OpenApiTag>
                {
                    new OpenApiTag { Name = "Test" }
                }
            };

            var visitor = new CountingVisitor();
            var walker = new OpenApiWalker(visitor,
                [
                    d => d.Info,
                    d => d.Paths,
                    d => d.Tags
                ]);

            // Act
            walker.Walk(doc);

            // Assert
            Assert.True(visitor.VisitedInfo);
            Assert.True(visitor.VisitedPaths);
            Assert.False(visitor.VisitedComponents);
            Assert.True(visitor.VisitedTags);
        }

        [Fact]
        public void WalkerSelectsPathsButNotComponents()
        {
            // Arrange
            var doc = new OpenApiDocument
            {
                Paths = new OpenApiPaths
                {
                    ["/test"] = new OpenApiPathItem()
                },
                Components = new OpenApiComponents
                {
                    Schemas = new Dictionary<string, IOpenApiSchema>
                    {
                        ["Pet"] = new OpenApiSchema { Type = JsonSchemaType.Object }
                    }
                }
            };

            var visitor = new CountingVisitor();
            var walker = new OpenApiWalker(visitor,
                [d => d.Paths]);

            // Act
            walker.Walk(doc);

            // Assert
            Assert.True(visitor.VisitedPaths);
            Assert.False(visitor.VisitedComponents);
        }

        [Fact]
        public void WalkerSelectsComponentsWithSchemas()
        {
            // Arrange
            var doc = new OpenApiDocument
            {
                Components = new OpenApiComponents
                {
                    Schemas = new Dictionary<string, IOpenApiSchema>
                    {
                        ["Pet"] = new OpenApiSchema { Type = JsonSchemaType.Object }
                    }
                }
            };

            var visitor = new CountingVisitor();
            var walker = new OpenApiWalker(visitor,
                [d => d.Components!]);

            // Act
            walker.Walk(doc);

            // Assert
            Assert.True(visitor.VisitedComponents);
        }

        [Fact]
        public void WalkerSelectionCachesExpressions()
        {
            // Arrange
            var doc = new OpenApiDocument
            {
                Info = new OpenApiInfo { Title = "Test" },
                Paths = new OpenApiPaths()
            };

            var visitor = new CountingVisitor();
            var nodesToVisit = new System.Linq.Expressions.Expression<Func<OpenApiDocument, object>>[]
            {
                d => d.Info
            };
            var walker = new OpenApiWalker(visitor, nodesToVisit);

            // Act
            walker.Walk(doc);

            // Assert - visitor should have been called and selection worked
            Assert.True(visitor.VisitedInfo);
        }

        [Fact]
        public void WalkerAllowsMultipleWalks()
        {
            // Arrange
            var doc = new OpenApiDocument
            {
                Info = new OpenApiInfo { Title = "Test" },
                Paths = new OpenApiPaths(),
                Components = new OpenApiComponents()
            };

            var visitor = new CountingVisitor();
            var walker = new OpenApiWalker(visitor,
                [d => d.Info, d => d.Paths]);

            // Act
            walker.Walk(doc);
            int visitedCount = visitor.DocumentVisitCount;
            
            walker.Walk(doc);

            // Assert - should visit again
            Assert.Equal(2, visitor.DocumentVisitCount);
        }

        private class CountingVisitor : OpenApiVisitorBase
        {
            public bool VisitedInfo { get; private set; }
            public bool VisitedPaths { get; private set; }
            public bool VisitedComponents { get; private set; }
            public bool VisitedTags { get; private set; }
            public int DocumentVisitCount { get; private set; }

            public override void Visit(OpenApiDocument doc)
            {
                DocumentVisitCount++;
                base.Visit(doc);
            }

            public override void Visit(OpenApiInfo info)
            {
                VisitedInfo = true;
                base.Visit(info);
            }

            public override void Visit(OpenApiPaths paths)
            {
                VisitedPaths = true;
                base.Visit(paths);
            }

            public override void Visit(OpenApiComponents components)
            {
                VisitedComponents = true;
                base.Visit(components);
            }

            public override void Visit(ISet<OpenApiTag> tags)
            {
                VisitedTags = true;
                base.Visit(tags);
            }
        }
    }
}
