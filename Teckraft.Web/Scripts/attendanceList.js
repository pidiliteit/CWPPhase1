angular.module('asfApp.ctrls.AttendanceListCtrls', [])
.controller('attendanceListCtrl', ['$scope', '$rootScope', '$routeParams', '$location', function ($scope, $rootScope, $rootParams, $location) {
    alert('hiii')

    $scope.items = [

         {
             id: 154151,
             activityName: 'Iftar Party',
             townName: 'Mumbai',
             date: '15-Sep-2015',
             time: '12:00 am',
             contactPersonName: 'Mustaqeem',
             contactNumber: '9819300810',
             status: 'Completed',
             attendance: [
                 {
                     id: 1,
                     attContactNumber: "19898989898",
                     
                 },

                 {
                     id: 2,
                     attContactNumber: "29898989898",

                 },

                  {
                      id: 3,
                      attContactNumber: "39898989898",

                  },
                  {
                      id: 3,
                      attContactNumber: "39898989898",

                  },
                  {
                      id: 3,
                      attContactNumber: "39898989898",

                  },
                  {
                      id: 3,
                      attContactNumber: "39898989898",

                  },
                   {
                       id: 4,
                       attContactNumber: "49898989898",

                   }


             ]
             


         },

          {
              id: 884874,
              activityName: 'Diwali Party',
              townName: 'Mumbai',
              date: '15-Sep-2015',
              time: '12:00 am',
              contactPersonName: 'Mustaqeem',
              contactNumber: '9819300810',
              status: 'Completed',
              attendance: [
                 {
                     id: 1,
                     attContactNumber: "19898989898",
                     
                 },

                 {
                     id: 2,
                     attContactNumber: "29898989898",

                 },

                  {
                      id: 3,
                      attContactNumber: "39898989898",

                  },
                   {
                       id: 4,
                       attContactNumber: "49898989898",

                   }
    ]

          },

           

         

    ];
   

}]);