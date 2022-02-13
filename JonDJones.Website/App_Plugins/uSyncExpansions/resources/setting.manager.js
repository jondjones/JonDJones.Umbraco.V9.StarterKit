(function () {
    'use strict';

    function settingManager(overlayService, localizationService) {


        return {
            showAppSettings: showAppSettings
        };

        /////////

        function showAppSettings(titlekey, contentkey, settings) {

            localizationService.localizeMany([titlekey, contentkey])
                .then(function (value) {

                    var options = {
                        view: Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath + '/uSyncExpansions/settings/overlay.html',
                        title: value[0],
                        content: value[1],
                        settings: settings,
                        disableBackgroundClick: true,
                        disableEscKey: true,
                        hideSubmitButton: true,
                        submit: function () {
                            overlayService.close();
                        },
                        close: function () {
                            overlayService.close();
                        }
                    };

                    overlayService.open(options);
                });
        }
    };


    angular.module('umbraco')
        .factory('uSyncSettingManager', settingManager);
})();
