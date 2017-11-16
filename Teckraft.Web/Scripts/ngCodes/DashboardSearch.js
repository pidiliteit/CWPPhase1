var __months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']

angular.module('asfApp.ctrls.DashboardSearchCtrls', [])
.controller('DashboardSearchCtrl', ['$scope', '$rootScope', '$routeParams', '$location', 'activityService', function ($scope, $rootScope, $routeParams, $location, activityService) {
    $scope.loading = false;
    $scope.IsVisible = false;
    $scope.IsVisible1 = false;
    $scope.IsVisible2 = false;
    $scope.IsVisible3 = false;
    $scope.IsVisible5 = false;
    $scope.loading = false;
    $scope.beyondCount = 0;
    $scope.withinCount = 0;
    $scope.userWiseColumnNames ={
        ChapterId:'',
    ID_Issue_Detail1:true,
    chapternamedisplay :'',
    id_data_load_date :'',
    title  :'',
    chapterOwner :'',
    id_request  :'',
    id_status :'',
    id_reported_by_name  :'',
    id_reported_by_email:'',  
    id_dept  :'',
    id_location :true,
    id_pending_with_name  :true,
    id_pending_with_email  :true,
    ID_Logged_Date :'', 
    pendingSince  :true,
    id_tat_status  :'',
    id_comments  :true,
    ID_Target_Date :'',
    ID_Category: '',
    UserId: '', 
    }
    

   
    //console.log( $scope.issueCommentDetails )
   
    activityService.getCurrentUserDetail(null).success(function (data) {
        $scope.currentUser = data;

        activityService.getUserRole($scope.currentUser.userName).success(function (data) {
            $scope.userRole = data.Items[0];
            console.log($scope.userRole);
            
            //alert();
            $scope.DisplayPendingValues();
            //if ($scope.userRole.RoleName1 == 'TopManagement') {
            //    $scope.DisplayChapterAllValues();
            //}
        });
    });
    activityService.getStatus(null).success(function (data) {
        $scope.statusData = data.items;
        
    });
    $scope.issueCommentDetails = [
       [{ iD_Comments: '', iD_Status_Id: { "id": 1, "statusDesc": "Open", "rct": "0001-01-01T00:00:00", "rut": "0001-01-01T00:00:00", "rcb": null } }]
    ];
    $scope.getIssues = function (cap) { 
        $scope.loading = true;
        activityService.getIssueDetails($scope.currentUser.email, cap, $scope.currentUser.userName).success(function (data) {
            $scope.issueDetails = data.Items;
            console.log($scope.issueDetails);
            angular.forEach($scope.issueDetails, function (value, key) {
                if (value.Comments == "") {
                    activityService.getAllCmments(value.RequestNo, value.SystemChapterName, value.PendingWithEmail).success(function (data) {
                        $scope.IssData = data.items;
                        if ($scope.IssData.length > 0) {
                            value.Comments = $scope.IssData[0].iD_Comments;
                        }
                    });
                }
            });
            $scope.loading = false;
        });
    }
    $scope.getAnchorComments = function (Details) {
        activityService.getAnchorCmments(value.RequestNo, value.SystemChapterName, value.PendingWithEmail).success(function (data) {
             $scope.IssData = data.items;
            if ($scope.IssData.length > 0) {
                 value.Comments = $scope.IssData[0].iD_Comments;
            }
        });
    }
    $scope.DisplayPendingValues = function () {
        $scope.loading = true;
        $scope.IsVisible = $scope.IsVisible ? false : true;
        $scope.IsVisible1 = false;
        $scope.IsVisible2 = false;
        $scope.IsVisible3 = false;
        $scope.IsVisible4 = false;
        $scope.IsVisible5 = false;
        if ($scope.IsVisible == true) {
            activityService.getChapters($scope.currentUser.email).success(function (data) {
                $scope.chapterDetails = data.Items;
                
                console.log($scope.chapterDetails);
                $scope.loading = false;
            });
        }
       

    }
    $scope.DisplayChapterOwnerValues = function () {
        $scope.loading = true;
        $scope.IsVisible1 = $scope.IsVisible1 ? false : true;
        $scope.IsVisible = false;
        $scope.IsVisible2 = false;
        $scope.IsVisible3 = false;
        $scope.IsVisible4 = false;
        $scope.IsVisible5 = false;
        if ($scope.IsVisible1 == true) {
            activityService.getOwnerChapters($scope.currentUser.email).success(function (data) {
                $scope.ownerChapterDetails = data.Items;
                console.log($scope.ownerChapterDetails);
                $scope.loading = false;
            });
        }
    }
    $scope.getChapterWiseIssues = function (cap) {
        $scope.loading = true;
        activityService.getChapterWiseIssueDetails($scope.currentUser.email, cap, $scope.currentUser.userName).success(function (data) {
            $scope.ownerIssueDetails = data.Items;
            console.log($scope.ownerIssueDetails);
            $scope.loading = false;
        });
    }
    $scope.getHODChapterWiseIssues = function (cap) {
        
        activityService.getHODChapterWiseIssueDetails("",$scope.currentUser.email, cap, $scope.currentUser.userName).success(function (data) {
            $scope.beyondCount = 0;
            $scope.withinCount = 0;

            $scope.hodIssueDetails = data.Items;
            for (var i = 0; i < $scope.hodIssueDetails.length; i++) {
                console.log($scope.hodIssueDetails[i].ChapterName);
                if ($scope.hodIssueDetails[i].ChapterName == "Dealer Feedback" || $scope.hodIssueDetails[i].ChapterName == "Customer Service Cell") {
                    if ($scope.hodIssueDetails[i].PendingSince > 7) {

                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                    }
                    else {
                        $scope.withinCount = $scope.withinCount + 1;
                        $scope.withinCount = $scope.withinCount;
                        console.log($scope.withinCount);
                    }
                }
                else if ($scope.hodIssueDetails[i].ChapterName == "WSS Service Cell" || $scope.hodIssueDetails[i].ChapterName == "My Pidilite") {
                    console.log($scope.hodIssueDetails[i]);
                    if ($scope.hodIssueDetails[i].TAT == "Beyond TAT") {
                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                    }
                    else {
                        $scope.withinCount = $scope.withinCount + 1;
                        $scope.withinCount = $scope.withinCount;
                        console.log($scope.withinCount);
                    }
                }
                else if ($scope.hodIssueDetails[i].ChapterName == "Pilworld WSS feedback" || $scope.hodIssueDetails[i].ChapterName == "Market Sudhar") {
                    if ($scope.hodIssueDetails[i].PendingSince > 45) {

                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                    }
                    else {
                        $scope.withinCount = $scope.withinCount + 1;
                        $scope.withinCount = $scope.withinCount;
                        console.log($scope.withinCount);
                    }
                }
                else if ($scope.hodIssueDetails[i].ChapterName == "Risk Management") {
                    if ($scope.hodIssueDetails[i].PendingSince > 7) {
                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                    }
                }
                else if ($scope.hodIssueDetails[i].ChapterName == "Birthday Lunch" || $scope.hodIssueDetails[i].ChapterName == "Sampark" || $scope.hodIssueDetails[i].ChapterName == "Pidilite Ideas Initiatives" || $scope.hodIssueDetails[i].ChapterName == "Simplified Suggestion") {
                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                }
            }
        });
    }
    $scope.DisplayChapterHODValues = function () {
        $scope.loading = true;
        $scope.IsVisible2 = $scope.IsVisible2 ? false : true;
        $scope.IsVisible = false;
        $scope.IsVisible1 = false;
        $scope.IsVisible3 = false;
        $scope.IsVisible4 = false;
        $scope.IsVisible5 = false;
        if ($scope.IsVisible2 == true) {
            activityService.getHODChapters($scope.currentUser.email).success(function (data) {
                $scope.hodChapterDetails = data.Items;
                console.log($scope.hodChapterDetails);
                $scope.loading = false;
            });
        }
    }
    
    $scope.getAllChapterWiseIssues = function (cap) {
        $scope.loading = true;
        $scope.beyondCount = 0;
        $scope.withinCount = 0;

        activityService.getAllChapterWiseIssueDetails($scope.currentUser.email, cap, $scope.currentUser.userName).success(function (data) {
            $scope.allIssueDetails = data.Items;
            for (var i = 0; i < $scope.allIssueDetails.length; i++) {
                console.log($scope.allIssueDetails[i].ChapterName);
                if ($scope.allIssueDetails[i].ChapterName == "Dealer Feedback" || $scope.allIssueDetails[i].ChapterName == "Customer Service Cell") {
                    if ($scope.allIssueDetails[i].PendingSince > 7) {

                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                    }
                    else {
                        $scope.withinCount = $scope.withinCount + 1;
                        $scope.withinCount = $scope.withinCount;
                        console.log($scope.withinCount);
                    }

                    //if($scope.allIssueDetails[i].PendingWithEmail)
                }
                else if ($scope.allIssueDetails[i].ChapterName == "WSS Service Cell" || $scope.allIssueDetails[i].ChapterName == "My Pidilite") {
                    console.log($scope.allIssueDetails[i]);
                    if ($scope.allIssueDetails[i].TAT == "Beyond TAT") {
                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                    }
                    else {
                        $scope.withinCount = $scope.withinCount + 1;
                        $scope.withinCount = $scope.withinCount;
                        console.log($scope.withinCount);
                    }
                }
                else if ($scope.allIssueDetails[i].ChapterName == "Pilworld WSS feedback" || $scope.allIssueDetails[i].ChapterName == "Market Sudhar") {
                    if ($scope.allIssueDetails[i].PendingSince > 45) {

                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                    }
                    else {
                        $scope.withinCount = $scope.withinCount + 1;
                        $scope.withinCount = $scope.withinCount;
                        console.log($scope.withinCount);
                    }

                    //if($scope.allIssueDetails[i].PendingWithEmail)
                }
                else if ($scope.allIssueDetails[i].ChapterName == "Risk Management") {
                    if ($scope.allIssueDetails[i].PendingSince > 7) {
                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                    }
                }
                else if ($scope.allIssueDetails[i].ChapterName == "Birthday Lunch" || $scope.allIssueDetails[i].ChapterName == "Sampark" || $scope.allIssueDetails[i].ChapterName == "Pidilite Ideas Initiatives" || $scope.allIssueDetails[i].ChapterName == "Simplified Suggestion") {
                    $scope.beyondCount = $scope.beyondCount + 1;
                    $scope.beyondCount = $scope.beyondCount;
                    console.log($scope.beyondCount);
                }
            }
            console.log($scope.allIssueDetails);
            $scope.loading = false;

        });
    }
    $scope.DisplayChapterAllValues = function () {
        $scope.loading = true;
        $scope.IsVisible3 = $scope.IsVisible3 ? false : true;
        $scope.IsVisible = false;
        $scope.IsVisible1 = false;
        $scope.IsVisible2 = false;
        $scope.IsVisible4 = false;
        $scope.IsVisible5 = false;
       
        if ($scope.IsVisible3 == true) {
            
            activityService.getAllChapters($scope.currentUser.email, $scope.userRole.RoleName1).success(function (data) {
                $scope.allChapterDetails = data.Items;                
                console.log($scope.allChapterDetails);
                $scope.loading = false;
            });
        }
    }
    $scope.visFlag = false;
    $scope.Index = 0;

    //$scope.isUserComment = function (item) {
       //console.log(item);
    //    return item.iD_Request !== '' && (item.active !== true || item.active === undefined);
        
    //}
    $scope.openModal1 = function (IssueItem) {
        // alert('');
        $scope.IssueItem = IssueItem;
        activityService.getAllCmments(IssueItem.RequestNo, IssueItem.SystemChapterName, IssueItem.PendingWithEmail).success(function (data) {
            if (data.items.length > 0) {
                $scope.issueCommentDetails = data.items;
                if ($scope.issueCommentDetails[0].iD_Status_Id.statusDesc == "Completed") {
                    $scope.visFlag = true;
                }
                console.log($scope.issueCommentDetails);
            }
            else {
               // alert('1');
                $scope.issueCommentDetails[0].iD_Comments = "";
                $scope.issueCommentDetails[0].iD_Request = "";
                $scope.issueCommentDetails[0].id_System_Name = "";
                $scope.issueCommentDetails[0].iD_Pending_With_Email = "";
                //$scope.
                $scope.issueCommentDetails[0].iD_Status = "";
                console.log($scope.issueCommentDetails);
                $scope.issueCommentDetails = [{}];
       //         $scope.issueCommentDetails = [
       //[{ iD_Comments: '', iD_Status_Id: { "id": 1, "statusDesc": "Open", "rct": "0001-01-01T00:00:00", "rut": "0001-01-01T00:00:00", "rcb": null } }]
       //         ];
                $scope.issueCommentDetails[0].active = true;
                $scope.issueCommentDetails[0].iD_Status_Id = { "statusDesc": "In Progress", "id": 1, "rct": "0001-01-01T00:00:00", "rut": "0001-01-01T00:00:00", "rcb": null };

            }
            $("#myModal1PendingBeyond").modal("toggle");           
        });
    }

     

    $scope.getConfirmation1 = function (commentDetail) {
        var issueItems = $scope.IssueItem;
        var commentIems = commentDetail;
        console.log(commentIems);
        if (commentIems != null) {
            if (commentIems[0].iD_Comments == "" || commentIems[0].iD_Comments == undefined || commentIems[0].iD_Comments == null) {
                alert("Enter Comments");
                return false;
            }
            //$('#myModal1' + issueItems.SystemChapterName + index).modal('toggle')
            $('#myModal1PendingBeyond').modal('toggle')
            console.log(commentIems);
            $scope.item =
                {
                    ID_Request: issueItems.RequestNo,
                    id_System_Name: issueItems.SystemChapterName,
                    ID_Pending_With_Email: issueItems.PendingWithEmail,
                    ID_Status: commentIems[0].iD_Status_Id != undefined ? commentIems[0].iD_Status_Id.statusDesc : null,
                    ID_Comments: commentIems[0].iD_Comments,
                    ID_Status_Id: commentIems[0].iD_Status_Id,
                };
            console.log($scope.item)
            if (commentIems.ID_Comments != "" || commentIems.ID_Status != null) {
                ConfirmDialog1('Would you like to save the changes?', $scope.item);
            }
        }
    }

    function ConfirmDialog1(message, commentIems) {
        $('<div></div>').appendTo('body')
                        .html('<div><h6>' + message + '?</h6></div>')
                        .dialog({
                            modal: true, title: 'Add Comment', zIndex: 10000, autoOpen: true,
                            width: 'auto', resizable: false,
                            buttons: {
                                Yes: function () {
                                    activityService.updateComment(commentIems).success(function (data) {
                                        $scope.saveCom = data;
                                        if (data.success) {
                                            alert(data.message);
                                            // alert("aaliya")
                                            $scope.beyondCount = 0;
                                            $scope.withinCount = 0;

                                            activityService.getAllChapterWiseIssueDetails($scope.currentUser.email, commentIems.id_System_Name, $scope.currentUser.userName).success(function (data) {
                                                $scope.allIssueDetails = data.Items;

                                                for (var i = 0; i < $scope.allIssueDetails.length; i++) {
                                                    console.log($scope.allIssueDetails[i].ChapterName);
                                                    if ($scope.allIssueDetails[i].ChapterName == "Dealer Feedback" || $scope.allIssueDetails[i].ChapterName == "Customer Service Cell") {
                                                        if ($scope.allIssueDetails[i].PendingSince > 7) {

                                                            $scope.beyondCount = $scope.beyondCount + 1;
                                                            $scope.beyondCount = $scope.beyondCount;
                                                            console.log($scope.beyondCount);
                                                        }
                                                        else {
                                                            $scope.withinCount = $scope.withinCount + 1;
                                                            $scope.withinCount = $scope.withinCount;
                                                            console.log($scope.withinCount);
                                                        }

                                                        //if($scope.allIssueDetails[i].PendingWithEmail)
                                                    }
                                                    else if ($scope.allIssueDetails[i].ChapterName == "WSS Service Cell" || $scope.allIssueDetails[i].ChapterName == "My Pidilite") {
                                                        console.log($scope.allIssueDetails[i]);
                                                        if ($scope.allIssueDetails[i].TAT == "Beyond TAT") {
                                                            $scope.beyondCount = $scope.beyondCount + 1;
                                                            $scope.beyondCount = $scope.beyondCount;
                                                            console.log($scope.beyondCount);
                                                        }
                                                        else {
                                                            withinCount = withinCount + 1;
                                                            $scope.withinCount = withinCount;
                                                            console.log($scope.withinCount);
                                                        }
                                                    }
                                                    else if ($scope.allIssueDetails[i].ChapterName == "Pilworld WSS feedback" || $scope.allIssueDetails[i].ChapterName == "Market Sudhar") {
                                                        if ($scope.allIssueDetails[i].PendingSince > 45) {

                                                            $scope.beyondCount = $scope.beyondCount + 1;
                                                            $scope.beyondCount = $scope.beyondCount;
                                                            console.log($scope.beyondCount);
                                                        }
                                                        else {
                                                            $scope.withinCount = $scope.withinCount + 1;
                                                            $scope.withinCount = $scope.withinCount;
                                                            console.log($scope.withinCount);
                                                        }

                                                        //if($scope.allIssueDetails[i].PendingWithEmail)
                                                    }
                                                    else if ($scope.allIssueDetails[i].ChapterName == "Risk Management") {
                                                        if ($scope.allIssueDetails[i].PendingSince > 7) {
                                                            $scope.beyondCount = $scope.beyondCount + 1;
                                                            $scope.beyondCount = $scope.beyondCount;
                                                            console.log($scope.beyondCount);
                                                        }
                                                    }
                                                    else if ($scope.allIssueDetails[i].ChapterName == "Birthday Lunch" || $scope.allIssueDetails[i].ChapterName == "Sampark" || $scope.allIssueDetails[i].ChapterName == "Pidilite Ideas Initiatives" || $scope.allIssueDetails[i].ChapterName == "Simplified Suggestion") {
                                                        $scope.beyondCount = $scope.beyondCount + 1;
                                                        $scope.beyondCount = $scope.beyondCount;
                                                        console.log($scope.beyondCount);
                                                    }
                                                }
                                                console.log($scope.allIssueDetails);
                                                angular.forEach($scope.allIssueDetails, function (value, key) {
                                                    //  if (value.Comments == "") {
                                                    activityService.getAllCmments(value.RequestNo, value.SystemChapterName, value.PendingWithEmail).success(function (data) {
                                                        $scope.IssData = data.items;
                                                        if ($scope.IssData.length > 0) {
                                                            value.Comments = $scope.IssData[0].iD_Comments;
                                                        }
                                                    });
                                                    // }
                                                });

                                            });
                                            // window.location.reload();
                                        }
                                    });

                                    $(this).dialog("close");

                                },
                                No: function () {
                                    $(this).dialog("close");

                                }
                            },
                        });
    };

    $scope.openModal = function (IssueItem, index, modelId) {
        //debugger;
        console.log(modelId);
        //debugger;
        $scope.visFlag = false;
        activityService.getAllCmments(IssueItem.RequestNo, IssueItem.SystemChapterName, IssueItem.PendingWithEmail).success(function (data) {
            if (data.items.length > 0) {
                $scope.issueCommentDetails = data.items;
                
                if ($scope.issueCommentDetails[0].iD_Status_Id.statusDesc == "Completed") {
                    $scope.visFlag = true;
                }
                console.log($scope.issueCommentDetails);
            }
            else {
               // alert("ff");
                $scope.issueCommentDetails[0].iD_Comments = "";
                $scope.issueCommentDetails[0].iD_Status_Id = { "statusDesc": "Open", "id": 1, "rct": "0001-01-01T00:00:00", "rut": "0001-01-01T00:00:00", "rcb": null };
                $scope.issueCommentDetails[0].iD_Request = "";
                $scope.issueCommentDetails[0].id_System_Name = "";
                $scope.issueCommentDetails[0].iD_Pending_With_Email = "";
                $scope.issueCommentDetails[0].iD_Status = "";
                 
              
            }
        });
       // debugger;
        // $('#myModal1' + IssueItem.SystemChapterName + index).modal('show');
       // modelId = modelId.replace(" ", "");
        $('#' + modelId).modal('show');

    }
    $scope.test123 = function (modelId) {
        //$('#myModal1PendingBeyond2Market_Sudhar1').style('display', 'block');
        //$('#myModal1PendingBeyond2Market_Sudhar1').toggleClass('fade in');
        //$('#myModal1PendingBeyond2Market_Sudhar1').modal('show');
         
    }

    $scope.getVal = function (chpname) {
        // chpname = chpname.replace(" ", "");
        chpname = chpname.replace(/ /g, "");
        return chpname;
    }
    $scope.getConfirmation = function (issueItems, commentIems, index, modelId) {
        console.log(commentIems);
        if (commentIems != null) {
            if (commentIems[0].iD_Comments == "" || commentIems[0].iD_Comments == undefined || commentIems[0].iD_Comments == null) {
                alert("Enter Comments");
                return false;
            }
            //$('#myModal1' + issueItems.SystemChapterName + index).modal('toggle')
            $('#' + modelId).modal('toggle')
            $scope.item =
                {
                    ID_Request: issueItems.RequestNo,
                    id_System_Name: issueItems.SystemChapterName,
                    ID_Pending_With_Email: issueItems.PendingWithEmail,
                    ID_Status: commentIems[0].iD_Status_Id.statusDesc,
                    ID_Comments: commentIems[0].iD_Comments,
                    ID_Status_Id: commentIems[0].iD_Status_Id,
                };
            console.log($scope.item)
            if (commentIems.ID_Comments != "" || commentIems.ID_Status != null) {
                ConfirmDialog('Would you like to save the changes?', $scope.item);
            }
        }
    }

    function ConfirmDialog(message, commentIems) {
        $('<div></div>').appendTo('body')
                        .html('<div><h6>' + message + '?</h6></div>')
                        .dialog({
                            modal: true, title: 'Add Comment', zIndex: 10000, autoOpen: true,
                            width: 'auto', resizable: false,
                            buttons: {
                                Yes: function () {
                                    activityService.updateComment(commentIems).success(function (data) {
                                        $scope.saveCom = data;
                                        if (data.success) {
                                            alert(data.message);
                                           // window.location.reload();
                                        }
                                    });

                                    $(this).dialog("close");

                                },
                                No: function () {
                                    $(this).dialog("close");

                                }
                            },
                        });
    };

    $scope.saveAnchorComments = function (issueItems, commentIems, index, modelId) {
         
        console.log(commentIems);
        if (commentIems != null) {
            if (commentIems[0].anchorComments == "" || commentIems[0].anchorComments == undefined || commentIems[0].anchorComments == null) {
                alert("Enter Comment");
                return false;
            }
            //$('#myModal1' + issueItems.SystemChapterName + index).modal('toggle')
            $('#' + modelId).modal('toggle')
            $scope.item =
                {
                    ID_Request: issueItems.RequestNo,
                    id_System_Name: issueItems.SystemChapterName,
                    ID_Pending_With_Email: issueItems.PendingWithEmail,
                    ID_Comments: commentIems[0].anchorComments,
                };
            console.log($scope.item)
            if (commentIems.anchorComments != "" ) {
                ConfirmDialogforAnchor('Would you like to save the changes?', $scope.item);
            }
        }
    }

    function ConfirmDialogforAnchor(message, commentIems) {
        $('<div></div>').appendTo('body')
                        .html('<div><h6>' + message + '?</h6></div>')
                        .dialog({
                            modal: true, title: 'Add Comment', zIndex: 10000, autoOpen: true,
                            width: 'auto', resizable: false,
                            buttons: {
                                Yes: function () {
                                    activityService.updateAnchorComment(commentIems).success(function (data) {
                                        $scope.saveCom = data;
                                        if (data.success) {
                                            alert(data.message);
                                        }
                                    });

                                    $(this).dialog("close");

                                },
                                No: function () {
                                    $(this).dialog("close");

                                }
                            },
                        });
    };

    $scope.TopMgmt = "";
    $scope.displayFlag = false;
    $scope.SystemTopMngt = "";
    $scope.getHOdsName = function (SystemChapName, ChapterNameDisplay, EditFlag, AnchorFlag) {
        $scope.displayFlag = false;
        if (AnchorFlag == "ChapterAnchor") {
            $scope.displayFlag = true;
        }
        else if (AnchorFlag == "Member") {
            if (EditFlag == 'true') {
                 $scope.displayFlag = true;
            }
        }
        $scope.hodIssueDetails = null;
        $scope.beyondCount = 0;
        $scope.withinCount = 0;
          activityService.getHodName(SystemChapName, $scope.userRole.RoleName1, $scope.currentUser.email).success(function (data) {
            $scope.hodNameData = data.Items;
            if ($scope.hodNameData.length > 0) {
                $scope.TopMgmt = ChapterNameDisplay;
                $scope.SystemTopMngt = SystemChapName;
                $scope.IsVisible4 = true;
                $scope.IsVisible = false;
                $scope.IsVisible1 = false;
                $scope.IsVisible2 = false;
                $scope.IsVisible3 = false;
                $scope.IsVisible5 = false;
                google.charts.load("current", { packages: ["corechart"] });
                google.charts.setOnLoadCallback(drawChart(SystemChapName));
               
                activityService.getColumnDetails($scope.currentUser.id, $scope.SystemTopMngt).success(function (data) {
                    
                    if (data.Items[0] != null && data.Items[0] != "")
                        $scope.userWiseColumnNames = data.Items[0]; 
                });
            }
          });
      
        
        console.log($scope.RecordEditFlag);
    }

    $scope.getHOdsNameforPending = function (SystemChapName, ChapterNameDisplay, EditFlag, AnchorFlag) { 
        $scope.displayFlag = false;
        if (AnchorFlag == "ChapterAnchor") {
            $scope.displayFlag = true;
        }
        else if (AnchorFlag == "Member") {
            if (EditFlag == 'true') {
                $scope.displayFlag = true;
            }
        }
        $scope.hodIssueDetails = null;
       
        //activityService.getIssueDetails($scope.currentUser.email, SystemChapName, $scope.currentUser.userName).success(function (data) {
        //activityService.getHodName(SystemChapName, $scope.userRole.RoleName1, $scope.currentUser.email).success(function (data) {

        activityService.getAllChapterWiseIssueDetails($scope.currentUser.email, SystemChapName, $scope.currentUser.userName).success(function (data) {
            $scope.beyondCount = 0;
            $scope.withinCount = 0;
            

            $scope.allIssueDetails = data.Items;
            for (var i = 0; i < $scope.allIssueDetails.length; i++) {
                console.log($scope.allIssueDetails[i].ChapterName);
                if ($scope.allIssueDetails[i].ChapterName == "Dealer Feedback" || $scope.allIssueDetails[i].ChapterName == "Customer Service Cell") {
                    if ($scope.allIssueDetails[i].PendingSince > 7) {

                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                    }
                    else {
                        $scope.withinCount = $scope.withinCount + 1;
                        $scope.withinCount = $scope.withinCount;
                        console.log($scope.withinCount);
                    }
                
                    //if($scope.allIssueDetails[i].PendingWithEmail)
                }
                else if ($scope.allIssueDetails[i].ChapterName == "WSS Service Cell" || $scope.allIssueDetails[i].ChapterName == "My Pidilite") {
                    console.log($scope.allIssueDetails[i]);
                    if ($scope.allIssueDetails[i].TAT == "Beyond TAT") {
                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                    }
                    else {
                        $scope.withinCount = $scope.withinCount + 1;
                        $scope.withinCount = $scope.withinCount;
                        console.log($scope.withinCount);
                    }
                }
                else if ($scope.allIssueDetails[i].ChapterName == "Pilworld WSS feedback" || $scope.allIssueDetails[i].ChapterName == "Market Sudhar") {
                    if ($scope.allIssueDetails[i].PendingSince > 45) {

                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                    }
                    else {
                        $scope.withinCount = $scope.withinCount + 1;
                        $scope.withinCount = $scope.withinCount;
                        console.log($scope.withinCount);
                    }

                    //if($scope.allIssueDetails[i].PendingWithEmail)
                }
                else if ($scope.allIssueDetails[i].ChapterName == "Risk Management") {
                    if ($scope.allIssueDetails[i].PendingSince > 7) {
                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                    }
                }
                else if ($scope.allIssueDetails[i].ChapterName == "Birthday Lunch" || $scope.allIssueDetails[i].ChapterName == "Sampark" || $scope.allIssueDetails[i].ChapterName == "Pidilite Ideas Initiatives" || $scope.allIssueDetails[i].ChapterName == "Simplified Suggestion") {
                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                }
            }
            console.log($scope.allIssueDetails.ChapterName);
            //$scope.hodNameData = data.Items;
            if ($scope.allIssueDetails.length > 0) {
                $scope.TopMgmt = ChapterNameDisplay;
                $scope.SystemTopMngt = SystemChapName;
                $scope.IsVisible5 = true;
                $scope.IsVisible4 = false;
                $scope.IsVisible = false;
                $scope.IsVisible1 = false;
                $scope.IsVisible2 = false;
                $scope.IsVisible3 = false;
                //google.charts.load("current", { packages: ["corechart"] });
                //google.charts.setOnLoadCallback(drawChart(SystemChapName));

                angular.forEach($scope.allIssueDetails, function (value, key) {
                  //  if (value.Comments == "") {
                        activityService.getAllCmments(value.RequestNo, value.SystemChapterName, value.PendingWithEmail).success(function (data) {
                            $scope.IssData = data.items;
                            if ($scope.IssData.length > 0) {
                                value.Comments = $scope.IssData[0].iD_Comments;
                            }
                        });
                   // }
                });

                activityService.getColumnDetails($scope.currentUser.id, $scope.SystemTopMngt).success(function (data) {

                    if (data.Items[0] != null && data.Items[0] != "")
                        $scope.userWiseColumnNames = data.Items[0];
                });
            }
        });

    }

    function drawChart(SystemChapName) {
        activityService.getHodCount(SystemChapName, $scope.userRole.RoleName1, $scope.currentUser.email).success(function (data) {
            var dataHodCount = data.Items;
            //alert("hii")
            
            var series = new Array();
            for (var i in dataHodCount) {
                var serie = new Array(dataHodCount[i].SAPDivisionName, dataHodCount[i].count);
                series.push(serie);
            }
            console.log("SERIES");
            console.log(series);
            DrawPieChart(series);

        });
    }
    function DrawPieChart(series) {
        $('#container').highcharts({

            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: 1, //null,  
                plotShadow: false
            },
            title: {
                text: 'Division wise pending issues'
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: false
                    },
                    showInLegend: true,

                }
            },
            series: [{
                type: 'pie',
                name: 'Pending Issue',
                data: series,
                events: {
                    click: function (event) {

                        var activePoints = myNewChart.getSegmentsAtEvent(evt);
                        alert(activePoints);

                    }
                }
            }]
        });
    }
  
    //google.charts.load("current", { packages: ["corechart"] });
    //google.charts.setOnLoadCallback(drawChartExample);

    //function drawChartExample() {
      
    //  //  var arrayToDataTable=new array();
    //    var data = google.visualization.arrayToDataTable([
    //      ['Division', 'Percentage'],
    //      ['IT', 11],
    //      ['FV', 10],
    //      ['CC', 50],
    //    ]);

    //    var options = {
    //        title: 'Division Wise Pending Issues'
    //    };

    //    var chart = new google.visualization.PieChart(document.getElementById('piechartexample'));
    //    chart.draw(data, options);
    //}

    $scope.back = function () {
        if ($scope.userRole.RoleName3 == 'ChapterOwner') {
            $scope.DisplayChapterOwnerValues();
        }
        else {
            $scope.DisplayChapterAllValues();
        }
        $scope.IsVisible4 = false;
    }

    $scope.backPending = function () {
        
        $scope.DisplayPendingValues();
        //alert("On Click 3");
        $scope.IsVisible5 = false;
    }

    $scope.trueFlag = 0;
    $scope.falseFlag = 0;

    //$scope.withinTATLength = 0;
    //$scope.BeyondTATLength = 0;
    $scope.checkiflessthan = function (prop, val) {
        return function (item) {
            //alert(item[prop])
            //console.log(item);
            if (item[prop] <= val) {
                $scope.falseFlag = parseInt($scope.falseFlag) + 1;
               // alert($scope.falseFlag)
            }
            //if (item.PendingSince <= val) {
            //    $scope.withinTATLength++;
            //}
            return item[prop] <= val;
        }   
    }

    $scope.checkifGreaterthan = function (prop, val) {
       // alert($scope.trueFlag)
        return function (item) {
            if (item[prop] > val) {
                $scope.trueFlag =parseInt($scope.trueFlag) + 1;
            }
            //console.log(item);
            //if (item.PendingSince > val) {
            //    $scope.BeyondTATLength++;
            //}
            return item[prop] > val;
        }   
    }

    $scope.getSelectedHODChapterWiseIssues = function (cap, Hod) {
        $scope.trueFlag = 0;
        $scope.falseFlag = 0;
       // alert($scope.SystemTopMngt)
        $scope.divisionVal = $scope.item.division.SAPDivisionName;
        $scope.hodIssueDetails = null;

        console.log($scope.divisionVal + ',' + "" + ',' + $scope.SystemTopMngt + ',' + $scope.userRole.RoleName1 + ',' + $scope.currentUser.userName);
        $scope.hodIssueDetails = [];
        $scope.beyondCount = 0;
        $scope.withinCount = 0;
        activityService.getHODChapterWiseIssueDetails($scope.divisionVal, "", $scope.SystemTopMngt, $scope.userRole.RoleName1, $scope.currentUser.userName).success(function (data) {
            $scope.hodIssueDetails = data.Items;
            

            for (var i = 0; i < $scope.hodIssueDetails.length; i++) {
                console.log($scope.hodIssueDetails[i].ChapterName);
                if ($scope.hodIssueDetails[i].ChapterName == "Dealer Feedback" || $scope.hodIssueDetails[i].ChapterName == "Customer Service Cell") {
                    if ($scope.hodIssueDetails[i].PendingSince > 7) {

                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                    }
                    else {
                        $scope.withinCount = $scope.withinCount + 1;
                        $scope.withinCount = $scope.withinCount;
                        console.log($scope.withinCount);
                    }

                   
                }
                else if ($scope.hodIssueDetails[i].ChapterName == "WSS Service Cell" || $scope.hodIssueDetails[i].ChapterName == "My Pidilite") {
                    console.log($scope.hodIssueDetails[i]);
                    if ($scope.hodIssueDetails[i].TAT == "Beyond TAT") {
                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                    }
                    else {
                        $scope.withinCount = $scope.withinCount + 1;
                        $scope.withinCount = $scope.withinCount;
                        console.log($scope.withinCount);
                    }
                }
                else if ($scope.hodIssueDetails[i].ChapterName == "Pilworld WSS feedback" || $scope.hodIssueDetails[i].ChapterName == "Market Sudhar") {
                    if ($scope.hodIssueDetails[i].PendingSince > 45) {

                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                    }
                    else {
                        $scope.withinCount = $scope.withinCount + 1;
                        $scope.withinCount = $scope.withinCount;
                        console.log($scope.withinCount);
                    }
                }
                else if ($scope.hodIssueDetails[i].ChapterName == "Risk Management") {
                    if ($scope.hodIssueDetails[i].PendingSince > 7)
                    {
                        $scope.beyondCount = $scope.beyondCount + 1;
                        $scope.beyondCount = $scope.beyondCount;
                        console.log($scope.beyondCount);
                    }                    
                }
                else if ($scope.hodIssueDetails[i].ChapterName == "Birthday Lunch" || $scope.hodIssueDetails[i].ChapterName == "Sampark" || $scope.hodIssueDetails[i].ChapterName == "Pidilite Ideas Initiatives" || $scope.hodIssueDetails[i].ChapterName == "Simplified Suggestion") {
                    $scope.beyondCount = $scope.beyondCount + 1;
                    $scope.beyondCount = $scope.beyondCount;
                    console.log($scope.beyondCount);
                }
            }

            console.log("Aaliya")
            console.log($scope.hodIssueDetails);
            console.log("Khan")
            //if ($scope.hodIssueDetails.length > 0) {
                // angular.forEach($scope.hodIssueDetails, function (value, key) { 
               // for (var i = 0; i < $scope.hodIssueDetails.length; i++) {
                   //// if ($scope.hodIssueDetails[i].Comments == "") {
                        // debugger;
                        //activityService.getAllCmments($scope.hodIssueDetails[i].RequestNo, $scope.hodIssueDetails[i].SystemChapterName, $scope.hodIssueDetails[i].PendingWithEmail).success(function (data) {
                        //    $scope.IssData = data.items;
                        //    console.log($scope.IssData);
                        //    if ($scope.IssData.length > 0) {
                        //        $scope.hodIssueDetails[i].Comments = $scope.IssData[0].iD_Comments;
                        //    }
                        //});
                    //}
                //}
                    //if (value.Comments == "") {
                    //   // debugger;
                    //    activityService.getAllCmments(value.RequestNo, value.SystemChapterName, value.PendingWithEmail).success(function (data) {
                    //        $scope.IssData = data.items;
                    //        console.log($scope.IssData);
                    //        if ($scope.IssData.length > 0) {
                    //            value.Comments = $scope.IssData[0].iD_Comments;
                    //        }
                    //    });
                    //}
              //  });
          //  }
            //console.log($scope.hodIssueDetails);

        });
    }

    $scope.downloadFullDump = function (cap, Hod,TAT) {
        window.open('Requisition/ExportFullDumpReport?ResponsibleEmail=' + $scope.item.division.SAPDivisionName + '&ChapterName=' + $scope.SystemTopMngt + '&Owner=HOD&RoleName=' + $scope.userRole.RoleName1 + '&UserName=' + $scope.currentUser.userName + '&TAT=' + TAT);
    }
    $scope.downloadFullDumpClose = function (cap) {
        window.open('Requisition/ExportFullDumpReportClose?ChapterName=' + $scope.SystemTopMngt );
    }

    $scope.ShowMoreData = function (Id,AnchorId,LesssId) {
       // alert(Id);
        $('#' + Id).css("height", "auto");
        $('#' + AnchorId).css('visibility', 'hidden');
        $('#' + LesssId).css("visibility", "visible");
        //alert("bye")
    }
    $scope.ShowLessData = function (Id, AnchorId, LesssId) {
        //alert(Id);
        $('#'+Id).removeAttr("style");
        $('#' + Id).addClass("less");
        $('#' + AnchorId).css("visibility", "visible");
        $('#' + LesssId).css('visibility', 'hidden');
        //$('#' + Id).css("width", "100%");


    }
   
    $scope.GetCheckedDetails = function () {
        console.log($scope.userWiseColumnNames);
        $scope.userWiseColumnNames.chapterName = $scope.SystemTopMngt;
        activityService.SubmitColumns($scope.userWiseColumnNames).success(function (data) {
            $scope.saveCom = data;
            if (data.success) {
                alert(data.message);
                window.location.reload();
            }
        });
        
    }
    $scope.ExportToExcelIssueDetails = function () {
      

            var req = { items: $scope.hodIssueDetails }

            var filePath = "";
            activityService.getExcelforHODIssues(req)
                .success(function (data, status) {
                    console.log(data);
                    filePath = data;
                    window.open("Reports/DownloadExcelInitiative?path=" + filePath);

                }).error(function () {
                    alert('Error in submitting request')
                });

        
    }

}]);