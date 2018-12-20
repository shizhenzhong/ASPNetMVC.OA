//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyWebApp.OA.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ActionInfo
    {
        public ActionInfo()
        {
            this.R_UserInfo_ActionInfo = new HashSet<R_UserInfo_ActionInfo>();
            this.Department = new HashSet<Department>();
            this.RoleInfo = new HashSet<RoleInfo>();
        }
    
        public int ID { get; set; }
        public System.DateTime SubTime { get; set; }
        public short DelFlag { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string Remark { get; set; }
        public string Url { get; set; }
        public string HttpMethod { get; set; }
        public string ActionMethodName { get; set; }
        public string ControllerName { get; set; }
        public string ActionInfoName { get; set; }
        public string Sort { get; set; }
        public short ActionTypeEnum { get; set; }
        public string MenuIcon { get; set; }
        public int IconWidth { get; set; }
        public int IconHeight { get; set; }
    
        public virtual ICollection<R_UserInfo_ActionInfo> R_UserInfo_ActionInfo { get; set; }
        public virtual ICollection<Department> Department { get; set; }
        public virtual ICollection<RoleInfo> RoleInfo { get; set; }
    }
}
