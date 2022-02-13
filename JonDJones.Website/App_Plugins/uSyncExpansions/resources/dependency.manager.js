(function () {
    'use strict';

    function dependencyManager() {

        var dependencyFlags = {
            none: 0,
            includeChildren: 2,
            includeAncestors: 4,
            includeDependencies: 8,
            includeFiles: 16,
            includeMedia: 32,
            includeLinked: 64,
            includeMediaFiles: 128,
            includeConfig: 256,
            adjacentOnly: 512
        };

        return {
            getFlags: getFlags,
            getOptionsFlags: getOptionsFlags,

            createBatches: createBatches
        };

        function getFlags(options) {

            var flags = 0;
            if (options.includeChildren) { flags |= dependencyFlags.includeChildren; }
            if (options.includeAncestors) { flags |= dependencyFlags.includeAncestors; }
            if (options.includeDependencies) { flags |= dependencyFlags.includeDependencies; }
            if (options.includeFiles) { flags |= dependencyFlags.includeFiles; }
            if (options.includeMedia) { flags |= dependencyFlags.includeMedia; }
            if (options.includeLinked) { flags |= dependencyFlags.includeLinked; }
            if (options.includeMediaFiles) { flags |= dependencyFlags.includeMediaFiles; }
            if (options.includeConfig) { flags |= dependencyFlags.includeConfig; }
            if (options.AdjacentOnly) { flags |= dependencyFlags.AdjacentOnly; }

            return flags;
        }

        function getOptionsFlags(options) {

            var flags = 0;
            if (options.includeChildren?.value) { flags |= dependencyFlags.includeChildren; }
            if (options.includeAncestors?.value) { flags |= dependencyFlags.includeAncestors; }
            if (options.includeDependencies?.value) { flags |= dependencyFlags.includeDependencies; }
            if (options.includeFiles?.value) { flags |= dependencyFlags.includeFiles; }
            if (options.includeMedia?.value) { flags |= dependencyFlags.includeMedia; }
            if (options.includeLinked?.value) { flags |= dependencyFlags.includeLinked; }
            if (options.includeMediaFiles?.value) { flags |= dependencyFlags.includeMediaFiles; }
            if (options.includeConfig?.value) { flags |= dependencyFlags.includeConfig; }
            if (options.AdjacentOnly?.value) { flags |= dependencyFlags.AdjacentOnly; }

            return flags;
        }


        function createBatches(items, size) {
            var batches = [];
            var count = Math.ceil(items.length / size);
            for (let b = 0; b < count; b++) {
                let batch = items.slice(b * size, (b + 1) * size);
                batches.push(batch);
            }
            return batches;
        }


    }

    angular.module('umbraco')
        .factory('uSyncDependencyManager', dependencyManager);
})();