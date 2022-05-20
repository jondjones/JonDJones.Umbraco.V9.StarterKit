using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Actions;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Web.BackOffice.Trees;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.ModelBinders;
using Umbraco.Extensions;

namespace JonDJones.Core.SectionAndTree
{
    [Tree(
        "myCustomSection",
        "myCustomTree",
        TreeTitle = "My Custom Tree",
        TreeGroup = "group",
        SortOrder = 5)]
    [PluginController("myCustomSection")]
    public class MyCUstomTreeController : TreeController
    {

        private readonly IMenuItemCollectionFactory _menuItemCollectionFactory;

        public MyCUstomTreeController(
            ILocalizedTextService localizedTextService,
            UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection,
            IMenuItemCollectionFactory menuItemCollectionFactory,
            IEventAggregator eventAggregator)
            : base(localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
            _menuItemCollectionFactory = menuItemCollectionFactory;
        }

        protected override ActionResult<TreeNodeCollection> GetTreeNodes(
            string id,
            [ModelBinder(typeof(HttpQueryStringModelBinder))] FormCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();
            if (id == Constants.System.Root.ToInvariantString())
            {
                var node = CreateTreeNode(
                                    "myId",
                                    "-1",
                                    queryStrings,
                                    "Option One",
                                    "icon-list",
                                    false);

                nodes.Add(node);
            }

            return nodes;
        }

        protected override ActionResult<MenuItemCollection> GetMenuForNode(
            string id,
            [ModelBinder(typeof(HttpQueryStringModelBinder))] FormCollection queryStrings)
        {
            var menu = _menuItemCollectionFactory.Create();
            menu.Items.Add<ActionDelete>(LocalizedTextService, true, opensDialog: true);
            menu.Items.Add<ActionMove>(LocalizedTextService, true, opensDialog: true);
            menu.Items.Add<ActionSort>(LocalizedTextService, true, opensDialog: true);
            return menu;
        }
    }
}