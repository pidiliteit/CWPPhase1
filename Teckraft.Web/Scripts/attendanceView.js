angular.module('asfApp.ctrls.AttendanceViewCtrls', [])
.controller('attendanceViewCtrl', ['$scope', '$rootScope', '$routeParams', '$location', function ($scope, $rootScope, $routeParams, $location) {
    alert('View');

    $scope.items =
        [
        { id: 123, mobileNo: '98989898' },
        { id: 123, mobileNo: '98989898' },
        { id: 123, mobileNo: '98989898' },
        { id: 123, mobileNo: '98989898' },
        { id: 123, mobileNo: '98989898' },
        { id: 123, mobileNo: '98989898' },
        { id: 123, mobileNo: '98989898' },
        { id: 123, mobileNo: '98989898' },
        { id: 123, mobileNo: '98989898' },
        { id: 123, mobileNo: '98989898' },
        { id: 123, mobileNo: '98989898' },
        { id: 123, mobileNo: '98989898' },
        { id: 123, mobileNo: '98989898' },
      
    ];
   

    if ($routeParams.id && $routeParams.id != '') {
        alert("");
      
        $scope.item = {
            id: 1,
           

        };

        //for (var arr in $scope.activity) {
        //    if ($scope.activity[arr].id == activityName.id) {

        //    }
        //}

    }

   // $scope.q = $scope.items[0]

}]);