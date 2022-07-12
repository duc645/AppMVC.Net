using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs68.models.Contacts {
    public class Contact {
        [Key]
        public int Id {get;set;}

        [Column(TypeName ="nvarchar")]
        [Required(ErrorMessage ="Phải nhập {0}")]
        [StringLength(50)]
        [Display(Name ="Họ tên")]
        public string FullName {set;get;}

        [Required(ErrorMessage ="Phải nhập {0}")]
        [StringLength(100)]
        // [EmailAddress(ErrorMessage ="Phải nhập đúng định dạng email")]
        [Display(Name ="Địa chỉ email")]
        public string Email {set;get;}

        //kieu datetime mac dinh not nul
        //neu muon null thi them dau ?
        public DateTime DateSent {set;get;}

        [Display(Name ="Nội dung")]
        public string Message {get;set;}

        [StringLength(50)]
        [Phone(ErrorMessage ="phải nhập đúng định dạng diện thoại")]
        [Display(Name ="Số điện thoại")]
        public string Phone {get;set;}
    }
    

}