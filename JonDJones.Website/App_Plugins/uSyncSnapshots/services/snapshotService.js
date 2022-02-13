(function () {
    'use strict';

    function snapshotService($http, umbRequestHelper) {

        var serviceRoot = Umbraco.Sys.ServerVariables.uSyncSnapshots.snapshotService;

        return {
            enabled: enabled,

            getSnapshots: getSnapshots,
            createSnapshot: createSnapshot,

            report: report,
            reportAll: reportAll,

            apply: apply,
            applyAll: applyAll,

            remove: remove,
            download: download,
            downloadAll: downloadAll,

            getSettings: getSettings,
            saveSettings: saveSettings
        };

        /////////////

        function enabled() {
            return $http.get(serviceRoot + 'Enabled');
        }

        function createSnapshot(name, includeFolders, groupName, clientId) {
            return $http.put(serviceRoot + 'CreateSnapshot', {
                name: name, 
                includeFolders: includeFolders,
                group: groupName,
                clientId: clientId
            });
        }

        function getSnapshots() {
            return $http.get(serviceRoot + 'GetSnapshots');
        }

        function remove(alias) {
            return $http.delete(serviceRoot + 'Remove/?alias=' + alias);
        }

        function download(alias) {
            var url = serviceRoot + 'ZipSnapshot/?alias=' + alias;
            return umbRequestHelper.downloadFile(url);
        }

        function downloadAll() {
            var url = serviceRoot + 'ZipAll';
            return umbRequestHelper.downloadFile(url);
        }

        ///
        function report(alias, clientId) {
            return $http.get(serviceRoot + 'Report/?alias=' + alias + '&clientId=' + clientId);
        }

        function reportAll(clientId) {
            return $http.get(serviceRoot + 'ReportAll/?clientId=' + clientId);
        }

        function apply(alias, clientId) {
            return $http.post(serviceRoot + 'Apply/', { Alias: alias, clientId: clientId });
        }

        function applyAll(clientId) {
            return $http.post(serviceRoot + 'ApplyAll/', { clientId: clientId });
        }

        ////
        function getSettings() {
            return $http.get(serviceRoot + 'GetSettings');
        }

        function saveSettings(settings) {
            return $http.post(serviceRoot + 'SaveSettings', settings);
        }
    }

    angular.module('umbraco')
        .factory('uSyncSnapshotService', snapshotService);
})();