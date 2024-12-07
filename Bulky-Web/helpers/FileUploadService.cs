public class FileUploadService
{
	private readonly IWebHostEnvironment _webHostEnvironment;

	public FileUploadService(IWebHostEnvironment webHostEnvironment)
	{
		_webHostEnvironment = webHostEnvironment;
	}

	public string UploadImage(IFormFile file, string subFolderName)
	{
		if (file == null || file.Length == 0)
		{
			throw new ArgumentException("File is invalid.");
		}

		string webRootPath = _webHostEnvironment.WebRootPath;
		string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
		string folderPath = Path.Combine(webRootPath, "images", subFolderName);

		// Ensure the directory exists
		if (!Directory.Exists(folderPath))
		{
			Directory.CreateDirectory(folderPath);
		}

		// Combine full path for the file
		string filePath = Path.Combine(folderPath, fileName);

		// Save the file
		using (var stream = new FileStream(filePath, FileMode.Create))
		{
			file.CopyTo(stream);
		}

		// Return the relative path to the saved file
		return Path.Combine("images", subFolderName, fileName).Replace("\\", "/");
	}

    public void RemoveImage(string imageUrl)
    {
        string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, imageUrl.TrimStart('\\'));

        // Check if the file exists and delete it
        if (System.IO.File.Exists(imagePath))
        {
            System.IO.File.Delete(imagePath);
        }
    }
}
