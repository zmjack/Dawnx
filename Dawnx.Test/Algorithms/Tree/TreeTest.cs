using System;
using System.Linq;
using Xunit;

namespace Dawnx.Algorithms.Tree.Test
{
    public class TreeTest
    {
        public class Entity : ITreeEntity
        {
            public Guid Id { get; set; }
            public long Index { get; set; }
            public Guid? Parent { get; set; }

            public string Name { get; set; }
        }

        public class EntityTree : Tree<EntityTree, Entity>
        {
            public override string Key { get => Model.Name; set => Model.Name = value; }
        }

        [Fact]
        public void Test1()
        {

            var tree11 = new Tree
            {
                ["A"] = new Tree
                {
                    ["A-a"] = new Tree
                    {
                        ["A-a-1"] = new Tree
                        {
                            ["A-a-1-i"] = new Tree()
                        }
                    },
                    ["A-b"] = new Tree()
                },
                ["B"] = new Tree()
            };


            var tree = new EntityTree
            {
                ["A"] = new EntityTree
                {
                    ["A-a"] = new EntityTree
                    {
                        ["A-a-1"] = new EntityTree
                        {
                            ["A-a-1-i"] = new EntityTree()
                        }
                    },
                    ["A-b"] = new EntityTree()
                },
                ["B"] = new EntityTree()
            };

            var tree2 = EntityTree.Create(new[]
            {
                new Entity
                {
                    Id = Guid.Parse("70548BCD-EDA3-4F6B-84BE-C5A665F9A4E6"), Name = "A"
                },
                new Entity
                {
                    Id = Guid.Parse("67DAD0E9-7F97-4B78-9137-EA59F16CE994"), Name = "A-a",
                    Parent = Guid.Parse("70548BCD-EDA3-4F6B-84BE-C5A665F9A4E6")
                },
                new Entity
                {
                    Id = Guid.Parse("3327EB26-2C9C-4047-9208-879971314B93"), Name = "A-a-1",
                    Parent = Guid.Parse("67DAD0E9-7F97-4B78-9137-EA59F16CE994")
                },
                new Entity
                {
                    Id = Guid.Parse("6BCA6A54-8DA6-4A0D-915A-90CFDC4D511B"), Name = "A-a-1-i",
                    Parent = Guid.Parse("3327EB26-2C9C-4047-9208-879971314B93")
                },
                new Entity
                {
                    Id = Guid.Parse("DE273E7A-B6B1-4E15-B25D-96151957B6CB"), Name = "A-b",
                    Parent = Guid.Parse("70548BCD-EDA3-4F6B-84BE-C5A665F9A4E6")
                },
                new Entity
                {
                    Id = Guid.Parse("DE33BC2D-4D7A-4997-8ABF-D23A1EE0B883"), Name = "B",
                },
            });

            CheckTree(tree);
            CheckTree(tree2);
        }

        private void CheckTree(EntityTree tree)
        {
            Assert.Equal(tree.Children.Select(x => x.Key), new[] { "A", "B" });
            Assert.Equal(tree["A"].Children.Select(x => x.Key), new[] { "A-a", "A-b" });
            Assert.Equal(tree["A"].Trees.Select(x => x.Key), new[] { "A-a" });
            Assert.Equal(tree["A"].Leafs.Select(x => x.Key), new[] { "A-b" });

            Assert.True(tree["A"].IsTree);
            Assert.True(tree["B"].IsLeaf);
            Assert.False(tree["A"].IsLeaf);
            Assert.False(tree["B"].IsTree);

            Assert.Equal(tree["A"].RecursiveChildren.Select(x => x.Key), new[] { "A-a", "A-a-1", "A-a-1-i", "A-b" });
            Assert.Equal(tree["A"].RecursiveTrees.Select(x => x.Key), new[] { "A-a", "A-a-1" });
            Assert.Equal(tree["A"].RecursiveLeafs.Select(x => x.Key), new[] { "A-a-1-i", "A-b" });
        }

    }
}
