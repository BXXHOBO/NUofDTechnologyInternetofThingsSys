//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SUC_DataEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class v_SUC_Courseware
    {
        public System.Guid CoursewareId { get; set; }
        public string CoursewareCode { get; set; }
        public string CoursewareName { get; set; }
        public string P_CoursewareCode { get; set; }
        public string UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string AuditOfficeCode { get; set; }
        public string AuditOfficeName { get; set; }
        public string IsWebPub { get; set; }
        public string Icon { get; set; }
        public Nullable<int> SortID { get; set; }
        public string CoursewarePath { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> Checked { get; set; }
        public int Disabled { get; set; }
        public string Remark { get; set; }
        public System.DateTime Rec_CreateTime { get; set; }
        public string Rec_CreateBy { get; set; }
        public Nullable<System.DateTime> Rec_ModifyTime { get; set; }
        public string Rec_ModifyBy { get; set; }
    }
}
