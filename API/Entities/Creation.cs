public class Creation
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    
    // Image principale
    public string PictureUrl { get; set; }
    public string PicturePublicId { get; set; }
    
    // Images supplémentaires
    public List<string> PictureUrls { get; set; }
    public List<string> PicturePublicIds { get; set; }

    public Creation()
    {
        PictureUrls = new List<string>();  // Initialisation des images supplémentaires
        PicturePublicIds = new List<string>();  // Initialisation des PublicIds des images supplémentaires
    }
}
