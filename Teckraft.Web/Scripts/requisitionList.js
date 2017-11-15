angular.module('asfApp.ctrls.RequisitionListCtrls', [])
.controller('requisitionListCtrl', ['$scope', '$rootScope', '$routeParams', '$location', function ($scope, $rootScope, $rootParams, $location) {
    //alert('hiii')

    $scope.items = [

         {
             id: 1,
             activityName: 'Iftar Party',
             townName: 'Mumbai',
             date: '15-Sep-2015',
             time: '12:00 am',
             contactPersonName: 'Mustaqeem',
             contactNumber: '9819300810',
             status: 'Send for Approval'
            
            
         },

          {
              id: 2,
              activityName: 'Iftar Party',
              townName: 'Mumbai',
              date: '15-Sep-2015',
              time: '12:00 am',
              contactPersonName: 'Mustaqeem',
              contactNumber: '9819300810',
              status: 'Reject'


          },

           {
               id: 3,
               activityName: 'Iftar Party',
               townName: 'Mumbai',
               date: '15-Sep-2015',
               time: '12:00 am',
               contactPersonName: 'Mustaqeem',
               contactNumber: '9819300810',
               status: 'Send Back'


           },
        
         {
             id: 3,
             activityName: 'Iftar Party',
             townName: 'Mumbai',
             date: '15-Sep-2015',
             time: '12:00 am',
             contactPersonName: 'Mustaqeem',
             contactNumber: '9819300810',
             status: 'Approved'


         },

    ];

  


    $scope.q = $scope.items[0]

}]);