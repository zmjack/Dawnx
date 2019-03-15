using Dawnx.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NxPanel
{
    public static class NxPanelMenu
    {
        public static ClaimsMenu TopMenu = new ClaimsMenu()
        {
            ["Schedule"] = new ClaimsMenu(new ClaimsMenuNode
            {
                Url = "/StarAdmin",
                Tag = "New",
            }),
            ["Application"] = new ClaimsMenu(new ClaimsMenuNode
            {
                IconClass = "mdi mdi-elevation-rise",
                Url = "/NxPanel/UserStatus/Identity",
            }),
            ["Score"] = new ClaimsMenu(new ClaimsMenuNode
            {
                IconClass = "mdi mdi-bookmark-plus-outline",
                Url = "/StarAdmin",
            }),
        };

        public static ClaimsMenu SidebarMenu = new ClaimsMenu()
        {
            ["Dashboard"] = new ClaimsMenu(new ClaimsMenuNode
            {
                IconClass = "menu-icon mdi mdi-television",
                Url = "/modules/star-admin/index.html",
            }),
            ["Basic UI Elements"] = new ClaimsMenu(new ClaimsMenuNode
            {
                IconClass = "menu-icon mdi mdi-content-copy",
            })
            {
                ["Buttons"] = new ClaimsMenu(new ClaimsMenuNode
                {
                    Url = "/modules/star-admin/pages/ui-features/buttons.html",
                }),
                ["Typography"] = new ClaimsMenu(new ClaimsMenuNode
                {
                    Url = "/modules/star-admin/pages/ui-features/typography.html",
                }),
            },
            ["Form elements"] = new ClaimsMenu(new ClaimsMenuNode
            {
                IconClass = "menu-icon mdi mdi-backup-restore",
                Url = "/modules/star-admin/pages/forms/basic_elements.html",
            }),
            ["Charts"] = new ClaimsMenu(new ClaimsMenuNode
            {
                IconClass = "menu-icon mdi mdi-chart-line",
                Url = "/modules/star-admin/pages/charts/chartjs.html",
            }),
            ["Tables"] = new ClaimsMenu(new ClaimsMenuNode
            {
                IconClass = "menu-icon mdi mdi-table",
                Url = "/modules/star-admin/pages/tables/basic-table.html",
            }),
            ["Icons"] = new ClaimsMenu(new ClaimsMenuNode
            {
                IconClass = "menu-icon mdi mdi-sticker",
                Url = "/modules/star-admin/pages/icons/font-awesome.html",
            }),
        };
    }

}
