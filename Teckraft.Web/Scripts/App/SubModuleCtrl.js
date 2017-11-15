//alert('hiiiiii');


angular
    .module('myApp.ctrl.subSubModule', [])
    .controller('SubModuleCtrl', [
        '$scope',
        '$rootScope',
        '$location',
        'SubModuleService',
        function ($scope, $rootScope, $location, divisionService) {
            $scope.items = [];
            $rootScope.goHome = function (path) {
                $rootScope.checkBack = true;
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

            var d = { CurrentPage: 1, PageSize: 99999, Parameters: { Name: "getSubmoduleMapping", Value: "list" } }
            console.log(d);
            SubModuleService.getSubModule(d).success(function (data) {
                $scope.items = data.items
                console.log($scope.items)
            });

        }]);


angular
    .SubModule('myApp.service.SubModuleService', [])
    .factory('SubModuleService', [
        '$http',
        function ($http) {
            return {
                getSubModule: function (d) {
                    alert('in SubModule service')
                    return $http({
                        method: 'GET',
                        url: '/api/SubModule?q=' + d
                        //         ProudctSearch
                    });
                }
            }
        }]);

