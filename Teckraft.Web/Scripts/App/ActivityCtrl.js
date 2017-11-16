
angular
    .module('asfApp.service.activityService', [])
    .factory('activityService', [
        '$http','$rootScope',
        function ($http, $rootScope) {
            return {
                getDepartment: function (query) {
                    if (query == 'DealerDivision') {
                        var strquery = JSON.stringify({ CurrentPage: 0, PageSize: 20, Parameters: [{ Name: 'DealerDivision', Value: query }] });
                        return $http({
                            method: 'GET',
                            url: "/api/Department?q=" + strquery
                        });
                    }
                    else {
                        return $http({
                            method: 'GET',
                            url: '/api/Department?q=' + query
                        });
                    }
                },
                
                getChapters: function (ResponsiblePersonEmail) {
                    return $http({
                        method: 'GET',
                        contentType: "application/json",
                        url: '/Requisition/getChapterDetails?ResponsibleEmail=' + ResponsiblePersonEmail + '&Owner=null',
                        //data: UserName

                    })
                },

                getOwnerChapters: function (ResponsiblePersonEmail) {
                    return $http({
                        method: 'GET',
                        contentType: "application/json",
                        url: '/Requisition/getChapterDetails?ResponsibleEmail=' + ResponsiblePersonEmail + '&Owner=owner' ,
                        //data: UserName

                    })
                },

                getHODChapters: function (ResponsiblePersonEmail) {
                    return $http({
                        method: 'GET',
                        contentType: "application/json",
                        url: '/Requisition/getChapterDetails?ResponsibleEmail=' + ResponsiblePersonEmail + '&Owner=HOD',
                        //data: UserName

                    })
                },

                getAllChapters: function (ResponsiblePersonEmail,RoleName) {
                    return $http({
                        method: 'GET',
                        contentType: "application/json",
                        //url: '/Requisition/getChapterDetails?ResponsibleEmail=' + ResponsiblePersonEmail + '&Owner=TopManagement',
                        url: '/Requisition/getChapterDetails?ResponsibleEmail=' + ResponsiblePersonEmail + '&Owner=' + RoleName,

                    })
                },

                getIssueDetails: function (ResponsiblePersonEmail,ChapterName,UserName) {
                    return $http({
                        method: 'GET',
                        contentType: "application/json",
                        url: '/Requisition/getIssueDetails?Division=""&ResponsibleEmail=' + ResponsiblePersonEmail + '&ChapterName=' + ChapterName + '&Owner=null&RoleName=null&UserName=' + UserName + '',
                        //data: UserName

                    })
                },

                getChapterWiseIssueDetails: function (ResponsiblePersonEmail, ChapterName, UserName) {
                    return $http({
                        method: 'GET',
                        contentType: "application/json",
                        url: '/Requisition/getIssueDetails?Division=""&ResponsibleEmail=' + ResponsiblePersonEmail + '&ChapterName=' + ChapterName + '&Owner=owner&RoleName=null&UserName=' + UserName + '',
                        //data: UserName

                    })
                },

                getHODChapterWiseIssueDetails: function (Division, ResponsiblePersonEmail, ChapterName, RoleName, UserName) {
                    return $http({
                        method: 'GET',
                        contentType: "application/json",
                        url: '/Requisition/getIssueDetails?Division='+ Division + '&ResponsibleEmail=' + ResponsiblePersonEmail + '&ChapterName=' + ChapterName + '&Owner=HOD&RoleName=' + RoleName + '&UserName=' + UserName + '',
                        //data: UserName

                    })
                },

               

                getAllChapterWiseIssueDetails: function (ResponsiblePersonEmail, ChapterName, UserName) {
                    return $http({
                        method: 'GET',
                        contentType: "application/json",
                        url: '/Requisition/getIssueDetails?Division=""&ResponsibleEmail=' + ResponsiblePersonEmail + '&ChapterName=' + ChapterName + '&Owner=null&RoleName=null&UserName=' + UserName + '',
                        //data: UserName

                    })
                },

                getAllCmments: function (RequestNo, ChapterName, PendingWithEmail) {
                    var strquery = JSON.stringify({ CurrentPage: 0, PageSize: 20, Parameters: [{ Name: 'RequestNo', Value: RequestNo }, { Name: 'ChapterName', Value: ChapterName }, { Name: 'PendingWithEmail', Value: PendingWithEmail }] });
                    return $http({
                        method: 'GET',
                        url: '/api/IssueManagementCommentLog?q=' + strquery
                    });
                },

              

                getUserRole: function (userName) {
                    return $http({
                        method: 'GET',
                        contentType: "application/json",
                        url: '/Requisition/getUserRole?Username=' + userName,
                    })
                },


                getCurrentUserDetail: function () {
                    var d = "{requestType:'currentUserDetail'}"
                    return $http({
                        method: 'POST',
                        data: d,
                        url: '/api/userHelper'
                    });
                },
                  getUsers: function () {
                    var d = "{requestType:'User'}"
                    return $http({
                        method: 'POST',
                        data: d,
                        url: '/api/userHelper'
                    });
                },
                updateComment: function (objComments) {
                    return $http({
                        method: 'Post',
                        url: '/api/IssueManagementCommentLog',
                        data: objComments
                    })
                },

                SubmitColumns: function (objColumnsData) {
                    return $http({
                        method: 'Post',
                        url: '/api/UserWiseColumnName',
                        data: objColumnsData
                    })
                },

                updateAnchorComment: function (objComments) {
                    return $http({
                        method: 'Post',
                        url: '/api/IssueManagementAnchorComment',
                        data: objComments
                    })
                },
                
                getStatus: function (query) {
                    return $http({
                        method: 'GET',
                        url: '/api/StatusMaster?q=' + query
                    });
                },
                getHodCount: function (SystemChapName, RoleName, Email) {
                    return $http({
                        method: 'GET',
                        contentType: "application/json",
                        url: '/Requisition/getHodwiseCount?SystemChapName=' + SystemChapName + '&RoleName=' + RoleName + '&Email=' + Email,
                        //data: UserName

                    })
                },
                getColumnDetails: function (UserId, SystemChapName) { 
                    return $http({
                        method: 'GET',
                        contentType: "application/json",
                        url: '/Requisition/getColumnDetails?UserId=' + UserId + '&SystemChapterName=' + SystemChapName,
                     })
                },
                getHodName: function (SystemChapName,RoleName,Email) {
                    return $http({
                        method: 'GET',
                        contentType: "application/json",
                        url: '/Requisition/getHodName?SystemChapterName=' + SystemChapName + '&RoleName=' + RoleName + '&Email=' + Email,
                        //data: UserName

                    })
                },
                getExcelforHODIssues: function (mData,ChapterName) {
                   // alert("1")
                    console.log(mData);
                    return $http({
                        method: 'PUT',
                        url: '/Requisition/getExcelReport',
                        data: mData
                    })
                },
            }
        }]);

