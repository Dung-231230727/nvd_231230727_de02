using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nvd_231230727_de02.Models;

public partial class NvdCatalog
{
    [Display(Name = "Mã")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int NvdId { get; set; }

    [Required]
    [Display(Name = "Tên danh mục")]
    public string? NvdCateName { get; set; }

    [Required]
    [Display(Name = "Giá bán")]
    [Range(100, 5000, ErrorMessage = "Giá phải từ 100 đến 5000")] // Yêu cầu Câu 5
    public decimal NvdCatePrice { get; set; }

    [Required]
    [Display(Name = "Số lượng")]
    public int NvdCateQty { get; set; }

    [Display(Name = "Hình ảnh")]
    [RegularExpression(@"\.(jpg|png|gif|tiff)$", ErrorMessage = "File ảnh phải có đuôi .jpg, .png, .gif, hoặc .tiff")] // Yêu cầu Câu 5
    public string? NvdPicture { get; set; }

    [Display(Name = "Trạng thái")]
    public bool NvdCateActive { get; set; }
}