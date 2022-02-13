(function () {
    'use strict';

    function licenceController($scope,
        notificationsService,
        uSyncExpansionService) {

        var vm = this;
        vm.loading = true;
        vm.save = save;

        vm.buttonState = 'init';

        init();

        function init() {
            vm.loading = true;

            uSyncExpansionService.getLicenceStatus('complete', '8.2.0')
                .then(function (result) {
                    vm.status = result.data;
                });

            uSyncExpansionService.getLicence('complete', '8.2.0')
                .then(function (result) {
                    vm.licence = result.data;
                    vm.loading = false;
                });
        }

        function save() {
            vm.buttonState = 'busy';
            uSyncExpansionService.saveLicence(vm.licence)
                .then(function (result) {
                    notificationsService.success('Saved', "Licence Info Saved");
                    vm.buttonState = 'success';
                    init();
                }, function (error) {
                    notificationsService.error('Error', "Failed to save" + error.data.ExceptionMessage);
                    vm.buttonState = 'error';

                });
        }

    }

    angular.module('umbraco')
        .controller('uSyncLicenceController', licenceController);
})();