(function () {
    'use strict';

    function snapshotDashboardController($scope, $timeout, navigationService) {

        var vm = this;

        vm.page = {
            title: 'uSync Snapshots',
            description: 'point in time views of umbraco setup',
            navigation: [
                {
                    'name': 'snapshots',
                    'alias': 'snapshots',
                    'icon': 'icon-flash',
                    'view': Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath + '/uSyncSnapshots/dashboard/default.html',
                    'active': true
                },
                {
                    'name': 'Create',
                    'alias': 'snapCreate',
                    'icon': 'icon-add',
                    'view': Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath + '/uSyncSnapshots/dashboard/create.html'
                },
                {
                    'name': 'settings',
                    'alias': 'snapsettings',
                    'icon': 'icon-settings',
                    'view': Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath + '/uSyncSnapshots/dashboard/settings.html'
                }
            ]
        };

        $timeout(function () {
            navigationService.syncTree({ tree: 'uSyncSnapshots', path: '-1' });
        });
    }

    angular.module('umbraco')
        .controller('uSyncSnapshotDashboardController', snapshotDashboardController);

})();