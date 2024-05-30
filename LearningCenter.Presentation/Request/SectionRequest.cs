using Microsoft.Build.Framework;

namespace _1_API.Request;

public class SectionRequest
{
    [Required] public string Title { get; set; }
}