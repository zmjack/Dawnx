using Dawnx.Algorithms.Tree;
using System.Linq;
using System.Security.Claims;

namespace Dawnx.AspNetCore
{
    public class ClaimsMenu : ClaimsMenu<ClaimsMenuNode>
    {
        public ClaimsMenu() : base() { }
        public ClaimsMenu(ClaimsMenuNode model) : base(model) { }
    }

    public class ClaimsMenu<TModel> : Tree<ClaimsMenu<TModel>, TModel>
        where TModel : IClaimsPermission, INameable, new()
    {
        public ClaimsMenu() : base() { }
        public ClaimsMenu(TModel model) : base(model) { }

        public override string Key { get => Model.Name; set => Model.Name = value; }

        public bool IsUserNode(ClaimsPrincipal user)
        {
            var users = Model.GetUsers();
            var roles = Model.GetRoles();

            if (!Children.Any())
                return (!roles.Any() && !users.Any())
                    || (roles?.Any(role => user.IsInRole(role)) ?? false)
                    || (users?.Contains(user.Identity.Name) ?? false);
            else return Children.Any(subTree => subTree.IsUserNode(user));
        }

        /// <summary>
        /// Gets user's menu from the tree.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ClaimsMenu<TModel> UserMenu(ClaimsPrincipal user)
        {
            var userMenu = new ClaimsMenu<TModel>();
            CopyToUserMenu(user, this, ref userMenu);
            return userMenu;
        }

        // TODO: May optimize.
        private void CopyToUserMenu(ClaimsPrincipal user, ClaimsMenu<TModel> node, ref ClaimsMenu<TModel> clonedNode)
        {
            if (node.IsUserNode(user))
            {
                clonedNode = node.Clone();
                {
                    foreach (var subTree in node.Children.Where(_subTree => _subTree.IsUserNode(user)))
                    {
                        var clonedSubTree = new ClaimsMenu<TModel>();
                        CopyToUserMenu(user, subTree, ref clonedSubTree);
                        clonedNode.Add(clonedSubTree);
                    }
                }
            }
        }

    }

}
