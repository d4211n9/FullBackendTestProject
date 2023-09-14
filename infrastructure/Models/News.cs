using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class News
{
    public int ArticleId { get; set; }
    
    [Required(ErrorMessage = "Headline is required.")]
    [StringLength(30, MinimumLength = 5, ErrorMessage = "Headline must be between 5 and 30 characters.")]
    public string Headline { get; set; }

    [Required(ErrorMessage = "Body is required.")]
    [MaxLength(1000, ErrorMessage = "Body can contain up to 1000 characters.")]
    public string Body { get; set; }

    [Required(ErrorMessage = "Author is required.")]
    [RegularExpression("^(Bob|Rob|Dob|Lob)$", ErrorMessage = "Invalid author.")]
    public string Author { get; set; }

    [Required(ErrorMessage = "Article image URL is required.")]
    public string ArticleImgUrl { get; set; }
}

public class NewsFeedItem
{
    public string Headline { get; set; }
    public int ArticleId { get; set; }
    public string ArticleImgUrl { get; set; }
    public string Body { get; set; }
}