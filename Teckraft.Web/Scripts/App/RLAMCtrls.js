angular
    .module('myApp.ctrl.settingRlamList', [])
    .controller('settingRlamListCtrl', [
        '$scope',
        '$rootScope',
        '$location',
        'rlamService',
        function ($scope, $rootScope, $location, rlamService) {
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
                $location.path("/settings/addrlam");
            }

            $scope.viewRlam = function (id) {
                alert();
                $location.path("/settings/detailrlam/" + id)
                isdeleteable = false;
            }
            $scope.editRlam = function (id) {
                $location.path("/settings/Editrlam/" + id)
            }
            $scope.deleteRlam = function (id) {
                //alert(id)
                $location.path("/settings/detailrlam/" + id)
                isdeleteable = true;
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

            rlamService.getRlams(null).success(function (data) {
                $scope.items=data.items
                //alert(data);
            });

        }]);


///////////////////////////////////////////////

angular
    .module('myApp.ctrl.settingRlamForm', [])
    .controller('settingRlamFormCtrl', [
        '$scope',
        '$rootScope',
        '$location',
        'rlamService',

        function ($scope, $rootScope, $location, rlamService) {
            //var header = document.querySelector('.navbar-fixed-top')
            //classie.remove(header, 'navbar-shrink');
            $rootScope.goHome = function (path) {
                $rootScope.checkBack = true;
                // $scope.slide = 'slide-left';
                $location.url(path);
            }

            $scope.isNew = true;
            $scope.rlam = { active: false }
            $scope.divisions = [
                { id: 10, divisionCode: 'FV', divisionName: 'Fevicol' },
            { id: 40, divisionCode: 'CC', divisionName: 'Construction Chemical' }
            ]

            $scope.plants = [
                { id: 1098, plantName: '1098-Plant1' }
            ]
            $scope.salesGroups = [
                { id: 105, salesGroupName: '105-Retail' }
            ]
            $scope.users = [
                { id: 1, userName: 'RLAM1' },
            { id: 2, userName: 'RLAM1'}
             ]
            $scope.navigationManager = navigationManagerFactory();

            function navigationManagerFactory() {
                var listPath = '/settings/rlam';
                return {
                    setListPage: function () {
                        listPath = $location.path();
                    },
                    goToListPage: function () {
                        $location.path(listPath);
                    }
                };
            }

            $scope.save = function () {
                alert('hiiiiiiiiiii');
                if ($scope.isNew) {
                    rlamService
                        .createRlams($scope.rlam)
                        .success(function (data, status, headers, config) {
                            $scope.navigationManager.goToListPage();

                        })
                        .error(function (data, status, headers, config) {
                            $scope.errorMessage = (data || { message: "Create operation failed." }).message + (' [HTTP-' + status + ']');
                        });
                }
                else {
                    rlamService
                        .updateRlams($scope.rlam)
                        .success(function (data, status, headers, config) {
                            $scope.navigationManager.goToListPage();
                        })
                        .error(function (data, status, headers, config) {
                            $scope.errorMessage = (data || { message: "Update operation failed." }).message + (' [HTTP-' + status + ']');
                        });
                }
            }


        }]);
/////////////////////////////////////////////////////////////////////

angular
    .module('myApp.ctrl.RlamDetail', [])
    .controller('RlamDetailCtrl', [
        '$scope',
        '$routeParams',
        '$route',
        'rlamService',
        function ($scope, $routeParams, $route, rlamService) {
            $scope.rlam = {}


            $scope.isDeleteRequested = isdeleteable;

            $scope.deleteRlam = function (id) {
                alert(id)
                rlamService
                    .deleteRlams($routeParams.id)
                    .success(function (data, status, headers, config) {
                        alert('success')
                        //$scope.navigationManager.goToListPage();
                    })
                    .error(function (data, status, headers, config) {
                        $scope.errorMessage = (data || { message: "Delete operation failed." }).message + (' [HTTP-' + status + ']');
                    });
            };


            $scope.returnToList = function () {
                $scope.navigationManager.goToListPage();
            };
            //alert();
            rlamService
            .readRlams($routeParams.id)
            .success(function (data, status, headers, config) {
                console.log(data);
                $scope.rlam.id = data.id;
                $scope.rlam.division = data.division;
                $scope.rlam.salesGroup = data.salesGroup;
                $scope.rlam.plant = data.plant;
                $scope.rlam.users = data.users;
            });
        }]);

/////////////////////////////////////////////////////////////////////

angular
    .module('myApp.ctrl.RlamEdit', [])
    .controller('RlamEditCtrl', [
        '$scope',
        '$routeParams',
        '$templateCache',
        'rlamService',
        function ($scope, $routeParams, $templateCache, rlamService) {


            $scope.rlam = {};
            $scope.divisions = [
                { id: 10, divisionCode: 'FV', divisionName: 'Fevicol' },
                { id: 40, divisionCode: 'CC', divisionName: 'Construction Chemical' }
            ]

            $scope.plants = [
                { id: 1098, plantName: '1098-Plant1' }
            ]
            $scope.salesGroups = [
                { id: 105, salesGroupName: '105-Retail' }
            ]
            $scope.users = [
                { id: 1, userName: 'RLAM1' },
            { id: 2, userName: 'RLAM1' }
            ]

            $scope.returnToList = function () {
                $scope.navigationManager.goToListPage();
            };
            $scope.isNew = angular.isUndefined($routeParams.id);

            rlamService.getRlamEditModel().success(function (data, status, headers, config) {
                console.log(data)
            });

            $scope.save = function () {
                console.log($scope.rlam)
                if ($scope.isNew) {
                    alert('create');
                    rlamService
                        .createRlams($scope.rlam)
                        .success(function (data, status, headers, config) {
                            $scope.navigationManager.goToListPage();

                        })
                        .error(function (data, status, headers, config) {
                            $scope.errorMessage = (data || { message: "Create operation failed." }).message + (' [HTTP-' + status + ']');
                        });
                }
                else {
                    alert('update');
                    console.log($scope.rlam);
                    rlamService
                        .updateRlams($scope.rlam)
                        .success(function (data, status, headers, config) {
                            //$scope.navigationManager.goToListPage();
                            alert('success');
                        })
                        .error(function (data, status, headers, config) {
                            $scope.errorMessage = (data || { message: "Update operation failed." }).message + (' [HTTP-' + status + ']');
                        });
                }
            }


            rlamService
                .getRlamEditModel()
                .success(function (data, status, headers, config) {
                    //console.log(data)
                    if (!$scope.isNew) {
                        rlamService
                            .readRlams($routeParams.id)
                            .success(function (data, status, headers, config) {
                                console.log(data)
                                $scope.rlam.id = data.id;
                                $scope.rlam.division = data.division;
                                $scope.rlam.salesGroup = data.salesGroup;
                                $scope.rlam.plant = data.plant;
                                $scope.rlam.users = data.users;
                                //active: true

                            });
                    }
                });
        }]);
/////////////////////////////////////////////////////////////////////


angular
    .module('myApp.service.rlam', [])
    .factory('rlamService', [
        '$http',
        function ($http) {
            return {
                getRlams: function (query) {
                    return $http({
                        method: 'GET',
                        url: '/api/rlam?q=' + query
                        //         ProudctSearch
                    });
                },
                getRlamEditModel: function () {
                    return $http({
                        method: 'GET',
                        url: '/api/rlam?RequestType=EditModel'
                    });
                },
                createRlams: function (rlam) {
                    alert();
                    return $http({
                        method: 'POST',
                        url: 'api/rlam',
                        data: rlam
                    });
                },
                readRlams: function (id) {
                    //alert(id);
                    return $http({
                        method: 'GET',
                        url: '/api/rlam/' + id
                    });
                },
                updateRlams: function (rlam) {
                    return $http({
                        method: 'PUT',
                        url: '/api/rlam',
                        data: rlam

                    });
                },
                deleteRlams: function (id) {
                    return $http({
                        method: 'DELETE',
                        url: '/api/rlam/' + id
                    });
                }
            }
        }]);

