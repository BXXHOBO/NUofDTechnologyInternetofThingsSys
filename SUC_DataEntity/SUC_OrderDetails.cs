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
    
    public partial class SUC_OrderDetails
    {
        public System.Guid OrderDetailsId { get; set; }
        public System.Guid OrderId { get; set; }
        public Nullable<int> Statu { get; set; }
        public string image { get; set; }
        public int Disabled { get; set; }
        public string Remark { get; set; }
        public string CreateBy { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
    }
}
