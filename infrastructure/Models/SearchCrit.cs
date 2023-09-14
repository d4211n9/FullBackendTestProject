using System.ComponentModel.DataAnnotations;

namespace infrastructure.DataModels;

public class SearchCrit
{
    [MinLength( 3, ErrorMessage = "Headline must be.")]
    public string SearchTerm { get; set; }
    
    public int PageSize { get; set; }
}