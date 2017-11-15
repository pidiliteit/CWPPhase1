//alert('hiiiiii');


angular
    .module('myApp.ctrl.module', [])
    .controller('moduleCtrl', [
        '$scope',
        '$rootScope',
        '$location',
        'moduleService',
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

            moduleService.getModule(null).success(function (data) {
                $scope.items = data.items
                console.log($scope.items)
            });

        }]);


angular
    .module('myApp.service.moduleService', [])
    .factory('moduleService', [
        '$http',
        function ($http) {
            return {
                getModule: function (query) {
                    alert('in module service')
                    return $http({
                        method: 'GET',
                        url: '/api/Module?q=' + query
                        //         ProudctSearch
                    });
                }
            }
        }]);

