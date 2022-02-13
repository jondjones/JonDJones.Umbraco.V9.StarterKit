(function () {
    'use strict';

    function errorDialogController($scope) {

        var vm = this;

        vm.pageTitle = $scope.model.title;
        vm.message = $scope.model.message;
        vm.error = $scope.model.error;
        vm.close = close;

        function close() {
            if ($scope.model.close) {
                $scope.model.close();
            }
        }

    }

    angular.module('umbraco')
        .controller('uSyncErrorDialogController', errorDialogController);
})();