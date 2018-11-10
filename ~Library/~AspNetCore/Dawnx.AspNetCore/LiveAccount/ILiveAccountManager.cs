﻿using Dawnx.AspNetCore.LiveAccount.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;

namespace Dawnx.AspNetCore.LiveAccount
{
    public interface ILiveAccountManager
    {
        DbContext Context { get; }
        void SaveChanges();

        LiveAccountTransaction FastProcessing { get; }
        bool CheckAuthorization(ActionExecutingContext actionExecutingContext);

        #region
        DbSet<IdentityUser> Users { get; }
        DbSet<LiveRole> LiveRoles { get; }
        DbSet<LiveRoleOperation> LiveRoleOperations { get; }
        DbSet<LiveUserRole> LiveUserRoles { get; }
        DbSet<LiveOperation> LiveOperations { get; }
        DbSet<LiveOperationAction> LiveOperationActions { get; }
        DbSet<LiveAction> LiveActions { get; }
        #endregion

        #region LiveRole
        void CreateLiveRole(LiveRole model);
        void UpdateLiveRole(LiveRole model);
        void DeleteLiveRole(LiveRole model);
        #endregion

        #region LiveOperation
        void CreateOperation(LiveOperation model);
        void UpdateOperation(LiveOperation model);
        void DeleteOperation(LiveOperation model);
        #endregion

        #region LiveRoleUser
        void SetUserRoles(string userName, Guid[] liveRoleIds);
        bool UserInRole(string userName, Guid liveRoleId);
        LiveRole[] GetUserRoles(string userName);
        #endregion

        #region LiveRoleOperation
        void SetRoleOperations(Guid liveRoleId, Guid[] liveOperationIds);
        LiveOperation[] GetRoleOperations(Guid liveRoleId);
        #endregion

        #region LiveOperationAction
        void SetOperationActions(Guid liveOperationId, Guid[] liveActionIds);
        LiveAction[] GetOperationActions(Guid liveOperationId);
        #endregion

        #region LiveAction
        void SyncActions();
        void ClearInvalidActions();
        #endregion

    }
}
