angular.module('asfApp.ctrls.RequisitionAddCtrls', [])
.controller('requisitionAddCtrl', ['$scope', '$rootScope', '$routeParams', '$location', function ($scope, $rootScope, $routeParams, $location) {
    //alert('hiii')

    $scope.item = {
        activityName: { id: 0, activityName: '' },
        townName: { id: 0, townName: '' },
        productName: { id: 0, productName: '' },
        contactPerson: '',
        contactNumber: '',
        expParticipant: '',
        approxCost: '',
        date: '',
        status: "draft",
        description: "",

    };

    $scope.users = [
        { userid: 123, username: 'rameez', usertype: 'HOD' },
        { userid: 124, username: 'mushtaqeem', usertype: 'creater' },
        { userid: 125, username: 'faisal', usertype: 'demo' },
    ];

     

    $scope.currentUser = { userid: 0, username: '', usertype: '' };

    $scope.activity = [
       { id: 1, activityName: 'Iftar Party' },
       { id: 2, activityName: 'Diwali Party' },
       { id: 3, activityName: 'New Opening' }]


    $scope.town = [
      { id: 1, townName: 'Town 1' },
      { id: 2, townName: 'Town 2' },
      { id: 3, townName: 'Town 3' }]


    $scope.products = [
          { id: 1, productName: 'Roff' },
          { id: 2, productName: 'Roff2' },
          { id: 3, productName: 'Roff3' }]


    //$scope.q = $scope.items[0]
    
    $scope.formValidate = function () {
        console.log($scope.item);
        if ($scope.item.activityName.id == 0) {
            alert("Please Select Activity Name");
        }

        else if ($scope.item.townName.id == 0) {
            alert("Please Select Town Name");
        }

        else if ($scope.item.productName.id == 0) {
            alert("Please Select Product Name");
        }

        else if (!$scope.item.contactPerson) {
            alert("Please Select Contact Person");
        }       

        else if (!$scope.item.contactNumber) {
            alert("Please Select Contact Number");
        }

        else if (!$scope.item.expParticipant) {
            alert("Please Select Expected Participant");
        }

        else if (!$scope.item.approxCost) {
            alert("Please Select Approx Cost");
        }

        else if (!$scope.item.date) {
            alert("Please Select Date");
        }
    }

    $scope.readOnly = false;

    if ($routeParams.id && $routeParams.id != '') {

        $scope.readOnly = true;

        $scope.item = {
            id:1,
            activityName: { id: 2, activityName: 'Diwali Party' },
            townName: { id: 3, townName: 'test' },
            productName: { id: 3, productName: 'sdfasdf' },
            contactPerson: 'Mustaqeem',
            contactNumber: '9224416581',
            expParticipant: '1000',
            approxCost: '25000',
            date: '15/12/2015',
            status: "draft",
            description:"Hello world ",

        };

        for (var arr in $scope.activity) {
           // console.log($scope.activity[arr]);
            if ($scope.activity[arr].id == $scope.item.activityName.id) {
                $scope.item.activityName = $scope.activity[arr];
            }
        }

        for (var arr in $scope.town) {
            //console.log($scope.town[arr]);
            if($scope.town[arr].id == $scope.item.townName.id){
                $scope.item.townName = $scope.town[arr]
            }
        }

        for (var arr in $scope.products) {
            console.log($scope.products[arr]);
            if ($scope.products[arr].id == $scope.item.productName.id) {
                $scope.item.productName = $scope.products[arr]
            }
        }

    }


    //console.log(test)
    $scope.show = function () {
       
        $scope.formValidate();


    }



}]);