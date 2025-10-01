namespace CoworkingReservation.Data
{
    public class ImageCopy
    {
        private string _imagePath { get; set; }

        public ImageCopy(IConfiguration configuration)
        {
            _imagePath = configuration["ImagePath"];
            
        }

        public async Task<string> SavePhoto(IFormFile fileInfo)
        {
            string? path = null;

            if(fileInfo.FileName != null && _imagePath != null)
            {
                path = Path.Combine(_imagePath, $"{Guid.NewGuid()}.png");

                using(FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await fileInfo.CopyToAsync(stream);
                };
            }
            else { throw new Exception(); }

            return path;
        }
    }
}
