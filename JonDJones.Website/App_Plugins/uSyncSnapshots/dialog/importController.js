(function () {
    'use strict';

    function importController($scope, Upload) {

        var vm = this;

        vm.upload = upload;
        vm.handleFiles = handleFiles;
        vm.close = close; 

        function upload(file) {
            vm.buttonState = 'busy';
            Upload.upload({
                url: Umbraco.Sys.ServerVariables.uSyncSnapshots.snapshotService + 'uploadFile',
                fields: {},
                file: file
            }).success(function (data, status, headers, config) {
                vm.buttonState = 'success';
                submit();
            }).error(function (evt, status, headers, config) {
                vm.buttonState = 'error';
            });
        }






        function handleFiles(files, event) {
            if (files && files.length > 0) {
                vm.file = files[0];
                // vm.upload(files[0]);
            }
        }

        /////////////////

        function submit() {
            if ($scope.model.submit) {
                $scope.model.submit();
            }
        }

        function close() {
            if ($scope.model.close) {
                $scope.model.close();
            }
        }
    }

    angular.module('umbraco')
        .controller('uSyncSnapshotImportController', importController);
})();