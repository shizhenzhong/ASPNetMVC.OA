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
    
    public partial class trace_info
    {
        public trace_info()
        {
            this.trace_data = new HashSet<trace_data>();
        }
    
        public int trace_info_id { get; set; }
        public int source_id { get; set; }
        public int collection_item_id { get; set; }
        public Nullable<int> last_snapshot_id { get; set; }
        public Nullable<System.DateTime> start_time { get; set; }
        public Nullable<long> last_event_sequence { get; set; }
        public Nullable<bool> is_running { get; set; }
        public Nullable<long> event_count { get; set; }
        public Nullable<int> dropped_event_count { get; set; }
    
        public virtual snapshots_internal snapshots_internal { get; set; }
        public virtual source_info_internal source_info_internal { get; set; }
        public virtual ICollection<trace_data> trace_data { get; set; }
    }
}