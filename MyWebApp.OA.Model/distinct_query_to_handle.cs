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
    
    public partial class distinct_query_to_handle
    {
        public long distinct_query_hash { get; set; }
        public byte[] sql_handle { get; set; }
        public int source_id { get; set; }
    
        public virtual source_info_internal source_info_internal { get; set; }
        public virtual distinct_queries distinct_queries { get; set; }
        public virtual notable_query_text notable_query_text { get; set; }
    }
}