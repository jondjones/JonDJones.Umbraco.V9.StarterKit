(function () {
    'use strict';

    var errorComponent = {
        templateUrl: Umbraco.Sys.ServerVariables.application.applicationPath + 'App_Plugins/uSyncExpansions/components/error.html',
        bindings: {
            error: '<',
            title: '@',
        },
        controllerAs: 'vm',
        controller: errorController
    };

    function errorController(editorService) {
        var vm = this; 

        vm.openErrorDialog = openErrorDialog;

        function openErrorDialog() {

            var options = {
                error: vm.error,
                title: 'Error ' + vm.title,
                message: vm.message,
                view: Umbraco.Sys.ServerVariables.application.applicationPath + 'App_Plugins/uSyncExpansions/components/errordialog.html',
                close: function () {
                    editorService.close();
                }
            };

            editorService.open(options);
        }
    }

    angular.module('umbraco')
        .component('usyncErrorBox', errorComponent);
})();