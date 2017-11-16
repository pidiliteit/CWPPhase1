angular
    .module('myApp.ctrl.SAPMasterSalesGroupList', [])
    .controller('SAPMasterSalesGroupListCtrl', [
        '$scope',
        '$rootScope',
        '$location',
        'SalesGroupService',
        function ($scope, $rootScope, $location, SalesGroupService) {
            //var header = document.querySelector('.navbar-fixed-top')
            //classie.remove(header, 'navbar-shrink');
            alert('hiiiiiiii')
            $scope.items = [];
            $rootScope.goHome = function (path) {
                $rootScope.checkBack = true;
                // $scope.slide = 'slide-left';
                $location.url(path);
            }

            //$scope.addNewRecord = function () {
            //    alert();
            //    $location.path("/settings/ApprovalMatrixForm");
            //}

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

            SalesGroupService.getSalesGroup(null).success(function (data) {
                alert();
                $scope.items = data.items
                console.log($scope.items)
            });

        }]);


///////////////////////////////////////////////

//angular
//    .module('myApp.ctrl.settingApprovalMatrixForm', [])
//    .controller('settingApprovalMatrixFormCtrl', [
//        '$scope',
//        '$rootScope',
//        '$location',
//        'ApprovalMatrixService',

//        function ($scope, $rootScope, $location, ApprovalMatrixService) {
//            //var header = document.querySelector('.navbar-fixed-top')
//            //classie.remove(header, 'navbar-shrink');
//            $rootScope.goHome = function (path) {
//                $rootScope.checkBack = true;
//                // $scope.slide = 'slide-left';
//                $location.url(path);
//            }

//            $scope.navigationManager = navigationManagerFactory();

//            function navigationManagerFactory() {
//                var listPath = '/';
//                return {
//                    setListPage: function () {
//                        listPath = $location.path();
//                    },
//                    goToListPage: function () {
//                        $location.path(listPath);
//                    }
//                };
//            }

//        }]);
/////////////////////////////////////////////////////////////////////


angular
    .module('myApp.service.SalesGroup', [])
    .factory('SalesGroupService', [
        '$http',
        function ($http) {
            return {
                getSalesGroup: function (query) {
                    return $http({
                        method: 'GET',
                        url: '/api/SalesGroup?q=' + query
                        //         ProudctSearch
                    });
                }
            }
        }]);

