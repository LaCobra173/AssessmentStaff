//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AssessmentStaff.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Answers
    {
        public int IDAnswer { get; set; }
        public int QuestionID { get; set; }
        public string Text { get; set; }
        public bool Result { get; set; }
    
        public virtual Question Question { get; set; }
    }
}