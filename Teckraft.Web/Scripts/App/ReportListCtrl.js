
angular.
    module('myApp.ctrl.ReportCRRegister', [])
    .controller('CRRegisterCtrl', [
        '$scope',
        '$rootScope',
        '$routeParams',
        '$location',
        'CRService',
       'divisionService',
        function ($scope, $rootScope, $routeParams, $location, CRService, divisionService) {
            $scope.crList = [];
            //CRService.getCRRegisterFilter({ CurrentPage: 0, PageSize: 20000, Parameters: [{ name: "crregister", value: "crregister" }, { name: "crregister", value: "crregister" }] }).success(function (data) {
            CRService.getCRRegisterFilter({ CurrentPage: 0, PageSize: 20000, Parameters: [] }).success(function (data) {
                //$scope.items = data.items;
                $scope.crList = data.items;
                console.log(data.items);
            });

            //$scope.newApplyFilter = function () {
            //    alert("444")
            //    CRService.CRRegApplyFilter();
            //}
            $scope.filters = [
               {name: "CRStatus", value: ""},
               { name: "DivisionName", value: "" },
               { name: "Requester", value: "" },
               { name: "CRCompletedSD", value: "" },
               { name: "CRCompletedED", value: "" }
            ];

            $scope.CRRegApplyFilter = function () {
                $scope.runningAjax = true;
                //var query = '{"CurrentPage":' + $scope.page + ',' + '"PageSize":' + 10 + ',"Parameters":' + JSON.stringify($scope.getFilters()) + '}';
                var query = {
                    CurrentPage: $scope.page,
                    PageSize: 10,
                    Parameters: $scope.getFilters(),
                }
                console.log(query);
                $scope.atLastPage = false;
                CRService
                .getCRRegisterFilter(query)
                .success(function (res, status, headers, config) {
                    if (typeof (data) == 'string') location.href = '/'
                    console.log(res.items);
                    $scope.crList = res.items;
                    $scope.page = res.currentPage;
                    $scope.totalRecords = res.totalRecords;
                    $scope.totalPages = Math.ceil(res.totalRecords / res.pageSize);
                    $scope.pageSize = res.pageSize;
                    $scope.currentRecords = $scope.crList.length + "-" + $scope.page * $scope.pageSize
                    //$scope.showing = $scope.currentRecords + " of " + $scope.totalRecords;
                    $scope.runningService = false;
                }).error(function () {
                    $scope.error = "User does not have permission";
                    $scope.loading = false;
                    $scope.runningService = false;
                });
            }
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
                console.log($scope.divisions);
            })

            $scope.clear = function () {
                $scope.filters[0].value = "";
                $scope.filters[1].value = "";
                $scope.filters[2].value = "";
                $scope.filters[3].value = "";
                $scope.filters[4].value = "";
                $scope.CRRegApplyFilter();
            }

        }
       
    ]);

angular.
    module('myApp.ctrl.ReportExceptionReport', [])
    .controller('ExceptionReportCtrl', [
        '$scope',
        '$rootScope',
        '$routeParams',
        '$location',
        'CRService',
       
        function ($scope, $rootScope, $routeParams, $location, CRService) {
            CRService.getCRFilter({ CurrentPage: 0, PageSize: 20, Parameters: [] }).success(function (data) {
                $scope.items = data.items;
            });
          
        }
    ]);

angular.
    module('myApp.ctrl.ReportStatusReport', [])
    .controller('StatusReportCtrl', [
        '$scope',
        '$rootScope',
        '$routeParams',
        '$location',
        'CRService',
        function ($scope, $rootScope, $routeParams, $location, CRService) {
            CRService.getStatusReport({ CurrentPage: 0, PageSize: 20, Parameters: [] }).success(function (data) {
                $scope.items = data.items;
            });
        }
    ]);

angular.
    module('myApp.ctrl.ReportProjectStatus', [])
    .controller('ProjectStatusCtrl', [
        '$scope',
        '$rootScope',
        '$routeParams',
        '$location',
        'CRService',
        function ($scope, $rootScope, $routeParams, $location, CRService) {
            //CRService.getPhaseTask({ CurrentPage: 0, PageSize: 20, Parameters: [] }).success(function (data) {
            CRService.getCRFilter({ CurrentPage: 0, PageSize: 20, Parameters: [] }).success(function (data) {
                $scope.items = data.items;
            });
        }
    ]);
