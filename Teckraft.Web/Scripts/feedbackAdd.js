angular.module('asfApp.ctrls.FeedbackAddCtrls', [])
.controller('feedbackAddCtrl', ['$scope', '$rootScope', '$routeParams', '$location', function ($scope, $rootScope, $rootParams, $location) {
    alert('Feedback Add');

    $scope.item = {
       
        numberOfParticipant: '',
        responseOfParticipant: '',
        expParticipant: '',
        recipe: '',
        feedbackProductAvailibility: '',
        
    };


    $scope.formValidate = function () {
        alert("hello");
        console.log($scope.item);
       
    }

    $scope.add = function () {
        
        $scope.formValidate();
    }

   // $scope.q = $scope.items[0]

}]);