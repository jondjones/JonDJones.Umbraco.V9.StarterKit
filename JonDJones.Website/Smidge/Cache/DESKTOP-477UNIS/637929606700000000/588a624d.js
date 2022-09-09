!function(){"use strict";angular.module("umbraco").controller("uSyncChangeDialogController",function($scope,assetsService){function getTypeName(typeName){return typeName.substring(typeName.lastIndexOf(".")+1)}var vm=this;vm.item=$scope.model.item;assetsService.loadJs("lib/jsdiff/diff.min.js",$scope).then(function(){vm.item.details.forEach(function(detail){let oldValueDiff=null===detail.oldValue?"":detail.oldValue,newValueDiff=null===detail.newValue?"":detail.newValue;detail.oldValueJson instanceof Object&&(oldValueDiff=JSON.stringify(detail.oldValue,null,1));detail.newValueJson instanceof Object&&(newValueDiff=JSON.stringify(detail.newValue,null,1));detail.diff=JsDiff.diffWords(oldValueDiff,newValueDiff)})});vm.close=function(){$scope.model.close&&$scope.model.close()};vm.getTypeName=getTypeName;vm.pageTitle=function(){return vm.item.change+" "+getTypeName(vm.item.itemType)+" "+vm.item.name}})}(),function(){"use strict";angular.module("umbraco.resources").factory("uSyncHub",function($rootScope,$q,assetsService){function hubSetup(callback){$.connection=(new signalR.HubConnectionBuilder).withUrl(Umbraco.Sys.ServerVariables.uSync.signalRHub).withAutomaticReconnect().configureLogging(signalR.LogLevel.Warning).build();var hub={},hub=void 0!==$.connection?{active:!0,start:function(cb){try{$.connection.start().then(function(){cb&&cb(!0)}).catch(function(){console.warn("Failed to start hub");cb&&cb(!1)})}catch(e){console.warn("Could not setup signalR connection",e);cd&&cb(!1)}},on:function(eventName,callback){$.connection.on(eventName,function(result){$rootScope.$apply(function(){callback&&callback(result)})})},invoke:function(methodName,callback){$.connection.invoke(methodName).done(function(result){$rootScope.$apply(function(){callback&&callback(result)})})}}:{on:function(){},invoke:function(){},start:function(){console.warn("no hub to start - missing signalR library ?")}};return callback(hub)}var starting=!1,callbacks=[],scripts=[Umbraco.Sys.ServerVariables.umbracoSettings.umbracoPath+"/lib/signalr/signalr.min.js"];return{initHub:function(callback){if(callbacks.push(callback),!starting)if(void 0===$.connection){starting=!0;var promises=[];scripts.forEach(function(script){promises.push(assetsService.loadJs(script))});$q.all(promises).then(function(){for(;callbacks.length;)hubSetup(callbacks.pop());starting=!1})}else{for(;callbacks.length;)hubSetup(callbacks.pop());starting=!1}}}})}(),function(){"use strict";angular.module("umbraco.services").factory("uSync8DashboardService",function($http){var serviceRoot=Umbraco.Sys.ServerVariables.uSync.uSyncService;return{getSettings:function(){return $http.get(serviceRoot+"GetSettings")},getChangedSettings:function(){return $http.get(serviceRoot+"GetChangedSettings")},getHandlers:function(){return $http.get(serviceRoot+"GetHandlers")},getHandlerSetSettings:function(set){return $http.get(serviceRoot+"GetHandlerSetSettings?id="+set)},report:function(group,clientId){return $http.post(serviceRoot+"report",{clientId:clientId,group:group})},exportItems:function(clientId,clean){return $http.post(serviceRoot+"export",{clientId:clientId,clean:clean})},importItems:function(force,group,clientId){return $http.put(serviceRoot+"import",{force:force,group:group,clientId:clientId})},importItem:function(item){return $http.put(serviceRoot+"importItem",item)},saveSettings:function(settings){return $http.post(serviceRoot+"savesettings",settings)},getActionHandlers:function(options){return $http.post(serviceRoot+"GetActionHandlers?action="+options.action,{group:options.group})},reportHandler:function(handler,options,clientId){return $http.post(serviceRoot+"ReportHandler",{handler:handler,clientId:clientId})},importHandler:function(handler,options,clientId){return $http.post(serviceRoot+"ImportHandler",{handler:handler,clientId:clientId,force:options.force})},importPost:function(actions,options,clientId){return $http.post(serviceRoot+"ImportPost",{actions:actions,clientId:clientId})},exportHandler:function(handler,options,clientId){return $http.post(serviceRoot+"ExportHandler",{handler:handler,clientId:clientId})},cleanExport:function(){return $http.post(serviceRoot+"cleanExport")},startProcess:function(action){return $http.post(serviceRoot+"StartProcess?action="+action)},finishProcess:function(action,actions){return $http.post(serviceRoot+"FinishProcess?action="+action,actions)},getLoadedHandlers:function(){return $http.get(serviceRoot+"GetLoadedHandlers")},getAddOns:function(){return $http.get(serviceRoot+"GetAddOns")},getAddOnSplash:function(){return $http.get(serviceRoot+"GetAddOnSplash")},getHandlerGroups:function(){return $http.get(serviceRoot+"GetHandlerGroups")},getSyncWarnings:function(action,group){return $http.post(serviceRoot+"GetSyncWarnings?action="+action,{group:group})},checkVersion:function(){return $http.get(serviceRoot+"CheckVersion")}}})}(),function(){"use strict";var uSyncProgressViewComponent={templateUrl:Umbraco.Sys.ServerVariables.application.applicationPath+"App_Plugins/uSync/components/usync.progressview.html",bindings:{status:"<",update:"<",hideLabels:"<"},controllerAs:"vm",controller:function(){this.calcPercentage=function(status){return void 0!==status?100*status.count/status.total:1}}};angular.module("umbraco").component("usyncProgressView",uSyncProgressViewComponent)}(),function(){"use strict";var uSyncReportViewComponent={templateUrl:Umbraco.Sys.ServerVariables.application.applicationPath+"App_Plugins/uSync/components/usync.reportview.html",bindings:{action:"<",results:"<",hideAction:"<",hideLink:"<",showAll:"<",hideToggle:"<",allowSelect:"<",selection:"="},controllerAs:"vm",controller:function($scope,editorService,uSync8DashboardService){function hasFailedDetail(details){return null!=details&&0!=details.length&&details.some(function(detail){return!detail.success})}var vm=this;vm.showChange=function(change){return vm.showAll||"NoChange"!==change&&"Removed"!==change};vm.getIcon=function(result){if(!result.success)return"icon-delete color-red";if(hasFailedDetail(result.details))return"icon-alert color-yellow";switch(result.change){case"NoChange":return"icon-check color-grey";case"Update":return"icon-check color-orange";case"Delete":return"icon-delete color-red";case"Import":case"Export":return"icon-check color-green";default:return"icon-flag color-red"}};vm.getChangeClass=function(result){var classString="";return vm.allowSelect&&(classString="-usync-can-select "),result.__selected&&(classString+="-selected "),result.success?hasFailedDetail(result.details)?classString+" usync-change-row-Warn":classString+" usync-change-row-"+result.change:classString+"usync-change-row-Fail"};vm.getTypeName=function(typeName){return void 0!==typeName?typeName.substring(typeName.lastIndexOf(".")+1):"??"};vm.countChanges=function(changes){var count=0;return angular.forEach(changes,function(val){"NoChange"!==val.change&&count++}),count};vm.openDetail=function(options){options={item:options,title:"uSync Change",view:Umbraco.Sys.ServerVariables.application.applicationPath+"App_Plugins/uSync/changedialog.html",close:function(){editorService.close()}};editorService.open(options)};vm.showAll=vm.showAll||!1;vm.$onInit=function(){vm.hideLink=!!vm.hideLink;vm.hideAction=!!vm.hideAction};vm.apply=function(item){item.applyState="busy";uSync8DashboardService.importItem(item).then(function(){item.applyState="success"},function(error){console.error(error);item.applyState="error"})};vm.status=function(item){return void 0===item.applyState?"init":item.applyState};vm.select=function(item){var index;vm.allowSelect&&void 0!==vm.selection&&(-1===(index=_.findIndex(vm.selection,x=>x.key==item.key&&x.name==item.name))?(vm.selection.push(item),item.__selected=!0):(vm.selection.splice(index,1),item.__selected=!1))}}};angular.module("umbraco").component("usyncReportView",uSyncReportViewComponent)}(),function(){"use strict";angular.module("umbraco").controller("uSyncExpansionController",function($scope,uSync8DashboardService){var vm=this;vm.loading=!0;uSync8DashboardService.getAddOnSplash().then(function(result){vm.addons=result.data;vm.loading=!1})})}(),function(){"use strict";angular.module("umbraco").controller("uSyncSettingsController",function($scope,uSync8DashboardService,overlayService,notificationsService){var vm=this;vm.working=!1;vm.loading=!0;vm.readonly=!0;vm.docslink="https://docs.jumoo.co.uk/uSync/v9/settings/";vm.umbracoVersion=Umbraco.Sys.ServerVariables.application.version;vm.saveSettings=function(){vm.working=!1;uSync8DashboardService.saveSettings(vm.settings).then(function(){vm.working=!1;notificationsService.success("Saved","Settings updated")},function(error){notificationsService.error("Saving",error.data.Message)})};vm.openAppSettingsOverlay=function(){uSync8DashboardService.getChangedSettings().then(function(options){options={"uSync:":function toPascal(o){var newO,origKey,newKey,value;if(o instanceof Array)return o.map(function(value){return"object"==typeof value?toCamel(value):value});for(origKey in newO={},o)o.hasOwnProperty(origKey)&&(newKey=(origKey.charAt(0).toUpperCase()+origKey.slice(1)||origKey).toString(),((value=o[origKey])instanceof Array||null!==value&&value.constructor===Object)&&(value=toPascal(value)),newO[newKey]=value);return newO}(options.data)};options={view:Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath+"/uSync/settings/settings.overlay.html",title:"appsettings.json snipped",content:JSON.stringify(options,null,4),docslink:vm.docslink,disableBackdropClick:!0,disableEscKey:!0,hideSubmitButton:!0,submit:function(){overlayService.close()}};overlayService.confirm(options)})};uSync8DashboardService.getSettings().then(function(setname){vm.settings=setname.data;vm.loading=!1;setname=vm.settings.defaultSet;uSync8DashboardService.getHandlerSetSettings(setname).then(function(result){vm.handlerSet=result.data})})})}(),function(){"use strict";angular.module("umbraco").controller("uSync8Controller",function($scope,$q,$controller,eventsService,overlayService,notificationsService,editorService,uSync8DashboardService,uSyncHub){function performAction(options,actionMethod){return $q(function(resolve,reject){uSync8DashboardService.getActionHandlers(options).then(function(result){vm.status.handlers=result.data,function(handlers,actionMethod,options){return $q(function(resolve,reject){var index=0;vm.status.message="Starting "+options.action;uSync8DashboardService.startProcess(options.action).then(function(){!function runHandlerAction(handler){vm.status.message=handler.name;handler.status=1;actionMethod(handler.alias,options,getClientId()).then(function(result){vm.results=vm.results.concat(result.data.actions);handler.status=2;handler.changes=countChanges(result.data.actions);++index<handlers.length?runHandlerAction(handlers[index]):(vm.status.message="Finishing "+options.action,uSync8DashboardService.finishProcess(options.action,vm.results).then(function(){resolve()}))},function(error){reject(error)})}(handlers[index])})})}(vm.status.handlers,actionMethod,options).then(function(){resolve()},function(error){reject(error)})})})}function report(options){vm.results=[];resetStatus(modes.REPORT);getWarnings("report");vm.reportButton.state="busy";var options={action:"report",group:options},start=performance.now();performAction(options,uSync8DashboardService.reportHandler).then(function(){vm.working=!1;vm.reported=!0;vm.perf=performance.now()-start;vm.status.message="Report complete";vm.reportButton.state="success"},function(error){vm.reportButton.state="error";notificationsService.error("Error",error.data.ExceptionMessage??error.data.exceptionMessage)})}function importForce(group){importItems(!0,group)}function importItems(options,group){vm.results=[];resetStatus(modes.IMPORT);getWarnings("import");vm.importButton.state="busy";var options={action:"import",group:group,force:options},start=performance.now();performAction(options,uSync8DashboardService.importHandler).then(function(){vm.status.message="Post import actions";uSync8DashboardService.importPost(vm.results,getClientId()).then(function(){vm.working=!1;vm.reported=!0;vm.perf=performance.now()-start;vm.importButton.state="success";eventsService.emit("usync-dashboard.import.complete"),function(duration){var time=26.5*countChanges(duration),duration=moment.duration(time,"seconds");if(180<=time){vm.savings.show=!0;vm.savings.title="You just saved "+duration.humanize()+"!";vm.savings.message="";for(let x=0;x<vm.godo.length&&vm.godo[x].time<time;x++)vm.savings.message=vm.godo[x].message}}(vm.results);vm.status.message="Complete"})},function(error){notificationsService.error("Error",error.data.ExceptionMessage??error.data.exceptionMessage)})}function exportItems(){exportGroup("")}function exportGroup(options){vm.results=[];resetStatus(modes.EXPORT);vm.exportButton.state="busy";var options={action:"export",group:options},start=performance.now();performAction(options,uSync8DashboardService.exportHandler).then(function(){vm.status.message="Export complete";vm.working=!1;vm.reported=!0;vm.perf=performance.now()-start;vm.exportButton.state="success";vm.savings.show=!0;vm.savings.title="All items exported.";vm.savings.message="Now go wash your hands 🧼!";eventsService.emit("usync-dashboard.export.complete")},function(error){notificationsService.error("Error",error.data.ExceptionMessage??error.data.exceptionMessage)})}function getWarnings(action){uSync8DashboardService.getSyncWarnings(action).then(function(result){vm.warnings=result.data})}function importGroup(group){importItems(!1,group)}function countChanges(changes){var count=0;return angular.forEach(changes,function(val){"NoChange"!==val.change&&count++}),count}function resetStatus(mode){switch(vm.fresh=!1,vm.warnings={},vm.reported=vm.showAll=!1,vm.working=!0,vm.showSpinner=!1,vm.runmode=mode,vm.hideLink=!1,vm.savings.show=!1,vm.status={Count:0,Total:1,Message:"Initializing",Handlers:vm.handlers},vm.hub.active||(vm.status.Message="Working ",vm.showSpinner=!0),vm.update={Message:"",Count:0,Total:1},vm.perf=0,mode){case modes.IMPORT:vm.action="Import";break;case mode.REPORT:vm.action="Report";break;case mode.EXPORT:vm.action="Export"}}function getClientId(){return void 0!==$.connection?$.connection.connectionId:""}var vm=this,modes;vm.fresh=!0;vm.loading=!0;vm.versionLoaded=!1;vm.working=!1;vm.reported=!1;vm.syncing=!1;vm.hideLink=!1;vm.showSpinner=!1;vm.showEverything=!0;vm.selection=[];vm.groups=[];vm.perf=0;vm.showAdvanced=!1;vm.hasuSyncForms=!1;vm.canHaveForms=!1;modes={NONE:0,REPORT:1,IMPORT:2,EXPORT:3};vm.runmode=modes.NONE;vm.showAll=!1;vm.status={};vm.reportAction="";vm.importButton={state:"init",defaultButton:{labelKey:"usync_import",handler:importItems},subButtons:[{labelKey:"usync_importforce",handler:function(){importForce("")}}]};vm.reportButton={state:"init",defaultButton:{labelKey:"usync_report",handler:function(){report("")}},subButtons:[]};vm.exportButton={state:"init",defaultButton:{labelKey:"usync_export",handler:function(){exportItems()}},subButtons:[{labelKey:"usync_exportClean",handler:function(){overlayService.open({title:"Clean Export",content:"Are you sure ? A clean export will delete all the contents of the uSync folder. You will loose any stored delete or rename actions.",disableBackdropClick:!0,disableEscKey:!0,submitButtonLabel:"Yes run a clean export",closeButtonLabel:"No, close",submit:function(){overlayService.close();uSync8DashboardService.cleanExport().then(function(){exportItems()})},close:function(){overlayService.close()}})}}]};vm.report=report;vm.versionInfo={IsCurrent:!0};vm.exportItems=exportItems;vm.importForce=importForce;vm.importItems=importItems;vm.importGroup=importGroup;vm.exportGroup=exportGroup;vm.getTypeName=function(umbType){return umbType=umbType.substring(0,umbType.indexOf(",")),umbType.substring(umbType.lastIndexOf(".")+1)};vm.showChange=function(change){return vm.showAll||"NoChange"!==change&&"Removed"!==change};vm.countChanges=countChanges;vm.calcPercentage=function(status){return 100*status.count/status.Total};vm.openDetail=function(options){options={item:options,title:"uSync Change",view:"/App_Plugins/uSync/changeDialog.html",close:function(){editorService.close()}};editorService.open(options)};vm.savings={show:!1,title:"",message:""};vm.godo=[{time:0,message:"Worth checking"},{time:180,message:"Go make a cup of tea"},{time:300,message:"Go have a quick chat"},{time:900,message:"Go for a nice walk outside 🚶‍♀️"},{time:3600,message:"You deserve a break"}];uSyncHub.initHub(function(hub){vm.hub=hub;vm.hub.on("add",function(data){vm.status=data});vm.hub.on("update",function(update){vm.update=update});vm.hub.start()});vm.showEverything=!1;uSync8DashboardService.getHandlerGroups().then(function(result){_.forEach(result.data,function(icon,group){"_everything"==group?vm.showEverything=!0:(vm.groups.push({name:group,icon:icon,key:group.toLowerCase()}),vm.importGroup[group]={state:"init",defaultButton:{labelKey:"usync_import",handler:function(){importGroup(group)}},subButtons:[{labelKey:"usync_importforce",handler:function(){importForce(group)}}]},"forms"===group.toLowerCase()&&(vm.hasuSyncForms=!0))});vm.hasuSyncForms||(vm.canHaveForms=function(){if(vm.hasuSyncForms)return!1}());vm.loading=!1},function(){vm.loading=!1});uSync8DashboardService.getHandlers().then(function(result){vm.handlers=result.data;vm.status.handlers=vm.handlers});uSync8DashboardService.checkVersion().then(function(result){vm.versionLoaded=!0;vm.versionInfo=result.data});vm.importGroup={}})}(),function(){"use strict";angular.module("umbraco").controller("uSyncSettingsDashboardController",function($controller,$scope,$timeout,navigationService,eventsService,uSync8DashboardService){var vm=this;vm.selectNavigationItem=function(item){eventsService.emit("usync-dashboard.tab.change",item)};vm.page={title:"uSync",description:"...",navigation:[{name:"uSync",alias:"uSync",icon:"icon-infinity",view:Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath+"/uSync/settings/default.html",active:!0},{name:"Settings",alias:"settings",icon:"icon-settings",view:Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath+"/uSync/settings/settings.html"}]};$timeout(function(){navigationService.syncTree({tree:"uSync",path:"-1"})});uSync8DashboardService.getAddOns().then(function(result){vm.version="v"+result.data.version;0<result.data.addOnString.length&&(vm.version+=" + "+result.data.addOnString);vm.page.description=vm.version;vm.addOns=result.data.addOns;var insertOffset=1;-1==vm.version.indexOf("Complete")&&(insertOffset=2,vm.page.navigation.push({name:"Add ons",alias:"expansion",icon:"icon-box",view:Umbraco.Sys.ServerVariables.umbracoSettings.appPluginsPath+"/uSync/settings/expansion.html"}));vm.addOns.forEach(function(value){""!==value.view&&vm.page.navigation.splice(vm.page.navigation.length-insertOffset,0,{name:value.displayName,alias:value.alias,icon:value.icon,view:value.view})})})})}()