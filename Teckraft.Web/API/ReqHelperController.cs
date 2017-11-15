using System;
using System.Web.Http;
using Teckraft.Core.Domian.Settings;
using Teckraft.Services;
using Teckraft.Web.Framework;

namespace Teckraft.Web.API
{
    public class ReqHelperController : ApiController
    {
            IService<Teckraft.Core.Domian.Settings.User> _userservice;
      //  private readonly IService<Requisition> ReqService;

        public ReqHelperController(IService<User> _userservice)
        {
          //  this.ReqService = ReqService;
            //this._mapper = _mapper;
            this._userservice = _userservice;
        }
        public Object Post(ApiRequestType requestType)
        {
            var curUser = _userservice.GetCurrentUser();
            if (requestType.RequestType == "currentUserDetail")
            {
                return _userservice.GetCurrentUser();
            }
            return null;
        }
         public class ApiRequestType {
          //  public List<QueryParameter> Parameters { get; set; }
            public string RequestType { get; set; }
        }

    }
}