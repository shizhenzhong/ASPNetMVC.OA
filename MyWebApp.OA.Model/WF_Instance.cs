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
    
    public partial class WF_Instance
    {
        public WF_Instance()
        {
            this.WF_StepInfo = new HashSet<WF_StepInfo>();
        }
    
        public int ID { get; set; }
        public string InstanceName { get; set; }
        public System.DateTime SubTime { get; set; }
        public int StartedBy { get; set; }
        public short Level { get; set; }
        public string SubForm { get; set; }
        public short Status { get; set; }
        public short Result { get; set; }
        public int WF_TempID { get; set; }
        public System.Guid ApplicationId { get; set; }
    
        public virtual ICollection<WF_StepInfo> WF_StepInfo { get; set; }
        public virtual WF_Temp WF_Temp { get; set; }
    }
}