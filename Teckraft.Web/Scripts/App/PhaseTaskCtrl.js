angular
    .module('myApp.ctrl.PhaseTask', [])
    .controller('PhaseTaskCtrl', [
        '$scope',
        '$rootScope',
        '$location',
        'PhaseTaskService',
         'divisionService',
         'CRService',
        function ($scope, $rootScope, $location, PhaseTaskService, divisionService,CRService) {
            $scope.items = [];
            $scope.page = 1;
            $scope.showing = "";
            $scope.totalRecords = 0;
            $scope.totalPages = 0;
            $scope.pageSize = 10;
            $scope.currentRecords = "";
            $scope.runningService = true;
            $scope.filters = [
                { name: "TaskCode", value: "" },
                { name: "assignedto", value: "" },
                { name: "requestedBy", value: "" },
                { name: "division", value:"" },
                { name: "StartDate", value: "" },
                { name: "EndDate", value: "" },
                { name: "Status", value: "" },
            ];
           
            $scope.getFilters = function () {
                var filters = [];
                $($scope.filters).each(function (x) {
                    if ($scope.filters[x].value != "") {
                        filters.push($scope.filters[x]);
                    }
                });
                return filters;
            }
           
            divisionService.getDivision().success(function (data) {
                $scope.divisions = data.items;
               // console.log($scope.divisions);
            })
            $scope.clear = function () {
                $scope.filters[0].value = "";
                $scope.filters[1].value = "";
                $scope.filters[2].value = "";
                $scope.filters[3].value = "";
                $scope.filters[4].value = "";
                $scope.filters[5].value = "";
                $scope.filters[6].value = "";
                $scope.ApplyFilter();
            }
            $scope.statusArr = ["Pending", "Sent Back", "Rejected", "Completed"]
            //$scope.modules = [];
            //$scope.submodules = [];

            $scope.showNext = function () {
                //alert();
                if ($scope.page >= $scope.totalPages) return;
                $scope.page++;
                $scope.ApplyFilter();
            }
            $scope.showPrev = function () {
                if ($scope.page == 1) return;
                $scope.page--;
                $scope.ApplyFilter();
            }
            $scope.pageChanged = function () {
                //alert($scope.page);
                if ($scope.page > $scope.totalPages) $scope.page = $scope.totalPages
                if ($scope.page < 1) $scope.page = 1;
                $scope.ApplyFilter();
            }
            $scope.ApplyFilter = function () {
                $scope.runningService = true;
                var query = {
                    CurrentPage: $scope.page,
                    PageSize: 10,
                    Parameters: $scope.getFilters(),
                }
              //  console.log(query);
                $scope.atLastPage = false;
                PhaseTaskService
                .getPhaseTaskFilter(query)
                .success(function (res, status, headers, config) {
                  //  console.log(res.items);
                    $scope.items = res.items;
                    $scope.page = res.currentPage;
                    $scope.totalRecords = res.totalRecords;
                    $scope.totalPages = Math.ceil(res.totalRecords / res.pageSize);
                    $scope.pageSize = res.pageSize;
                    $scope.currentRecords = $scope.items.lenght + "-" + $scope.page * $scope.pageSize
                    $scope.runningService = false;
                }).error(function () {
                    $scope.error = "User does not have permission";
                    $scope.runningService = false;
                });
            }

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

            PhaseTaskService.getPhaseTask().success(function (data) {
               // console.log(data)
                $scope.items = data.items;
                $scope.page = data.currentPage;
                $scope.totalRecords = data.totalRecords;
                $scope.totalPages = Math.ceil(data.totalRecords / data.pageSize);
                $scope.pageSize = data.pageSize;
                $scope.currentRecords = $scope.items.lenght + "-" + $scope.page * $scope.pageSize
                $scope.runningService = false;
            });

        }]);

angular
    .module('myApp.ctrl.CreatePhaseTask', [])
    .controller('createPhaseTaskCtrl', [
        '$scope',
        '$rootScope',
        '$routeParams',
        '$location',
        'PhaseTaskService',
        function ($scope, $rootScope, $routeParams, $location, PhaseTaskService) {
            $scope.items = [];
            $scope.page = 1;
            $scope.showing = "";
            $scope.totalRecords = 0;
            $scope.totalPages = 0;
            $scope.pageSize = 10;
            $scope.currentRecords = "";
            $scope.action = { statusCode: 0, item: null };
            $scope.task = null;
            $scope.filters = [
                { name: "TaskNo", value: "" },
                { name: "CRNo", value: "" },
                { name: "assignedto", value: "" },
                { name: "StartDate", value: "" },
                { name: "EndDate", value: "" },
                { name: "Status", value: "" },
            ];
           
            $scope.submit = function () {
                //alert($scope.task.id)
                //console.log($scope.action);
                $scope.action.item = $scope.task;
                if ($scope.task.id && $scope.task.id != 0) {
                    PhaseTaskService.updateTask($scope.action)
                    .success(function (status) {
                        $(".form-group").addClass("has-success");
                    })
                }
                
           }
          //  alert()
            if ($routeParams.id && $routeParams.id != '') {
                PhaseTaskService.getPhaseTaskById($routeParams.id).success(function (data) {
                   // console.log(data);
                    $scope.task = data;
                }).error(function () {
                    alert('error')
                });
            }

        }]);



angular
    .module('myApp.service.PhaseTask', [])
    .factory('PhaseTaskService', [
        '$http',
        function ($http) {
            return {
                getPhaseTaskById: function (id) {
                    return $http({
                        method: 'GET',
                        url: '/api/PhaseTask/' + id
                    });
                },
                getPhaseTask: function () {
                    //alert('in service getPhaseTask');
                    return $http({
                        method: 'GET',
                        url: '/api/phasetask?query='
                    });
                },
                updateTask: function (action) {
                    //console.log(action)
                    return $http({
                        method: 'Put',
                        url: '/api/PhaseTask',
                        data: action
                    })
                },
                getPhaseTaskFilter: function (query) {
                   // console.log(JSON.stringify(query));
                    var strquery = JSON.stringify({ CurrentPage: query.CurrentPage, PageSize: query.PageSize, Parameters: query.Parameters });
                    return $http({
                        method: 'GET',
                        url: "/api/phasetask?query=" + strquery

                    });
                    //var strquery = JSON.stringify({ CurrentPage: 1, PageSize: 10, Parameters: [{ Name: 'CRCode', Value: 'CR00009' }] });
                },
            }
        }]);

