﻿using Dawnx.AspNetCore.LiveAccount.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Dawnx.AspNetCore.LiveAccount
{
    public partial class LiveAccountManager<TDbContext> : ILiveAccountManager
        where TDbContext : IdentityDbContext, ILiveAccountDbContext
    {
        public DbContext Context => _context;
        public void SaveChanges() => _context.SaveChanges();

        public DbSet<IdentityUser> Users => _context.Users;
        public DbSet<LiveRole> LiveRoles => _context.LiveRoles;
        public DbSet<LiveRoleOperation> LiveRoleOperations => _context.LiveRoleOperations;
        public DbSet<LiveUserRole> LiveUserRoles => _context.LiveUserRoles;
        public DbSet<LiveOperation> LiveOperations => _context.LiveOperations;
        public DbSet<LiveOperationAction> LiveOperationActions => _context.LiveOperationActions;
        public DbSet<LiveAction> LiveActions => _context.LiveActions;

        public void SetRoleOperations(Guid liveAction, Guid liveOperation)
        {
            LiveOperationActions.Add(new LiveOperationAction
            {
                Action = liveAction,
                Operation = liveOperation,
            });
            SaveChanges();
        }

        public void ClearInvalidActions()
        {
            LiveActions.RemoveRange(LiveActions.Where(x => !x.IsExisted));
            SaveChanges();
        }

        public void CreateOperation(LiveOperation model)
        {
            LiveOperations.Add(model);
            SaveChanges();
        }

        public void CreateLiveRole(LiveRole model)
        {
            LiveRoles.Add(model);
            SaveChanges();
        }

        public void DeleteOperation(LiveOperation model)
        {
            LiveOperations.Remove(model);
            SaveChanges();
        }

        public void DeleteLiveRole(LiveRole model)
        {
            LiveRoles.Remove(model);
            SaveChanges();
        }

        public void SyncActions()
        {
            LiveActions.Each(x => x.IsExisted = false);
            SaveChanges();

            var controllerTypes = Assembly.GetEntryAssembly().GetTypesWhichExtends<Controller>(true);
            foreach (var controllerType in controllerTypes)
            {
                var areaAttr = controllerType.GetCustomAttribute<AreaAttribute>();

                var liveActions = controllerType
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .Select(method =>
                    {
                        var liveAuthorizeAttr = method.GetCustomAttribute<LiveAuthorizeAttribute>();
                        return new LiveAction
                        {
                            Area = areaAttr?.RouteValue,
                            Controller = controllerType.Name.Project("^(.+?)(?:Controller)?$"),
                            Action = method.Name,
                            IsExisted = true,
                            IsEnabled = liveAuthorizeAttr != null,
                        };
                    })
                    .Distinct(x => x.Name);

                foreach (var liveAction in liveActions)
                {
                    var find = LiveActions.FirstOrDefault(x =>
                        x.Area == liveAction.Area
                        && x.Controller == liveAction.Controller
                        && x.Action == liveAction.Action);

                    if (find != null)
                    {
                        find.IsExisted = liveAction.IsExisted;
                        find.IsEnabled = liveAction.IsEnabled;
                    }
                    else LiveActions.Add(liveAction);
                }
                SaveChanges();
            }
        }

        public void UpdateOperation(LiveOperation model)
        {
            LiveOperations.Update(model);
            SaveChanges();
        }

        public void UpdateLiveRole(LiveRole model)
        {
            LiveRoles.Update(model);
            SaveChanges();
        }

        public bool UserInRole(string userName, Guid liveRole)
        {
            var normalizedUserName = userName.ToUpper();
            var userId = Users.First(x => x.NormalizedUserName == normalizedUserName).Id;

            return LiveUserRoles.Any(x => x.User == userId && x.Role == liveRole);
        }

        public LiveRole[] GetUserRoles(string userName)
        {
            var normalizedUserName = userName.ToUpper();
            var userId = Users.First(x => x.NormalizedUserName == normalizedUserName).Id;

            return LiveUserRoles
                .Include(x => x.RoleLink)
                .Where(x => x.User == userId).Select(x => x.RoleLink).ToArray();
        }

        public void SetUserRoles(string userName, Guid[] liveRoleIds)
        {
            var normalizedUserName = userName.ToUpper();
            var userId = Users.First(x => x.NormalizedUserName == normalizedUserName).Id;

            foreach (var liveRole in LiveRoles.ToArray())
            {
                var find = LiveUserRoles.Where(x => x.User == userId && x.Role == liveRole.Id);

                if (find.Any() && !liveRole.Id.In(liveRoleIds))
                {
                    LiveUserRoles.RemoveRange(find);
                    continue;
                }

                if (!find.Any() && liveRole.Id.In(liveRoleIds))
                {
                    LiveUserRoles.Add(new LiveUserRole
                    {
                        User = userId,
                        Role = liveRole.Id,
                    });
                    continue;
                }
            }
            SaveChanges();
        }

        public void SetRoleOperations(Guid liveRoleId, Guid[] liveOperationIds)
        {
            foreach (var liveOperation in LiveOperations.ToArray())
            {
                var find = LiveRoleOperations.Where(x => x.Role == liveRoleId && x.Operation == liveOperation.Id);

                if (find.Any() && !liveOperation.Id.In(liveOperationIds))
                {
                    LiveRoleOperations.RemoveRange(find);
                    continue;
                }

                if (!find.Any() && liveOperation.Id.In(liveOperationIds))
                {
                    LiveRoleOperations.Add(new LiveRoleOperation
                    {
                        Role = liveRoleId,
                        Operation = liveOperation.Id,
                    });
                    continue;
                }
            }
            SaveChanges();
        }

        public LiveOperation[] GetRoleOperations(Guid liveRoleId)
        {
            return LiveRoleOperations
                .Where(x => x.Role == liveRoleId)
                .Select(x => x.OperationLink)
                .ToArray();
        }

        public void SetOperationActions(Guid liveOperationId, Guid[] liveActionIds)
        {
            foreach (var liveAction in LiveActions.ToArray())
            {
                var find = LiveOperationActions.Where(x => x.Operation == liveOperationId && x.Action == liveAction.Id);

                if (find.Any() && !liveAction.Id.In(liveActionIds))
                {
                    LiveOperationActions.RemoveRange(find);
                    continue;
                }

                if (!find.Any() && liveAction.Id.In(liveActionIds))
                {
                    LiveOperationActions.Add(new LiveOperationAction
                    {
                        Operation = liveOperationId,
                        Action = liveAction.Id,
                    });
                    continue;
                }
            }
            SaveChanges();
        }

        public LiveAction[] GetOperationActions(Guid liveOperationId)
        {
            return LiveOperationActions
                .Where(x => x.Operation == liveOperationId)
                .Select(x => x.ActionLink)
                .ToArray();
        }
    }
}
