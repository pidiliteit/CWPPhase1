'use strict';

var app = angular.module('asfApp', [
    'ngTouch',
    'ngRoute',
    'ngAnimate',
    'ngSanitize',
    'asfApp.ctrls.RequisitionListCtrls',
    'asfApp.ctrls.RequisitionAddCtrls',
    'asfApp.ctrls.AttendanceListCtrls',
    'asfApp.ctrls.AttendanceViewCtrls',
    'asfApp.ctrls.FeedbackListCtrls',
    'asfApp.ctrls.FeedbackAddCtrls',
   
    'ui.bootstrap',
    'ui.bootstrap.datepicker',
    
]).

config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

   
    $routeProvider.when('/home', { templateUrl: '/Requisition/Index', controller: 'requisitionListCtrl' });
    $routeProvider.when('/addReq', { templateUrl: '/Requisition/AddRequisition', controller: 'requisitionAddCtrl' });
    $routeProvider.when('/addReq/:id', { templateUrl: '/Requisition/AddRequisition', controller: 'requisitionAddCtrl' });
    $routeProvider.when('/attendanceList', { templateUrl: '/Attendance/Index', controller: 'attendanceListCtrl' });
    $routeProvider.when('/attendanceView', { templateUrl: '/Attendance/ViewAttendance', controller: 'attendanceViewCtrl' });
    $routeProvider.when('/attendanceView/:id', { templateUrl: '/Attendance/ViewAttendance', controller: 'attendanceViewCtrl' });
    $routeProvider.when('/feedbackList', { templateUrl: '/Feedback/Index', controller: 'feedbackListCtrl' });
    $routeProvider.when('/addfeedback', { templateUrl: '/Feedback/AddFeedback', controller: 'feedbackAddCtrl' });



    $routeProvider.otherwise({ redirectTo: '/home' });
    //  $locationProvider.html5Mode(true);
}]);



