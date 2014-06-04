using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;

namespace MainBit.MarkupTags.Models
{
    public class MarkupTagRecord
    {
        public virtual int Id { get; set; }
        [Required]
        public virtual string Title { get; set; }
        [Required]
        public virtual string Content { get; set; }
        [Required]
        public virtual string Zone { get; set; }
        [Required]
        public virtual string Position { get; set; }
        [Required]
        public virtual bool Enable { get; set; }
    }
}