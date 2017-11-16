'use strict';

var app = angular.module('asfApp', [
    'ngTouch',
    'ngRoute',
    'ngAnimate',
    'ngSanitize',
    'asfApp.ctrls.DashboardSearchCtrls',
    'asfApp.service.activityService',
    'ui.bootstrap',
    'ui.bootstrap.datepicker',
    'asfApp.service.CommonService',
])
.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

    $routeProvider.when('/DashboardSearch', { templateUrl: '/Requisition/IssueDetails1', controller: 'DashboardSearchCtrl' });
    $routeProvider.when('/IssueDetailNew', { templateUrl: '/Requisition/IssueDetailsNew', controller: 'DashboardSearchCtrl' });
    $routeProvider.when('/PilworldWSSfeedback', { templateUrl: '/Requisition/PilworldWSSfeedback', controller: 'DashboardSearchCtrl' });
    $routeProvider.when('/mypidilite', { templateUrl: '/Requisition/mypidilite', controller: 'DashboardSearchCtrl' });
    $routeProvider.when('/WSSServiceCell', { templateUrl: '/Requisition/WSSServiceCell', controller: 'DashboardSearchCtrl' });
    $routeProvider.when('/RiskManagement', { templateUrl: '/Requisition/RiskManagement', controller: 'DashboardSearchCtrl' });
    $routeProvider.when('/DealerFeedback', { templateUrl: '/Requisition/DealerFeedback', controller: 'DashboardSearchCtrl' });
    $routeProvider.when('/AllOther', { templateUrl: '/Requisition/AllOther', controller: 'DashboardSearchCtrl' });
    
    $routeProvider.otherwise({ redirectTo: '/DashboardSearch' });
    
}])

.directive('autoComplete', function () {
    return {
        restrict: 'A',
        scope: {
            httpService: "=",
            renderItem: '=',
            ngModel: '=',
            minLength: '=',
            onSelect: '='
        },
        link: function (scope, elem) {
            elem.autocomplete({
                source: function (request, response) {
                    console.log(request);
                    scope.httpService(request.term).then(function (data) {
                        console.log(data);
                        if (!data.data.items.length) {
                            var result = [
                                {
                                    label: 'No matches found',
                                    value: response.term
                                }
                            ];
                            response(result);
                        } else {
                            response(
                                $.map(data.data.items, function (item) {
                                    return scope.renderItem(item);
                                }));
                        }
                    });
                },
                minLength: scope.minLength,
                select: function (event, ui) {
                    if (scope.onSelect) {
                        scope.onSelect(ui.item.item);
                    }
                    scope.$apply(function () {
                        scope.ngModel = ui.item.item;
                    });
                }
            });
        }
    };
})
.run(['$rootScope', '$http', 'CommonService', function ($rootScope, $httpProvider, CommonService) {
    $httpProvider.defaults.cache = false;
    $rootScope.setCurrentUser = function () {
        CommonService.getCurrentUserDetail(null).success(function (data) {
            console.log(data);
            $rootScope.currentUser = data;
            $httpProvider.defaults.headers.common.UID = data.csfrToken
        });
    }
    $rootScope.setCurrentUser();
}]);
app.filter('unique', function () {
    return function (input, key) {
        var unique = {};
        var uniqueList = [];
        if (input != null) {
            for (var i = 0; i < input.length; i++) {
                if (typeof unique[input[i][key]] == "undefined") {
                    unique[input[i][key]] = "";
                    uniqueList.push(input[i]);
                }
            }
        }
        return uniqueList;
    };
});
angular
    .module('asfApp.service.CommonService', [])
    .factory('CommonService', [
        '$http',
        function ($http) {
            return {
                getCurrentUserDetail: function () {
                    return $http({
                        method: 'Get',
                        //data: d,
                        url: "/api/approvalUser?requestType=" + 'currentUserDetail'
                    });
                },
            }
        }]);




