using System.ComponentModel.DataAnnotations;

namespace MainBit.MarkupTags.ViewModels
{
    public class MarkupTagViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public bool Enable { get; set; }
    }
}