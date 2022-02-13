(function () {
    'use strict';

    function itemManager($http) {

        return {
            getEntity: getEntity
        };

        function getEntity(treeItem) {
            return $http.post(getUrl('getEntity'), treeItem);
        }

        // private
        function getUrl(method) {
            return Umbraco.Sys.ServerVariables.uSyncComplete.itemManager + method;
        };
    };

    angular.module('umbraco')
        .factory('uSyncItemManager', itemManager);
})();