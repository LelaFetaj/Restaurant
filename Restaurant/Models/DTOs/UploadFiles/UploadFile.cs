namespace Restaurant.API.Models.DTOs.UploadFiles
{
    public class UploadFile
    {
        public Guid Id { get; set; }

        public IFormFile File { get; set; }

        public string Name { get; set; }
    }
}
