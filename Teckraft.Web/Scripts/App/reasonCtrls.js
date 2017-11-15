angular
    .module('myApp.ctrl.settingReasonList', [])
    .controller('settingReasonListCtrl', [
        '$scope',
        '$rootScope',
        '$location',
        'reasonService',
        function ($scope, $rootScope, $location, reasonService) {
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
                $location.path("/settings/addreason");
            }
            $scope.viewReason = function (id) {
                $location.path("/settings/detailreason/" + id)
                isdeleteable = false;
            }
            $scope.editReason = function (id) {
                $location.path("/settings/EditReason/" + id)
            }
            $scope.deleteReason = function (id) {
                //alert(id)
                $location.path("/settings/detailreason/" + id)
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

            reasonService.getReasons(null).success(function (data) {
                $scope.items = data.items
                console.log(data.items);
                //alert(data);
            });
        }]);


///////////////////////////////////////////////

angular
    .module('myApp.ctrl.settingReasonForm', [])
    .controller('settingReasonFormCtrl', [
        '$scope',
        '$rootScope',
        '$location',
        'reasonService',

        function ($scope, $rootScope, $location, reasonService) {
            //var header = document.querySelector('.navbar-fixed-top')
            //classie.remove(header, 'navbar-shrink');
            $rootScope.goHome = function (path) {
                $rootScope.checkBack = true;
                // $scope.slide = 'slide-left';
                $location.url(path);
            }
            $scope.isNew = true;
            $scope.reason = { active: false }
            $scope.divisions = [
                { id: 10, divisionCode: 'FV', divisionName: 'Fevicol' },
            { id: 40, divisionCode: 'CC', divisionName: 'Construction Chemical' }
            ]

            $scope.navigationManager = navigationManagerFactory();

            function navigationManagerFactory() {
                var listPath = '/settings/reason';
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
                if ($scope.isNew) {
                    reasonService
                        .createReason($scope.reason)
                        .success(function (data, status, headers, config) {
                            $scope.navigationManager.goToListPage();

                        })
                        .error(function (data, status, headers, config) {
                            $scope.errorMessage = (data || { message: "Create operation failed." }).message + (' [HTTP-' + status + ']');
                        });
                }
                else {
                    reasonService
                        .updateReason($scope.Reasons)
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
    .module('myApp.ctrl.ReasonDetail', [])
    .controller('ReasonDetailCtrl', [
        '$scope',
        '$routeParams',
        '$route',
        'reasonService',
        function ($scope, $routeParams, $route, reasonService) {
            $scope.reason = {}
            

            $scope.isDeleteRequested = isdeleteable;

            $scope.deleteReason = function (id) {
               alert(id)
                reasonService
                    .deleteReason($routeParams.id)
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
            reasonService
            .readReason($routeParams.id)
            .success(function (data, status, headers, config) {
                console.log(data);
                $scope.reason.id = data.id;
                $scope.reason.division = data.division;
                $scope.reason.reasonCode = data.reasonCode;
                $scope.reason.reasonDesc = data.reasonDesc;
                $scope.reason.reasonType = data.reasonType;
                $scope.reason.active = data.active;
            });
        }]);

/////////////////////////////////////////////////////////////////////
/// <reference path="divisionMobileService.js" />

angular
    .module('myApp.ctrl.ReasonEdit', [])
    .controller('ReasonEditCtrl', [
        '$scope',
        '$routeParams',
        '$templateCache',
        'reasonService',
        function ($scope, $routeParams, $templateCache, reasonService) {

           
            $scope.reason = {};
            $scope.divisions = [
                { id: 10, divisionCode: 'FV', divisionName: 'Fevicol' },
            { id: 40, divisionCode: 'CC', divisionName: 'Construction Chemical' }
            ]

            $scope.returnToList = function () {
                $scope.navigationManager.goToListPage();
            };
            $scope.isNew = angular.isUndefined($routeParams.id);

            reasonService.getReasonEditModel().success(function (data, status, headers, config) {
                console.log(data)
            });

            $scope.save = function () {
                console.log($scope.reason)
                if ($scope.isNew) {
                    alert('create');
                    reasonService
                        .createReason($scope.reason)
                        .success(function (data, status, headers, config) {
                            $scope.navigationManager.goToListPage();

                        })
                        .error(function (data, status, headers, config) {
                            $scope.errorMessage = (data || { message: "Create operation failed." }).message + (' [HTTP-' + status + ']');
                        });
                }
                else {
                    alert('update');
                    console.log($scope.reason);
                    reasonService
                        .updateReason($scope.reason)
                        .success(function (data, status, headers, config) {
                            //$scope.navigationManager.goToListPage();
                            alert('success');
                        })
                        .error(function (data, status, headers, config) {
                            $scope.errorMessage = (data || { message: "Update operation failed." }).message + (' [HTTP-' + status + ']');
                        });
                }
            }

            reasonService
                .getReasonEditModel()
                .success(function (data, status, headers, config) {
                    //console.log(data)
                    if (!$scope.isNew) {
                        reasonService
                            .readReason($routeParams.id)
                            .success(function (data, status, headers, config) {
                                console.log(data)
                                $scope.reason.id = data.id;
                                $scope.reason.division = data.division;
                                $scope.reason.reasonCode = data.reasonCode
                                $scope.reason.reasonDesc=data.reasonDesc
                                $scope.reason.reasonType = data.reasonType
                                $scope.reason.Active=data.Active
                                //active: true

                            });
                    }
                });
        }]);
/////////////////////////////////////////////////////////////////////

angular
    .module('myApp.service.reason1', [])
    .factory('reasonService', [
        '$http',
        function ($http) {
            return {
                getReasons: function (query)
                {
                    return $http({
                        method: 'GET',
                        url: '/api/reason?q=' + query
                        //         ProudctSearch
                    });
                },
                getReasonEditModel: function () {
                    return $http({
                        method: 'GET',
                        url: '/api/reason?RequestType=EditModel'
                    });
                },
                createReason: function (reason) {
                    return $http({
                        method: 'POST',
                        url: 'api/reason',
                        data: reason
                    });
                },
                readReason: function (id) {
                    //alert(id);
                    return $http({
                        method: 'GET',
                        url: '/api/reason/' + id
                    });
                },
                updateReason: function (reason) {
                    return $http({
                        method: 'PUT',
                        url: '/api/reason',
                        data: reason

                    });
                },
                
                deleteReason: function (id) {
                    alert(id);
                    return $http({
                        method: 'DELETE',
                        url: '/api/reason/' + id
                    });
                }
            }
        }]);

