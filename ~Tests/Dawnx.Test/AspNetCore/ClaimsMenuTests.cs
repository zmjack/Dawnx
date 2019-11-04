using NStandard;
using Dawnx.Algorithms.Tree;
using System;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace Dawnx.AspNetCore.Test
{
    public class ClaimsMenuTests
    {
        [Fact]
        public void Test1()
        {
            var navMenu1 = new ClaimsMenu<MyMenuEntity>
            {
                ["1"] = new ClaimsMenu<MyMenuEntity>
                {
                    ["1-1"] = new ClaimsMenu<MyMenuEntity>(new MyMenuEntity { Users = "user1", Roles = "manager1" }),
                    ["1-2"] = new ClaimsMenu<MyMenuEntity>(new MyMenuEntity { Users = "user2", Roles = "manager1" }),
                },
                ["2"] = new ClaimsMenu<MyMenuEntity>
                {
                    ["2-1"] = new ClaimsMenu<MyMenuEntity>(new MyMenuEntity { Users = "user1", Roles = "manager2" }),
                    ["2-2"] = new ClaimsMenu<MyMenuEntity>(new MyMenuEntity { Users = "user2", Roles = "manager2" }),
                },
                ["3"] = new ClaimsMenu<MyMenuEntity>(new MyMenuEntity { Users = null, Roles = "manager1" }),
            };
            var navMenu2 = new ClaimsMenu<MyMenuEntity>().Then(_ =>
            {
                _.AddRange(new[]
                {
                    new ClaimsMenu<MyMenuEntity>(new MyMenuEntity { Name = "1" }).Then(__ =>
                    {
                        __.AddRange(new[]
                        {
                            new ClaimsMenu<MyMenuEntity>(new MyMenuEntity { Name = "1-1", Users = "user1", Roles = "manager1" }),
                            new ClaimsMenu<MyMenuEntity>(new MyMenuEntity { Name = "1-2", Users = "user2", Roles = "manager1" }),
                        });
                    }),
                    new ClaimsMenu<MyMenuEntity>(new MyMenuEntity { Name = "2" }).Then(__ =>
                    {
                        __.AddRange(new[]
                        {
                            new ClaimsMenu<MyMenuEntity>(new MyMenuEntity { Name = "2-1", Users = "user1", Roles = "manager2" }),
                            new ClaimsMenu<MyMenuEntity>(new MyMenuEntity { Name = "2-2", Users = "user2", Roles = "manager2" }),
                        });
                    }),
                    new ClaimsMenu<MyMenuEntity>(new MyMenuEntity { Name = "3", Users = null, Roles = "manager1" }),
                });
            });
            var navMenu3 = ClaimsMenu<MyMenuEntity>.Create(new[]
            {
                new MyMenuEntity
                {
                    Id = Guid.Parse("90FF77BB-D33D-47AB-9BF3-BC8937DF6940"), Name = "1",
                },
                new MyMenuEntity
                {
                    Id = Guid.Parse("36A886C1-6ACC-4F86-BFF9-F455DCE27E3F"), Name = "2",
                },
                new MyMenuEntity
                {
                    Id = Guid.Parse("721D52F0-9768-4E19-8988-7EE6657433CF"), Name = "1-1",
                    Users = "user1", Roles = "manager1",
                    Parent = Guid.Parse("90FF77BB-D33D-47AB-9BF3-BC8937DF6940"),
                },
                new MyMenuEntity
                {
                    Id = Guid.Parse("F10529CD-B998-4F81-9FC8-37D417A51997"), Name = "1-2",
                    Users = "user2", Roles = "manager1",
                    Parent = Guid.Parse("90FF77BB-D33D-47AB-9BF3-BC8937DF6940"),
                },
                new MyMenuEntity
                {
                    Id = Guid.Parse("33233ED9-16E1-417C-BCB2-E6F8D4C264DF"), Name = "2-1",
                    Users = "user1", Roles = "manager2",
                    Parent = Guid.Parse("36A886C1-6ACC-4F86-BFF9-F455DCE27E3F"),
                },
                new MyMenuEntity
                {
                    Id = Guid.Parse("5300D375-338A-4C0F-915E-578A4DF7C8CF"), Name = "2-2",
                    Users = "user2", Roles = "manager2",
                    Parent = Guid.Parse("36A886C1-6ACC-4F86-BFF9-F455DCE27E3F"),
                },
                new MyMenuEntity
                {
                    Id = Guid.Parse("BA8087DC-E5F5-4207-836D-DA1C605354E5"), Name = "3",
                    Users = null, Roles = "manager1",
                },
            });

            Assert.Equal(Guid.Parse("BA8087DC-E5F5-4207-836D-DA1C605354E5"), navMenu3.Children.Last().Model.Id);

            Validate(navMenu1);
            Validate(navMenu2);
            Validate(navMenu3);
        }

        private class MyMenuEntity : ITreeEntity, IClaimsPermission, INameable
        {
            public string Name { get; set; }

            //IClaimsPermission
            public string Users { get; set; }
            public string Roles { get; set; }

            //ITreeEntity
            public Guid Id { get; set; }
            public long Index { get; set; }
            public Guid? Parent { get; set; }
        }

        private void Validate<TModel>(ClaimsMenu<TModel> menu)
            where TModel : INameable, IClaimsPermission, new()
        {
            Assert.Equal(menu["1"], menu.Children.First(tree => tree.Key == "1"));

            var user1 = CreateUser("user1", "");
            var user2 = CreateUser("user2", "manager2");
            var manager1 = CreateUser("manager1", "manager1");
            var manager2 = CreateUser("manager2", "manager2");

            var root = menu.Description;
            var tree_user1 = user1.Menu(menu).Description;
            var tree_user2 = user2.Menu(menu).Description;
            var tree_manager1 = manager1.Menu(menu).Description;
            var tree_manager2 = manager2.Menu(menu).Description;

            Assert.Equal("1\r\n  1-1\r\n2\r\n  2-1", tree_user1);
            Assert.Equal("1\r\n  1-2\r\n2\r\n  2-1\r\n  2-2", tree_user2);
            Assert.Equal("1\r\n  1-1\r\n  1-2\r\n3", tree_manager1);
            Assert.Equal("2\r\n  2-1\r\n  2-2", tree_manager2);
        }

        private ClaimsPrincipal CreateUser(string username, string role)
        {
            var identity = new ClaimsIdentity() { };
            identity.AddClaims(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
            });
            return new ClaimsPrincipal(identity);
        }

    }
}
