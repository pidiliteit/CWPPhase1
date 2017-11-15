angular
    .module('myApp.ctrl.settingApprovalHierarchyList', [])
    .controller('settingApprovalHierarchyListCtrl', [
        '$scope',
        '$rootScope',
        '$location',
        'ApprovalHierarchyService',
        function ($scope, $rootScope, $location, ApprovalHierarchyService) {
            //var header = document.querySelector('.navbar-fixed-top')
            //classie.remove(header, 'navbar-shrink');
            //alert('')
            $scope.items = [];
            $rootScope.goHome = function (path) {
                $rootScope.checkBack = true;
                // $scope.slide = 'slide-left';
                $location.url(path);
            }

            $scope.addNewRecord = function () {
                alert();
                $location.path("/settings/AddApprovalHierarchy");
            }

            $scope.navigationManager = navigationManagerFactory();

            function navigationManagerFactory() {
                var listPath = '/';
                return {
                    setListPage: function () {
                        listPath = $location.path();
                    },
                    goToListPage: function () {
                        $location.path(listPath);
                    }
                };
            }
            
            ApprovalHierarchyService.getApprovalHierarchy(null).success(function (data) {
                $scope.items = data.items
            });

        }]);


///////////////////////////////////////////////

angular
    .module('myApp.ctrl.settingAddApprovalHierarchy', [])
    .controller('settingAddApprovalHierarchyCtrl', [
        '$scope',
        '$rootScope',
        '$location',
        'ApprovalHierarchyService',

        function ($scope, $rootScope, $location, ApprovalHierarchyService) {
            //var header = document.querySelector('.navbar-fixed-top')
            //classie.remove(header, 'navbar-shrink');
            $rootScope.goHome = function (path) {
                $rootScope.checkBack = true;
                // $scope.slide = 'slide-left';
                $location.url(path);
            }

            $scope.navigationManager = navigationManagerFactory();

            function navigationManagerFactory() {
                var listPath = '/';
                return {
                    setListPage: function () {
                        listPath = $location.path();
                    },
                    goToListPage: function () {
                        $location.path(listPath);
                    }
                };
            }

        }]);
/////////////////////////////////////////////////////////////////////


angular
    .module('myApp.service.ApprovalHierarchy', [])
    .factory('ApprovalHierarchyService', [
        '$http',
        function ($http) {
            return {
                getApprovalHierarchy: function (query) {
                    return $http({
                        method: 'GET',
                        url: '/api/ApprovalHierarchy?q=' + query
                        //         ProudctSearch
                    });
                }
            }
        }]);

