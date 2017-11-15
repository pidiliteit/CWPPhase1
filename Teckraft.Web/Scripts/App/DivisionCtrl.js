//alert('hiiiiii');


angular
    .module('myApp.ctrl.SAPMasterDivisonList', [])
    .controller('SAPMasterDivisonListCtrl', [
        '$scope',
        '$rootScope',
        '$location',
        'divisionService',
        function ($scope, $rootScope, $location, divisionService) {
            //var header = document.querySelector('.navbar-fixed-top')
            //classie.remove(header, 'navbar-shrink');
            //alert('hiiii')
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

            divisionService.getDivision(null).success(function (data) {
                $scope.items = data.items
                console.log($scope.items)
            });

        }]);


angular
    .module('myApp.service.division', [])
    .factory('divisionService', [
        '$http',
        function ($http) {
            return {
                getDivision: function (query) {
                  //  alert('in service')
                    return $http({
                        method: 'GET',
                        url: '/api/division?q=' + query
                        //         ProudctSearch
                    });
                }
            }
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



