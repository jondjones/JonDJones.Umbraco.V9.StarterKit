/*
 * displays status of the licence in a box, it doesn't turn 
 * licence functionality on or off, just tells you if you are
 */
(function () {
    'use strict';

    var licenceComponent = {
        templateUrl: Umbraco.Sys.ServerVariables.application.applicationPath + 'App_Plugins/uSyncExpansions/components/licence.html',
        bindings: {
            product: '<',
            version: '<'
        },
        controllerAs: 'vm',
        controller: licenceComponentController
    };

    function licenceComponentController(uSyncExpansionService) {

        var vm = this;
        vm.status = {};

        vm.$onInit = function () {
            uSyncExpansionService.getLicenceStatus(vm.product, vm.version)
                .then(function (result) {
                    vm.status = result.data;
                });
        };
    }

    angular.module('umbraco')
        .component('usyncExpansionLicence', licenceComponent);

})();