(function(){"use strict";function licenceComponentController(uSyncExpansionService){var vm=this;vm.status={};vm.$onInit=function(){uSyncExpansionService.getLicenceStatus(vm.product,vm.version).then(function(result){vm.status=result.data})}}var licenceComponent={templateUrl:Umbraco.Sys.ServerVariables.application.applicationPath+"App_Plugins/uSyncExpansions/components/licence.html",bindings:{product:"<",version:"<"},controllerAs:"vm",controller:licenceComponentController};angular.module("umbraco").component("usyncExpansionLicence",licenceComponent)})()