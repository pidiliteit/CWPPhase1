angular
    .module('myApp.ctrl.dashboard', [])
    .controller('dashboardCtrl', [
        '$scope',
        '$rootScope',
        '$location',
        'CRService',

        function ($scope, $rootScope, $location, CRService) {
          //  alert('in dashboard ctrl')
            //var header = document.querySelector('.navbar-fixed-top')
            //classie.remove(header, 'navbar-shrink');
            $rootScope.goHome = function (path) {
                $rootScope.checkBack = true;
               // $scope.slide = 'slide-left';
                $location.url(path);
            }
            //alert()
            $scope.runningService = false;
            $scope.loadingCR = true;
            $scope.loadingTask = true;
            $scope.toggleDetail = function (i) {
               // alert(i.id)
                console.log(i.showdetail);
                if (!i.showdetail)
                    i.showdetail = true;
                else
                    i.showdetail = false;
            }

            $scope.toggleDetail1 = function (i) {
                // alert(i.id)
              //  alert(i.showdetail1);
                console.log(i.showdetail1);
                if (!i.showdetail1)
                    i.showdetail1 = true;
                else
                    i.showdetail1 = false;
            }
            var query = {
                CurrentPage: 1,
                PageSize: 100000,
                Parameters: [{ name: "Status", value: "Pending" }, { name: "mypending", value: "" }]
            }
            CRService.getCRFilter(query).success(function (data) {
                if(typeof(data)=='string') location.href='/'
                for (var arr in data.items) {
                    data.items.showdetail = false;
                }
                $scope.crList = data.items;
                $scope.loadingCR = false;
            })

            var taskquery = {
                CurrentPage: 1,
                PageSize: 100000,
                Parameters: [{ name: "Status", value: "Pending" }, { name: "mypending", value: "" }]
            }
            CRService.getPhaseTask(taskquery).success(function (data) {
                if (typeof (data) == 'string') location.href = '/'
                $scope.pendingTask = data.items;
                $scope.loadingTask = false;
            });

            CRService.getCurrentUserDetail(null).success(function (data) {
                if (typeof (data) == 'string') location.href = '/'
                // console.log(data);
             //   alert(data)
                $scope.currentUser = data;
                //$scope.cr.company = $scope.currentUser.company;
                //    $scope.cr.division = $scope.currentUser.division;
              //  $scope.cr.rcb = data;
            });

            $scope.navigationManager = navigationManagerFactory();
            //$scope.subModuleCodes = function (cr) {
            //    var text = "";
            //    angular.forEach(cr.mdcSubModules, function (value, key) {
            //        text = text += value.subModuleName + ", ";
            //    });
            //    if (text.length > 0) text = text.substr(0, text.length - 2);
            //    return text;
            //}
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

            $scope.loadCalendar = function () {
               // alert('full calendar')
                /* initialize the external events

                 -----------------------------------------------------------------*/
                function ini_events(ele) {
                    ele.each(function () {

                        // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
                        // it doesn't need to have a start or end
                        var eventObject = {
                            title: $.trim($(this).text()) // use the element's text as the event title
                        };

                        // store the Event Object in the DOM element so we can get to it later
                        $(this).data('eventObject', eventObject);

                        // make the event draggable using jQuery UI
                        $(this).draggable({
                            zIndex: 1070,
                            revert: true, // will cause the event to go back to its
                            revertDuration: 0  //  original position after the drag
                        });

                    });
                }
                ini_events($('#external-events div.external-event'));

                /* initialize the calendar
                 -----------------------------------------------------------------*/
                //Date for the calendar events (dummy data)
                var date = new Date();
                var d = date.getDate(),
                        m = date.getMonth(),
                        y = date.getFullYear();
               // alert('hiii')
                $('#calendar').fullCalendar({
                    header: {
                        left: 'prev,next today',
                        center: 'title',
                        right: 'month,agendaWeek,agendaDay'
                    },
                    buttonText: {
                        today: 'today',
                        month: 'month',
                        week: 'week',
                        day: 'day'
                    },
                    //Random default events
                    events: [
                      {
                          title: 'All Day Event',
                          start: new Date(y, m, 1),
                          backgroundColor: "#f56954", //red
                          borderColor: "#f56954" //red
                      },
                      {
                          title: 'Long Event',
                          start: new Date(y, m, d - 5),
                          end: new Date(y, m, d - 2),
                          backgroundColor: "#f39c12", //yellow
                          borderColor: "#f39c12" //yellow
                      },
                      {
                          title: 'Meeting',
                          start: new Date(y, m, d, 10, 30),
                          allDay: false,
                          backgroundColor: "#0073b7", //Blue
                          borderColor: "#0073b7" //Blue
                      },
                      {
                          title: 'Lunch',
                          start: new Date(y, m, d, 12, 0),
                          end: new Date(y, m, d, 14, 0),
                          allDay: false,
                          backgroundColor: "#00c0ef", //Info (aqua)
                          borderColor: "#00c0ef" //Info (aqua)
                      },
                      {
                          title: 'Birthday Party',
                          start: new Date(y, m, d + 1, 19, 0),
                          end: new Date(y, m, d + 1, 22, 30),
                          allDay: false,
                          backgroundColor: "#00a65a", //Success (green)
                          borderColor: "#00a65a" //Success (green)
                      },
                      {
                          title: 'Click for Google',
                          start: new Date(y, m, 28),
                          end: new Date(y, m, 29),
                          url: 'http://google.com/',
                          backgroundColor: "#3c8dbc", //Primary (light-blue)
                          borderColor: "#3c8dbc" //Primary (light-blue)
                      }
                    ],
                    editable: true,
                    droppable: true, // this allows things to be dropped onto the calendar !!!
                    drop: function (date, allDay) { // this function is called when something is dropped

                        // retrieve the dropped element's stored Event Object
                        var originalEventObject = $(this).data('eventObject');

                        // we need to copy it, so that multiple events don't have a reference to the same object
                        var copiedEventObject = $.extend({}, originalEventObject);

                        // assign it the date that was reported
                        copiedEventObject.start = date;
                        copiedEventObject.allDay = allDay;
                        copiedEventObject.backgroundColor = $(this).css("background-color");
                        copiedEventObject.borderColor = $(this).css("border-color");

                        // render the event on the calendar
                        // the last `true` argument determines if the event "sticks" (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
                        $('#calendar').fullCalendar('renderEvent', copiedEventObject, true);

                        // is the "remove after drop" checkbox checked?
                        if ($('#drop-remove').is(':checked')) {
                            // if so, remove the element from the "Draggable Events" list
                            $(this).remove();
                        }

                    }
                });

                /* ADDING EVENTS */
                var currColor = "#3c8dbc"; //Red by default
                //Color chooser button

            };
           // $scope.loadCalendar();

            /*
      * BAR CHART
      * ---------
      */

            var bar_data = {
                data: [["Ontime", 0], ["1-5", 0], ["6-10", 0], ["11-15", 0], [">15", 0]],
                color: "#3c8dbc"
            };
            $.plot("#taskOverdue", [bar_data], {
                grid: {
                    borderWidth: 1,
                    borderColor: "#f3f3f3",
                    tickColor: "#f3f3f3"
                },
                series: {
                    bars: {
                        show: true,
                        barWidth: 0.5,
                        align: "center"
                    }
                },
                xaxis: {
                    mode: "categories",
                    tickLength: 0
                }
            });
            /* END BAR CHART */

            var g1 = new JustGage({
                id: "pendingTaskIndicator",
                value: 0,
                min: 0,
                max: 0,
                title: "Pending",
                label: "Tasks"
            });
        }]);


