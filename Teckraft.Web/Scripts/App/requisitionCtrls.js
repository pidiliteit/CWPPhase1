﻿
//angular.
//    module('myApp.ctrl.MY', [])
//    .controller('myCtrl', ['$scope', '$location', function ($scope, $location) {

//        alert('')
//        console.log('mycontroller')
//        //alert('my controller')
//        $scope.student = { name: 'mushfiq', age: 10 };
//        $scope.showJson = function () {
//            alert(JSON.stringify($scope.student))
//        }

//    }]);
var __months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']

angular.
    module('myApp.ctrl.CR', [])
    .controller('CrCtrl', [
        '$scope',
        '$rootScope',
        '$routeParams',
        '$location',
        'CRService',
        function ($scope, $rootScope, $routeParams, $location, CRService) {
            $scope.crList = [];
            $scope.page = 1;
            $scope.showing = "";
            $scope.totalRecords = 0;
            $scope.totalPages = 0;
            $scope.pageSize = 10;
            $scope.currentRecords = "";
            $scope.runningService = true;
            $scope.filters = [{ name: "CRCode", value: "" },
                { name: "RequestedDate", value: "" },
                { name: "RequestType", value: "" },
                { name: "Module", value: "" },
                { name: "SubModule", value: "" },
                { name: "StatusDraft", value: "" },
                { name: "StatusApproved", value: "" },
                { name: "StatusPending", value: "" },
                { name: "Status", value: "" }

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
            $scope.modules = [];
            $scope.submodules = [];
            $scope.businessFunctions = [];
            $scope.tempSubmodules = [];
            $scope.tempfunctions = [];
            $scope.requestTypes = [];
            $scope.itfhUsers = [];
            //  $scope.processApprovers[];
            $scope.statusArr = ["Pending", "Sent Back", "Rejected", "Completed"]
            //$scope.subModuleCodes = function (cr) {
            //    var text = "";
            //    angular.forEach(cr.mdcSubModules, function (value, key) {
            //        text = text += value.subModuleName + ", ";
            //    });
            //    if (text.length > 0) text = text.substr(0, text.length - 2);
            //    return text;
            //}
            
            $scope.showNext = function () {
                if ($scope.page >= $scope.totalPages) return;
                $scope.page++;
                $scope.ApplyFilter();
            }

            $scope.toggleDetail = function (i) {
             //   alert(i)
                console.log(i.showdetail);
                if (!i.showdetail)
                    i.showdetail = true;
                else
                    i.showdetail = false;
            }

            $scope.toggleDetail1 = function (i) {
               // alert('hiii')
                //alert(i.showdetail1)
//                console.log(i.showdetail1);
                if (!i.showdetail1)
                    i.showdetail1 = true;
                else
                    i.showdetail1 = false;
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
                .getCRFilter(query)
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
 
            $scope.clear = function () {
                $scope.filters[0].value = "";
                $scope.filters[1].value = "";
                $scope.filters[2].value = "";
                $scope.filters[3].value = "";
                $scope.filters[8].value = "";
                $scope.ApplyFilter();
            }

 
            $rootScope.goHome = function (path) {
                $rootScope.checkBack = true;
                $location.url(path);
            }


           // $scope.cr.pocessApproverFlag = false;



            $scope.addNewRecord = function () {
                $location.path("/settings/addreason");
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
            console.log($routeParams)
            
            if ($routeParams.filter && $routeParams.filter == "status" && $routeParams.value != "") {
                //$scope.filters.push({ name: "Status", value: $routeParams.value });
                console.log();
                $scope.filters[8].value = $routeParams.value;

                $scope.ApplyFilter();
            }
            else if ($routeParams.filter && $routeParams.filter == "search" && $routeParams.value != "") {
                $scope.filters.push({ name: "CRCode", value: $routeParams.value });
                $scope.ApplyFilter();
            } 
            else {
                ///alert();
                //$scope.filters.push({ name: "Status", value: "Pending" });
                $scope.filters[8].value = "Pending";
                $scope.ApplyFilter();
            }
           
            CRService.getModules(null).success(function (data) {
                if (typeof (data) == 'string') location.href = '/'
                //console.log(data);
                $scope.modules = data.items;
            }).error(function (e) {

            });
            CRService.getitfhUsers(null).success(function (data) {
                //console.log(data);
                $scope.itfhUsers = data.items;
            });
           
            CRService.getsubitfhUsers(null).success(function (data) {
                $scope.subitfhUsers = data.items;
            });

            CRService.getSubModules(null).success(function (data) {
                //console.log(data);
                $scope.submodules = data.items;
                $scope.tempSubmodules = data.items;
            });
            CRService.getBusinessFunctions(null).success(function (data) {
                //console.log(data);
                //$scope.bfunctions = data.items;
                $scope.businessFunctions = data.items;
            });
            CRService.getRequestTypes(null).success(function (data) {
                //console.log(data);
                //$scope.bfunctions = data.items;
                $scope.requestTypes = data.items;
            });
        }
    ]);



///////////////////////////////////////////////

angular
    .module('myApp.ctrl.createRequisition', [])
    .controller('createRequisitionCtrl', [
        '$scope',
        '$rootScope',
        '$routeParams',
        '$location',
        'CRService',
        'divisionService',
        'reasonService'
        ,
function ($scope, $rootScope, $routeParams, $location, CRService, divisionService, reasonService) {
    $scope.dateOptions = {
        showWeeks: 'false'
    };

           $rootScope.goHome = function (path) {
                $rootScope.checkBack = true;
           
                $location.url(path);
           }
           $scope.names = new Array();
           $scope.names.push({ name: 'mushfiq' });
           $scope.names.push({ name: 'mushfiq1' });
           $scope.names.push({ name: 'mushfiq2' });

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
            
          
    $scope.cr = {
        crTypeId: 0,
                //changeRequestNo: '1',
                //changeRequestCode: '001',
                //subject: 'abc',
                //requestDate: '02/02/2015',
                //companyCode: '100',
                //divisionCode: 'A1',
                //requestTypeId: '1',
                //moduleId: '1',
                //subModuleId: '1',
                //functionId: '1',
                //agreed: 'true',
                //ITFHUser: '2',
                //details: 'qwert',
                //RCT: '02/02/2015',
                //RUT: '02/02/2015',
                //RCB: '1',
                //RUB: '1',
                //statusCode: 'B01',
                //projectStage: '',
                //requestType: '',


                //RequestedBy: 'abc',
                //Company: 'Teckraft',
                //RequestedDate: '',
                //RequestType: 'qwe',

                //-------------------------
                changeRequestNo: '0',
                changeRequestCode: '',
                subject: '',
        requestDate: 0,
                companyCode: '',
                divisionCode: '',
                requestTypeId: '',
                moduleId: '',
                subModuleId: '1',
                functionId: '1',
                agreed: null,
                details: '',
                RCT: '',
                RUT: '',
                RCB: '1',
                RUB: '1',
                statusCode: '0',
                projectStage: '',
                requestType: '',
            }
    $scope.currentDate = new Date();
    $scope.fileTemplate = function (format) {
        if (format == 'ScopeDocument' && $scope.cr.subModule && $scope.cr.subModule.subModuleCode == 'TCODE') return ('formats/ANNEXURE-4 New TCODE Data Sheet.xls')
        else if (format == 'ScopeDocument' && $scope.cr.subModule && $scope.cr.subModule.subModuleCode == 'CHROLE') return ('formats/ANNEXURE-5 SAP Change in Rights.xls')
        else if (format == 'ScopeDocument' && $scope.cr.subModule && $scope.cr.subModule.subModuleCode == 'NUC') return ('formats/ANNEXURE-3 SAP New User Creation Sheet.xls')
        else if (format == 'HW / SW') {
            return ('formats/PIL-Infra Sizing.xls')
        }
        else if (format == 'UAT') {
            return ('formats/FormatofUAT.doc')
        }
    }
    $scope.downloadSubmodules = function (format) {
                 
        window.open($scope.fileTemplate(format));
                //location.href = '/Requisition/ExportSubmodulesToExcel'
                 
            }

            $scope.isEditable = true;
            
            $scope.toggleCrAssignmentDetail = function (i) {
               // console.log(i.showdetail);
                if (!i.hidedetail)
                    i.hidedetail = true;
                else
                    i.hidedetail = false;
            }
 
            $scope.reasons = [];
            reasonService.getReasons(null).success(function (data) {
                $scope.reasons = data.items
                //console.log(data.items);
                //alert(data);
            });

            $scope.AddPhase = function () {
                if ($scope.cr.phases == null) $scope.cr.phases = []
        var pno = 1;
                if ($scope.cr.phases && $scope.cr.phases.length > 0) pno = $scope.cr.phases.length + 1
                $scope.cr.phases.push({
                    // phaseTitle: 'Phase ' + pno, remarks: $scope.cr.module.moduleName, startDate: '', endDate: ''
                    phaseTitle: 'Phase ' + pno, remarks: ($scope.cr.subject != null && $scope.cr.subject.length > 50) ? $scope.cr.subject.substr(0, 49) : $scope.cr.subject, startDate: '', endDate: ''
            , itatDate: '', uatDate: '',
            tasks: [{ subject: ($scope.cr.subject != null && $scope.cr.subject.length > 50) ? $scope.cr.subject.substr(0, 49) : $scope.cr.subject }]
                    
                });
            }
            $scope.AddTask = function (i) {
                if (i.tasks == null) i.tasks = []
                i.tasks.push({
                     subject: ($scope.cr.subject != null && $scope.cr.subject.length > 50) ? $scope.cr.subject.substr(0, 49) : $scope.cr.subject
                });
            }
            $scope.SaveAssigment = function () {
                console.log($scope.crAssignmentPhaseList);
            }

            $scope.removePhase = function (i) {
        //console.log(i);
                $scope.cr.phases.splice(i, 1);
            }
            $scope.removePhaseTask = function (ph, e) {
                ph.tasks.splice(e, 1);

            }
            $scope.removePhaseVendor = function (i) {
                //console.log(i);
                $scope.cr.vendorDetails.splice(i, 1);
            }
            
            $scope.getAttachment = function (fileId) {
                for (var e in $scope.cr.attachments) {
                    if ($scope.cr.attachments[e].fileId == fileId) return $scope.cr.attachments[e];
                }
            }
            $scope.removeAttachment = function (id) {
                //console.log(i);
                //alert($scope.cr.attachments[i].id)
                var i = $scope.cr.attachments.indexOf($scope.getAttachment(id));
                if ($scope.cr.id == 0 || !$scope.cr.attachments[i].id || $scope.cr.attachments[i].id == 0)
                    $scope.cr.attachments.splice(i, 1);
                else $scope.cr.attachments[i].deleted = true;
            }
            
            $scope.attachWireframe = function () {
                $scope.action.statusCode = 2;
            }

            $scope.crAssignmentPhaseList = [//{
                //crPhase: [
                    {
                Phase: "Phase1", PhaseDesc: "Some text here.", showdetail: false, StartDate: "21/06/2015", EndDate: "29/06/2015", ITATdate: "23/06/2015", UATdate: "18/07/2015",
                        PhaseTask: [
                            { TaskNo: "T1001", TaskName: "Task1", TaskMember: "Gary", StartDate: "20/06/2015", EndDate: "29/06/2015", ITATdate: "20/06/2015", UATdate: "01/07/2015" },
                            { TaskNo: "T1002", TaskName: "Task2", TaskMember: "Chintan", StartDate: "19/06/2015", EndDate: "21/06/2015", ITATdate: "20/06/2015", UATdate: "21/06/2015" }
                        ]
                    },
                    {
                        Phase: "Phase2", PhaseDesc: "Some text here.", showdetail: false, StartDate: "19/06/2015", EndDate: "24/06/2015", ITATdate: "18/06/2015", UATdate: "19/07/2015",
                        PhaseTask: [
                            { TaskNo: "T1003", TaskName: "Task1", TaskMember: "Akshay", StartDate: "20/06/2015", EndDate: "29/06/2015", ITATdate: "20/06/2015", UATdate: "01/07/2015" },
                            { TaskNo: "T1003", TaskName: "Task2", TaskMember: "Chintan", StartDate: "19/06/2015", EndDate: "21/06/2015", ITATdate: "20/06/2015", UATdate: "21/06/2015" }
                        ]
                    },
                //],
                

                //}
    ];
                

             var date = new Date();
             //var d = date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
             $scope.cr.requestDate = date

             $scope.priorities = [{ name: "High", id: 1 },
                 { name: "Medium", value: 2 },
                 { name: "Low", value: 3 }];
    
    $scope.CRAttachments = [{ fileName: "", filePath: "" }];
    $scope.divisions = []
            $scope.modules = [];
            $scope.submodules = [];
            $scope.businessFunctions = [];
            $scope.tempSubmodules = [];
            $scope.tempfunctions = [];
            $scope.requestTypes = [];
            $scope.itfhUsers = [];
    $scope.action = { statusCode: 0, item: null };
            $scope.AttachmentType = "";
            $scope.disableSubmit = false;
            
            $scope.actionTypeChanged = function (tk) {
                //alert(tk.statusCode1)
               
                if (tk.statusCode1 == 5) {
                    CRService.getTeamByITFH(tk.assignedTo.id).success(function (data) {
                tk.itTeam = data.items
                    });
                }
            }
            $scope.actionTypeChangedAssignment = function (tk) {
                    CRService.getTeamByITFH(tk.assignedTo.id).success(function (data) {
                        tk.itTeam = data.items
                    });
            }
            $scope.AttachFile = function (title) {
                //alert($scope.AttachmentType);
                $scope.AttachmentType = title;
                $("#if_CRAttachment").attr("src", "../Requisition/_UploadAttachment");
                //var frame = window.frames["frame_CRAttachment"];
                //frame.document.getElementById('attachmentType').value = $scope.AttachmentType;
                $('#CRAttachment').modal('show')
            }

            $scope.UploadCRAttachment = function () {
                var frame = window.frames["frame_CRAttachment"];
                frame.document.getElementById('attachmentType').value = $scope.AttachmentType;
        frame.document.getElementById('selectedSubModule').value = $scope.cr.subModule ? $scope.cr.subModule.subModuleCode : '';

                var file = window.frames[0].document.getElementById('file1').value.split('.');
                console.log(file[1])
                var ext = file[file.length - 1].toLowerCase();
                if (file == "") {
                    alert("Please select file")
                    return false;
                }
        else if (file[1] != null) {
                    var invalidformate;
                    var fileExtension = ['jpg', 'jpeg', 'gif', 'png', 'xls', 'xlsx', 'doc', 'docx', 'txt', 'pdf', 'tif', 'ppt', 'pptx', 'pps', 'ppsx', 'odt', 'ods', 'odp', 'csv', 'eml', 'zip', '7z', 'rep','xps']; // ['xls', 'xlsx','doc','docx','pdf','jpg'];
            //if (jQuery.inArray(file[1].toLowerCase(), fileExtension) != -1) {
                    if (jQuery.inArray(file[file.length - 1].toLowerCase(), fileExtension) != -1) {
                                window.top.frame_CRAttachment.document.CRForm.submit();
                                return true;
                            }
                            else {
                                alert("File extension not supported!");
                                return false;
                            }
 
                        }
                

                //var fileExtension = ['pdf', 'xls', 'xlsx', 'csv', 'doc', 'docx'];
                //if ($.inArray($("#if_CRAttachment form .Selectedfile").val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                //    alert("Selected File is not valid for Uploading.");
                //    return false;
                //}
                //else {

                //    var file = $(".Selectedfile").val().split('.');
                //    if (file == "") {
                //        alert("Please select file")
                //        return false;
                //    }
                //    else {
                //        $("#btn_FileAdd").val("Uploading......");
                //        $("#btn_FileAdd").attr("disabled", "disabled");
                //        $("#btn_Cancel").hide();
                //        var ext = file[file.length - 1];
                //        return true
                //    }
                //}
            }
            
        
            //jQuery.inArray(file[file.length - 1].toLowerCase(), fileExtension) != -1

            $scope.validateDays = function (vd) {
                var str = vd.estimatedDaysEffort;
                var a = String(str)
                if (a.indexOf(".") > 0) {
                    alert("Please enter proper days.")
                    return false;
                }
            }

          

            $scope.grcSubModuleChanged = function () {
                if ($scope.hasAttachment($scope.cr, 'ScopeDocument') > 0) {
                    alert('The previous Attachments will be removed')
                    if (1==1) {
                        for (var e in $scope.cr.attachments) {
                            if ($scope.cr.attachments[e].attachmentType == 'ScopeDocument')
                                $scope.removeAttachment($scope.cr.attachments[e].fileId);
                        }

                    }
                    else {
                        return false;
                    }
                }
                return true;
            }


            //$scope.subModuleCodes = function () {
            //    var text = "";

            //    angular.forEach($scope.cr.mdcSubModules, function (value, key) {
            //      //  alert(value)
            //        text = text += value.subModuleName + ", ";
            //    });
            //    if (text.length > 0) text = text.substr(0, text.length - 2);
            //    return text;
            //}
            $scope.moduleChanged = function () {
                //alert("hii")
               // console.log($scope.cr.module);
               
                //console.log($scope.cr.userDetail.module);
                //if ($scope.cr.userDetail.module.id>= 1) {
                //    alert();
                //    var d = "{ CurrentPage: 1, PageSize: 99999, Parameters: { Name: 'getSubmoduleMapping', Value: " + $scope.cr.userDetail.module.id + " } }";
                //    console.log(d);
                //    CRService.getSubModules(d).success(function (data) {
                //        console.log(data);
                //        $scope.submodules = data.items;
                //    });
                //}
              //  console.log($scope.cr.userDetail.module);
                //if ($scope.cr.module!=null && $scope.cr.module.id >= 1) {
                //    $scope.submodules = [];
                //   // $scope.businessFunctions = [];
                //    for (var arr in $scope.tempSubmodules) {
                //        //console.log($scope.tempSubmodules[arr]);
                //        if ($scope.tempSubmodules[arr].moduleId == $scope.cr.module.id) {
                //            $scope.submodules.push($scope.tempSubmodules[arr]);
                //        }
                //    }
                //  //  $("#submodule").removeAttr("readonly", "readonly");
                //}
                //else {
                //    $scope.submodules = [];
                //    //$("#submodule").attr("readonly", "readonly");
                //}
                
            }
    $scope.open = function ($event, item, name) {
            
                if (!item.datepickers) item.datepickers = {};

                $event.preventDefault();
                $event.stopPropagation();
                item.datepickers[name] = true;
            };
            $scope.setTaskEndDate = function (task) {
                if (task.startDate && task.noOfDays) {
                    var d = parseInt(task.noOfDays)
                    var date = new Date(task.startDate);
                    var newdate = new Date(date.setDate(date.getDate() + d));
                    //alert(newdate)
                    task.endDate = newdate;
                }
            }
            $scope.isDtBoxOpened = function () {
                return true;
            }
 
            $scope.addAttachment = function (file) {
                //  alert($scope.cr.subject);
                if (!$scope.cr.attachments) $scope.cr.attachments = [];
                $scope.cr.attachments.push({
                    deleted: false,
                    id:0,
                    fileName: file.FileName,
                    fileId: file.FileId,
                    tempPath: file.TempPath,
                    serverPath: file.ServerPath,
                    attachmentType: file.AttachmentType
                });
                //alert(file.AttachmentType+'----')
               // alert($scope.cr.attachments.length);
                $scope.$apply();
                $scope.$digest();

                $('#CRAttachment').modal('toggle');
            }
            window.addAttachment = function (file) {
                $scope.addAttachment(file);
            }
            $scope.crTypeChanged = function () {
               // alert($scope.cr.id)
                //alert('hiii')
                $scope.cr.module = null;
                $scope.cr.subModule = null;
                //$scope.cr.mdcSubModules = null;
                if (!$scope.cr.id  || $scope.cr.id == 0) {
                    $scope.cr.agreed = null;
                    $scope.cr.meetingRequestDate = null;
                    
                }
                $("#testselect4").multiselect('destroy');
                window.setTimeout(function () {
                    $("#testselect4").multiselect();
                }, 100);
            }
            $scope.crModSubMod = function () {
                $scope.cr.module = null;
                $scope.cr.subModule = null;
                $scope.cr.itfhUser = null;
                //$scope.cr.mdcSubModules = null;
                if ($scope.cr.id == 0) {
                    $scope.cr.agreed = null;
                    $scope.cr.meetingRequestDate = null;
                }
            }
            $scope.disabledTypeId = function () {
                if (($scope.cr.changeRequestCode.substring(0, 1)) == 'M' || ($scope.cr.changeRequestCode.substring(0, 1)) == 'A') {
                    jQuery("input[name='crType']").each(function(i) {
                        jQuery(this).attr('disabled', 'disabled');

                       
                    });}
                else if (($scope.cr.crTypeId < 3) && $scope.cr.statusCode != 80 && $scope.cr.statusCode == 1 && $scope.cr.statusCode == 0) {
                    ///03aug2015
                  //  $scope.cr.typeIdMDC.disabled = true;
                    //$scope.cr.typeSAPAuth.disabled = true;
                    //disable mdc and sap authorization

                }
            }
    //03 aug 2015
            //$scope.removeAttachments = function (newAttachmentType) {
            //    alert("hii");
            //    $.each($scope.cr.attachments, function () {
            //        if (newAttachmentType == 'ScopeDocument') {
            //            $scope.cr.attachments.splice(cr.attachments[0], 1);
            //            //this = "Do Something Here";
            //        }
            //    });
            //}

    // 03 aug 2015
            $scope.setMultiselect = function () {
                $("#testselect4").multiselect();
            }

            $scope.phaseUATDateChanged = function (ph) {
                $scope.resetDate(ph, 'uatDate')
                $scope.cr.uatDate = ph.uatDate;
            }
            $scope.setEndDate = function (ph) {ph.startDate
                //alert("11")
        if (ph.startDate && ph.startDate != null && $scope.cr.estimatedDays && $scope.cr.estimatedDays > 0) {
                    var d = parseInt($scope.cr.estimatedDays)
                    var date = new Date(ph.startDate);
                    ph.endDate = getDate(date.setDate(date.getDate() + d));

                    var Edate = new Date(ph.endDate);
                    ph.itatDate = getDate(Edate.setDate(Edate.getDate() +1));
                    ph.tasks[0].itatDate = ph.itatDate;
                    
                    // 12 Oct 2015  
                    ph.tasks[0].noOfDays = $scope.cr.estimatedDays;
                    ph.tasks[0].startDate = ph.startDate;
                    ph.tasks[0].endDate = ph.endDate;
            // ph.tasks[0].subject = ph.remarks;

                    // ph.tasks[0].itatDate = ph.itatDate;
                    // 12 oct 2015
            
                   // alert(ph.endDate)
                }
            }
            $scope.addVendor = function () {
                //alert('iiiii')
                if (!$scope.cr.vendorDetails) $scope.cr.vendorDetails = [];
                $scope.cr.vendorDetails.push({ id: 0,isPreferredVendor:$scope.cr.vendorDetails.length==0? true:false, platform: $scope.cr.crTypeId == 3 ? 'MIS BO' : '', developmentBy: 'Internal', hardwareCost: 0, softwareCost: 0, securityCost: 0,platform: ($scope.cr.module!=null) ? $scope.cr.module.moduleName : '', description: ($scope.cr.subject != null && $scope.cr.subject.length > 50) ? $scope.cr.subject.substr(0, 50) : $scope.cr.subject });

               
                //if ($scope.cr.vendorDetails.platform != null) {
                 //   $scope.cr.vendorDetails.isPreferredVendor = $scope.cr.module.moduleName;
               // }
            }

            $scope.projectCategoryChanged = function () {
               // alert("abc length :" + $scope.cr.vendorDetails.length)
                if ($scope.cr.vendorDetails && $scope.cr.vendorDetails.length > 0) {
                    if ($scope.cr.subject && $scope.cr.subject.length > 50)
                        $scope.cr.vendorDetails[0].description = $scope.cr.subject.substr(0, 50);
                    else $scope.cr.vendorDetails[0].description = $scope.cr.subject;

                }
                //if ($scope.cr.vendorDetails[0].platform != null) {
                //    $scope.cr.vendorDetails[0].platform = $scope.cr.module.moduleName;
                //}
            }

            $scope.submoduleChanged = function () {
                console.log($scope.cr.submodule);
                if ($scope.cr.submodule != null && $scope.cr.submodule.id >= 1) {
                    //$scope.businessFunctions = [];
                    //for (var arr in $scope.tempfunctions) {
                    //    if ($scope.tempfunctions[arr].subModuleId == $scope.cr.userDetail.submodule.id) {
                    //        $scope.businessFunctions.push($scope.tempfunctions[arr]);
                    //    }
                    //}
                    //$("#bfunction").removeAttr("readonly", "readonly");
                }
                else {
                    //$scope.businessFunctions = [];
                    //$("#businessFunction").attr("readonly", "readonly");
                }

                if ($scope.cr.crTypeId == 5) {
                    return $scope.grcSubModuleChanged();
                }
                return true;
            }
            CRService.getCurrentUserDetail(null).success(function (data) {
               // console.log(data);
                $scope.currentUser = data;
                if (!$scope.cr.rcb) {
                    $scope.cr.company = $scope.currentUser.company;
                    //    $scope.cr.division = $scope.currentUser.division;

                    $scope.cr.rcb = data;
                }
            });
            CRService.getModules(null).success(function (data) {
                console.log(data);
        $scope.modules = data.items;
            });
            CRService.getitfhUsers(null).success(function (data) {
                console.log(data);
                $scope.itfhUsers = data.items;
            });
            
            CRService.getsubitfhUsers(null).success(function (data) {
                $scope.subitfhUsers = data.items;
            });

            CRService.getSubModules(null).success(function (data) {
                console.log(data);
                $scope.submodules = data.items;
                 
                setTimeout($scope.setMultiselect, 1000);
            });
            
            //CRService.getDivisionFunction().success(function (data) {
            //    //alert();
            //    $scope.businessFunctions = data.items
            //})
            $scope.divisionChanged = function () {
                //CRService.getDivisionFunction($scope.cr.division.id, $scope.cr.crTypeId == 5 ? 1 : $scope.cr.crTypeId).success(function (data) {
                CRService.getDivisionFunction($scope.cr.division.id).success(function (data) {
            $scope.businessFunctions = data.items
                })
            }
            //CRService.getBusinessFunctions(null).success(function (data) {
            //    console.log(data);
            //    //$scope.bfunctions = data.items;
            //    $scope.businessFunctions = data.items;
    //}); 
            divisionService.getDivision().success(function (data) {
                $scope.divisions = data.items;
                
            })

            $scope.attachmentsCount = function (cr, type) {
                var count = 0;
                if (cr && cr.attachments != null) {
                    for (var i = 0; i < cr.attachments.length; i++) {
                       // alert(cr.attachments[i].deleted)
                        if (cr.attachments[i].attachmentType == type && !cr.attachments[i].deleted) count++;
                    }
                }
                return count;
            }

            $scope.hasAttachment1 = function (cr, type) {
                //  alert(cr.attachments)
                if (cr && cr.attachments != null) {
                    for (var i = 0; i < cr.attachments.length; i++) {
                        alert(cr.attachments[i].attachmentType + '==' + type)
                        if (cr.attachments[i].attachmentType == type) return true;
                    }
                }
                return false;

            }

            $scope.hasAttachment = function (cr, type) {
              //  alert(cr.attachments)
                if (cr && cr.attachments != null) {
                    for (var i = 0; i < cr.attachments.length; i++) {
                      //  alert(cr.attachments[i].attachmentType +'=='+ type)
                        if (cr.attachments[i].attachmentType == type) return true;
                    }
                }
                return false;

            }
            $scope.checkDuplicate = function (arr, field, value) {
                var count = 0;
                for (var i = 0; i < arr.length; i++) {
                   // alert(arr[i][field]+'==='+value)
                    if (arr[i][field] && arr[i][field] != '' && arr[i][field] == value) count++;
                   // alert(count);
                    if (count > 1) return true;
                }
                return false;
            }

            $scope.getLogField= function (logDetail,field) {
                for (var i = $scope.cr.logs.length - 1; i >= 0; i--) {
                    if (logDetail.indexOf($scope.cr.logs[i].logDetail) >= 0) return $scope.cr.logs[i][field];
                }
            }
            $scope.getComment = function (logDetail) {
                for (var i = $scope.cr.logs.length - 1; i >= 0; i--) {
                    if (logDetail.indexOf($scope.cr.logs[i].logDetail)>=0) return $scope.cr.logs[i].comments;
                }
            }
            $scope.validatePrjCat = function () { 
                var result = true;
                // if ($scope.cr.infraCosting == 'Y' && !$scope.cr.hardwareCostReceived) return true;
               
                if ($scope.cr.understandingDocumentRequired && !$scope.hasAttachment($scope.cr, 'Understnding Document')) {
                    alert('Attach understanding document');
                    $('.pdd').focus()
                    result = false;
                }
        else if ($scope.cr.infraCosting && $scope.cr.infraCosting != 'N' && !$scope.cr.hardwareCostReceived && !$scope.hasAttachment($scope.cr, 'HW / SW')) {
                    alert('Attach Hardware Requirement Document');
                    $('.hwsw').focus()
                    result = false;
                }
                else if (($scope.cr.vendorDetails)) {
                   
                    for (var arr in $scope.cr.vendorDetails) {
                        //alert($scope.cr.vendorDetails[arr].selectedVendor)
                        if (!($scope.cr.vendorDetails[arr].description) || $scope.cr.vendorDetails[arr].description == "") {
                            alert("Enter Description");
                            $('.vdesc').eq(arr).focus()
                            result = false;
                            break;
                        }
                        else if (!($scope.cr.vendorDetails[arr].platform) || $scope.cr.vendorDetails[arr].platform == "") {
                            alert("Enter Platform");
                            $('.vplatform').eq(arr).focus()
                            result = false;
                            break;
                        }
                        else if (!($scope.cr.vendorDetails[arr].developmentBy) || $scope.cr.vendorDetails[arr].developmentBy == "") {
                            alert("Enter Internal / External");
                            $('.vdevby').eq(arr).focus()
                            result = false;
                            break;
                        }
                        else if ($scope.cr.vendorDetails[arr].developmentBy == 'External' && (!$scope.cr.vendorDetails[arr].agencyName || $scope.cr.vendorDetails[arr].agencyName == "")) {
                            alert("Enter Agency Name");
                            $('.vagency').eq(arr).focus()
                            result = false;
                            break;
                        }
                        else if ($scope.checkDuplicate($scope.cr.vendorDetails, 'agencyName', $scope.cr.vendorDetails[arr].agencyName)) {
                            alert("Duplicate Agency Name");
                            $('.vagency').eq(arr).focus()
                            result = false;
                            break;
                        }
                else if (($scope.cr.projectCategory == 'A' || $scope.cr.vendorDetails[arr].developmentBy == 'External') && (!$scope.cr.vendorDetails[arr].softwareCost || $scope.cr.vendorDetails[arr].softwareCost == "")) {
                            alert("Enter Software Cost");
                            $('.vsoftwarecost').eq(arr).focus()
                            result = false;
                            break;
                        }
                        else if (($scope.cr.projectCategory == 'A' || $scope.cr.infraCosting == 'Y') && (!$scope.cr.vendorDetails[arr].hardwareCost || $scope.cr.vendorDetails[arr].hardwareCost < 1)) {
                            alert("Enter Hardware Cost");
                            $('.vhardwarecost').eq(arr).focus()
                            result = false;
                            break;
                        }
                        //else if ($scope.cr.vendorDetails[arr].developmentBy == 'External' && !($scope.cr.vendorDetails[arr].securityCost) || $scope.cr.vendorDetails[arr].securityCost == "") {
                        //    alert("Enter Security Cost");
                        //    $('.vsecuritycost').eq(arr).focus()
                        //    result = false;
                        //    break;
                        //}
                        else if (!($scope.cr.vendorDetails[arr].estimatedDaysEffort) || $scope.cr.vendorDetails[arr].estimatedDaysEffort == "" || $scope.cr.vendorDetails[arr].estimatedDaysEffort == "0") {
                            alert("Enter Estimate Days");
                            $('.vestimatedays').eq(arr).focus()
                            result = false;
                            break;
                        }
                    }
                }
                 
                if ($scope.cr.currentTask && $scope.cr.currentTask.taskType.taskTypeCode=='PRJCAT' && !$scope.hasSelectedVendor() && $scope.cr.projectCategory != 'C') {
                    alert("Select atleast one Prefered Vendor")
                    $('.crPreferedVendor').focus();
                    result = false;
                }

                if (result && $scope.cr.currentTask != null && $scope.cr.currentTask.taskType.taskTypeCode == 'PRJCAT' && $scope.action.statusCode == 2 && (!($scope.action.comments) || $scope.action.comments == "")) {
                    alert("Enter Comment");
                    $('.comments').focus()
                    result = false;
                }
            
                

                return result;
            }

            $scope.hasSelectedVendor = function () {
                var Result = true;
                if (!$scope.cr.vendorDetails || $scope.cr.vendorDetails.length == 0) return false;
                for (var arr in $scope.cr.vendorDetails) {
                    if($scope.cr.vendorDetails[arr].isPreferredVendor==true){
                        Result = true;
                        return Result;
                    }
                    else{
                        Result=false;
                    }
                }
                return Result;
            }

            var datestring = function (dt) {
                return dt.getDate() + '-' + (dt.getMonth() + 1) + '-' + dt.getFullYear()
            }
            $scope.validateAssignment = function () {
                //alert()

                var maxtaskenddate = new Date(2015, 1, 1);
                var result = true;
                if (!$scope.cr.phases || $scope.cr.phases.length == 0) {
                    alert('Please add at least one Phase')
                    $('.addphasebtn').focus()
                    result = false;
                }
                else if ($scope.cr.phases.length > 0) {
                    for (var i = 0; i < $scope.cr.phases.length; i++) {
                        //aaliya
                        if (!$scope.cr.phases[i].remarks) {
                            alert("Please enter Phase Desc");
                            $('.phaseDesc').eq(i).focus();
                            result = false;
                            break;
                        }
                        else if (!$scope.cr.phases[i].startDate) {
                            alert("Please enter Phase Start date")
                            $('.phaseStartDate').eq(i).focus();
                            result = false;
                            break;
                        }
                        else if (!$scope.cr.phases[i].endDate) {//aaliya
                            alert('Please enter Phase End date');
                            $('.phaseEndDate').eq(i).focus();
                            result = false;
                            break;
                        }
                        else if (!$scope.cr.phases[i].itatDate) {//aaliya
                            alert('Please enter Phase ITAT date');
                            $('.phaseITATDate').eq(i).focus();
                            result = false;
                            break;
                        }

                        else if (!$scope.cr.phases[i].uatDate) {//aaliya
                            alert('Please enter Phase UAT date');
                            $('.phaseUATDate').eq(i).focus();
                            result = false;
                            break;
                        }
                      
                        //aaliya

                        if ($scope.cr.phases[i].tasks) {
                            for (var j = 0; j < $scope.cr.phases[i].tasks.length; j++) {
                                var task = $scope.cr.phases[i].tasks[j]
                                //      alert(new Date(task.endDate));
                                if (task.endDate && (new Date(task.endDate)) > maxtaskenddate) maxtaskenddate = new Date(task.endDate);

                                //aaliya

                                if (!task.subject) {//aaliya
                                    alert('Please enter Task Name');
                                    $('.taskName').eq(i).focus();
                                    result = false;
                                    break;
                                }

                                else if (!task.assignedTo) {//aaliya
                                    alert('Please select Task Assign to');
                                    $('.taskMember').eq(i).focus();
                                    result = false;
                                    break;
                                }
                                //else if (!task.teamMember) {//aaliya
                                //    alert('Please select Task Team Member');
                                //    $('.AssignmentTeamMember').eq(i).focus();
                                //    result = false;
                                //    break;
                                //}
                                else if (!task.noOfDays) {//aaliya
                                    alert('Please enter Task Days');
                                    $('.taskNoOfDays').eq(i).focus();
                                    result = false;
                                    break;
                                }

                                else if (!task.startDate) {//aaliya
                                    alert('Please enter Task Start date');
                                    $('.taskStartDate').eq(i).focus();
                                    result = false;
                                    break;
                                }

                                else if (!task.endDate) {//aaliya
                                    alert('Please enter Task End date');
                                    $('.taskEndDate').eq(i).focus();
                                    result = false;
                                    break;
                                }
                                //aaliya

                                if (!task.itatDate) {
                                    alert('Please enter Task ITAT date')
                                    $('.taskiatat').eq(j * i).focus();
                                    result = false;
                                    break;

                                }



                            }
                        }




                        //alert(maxtaskenddate)
                        // alert('--' + datestring(maxtaskenddate) + '!=' + datestring((new Date($scope.cr.phases[i].endDate))));
                        if (!$scope.cr.phases[i].tasks || $scope.cr.phases[i].tasks.length == 0) {
                            alert('Please add at least one task')
                            $('.addtaskbtn').eq(0).focus()
                            result = false;
                            break;
                        }
                        else if (datestring(maxtaskenddate) != datestring(getDate2($scope.cr.phases[i].endDate))) {
                            
                            var dt = getDate2($scope.cr.phases[i].endDate);
                            alert('At least one task should have end date till ' + dt.getDate() + '-' + (__months[ dt.getMonth() ]) + '-' + dt.getFullYear())
                            result = false;
                        }
                    }
                }
                if (result) {
                    if ($scope.cr.securityCheckRequired == true && ! $scope.action.securityCheckStartDate) {
                        alert('Please enter start date for security check');
                        // $('onholdtilldate').focus();
                        return false;
                    }

                    if ($scope.cr.securityCheckRequired == true && !$scope.action.securityCheckEndDate) {
                        alert('Please enter end date for security check');
                        // $('onholdtilldate').focus();
                        return false;
                    }

                    if ((!($scope.action.comments) || $scope.action.comments == "")) {

                        result = false;
                        $('.comments').focus()
                        alert("Enter Comment");
                       
                    }
                }
                return result;
            }

    CRService.getRequestTypes().success(function (data) {
                console.log(data);
                //$scope.bfunctions = data.items;Select MDC Sub Module
        $scope.requestTypes = data.items;
            });

            $scope.validate = function () {
               
                console.log("------------------------------------------------------------------------");
                console.log($scope.cr);
                
                if (!($scope.cr.division)) {
                    alert("Select Division");
                    $('.division').focus();
                }
                //else if (!($scope.cr.requestType)) {
                //    alert("Select Request Type");
                //    $('.requestType').focus();
                //}
                    //else if (!($scope.cr.crTypeId))
                    //    alert("Select CR Type");
                else if (!($scope.cr.module) || $scope.cr.module == "") {
                    alert("Select Module");
                    $('.module').focus();
                }
                //else if ($scope.cr.crTypeId != 4 && (!($scope.cr.subModule) || $scope.cr.subModule == "")) {
                else if (!($scope.cr.subModule) || $scope.cr.subModule == "") {
                    alert("Select Sub Module");
                    $('.subModule').focus();
                }
                //else if ($scope.cr.crTypeId == 4 && (!$scope.cr.mdcSubModules || $scope.cr.mdcSubModules.length == 0)) {
                //else if ($scope.cr.crTypeId == 4) {
                //    alert("Select MDC Sub Module");
                //    $('.subModule').focus();

                //}
                else if (!($scope.cr.businessFunction) || $scope.cr.businessFunction == "") {
                    alert("Select Function");
                    $('.businessFunction').focus();

                }
                    //if crTypeId == 1,2,3
                else if (($scope.cr.crTypeId == 1 || $scope.cr.crTypeId == 2 || $scope.cr.crTypeId == 3) && $scope.cr.agreed == null) {
                    alert("Select 'Discussed & agreed'");
                    $('.agreed').focus();
                }
                else if (($scope.cr.crTypeId == 1 || $scope.cr.crTypeId == 2 || $scope.cr.crTypeId == 3) && !($scope.cr.itfhUser)) {
//alert($scope.cr.agreed)
                    alert("Select ITFH");
                    $('.itfh').focus();
                }
                else if ($scope.cr.agreed == false && $scope.cr.crTypeId<=3 && !($scope.cr.meetingRequestDate)) {//aaliya
                    //aaliya
                    
                        alert("Enter Suggested Date for Discussion.");
                        $('.meetingRequestDate').focus();
                    
                }
                    //end crTypeId == 1,2,3 
                else if (!($scope.cr.subject)) {
                    alert("Subject is Required");
                    $('.subject').focus();
                }
                else if (!($scope.cr.details)) {
                    alert("Details are Required");
                    $('.detail').focus();
                }
                    //statusCode = 1  
                    //else if($scope.action.statusCode == 1)
                    //alert("statusCode==1");
                    //statusCode = 2  
                else if ($scope.action.statusCode!=0 && (!$scope.cr.id || $scope.cr.id == 0 || $scope.cr.statusCode == 0) && $scope.cr.crTypeId == 5 && ($scope.cr.attachments == null || $scope.cr.attachments.length == 0)) {
            
            alert('Attachment missing!')
                    $('.scopedocument').focus()
                }
                else if ($scope.cr.currentTask != null && $scope.cr.currentTask.taskType.taskTypeCode == 'PRJCAT' && !$scope.action.statusCode) {
                    alert("Select Project Action");
                    $('.prjaction').focus()
                }
                else if ($scope.cr.currentTask != null && $scope.cr.currentTask.taskType.taskTypeCode == 'PRJCAT' && $scope.action.statusCode == 2 && !($scope.cr.projectCategory)) {
                    alert("Select Project Category");
                    $('.projectcategory').focus()
                }
                else if ($scope.cr.currentTask != null && $scope.cr.currentTask.taskType.taskTypeCode == 'PRJCAT' && $scope.action.statusCode == 1 && !($scope.action.revisedMeetingDate || $scope.action.revisedMeetingDate == '')) {
                    alert("Enter new date for discussion");
                    $('.newdatefordiscussion').focus()
                }
                else if ($scope.cr.currentTask != null && $scope.cr.currentTask.taskType.taskTypeCode == 'PRJCAT' && $scope.action.statusCode == 2 && !$scope.validatePrjCat()) {
                }

                else if ($scope.cr.currentTask != null && $scope.cr.currentTask.taskType.taskTypeCode == 'ITAT' && $scope.action.statusCode == 2 && !$scope.validateITATDate() && $scope.action.earlyRelease != true) {
                    alert("ITAT date should not be less than or equal to current date.");
                   // $('.comments').focus()
                    result = false;
                }
                else if ($scope.cr.currentTask != null && $scope.cr.currentTask.taskType.taskTypeCode == 'PRJCAT' && $scope.action.statusCode == 3 && (!($scope.cr.sendBackReason) || $scope.cr.sendBackReason.id == 0)) {
                    alert("Please select send back reason");
                    $('.comments').focus()
                    result = false;
                }
                else if ($scope.cr.currentTask != null && $scope.cr.currentTask.taskType.taskTypeCode == 'ASSIGNMENT' && !$scope.validateAssignment()) {
                }

                else if ($scope.cr.currentTask != null && $scope.cr.currentTask.taskType.taskTypeCode == 'SDD' && $scope.action.statusCode == 2 && !$scope.hasAttachment($scope.cr, 'SSD')) {
                    alert("Please attach SDD/Wireframe");
                    $('.SSD').focus();

                }

                else if ($scope.cr.currentTask != null && $scope.cr.currentTask.taskType.taskTypeCode == 'ITAT' && $scope.action.statusCode == 2 && !$scope.hasAttachment($scope.cr, 'ITAT')) {
                    alert("Please attach test cases");
                    $('.testcases').focus();

                }
                else if ($scope.cr.currentTask != null && $scope.cr.currentTask.taskType.taskTypeCode == 'INFRA' && $scope.action.statusCode == 2 && !$scope.hasAttachment($scope.cr, 'INFRA')) {
                    alert("Please attach updated INFRA Sheet");
                    $('.testcases').focus();

                }
                else if ($scope.cr.currentTask != null && $scope.cr.currentTask.taskType.taskTypeCode == 'UAT' && $scope.action.statusCode == 1 && (!$scope.action.onHoldReason)) {
                    alert("Please select on-hold reason");
                    $('.onholdreason').focus();
                }
                else if ($scope.cr.currentTask != null && $scope.cr.currentTask.taskType.taskTypeCode == 'UAT' && $scope.action.statusCode == 1 && (!$scope.action.onHoldDate || $scope.action.onHoldDate == '')) {
                    alert("Please enter on-hold till date");
                    $('.onholdtilldate').focus();
                }
                else if ($scope.cr.currentTask != null && $scope.cr.currentTask.taskType.taskTypeCode == 'UAT' && $scope.action.statusCode == 2 && !$scope.hasAttachment($scope.cr, 'UAT')) {
                    alert("Please attach UAT document");
                    $('.uatatach').focus();
                }
                else if ($scope.cr.currentTask != null && $scope.cr.currentTask.taskType.taskTypeCode == 'UATSO' && $scope.action.statusCode == 2 && !$scope.hasAttachment($scope.cr, 'UATSO')) {
                    alert("Please attach UAT signoff document");
                    $('.uatatach').focus();
                }
                else if ($scope.cr.currentTask != null && $scope.cr.currentTask.taskType.taskTypeCode == 'PRJCAT' && $scope.cr.infraCosting == 'N' && $scope.hasAttachment($scope.cr, 'HW / SW') == true) {
                    alert("You have attached the hardware sizing document, please change H/W required to Yes or Later or remove the sizing document");
                    $('.uatatach').focus();
                }
                else if ($scope.cr.currentTask != null && (!$scope.action.comments || $scope.action.comments == '')) {
                    alert("Please enter Comment");
                    $('.comments').each(function (x) {
                        console.log($('.comments').eq(x).css.display)
                        if ($('.comments').eq(x).css.display != 'none') $(this).focus();
                    });//.focus();
                }

                else
                    // return false;
                    return true;

                return false;
            }
            $scope.validateITATDate = function () {
                //alert("1")
                if ($scope.action.statusCode == 2) {
                    var newDate = new Date();
                    var actionITATDate = new Date();
                    actionITATDate = $scope.cr.itatDate;
                    if ($scope.cr.revisedITATDate) actionITATDate = $scope.cr.revisedITATDate;
                 //   alert($scope.getTime(getDate1(actionITATDate)) + '----' + $scope.getTime(new Date()))
                    if ($scope.action.statusCode == 2 && $scope.getTime(getDate1(actionITATDate)) > $scope.getTime(new Date())) {
                        //alert("ok.")
                        //$scope.ajaxCallCompleted()
                        return false;
                    }
                    else { return true; }
                }
            }

            //$scope.setTaskDates = function () {alert("hiii")
            //    if ($scope.cr.phases && $scope.cr.phases.length > 0) {
            //        $scope.cr.phases[0].tasks[0].startDate= $scope.cr.phases[0].startDate;
            //    }
            //    //$scope.cr.phases[0].tasks   $scope.cr.phases[0].star
            //}
            $scope.ajaxCallCompleted = function (event) {
                $scope.runningAjax = false;
                $(event.delegateTarget).children('i.fa').addClass('fa-save');
                $(event.delegateTarget).children('i.fa').removeClass('fa-spinner fa-spin');

            }
            $scope.submitPhaseTask = function (phaseno, taskno,event) {
                   // alert('phaseno' + phaseno)
               // alert($scope.cr.phases[phaseno].tasks.length);
                try {
                    
                    if (taskno >= $scope.cr.phases[phaseno].tasks.length) {
                       // alert('hiii')
                        $scope.ajaxCallCompleted(event);
                        phaseno++;
                        taskno = 0
                        if (phaseno>=$scope.cr.phases.length  ) return;
                        $scope.submitPhaseTask(phaseno, taskno, event);
                        return;
                    }
            var task = $scope.cr.phases[phaseno].tasks[taskno];
            task.comments = $scope.action.comments;
            if (task.completed || task.statusCode1==0) { $scope.submitPhaseTask(phaseno, taskno + 1,event) }
                    else {
                        // alert(task.subject)
                        var act = {};
                        act.item = JSON.parse(JSON.stringify(task));;
                        //alert(act.item.statusCode)
                        //alert(act.item.subStatusCode)
                        if (act.item.statusCode1 == 4) {
                            act.item.assignedTo = act.item.reAssignTo
                            act.item.subStatusCode='reassigned'
                            act.item.statusCode = 1;
                        }
                        else if (act.item.statusCode1 == 5) {
                            act.item.statusCode = 1;
                            act.item.subStatusCode = 'assigntoteam'
                        }
                        else if (act.item.statusCode1 == 6) {
                            act.item.statusCode = 1;
                            act.item.subStatusCode = 'reviseddate'
                        }
                        else if (act.item.statusCode1 == 2) {
                            act.item.statusCode = 2;
                            act.item.subStatusCode = 'releaseforitat'
                        }
                        //alert(act.item.subStatusCode)
                        act.statusCode = act.item.statusCode;
                        act.item.cr = JSON.parse(JSON.stringify($scope.cr));
                        act.item.cr.currentTask = null;
                        
                        //alert(act.statusCode)
                taskno = taskno + 1
                        CRService.updateTask(act).success(function (data) {
                            if (typeof (data) == 'string') location.href = '/'
                            $scope.ajaxCallCompleted(event);
                            alert(data.message)
                            if (data.success) {
                                
                                
                              
                                location.href = '#/';//aaliya
                            }
                            $scope.submitPhaseTask(phaseno, taskno,event);
                        }).error(function (data) {
                            $scope.ajaxCallCompleted(event);

                        });
                    }
                }
                catch (e) {
                }

            }
            $scope.
            infraCostingRequired = function () {

                if ($scope.cr.infraCosting == 'Y') {
                    if ($scope.cr.hardwareRequired && !$scope.hasAttachment($scope.cr, 'HW / SW')) {
                        alert('Attach Hardware Requirement Document');
                        $('.hwsw').focus()
                        return false;
                    }

                    if (confirm('System will send the cr to infra team for hardware costing. Do you want to continue?')) {
                        $scope.action.item = $scope.cr.currentTask;
                        $scope.action.item.cr = JSON.parse(JSON.stringify($scope.cr));
                        $scope.action.item.cr.currentTask = null;
                        $scope.action.statusCode == 6
                        var act = { statusCode: 6, comments: $scope.action.comments }
                        act.item = $scope.action.item;
                        //act.item.cr.vendorDetails = null;
                        CRService.updateTask(act).success(function (data) {
                            if (typeof (data) == 'string') location.href = '/'
                            alert(data.message);
                            if (data.success) {

                                $scope.disableSubmit = true;
                                location.href = '#/';//aaliya
                            }
                        }).error(function (data) {
                            $scope.ajaxCallCompleted(event);

                        });
                        return false;
                    }
                    else {
//                        $scope.cr.infraCosting='L'
                    }
                }
                else {
                }
                return true;
            }


            var getDate2 = function (dt1) {
                var dtarr = dt1.split('-')
                return new Date(dtarr[2],__months.indexOf(dtarr[1]),dtarr[0])
            }

            var getDate = function (dt1) {
                if (!dt1) return null;
                var dt = new Date(dt1);
                return (dt.getDate() + '-' + __months[dt.getMonth()]) + '-' + dt.getFullYear();

            }

            var getDate1= function (dt1) {
                if (!dt1) return null;
                var dt = new Date(dt1);
                return dt;

            }

            $scope.resetDate = function (obj, dateField) {
                if (obj[dateField] && obj[dateField]!= '') {//Date(date.getTime() + (date.getTimezoneOffset() * 3600)).toJSON()
                    obj[dateField] = getDate(obj[dateField])
                    
                    //$scope.cr.meetingRequestDate = $scope.cr.meetingRequestDate.getFullYear() + '-' + ($scope.cr.meetingRequestDate.getMonth() + 1) + '-' + $scope.cr.meetingRequestDate.getDate();
                }

            }
            $scope.refreshDates = function () {
                $scope.action.securityCheckStartDate = "";
                $scope.action.securityCheckEndDate = "";
            }
            $scope.meetingDateChanged = function () {
                
                if ($scope.cr.meetingRequestDate && $scope.cr.meetingRequestDate != '') {//Date(date.getTime() + (date.getTimezoneOffset() * 3600)).toJSON()
                    $scope.cr.meetingRequestDate = $scope.cr.meetingRequestDate.getFullYear() + '-' + ($scope.cr.meetingRequestDate.getMonth() + 1) + '-' + $scope.cr.meetingRequestDate.getDate();
                }

            }
            $scope.prcatactionChanged = function () {
               // alert($scope.action.statusCode)
                if ($scope.action.statusCode == '2') {
            if ($scope.cr.vendorDetails == null || $scope.cr.vendorDetails.length == 0) $scope.addVendor();
                }
                return true;
            }
            $scope.setCorrectDates = function () {
                for (var e in $scope.cr.phases) {
                    $scope.cr.phases[e].startDate = getDate($scope.cr.phases[e].startDate);
                    $scope.cr.phases[e].endDate = getDate($scope.cr.phases[e].endDate);
                    $scope.cr.phases[e].itatDate = getDate($scope.cr.phases[e].itatDate);
                    $scope.cr.phases[e].uatDate = getDate($scope.cr.phases[e].uatDate);
                    for (var t in $scope.cr.phases[e].tasks) {
                        $scope.cr.phases[e].tasks[t].startDate = getDate($scope.cr.phases[e].tasks[t].startDate);
                        $scope.cr.phases[e].tasks[t].endDate = getDate($scope.cr.phases[e].tasks[t].endDate);
                        $scope.cr.phases[e].tasks[t].itatDate = getDate($scope.cr.phases[e].tasks[t].itatDate);
                   //     $scope.cr.phases[e].tasks[t].startDate = getDate($scope.cr.phases[e].tasks[t].startDate);

                    }
                }
                console.log(JSON.stringify($scope.cr.phases));
            }

            $scope.getTime = function (dt) {
                //var dt1 = new Date();
              //  alert(dt)
                var d1 = "0" + dt.getDate();
                d1=(d1.substr(d1.length - 2, 2));

                return parseInt(dt.getFullYear() + "" + ("0" + dt.getMonth()).substr(0, 2) + "" + d1, 10);
            }

            $scope.hasSelectedVendorDetails = function () {
                var Result = true;
                for (var arr in $scope.cr.vendorDetails) {
                    if ($scope.cr.vendorDetails[arr].selectedVendor == true) {
                        Result = true;
                        return Result;
                    }
                    else {
                        Result = false;
                    }
                }
                return Result;
            }

            //$scope.testt = function () {
            //    alert($scope.cr.PocessApproverFlag);
            //    if ($scope.cr.PocessApproverFlag == true) {
            //        $scope.cr.ProcessApproverId.style.display='';
            //    }
            //}

            $scope.resetProcessApprover = function () {
                $scope.cr.processApprover = null;
            }

            $scope.submit = function (event) {

            

                if ($scope.cr.currentTask && $scope.cr.currentTask.taskType.taskTypeCode == 'ITEVAL' && !$scope.hasSelectedVendorDetails() && $scope.cr.projectCategory != 'C' ) {
                    alert("Select atleast one Vendor")
                    $('.crSelectedVendorDetails').focus();
                    return false;
                }
                //alert()
                if ($scope.cr.statusCode == 80 && $scope.cr.sendBackReason && $scope.cr.sendBackReason.reasonCode == 'TESTSENREQ' && $scope.hasAttachment($scope.cr, 'ScopeDocument') == false) {
                    alert("Select attach Test Scenario")
                   // $('.crSelectedVendorDetails').focus();
                    return false;
                }

                if ($scope.cr.currentTask && ($scope.cr.currentTask.taskType.taskTypeCode != 'ASSIGNMENT'
                    && $scope.cr.currentTask.taskType.taskTypeCode != 'ROLLREQ'
                    && $scope.cr.currentTask.taskType.taskTypeCode != 'ROLLCONF'
                    && $scope.cr.currentTask.taskType.taskTypeCode != 'NONSAPEXE') && ($scope.action.statusCode == 0)) {
                    alert('Please select action');
                    return 
                }

                if ($scope.cr.currentTask && $scope.cr.currentTask.taskType.taskTypeCode == 'MDCEVAL' && $scope.cr.processApproverFlag == true && (!$scope.cr.processApprover)) {
                    alert("Please select Process Approver")
                    $('.procesApprover').focus();
                    return;
                }


                //console.log(event)
                //$scope.validate()
               // return
                if ($scope.validate()) {
                    //if ($scope.cr.currentTask && $scope.cr.currentTask.taskType.taskTypeCode == 'UAT' && $scope.cr.infraCosting == 'L') {
                    //    alert("Costing recived for CR " + $scope.cr.changeRequestCode + " from INFRA; mail sent to ITFH " + $scope.cr.itfhUser.title)
                    //    $location.goHome();
                    //    return;
                    //}
                    if ($scope.cr.currentTask && $scope.cr.currentTask.taskType.taskTypeCode == 'UAT' && $scope.action.statusCode == 3) {
                        if (!confirm('Due to change in original scope hence short close this CR? ')) return false;
                    }
                    //return;

                    if ($scope.cr.currentTask && $scope.cr.currentTask.taskType.taskTypeCode == 'FAAPPROVAL' && $scope.action.statusCode == 1) {
                        alert('Please select your action');
                        return;
                    }
            if ($scope.cr.currentTask && $scope.cr.currentTask.taskType.taskTypeCode == 'PRJCAT' && $scope.cr.infraCosting == 'Y' && !$scope.cr.hardwareCostReceived) return $scope.infraCostingRequired();
                    //return;
                   // alert("Validataion Complete");
                   $scope.runningAjax = true;
                   $(event.delegateTarget).children('i.fa').removeClass('fa-save');
                   $(event.delegateTarget).children('i.fa').addClass('fa-spinner fa-spin');
                   var crchanged = false;
                  // alert($scope.cr.phases.length)
                   if ($scope.cr.phases && $scope.cr.phases.length > 0) {
                       for (var i = 0; i < $scope.cr.phases.length; i++) {
                           //alert($scope.cr.phases[i].tasks)
                    for (var j = 0; j < $scope.cr.phases[i].tasks.length; j++) {
                               var ph = $scope.cr.phases[i].tasks[j];
                               if (ph.statusCode1 == 6) {
                                   crschanged = true;
                               }

                               
                               if (ph.statusCode1 == 2 ) {
                                   
                                   var date = new Date();
                                   var rvdate = new Date();
                                   var eDate = new Date();
                                   rvdate = ph.revisedEndDate;
                                   eDate = ph.endDate;
                                 //    alert($scope.getTime(getDate1(rvdate)) > $scope.getTime(new Date()))
                                  // alert($scope.getTime(getDate1(eDate)) < $scope.getTime(new Date()))
                                   if ($scope.action.earlyRelease != true) {
                                       if (ph.revisedEndDate != null && $scope.getTime(getDate1(rvdate)) > $scope.getTime(new Date())) {
                                           alert("You cannot release for ITAT before end date\nPlease revise end date before releasing for ITAT.")
                                           $scope.ajaxCallCompleted(event)
                                           return false;
                                       }
                                       else if (ph.revisedEndDate == null && $scope.getTime(getDate1(eDate)) > $scope.getTime(new Date())) {
                                           alert("You cannot release for ITAT before end date.\nPlease revise end date before releasing for ITAT")
                                           $scope.ajaxCallCompleted(event)
                                           return false;

                                       }

                                   }// return
                               }
                           }
                       }
                   }
                   //return false;
                   // //6th Aug 2015
                   //if ($scope.action.statusCode == 2) {alert("")
                   //    var newDate = new Date();
                   //    var actionITATDate = new Date();
                   //    actionITATDate = $scope.cr.itatDate;
                   //    if ($scope.action.statusCode == 2 && $scope.getTime(getDate1(actionITATDate)) <= $scope.getTime(new Date())) {
                   //        alert("ok.")
                   //        $scope.ajaxCallCompleted()
                   //        return false;
                   //    }
                   //}
                  
                   // //6th Aug 2015


                   if (crchanged) {
                var act = { item: $scope.cr, statusCode: 8 };
                       CRService.updateCR(act)
                       .success(function (data) {
                           if (typeof (data) == 'string') location.href = '/'
                           $scope.ajaxCallCompleted(event);
                           if (data.success) {
                               alert(data.message);
                               $scope.cr = data.item;
                               $scope.disableSubmit = true;
                               //                            $(".form-group").addClass("has-success");
                           }
                       }).error(function (data) {
                           $scope.ajaxCallCompleted(event);

                       })
                   }
                   else if ($scope.cr.currentTask && $scope.cr.currentTask.taskType.taskTypeCode == 'NONSAPEXE') {
                       //alert('aaaa')

                       $scope.submitPhaseTask(0, 0,event);
                    //for (var p = 0; p < $scope.cr.phases.length; p++) {

                    //    for (var e = 0; e < $scope.cr.phases[p].tasks.length; e++) {
                    //        var task = $scope.cr.phases[p].tasks[e];
                    //       // alert(task.subject)
                    //        var act = {};
                    //        act.item = task;
                    //        act.statusCode = task.statusCode;
                    //        act.item.cr = JSON.parse(JSON.stringify($scope.cr));
                    //        act.item.cr.currentTask = null;
                    //        //alert(act.statusCode)
                    //    }
                    //}
                }
                   else if ($scope.cr.currentTask && $scope.cr.currentTask.id > 0) {
                       if ($scope.cr.currentTask.taskType.taskTypeCode == 'ASSIGNMENT') {
                           $scope.action.statusCode = 2;
                           $scope.action.statusCode = 2;
                           if ($scope.cr.securityCheckRequired) {
                               $scope.cr.securityCheckStartDate = $scope.action.securityCheckStartDate;
                               $scope.cr.securityCheckEndDate = $scope.action.securityCheckEndDate;
                           }
                           $scope.setCorrectDates();

                       }

                       else if ($scope.cr.currentTask.taskType.taskTypeCode == 'MDCCHANGE') {
                           $scope.action.statusCode = 2;

                       }


                        else if ($scope.cr.currentTask.taskType.taskTypeCode == 'ASSIGNMENT') {
                           
                        //$scope.runningAjax = false;
                        //$scope.disableSubmit = false;
                        //return false;
                    }
                   
                    
                    if ($scope.cr.currentTask.taskType.taskTypeCode == 'ROLLREQ') $scope.action.statusCode = 2;
                    if ($scope.action.statusCode == 1 && $scope.action.revisedMeetingDate != null) $scope.cr.revisedMeetingDate = $scope.action.revisedMeetingDate
                    $scope.action.item = $scope.cr.currentTask;
                    $scope.action.item.cr = JSON.parse(JSON.stringify($scope.cr));
                    
                    if ($scope.cr.currentTask.taskType.taskTypeCode == 'UAT') {
                        if ($scope.action.statusCode == 1) {
                            $scope.action.item.cr.onHoldDate = $scope.action.onHoldDate;
                            $scope.action.item.cr.onHoldReason = $scope.action.onHoldReason;
                            $scope.action.item.subStatusCode = "hold";

                        }
                    }
                    $scope.action.item.cr.currentTask = null;
                    if ($scope.cr.currentTask.taskType.taskTypeCode == 'SDD') {
                    if ($scope.action.statusCode == 1 && $scope.action.wireFrameDate)
                            $scope.action.item.cr.wireFrameDate = $scope.action.wireFrameDate
                        else if ($scope.action.statusCode == 1 && $scope.action.revisedWireFrameDate)
                            $scope.action.item.cr.revisedWireFrameDate = $scope.action.revisedWireFrameDate
                    }
                    CRService.updateTask($scope.action).success(function (data) {
                        if (typeof (data) == 'string') location.href = '/'
                        alert(data.message);
                        if (data.success) {

                            $scope.disableSubmit = true;
                            location.href = '#/';//aaliya
                        }
                        else {
                            $scope.ajaxCallCompleted(event);
                        }
                    }).error(function (data) {
                        $scope.ajaxCallCompleted(event);

                    });
                }
                   else {
                      
                    $scope.action.item = $scope.cr;
                    if ($scope.cr.id && $scope.cr.id != 0) {
                        CRService.updateCR($scope.action)
                        .success(function (data) {
                            if (typeof (data) == 'string') location.href = '/'
                            $scope.ajaxCallCompleted(event);
                            if (data.success) {
                                alert(data.message);
                                //$scope.cr = data.item;
                                $scope.disableSubmit = true;
                                location.href = '#/';//aaliya
                                //                            $(".form-group").addClass("has-success");
                            }
                        }).error(function (data) {
                            $scope.ajaxCallCompleted(event);

                        })
                    }
                    else {
            //        localStorage['cr' + $scope.cr.crTypeId] = JSON.stringify($scope.cr);
                        CRService.createCR($scope.action)
                        .success(function (data) {
                            if (typeof (data) == 'string') location.href = '/'
                            $scope.ajaxCallCompleted(event);
                            if (data.success) {
                                alert(data.message);
                                //$scope.cr = data.item;
                                $scope.disableSubmit = true;
                                location.href = '#/';//aaliya
                                //                            $(".form-group").addClass("has-success");
                            }
                        }).error(function (data) {
                            $scope.ajaxCallCompleted(event);

                        })
                    }
                }
            }
            }
            $scope.calculattotalcost = function (vd) {
                vd.totalCost = parseInt(vd.hardwareCost) + parseInt(vd.softwareCost) + parseInt(vd.securityCost)
            }
            $scope.addnonsaptest = function () {
                $scope.cr = JSON.parse(localStorage.cr2);
               // $scope.submit();
            }
            var hasOpenTaskforCurrent = function (cr) {
                //return true;
                var hasOpenTask=false;
                if(cr.phases && cr.phases.length>0) {
                    angular.forEach(cr.phases, function (phase, key) {
                        //alert(JSON.stringify(phase))
                        angular.forEach(phase.tasks, function (task, tid) {
                            //alet(task1)
                          //  var task=task1[0]
                            if (task.assignedTo.id == $scope.currentUser.id || (task.teamMember && task.teamMember.id == $scope.currentUser.id)) hasOpenTask = true; 
                        });
                        //text = text += value.subModuleName + ", ";
                    });
                }
              //  alert(hasOpenTask)
               // alert("tata")
               // hasOpenTask = true;
                return hasOpenTask;
            }
            if ($routeParams.id && $routeParams.id != '') {
                // alert()
                setTimeout(function () {
                    CRService.getCRByIdEdit($routeParams.id).success(function (data) {
                        if (typeof (data) == 'string') location.href = '/';
                        console.log("Edit Mode")
                        console.log(data)
                        $scope.cr = data;
                        $scope.moduleChanged();
                        $scope.divisionChanged();

                        if ($scope.cr.currentTask && $scope.cr.currentTask.taskType.taskTypeCode == 'ASSIGNMENT') $scope.cr.securityCheckRequired = false;

                        // if ($scope.cr.currentTask && $scope.cr.currentTask.taskType.taskTypeCode == 'PRJCAT' && $scope.cr.statusCode != 2 && $scope.cr.projectCategory != 'C') {

                        //}

                        if (data.statusCode >= 95) $scope.disableSubmit = true;
                        if (!$scope.cr.vendorDetails) {
                            //$scope.cr.vendorDetails = [];
                            //$scope.addVendor();
                        }
                        $scope.isEditable = false;
                        $scope.disableSubmit = true;
                        if ($scope.cr.pendingWithUser && $scope.cr.pendingWithUser.id == $scope.currentUser.id) $scope.disableSubmit = false;
                        else if ($scope.cr.currentTask && $scope.cr.currentTask.assignedTo && ($scope.cr.currentTask.assignedTo.id == $scope.currentUser.id || ($scope.cr.currentTask.teamMember && $scope.cr.currentTask.teamMember.id == $scope.currentUser.id))) $scope.disableSubmit = false;
                        else if ($scope.cr.phases && hasOpenTaskforCurrent($scope.cr)) {
                            $scope.disableSubmit = false;
                        }
                        else if ($scope.cr.rcb.id == $scope.currentUser.id && ($scope.cr.statusCode == 0 || $scope.cr.statusCode == 80 || $scope.cr.statusCode == 11))
                            $scope.disableSubmit = false;

                        //cr.statusCode==0 || cr.statusCode==80 || (cr.statusCode==11 && cr.crTypeId==4)
                        if (($scope.cr.statusCode == 0 || $scope.cr.statusCode == 80 || $scope.cr.statusCode == 11) && $scope.cr.rcb.id == $scope.currentUser.id) $scope.isEditable = true;
                        else if ($scope.cr.statusCode == 1 && $scope.cr.itfhUser.id == $scope.currentUser.id) $scope.isEditable = true;
                        // $scope.cr.subModule = $scope.cr.subModule
                        $scope.action.statusCode = 0;

                        // alert($scope.action.statusCode)
                        //alert("onload");
                        // $scope.disabledTypeId(); //check
                        //alert($scope.cr.taskType.task
                       
                      
                            
                        if ($scope.cr.currentTask && $scope.cr.currentTask.taskType.taskTypeCode == 'PRJCAT' && $scope.cr.projectCategory) $scope.action.statusCode = 2;
                        if ($scope.cr.projectCategory != null && $scope.cr.statusCode >= 1 && $scope.cr.statusCode != 80) $scope.itfhaction = 2;
                        else if ($scope.cr.sendBackReason != null || $scope.cr.statusCode == 80 || $scope.cr.statusCode == 0 || $scope.cr.statusCode == 11) $scope.itfhaction = 3;
                        else if ($scope.cr.revisedMeetingDate) $scope.itfhaction = 1;

                        if ($scope.cr.phases && $scope.cr.phases.length > 0) {
                            for (var e in $scope.cr.phases) {
                                for (var j in $scope.cr.phases[e].tasks) {
                                    $scope.cr.phases[e].tasks[j].statusCode1 = 0
                                }

                            }
                        }
                        if ($scope.cr.crTypeId == 5 && $scope.cr.currentTask && $scope.cr.currentTask.taskType.taskTypeCode == 'GRCEVAL') {
                            CRService.getGRCHOD($scope.cr.division.id, $scope.cr.subModule.id).success(function (data) {
                                $scope.sapHods = data.items;
                            });
                        }

                        if ($scope.cr.crTypeId == 4 && $scope.cr.currentTask && $scope.cr.currentTask.taskType.taskTypeCode == 'MDCEVAL') {
                            CRService.getProcessApprovers().success(function (data) {
                                $scope.processApprovers = data.items;
                            });
                        }

                        if ($scope.cr.currentTask && $scope.cr.currentTask.taskType.taskTypeCode == 'ITEVAL') {

                            if ($scope.cr.vendorDetails.length == 1) {
                                $scope.cr.vendorDetails[0].selectedVendor = true;
                            }

                        }
                    
                        setTimeout(function () {
                            $scope.$apply();
                        }, 600);
                    }).error(function () {
                        alert('error')
                    });
                }, 500);
            }
            else {
                $scope.cr.rcb = $scope.currentUser;
            }
        }]);

angular.
    module('myApp.service.CR', [])
    .factory('CRService', [
        '$http',
        function ($http) {
            return {
                getCRByIdEdit: function (id) {
                    var d = "{requestType:'crForEdit',parameters:[{name:'crid',value:" + id + "}]}"
                    return $http({
                        method: 'POST',
                        data: d,
                        url: '/api/crhelper'
                    });
                },
                getCRById: function (id) {
                    return $http({
                        method: 'GET',
                        url: '/api/cr/' + id
                    });
                },
                getCR: function () {
                    return $http({
                        method: 'GET',
                        url: '/api/cr?query='
                    });
                },

                getPhaseTask: function (query) {
                    var strquery = JSON.stringify({ CurrentPage: query.CurrentPage, PageSize: query.PageSize, Parameters: query.Parameters });
                    return $http({
                        method: 'GET',
                        url: "/api/phasetask?query=" + strquery

                    });                    //alert('in service getPhaseTask');
                   
                },
                getGRCHOD: function (divId,subModuleId) {
                    var d = "{requestType:'grchod',parameters:[{name:'divisionid',value:" + divId + "},{name:'submoduleid',value:" + subModuleId + "}]}"
                    return $http({
                        method: 'POST',
                        data: d,
                        url: '/api/crhelper'
                    });
                },
                updateTask: function (action) {
                    return $http({
                        method: 'Put',
                        url: '/api/PhaseTask',
                        data: action
                    })

                },
                getCRFilter: function (query) {
                    console.log(JSON.stringify(query));
                    var strquery = JSON.stringify({ CurrentPage: query.CurrentPage, PageSize: query.PageSize, Parameters: query.Parameters });
                    return $http({
                        method: 'GET',
                        url: "/api/cr?query=" + strquery

                    });
                    //var strquery = JSON.stringify({ CurrentPage: 1, PageSize: 10, Parameters: [{ Name: 'CRCode', Value: 'CR00009' }] });
                },

                getCRRegisterFilter: function (query) {
                    
                    var strquery = JSON.stringify({ CurrentPage: query.CurrentPage, PageSize: query.PageSize, Parameters: query.Parameters });
                     // strquery = JSON.stringify("test");
                    console.log(strquery);
                    return $http({
                        method: 'GET',
                        url: "/api/crregister?q=" + strquery

                    });
                },
                 

                getStatusReport: function (query) {
                    console.log(JSON.stringify(query));
                    var strquery = JSON.stringify({ CurrentPage: query.CurrentPage, PageSize: query.PageSize, Parameters: query.Parameters });
                    return $http({
                        method: 'GET',
                        url: "/api/statusreport?q=" + strquery

                    });
                },

                

                getRequestTypes: function (query) {
                   // alert("getRequestTypes");
                    console.log(query);
                    return $http({
                        method: 'GET',
                        url: '/api/crrequestType?q=' + query
                    });
                },

                getModules: function (query) {
                    return $http({
                        method: 'GET',
                        url: '/api/module?q=' + query
                    });
                },
                
                getTeamByITFH: function (itfh) {
                    
                    var d = "{requestType:'getitfhteam',parameters:[{name:'itfhuser',value:" + itfh + "}]}"
                    return $http({
                        method: 'POST',
                        data: d,
                        url: '/api/crhelper'
                    });
                },
                getProcessApprovers: function (itfh) {
                    var d = "{requestType:'getprocessapprover',parameters:[]}"
                    return $http({
                        method: 'POST',
                        data: d,
                         url: '/api/crhelper'

                    });
                },
                getitfhUsers: function (query) {
                return $http({
                    method: 'GET',
                    url: '/api/itfhUser?q=' + query
                });
            },
                getsubitfhUsers: function () {
                    var d = "{requestType:'getsubitfh',parameters:[]}"
                    return $http({
                        method: 'POST',
                        data: d,
                        url: '/api/crhelper'

                    });
                }, getSubModules: function (query) {
                    return $http({
                        method: 'GET',
                        url: '/api/submodule?q=' + query
                    });
                },
                
                
                getBusinessFunctions: function (query) {
                    return $http({
                        method: 'GET',
                        url: '/api/businessfunction?q=' + query
                    });
                },
                getCurrentUserDetail: function () {
                    var d = "{requestType:'currentUserDetail'}"
                    return $http({
                        method: 'POST',
                        data: d,
                        url: '/api/crhelper'
                    });
                },
                getDivisionFunction: function (divId, crtypeid) {
                    var d = "{requestType:'divisionFunctions',parameters:[{name:'DivisionId',value:'" + divId + "'}]}"
                    return $http({
                        method: 'POST',
                        data: d,
                        url: '/api/crhelper'
                    });
                },
                //getDivisionFunction: function () {
                //    var d = "{requestType:'divisionFunctions'}"
                //    return $http({
                //        method: 'POST',
                //        data: d,
                //        url: '/api/crhelper'
                //    });
                //},
                createCR: function (CRData) {
                    console.log(CRData)
                    return $http({
                        method: 'Post',
                        url: '/api/cr',
                        data: CRData
                    })
                },
                //CRRegApplyFilter: function () {
                //    alert("in apply filter")
                //    $scope.runningAjax = true;
                //    //var query = '{"CurrentPage":' + $scope.page + ',' + '"PageSize":' + 10 + ',"Parameters":' + JSON.stringify($scope.getFilters()) + '}';
                //    var query = {
                //        CurrentPage: $scope.page,
                //        PageSize: 10,
                //        Parameters: $scope.getFilters(),
                //    }
                //    console.log(query);
                //    $scope.atLastPage = false;
                //    CRService
                //    .getCRFilter(query)
                //    .success(function (res, status, headers, config) {
                //        if (typeof (data) == 'string') location.href = '/'
                //        console.log(res.items);
                //        $scope.crList = res.items;
                //        $scope.page = res.currentPage;
                //        $scope.totalRecords = res.totalRecords;
                //        $scope.totalPages = Math.ceil(res.totalRecords / res.pageSize);
                //        $scope.pageSize = res.pageSize;
                //        $scope.currentRecords = $scope.crList.length + "-" + $scope.page * $scope.pageSize
                //        //$scope.showing = $scope.currentRecords + " of " + $scope.totalRecords;
                //        $scope.runningService = false;
                //    }).error(function () {
                //        $scope.error = "User does not have permission";
                //        $scope.loading = false;
                //        $scope.runningService = false;
                //    });
                //},
                updateCR: function (CRData) {
                    console.log(CRData)
                    return $http({
                        method: 'Put',
                        url: '/api/cr',
                        data: CRData
                    })
                }
            }
        }
    ]);


//////////////////////////////

angular
    .module('myApp.ctrl.approvals', [])
    .controller('approvalListCtrl', [
        '$scope',
        '$rootScope',
        '$location',
        'CRService',
        //'requisitionService',
function ($scope, $rootScope, $location, CRService) {
            $scope.crList = [];
            $rootScope.goHome = function (path) {
                $rootScope.checkBack = true;
                $location.url(path);
            }

            $scope.addNewRecord = function () {
                $location.path("/settings/addreason");
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

            CRService.getCR().success(function (data) {
                            $scope.crList = data.items;
                            console.log($scope.crList)
                        })

        }]);

//function addAttachment(att) {
//    //alert(att.AttachmentType)
//    if (Attachment == "AnnouncerAttachment") {
//        Main.Current.Announcer.FileName(att.FileName);
//        Main.Current.Announcer.TempPath(att.TempPath);
//        //$("#span_AnnouncerAttachment").text(att.FileName)
//        $("#Accouncers").modal('hide');
//    }
//    else {
//        var obj = att;
//        obj.SchemeAttachmentType = $("#ddlSchemeAttachType").val();
//        Main.Current.SchemeAttachments.push(obj);
//        $("#SchemeAttachment").modal('hide');

//    }
//}
///////////////////////////////////////////////



/////////////////////////////////////////////////////////////////////


//angular
//    .module('myApp.service.reason', [])
//    .factory('requisitionService', [
//        '$http',
//        function ($http) {
//            return {
//                getReasons: function (query) {
//                    return $http({
//                        method: 'GET',
//                        url: '/api/reason?q=' + query
//                        //         ProudctSearch
//                    });
//                }
//            }
//        }]);



