(function () {
    'use strict';

    function expansionService($http) {

        var serviceRoot = Umbraco.Sys.ServerVariables.uSyncComplete.expansionService;
        
        var service = {
            getLicenceStatus: getLicenceStatus,
            getLicence: getLicence,
            saveLicence: saveLicence,
            isLicenced: isLicenced,

            importItems: importItems,
            reportItems: reportItems,
            exportItems: exportItems
        };

        return service;

        /////////////////
        function isLicenced(product, version) {
            return $http.get(serviceRoot + 'IsLicenced/?product=' + product + '&version=' + version)
        }

        function getLicenceStatus(product, version) {
            return $http.get(serviceRoot + 'GetLicenceStatus/?product=' + product + '&version=' + version);
        }

        function getLicence(product, version) {
            return $http.get(serviceRoot + 'GetLicence/?product=' + product + '&version=' + version);
        }

        function saveLicence(licence) {
            return $http.post(serviceRoot + 'SaveLicence', licence);
        }

        function importItems(entityTypes, force, clientId) {
            return $http.put(serviceRoot + 'importItems', {
                entityTypes: entityTypes,
                force: force,
                clientId: clientId
            });
        }

        function exportItems(entityTypes, clientId) {
            return $http.post(serviceRoot + 'exportItems', {
                entityTypes: entityTypes,
                clientId: clientId
            });
        }

        function reportItems(entityTypes, clientId) {
            return $http.post(serviceRoot + 'reportItems', {
                entityTypes: entityTypes,
                clientId: clientId
            });
        }
    }

    angular.module('umbraco')
        .factory('uSyncExpansionService', expansionService);
})();