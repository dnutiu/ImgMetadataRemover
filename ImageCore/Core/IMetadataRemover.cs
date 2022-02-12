namespace Image.Core
{
    /// <summary>
    ///     Interface for implementing metadata removers.
    /// </summary>
    public interface IMetadataRemover
    {
        /// <summary>
        ///     Cleans an image and saves it under a new path.
        /// </summary>
        /// <param name="newFilePath">The file path to save the clean image.</param>
        void CleanImage(string newFilePath);

        /// <summary>
        /// GetImagePath gets the current image path on the filesystem.
        /// </summary>
        /// <returns>A string representing the absolute path.</returns>
        string GetImagePath();
    }
}