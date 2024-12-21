public class CreationDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    // Image principale
    public IFormFile MainImage { get; set; } 
    
    // Images supplémentaires
    public List<IFormFile> AdditionalImages { get; set; } 

    // URLs des images
    public string PictureUrl { get; set; }
    public List<string> PictureUrls { get; set; }

    // PublicIds des images
    public string PicturePublicId { get; set; }
    public List<string> PicturePublicIds { get; set; }

    public CreationDTO()
    {
        PictureUrls = new List<string>();
        PicturePublicIds = new List<string>();
    }
}
