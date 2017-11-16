angular.module('asfApp.ctrls.FeedbackListCtrls', [])
.controller('feedbackListCtrl', ['$scope', '$rootScope', '$routeParams', '$location', function ($scope, $rootScope, $rootParams, $location) {
    alert('Feedback List')


    $scope.items = [

         {
             id: 1,
             activityName: 'Iftar Party',
             townName: 'Mumbai',
             date: '15-Sep-2015',
             time: '12:00 am',
             contactPersonName: 'Mustaqeem',
             contactNumber: '9819300810',
             status: 'Completed'


         },

          {
              id: 2,
              activityName: 'Diwali Party',
              townName: 'Mumbai',
              date: '15-Sep-2015',
              time: '12:00 am',
              contactPersonName: 'Mustaqeem',
              contactNumber: '9819300810',
              status: 'Completed'


          },

           {
               id: 3,
               activityName: 'Workshop',
               townName: 'Mumbai',
               date: '15-Sep-2015',
               time: '12:00 am',
               contactPersonName: 'Mustaqeem',
               contactNumber: '9819300810',
               status: 'Completed'


           },

         {
             id: 3,
             activityName: 'Iftar Party',
             townName: 'Mumbai',
             date: '15-Sep-2015',
             time: '12:00 am',
             contactPersonName: 'Mustaqeem',
             contactNumber: '9819300810',
             status: 'Completed'


         },

    ];
    $scope.q = $scope.items[0]

}]);