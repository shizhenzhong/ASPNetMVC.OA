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
    
    public partial class performance_counter_report_group_items
    {
        public int counter_group_item_id { get; set; }
        public string counter_group_id { get; set; }
        public string counter_subgroup_id { get; set; }
        public string series_name { get; set; }
        public string object_name { get; set; }
        public bool object_name_wildcards { get; set; }
        public string counter_name { get; set; }
        public string instance_name { get; set; }
        public string not_instance_name { get; set; }
        public decimal multiply_by { get; set; }
        public bool divide_by_cpu_count { get; set; }
    }
}