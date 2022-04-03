using System.IO;

namespace Image.Core
{
    /// <summary>
    ///     Interface for implementing metadata removers.
    /// </summary>
    public interface IMetadataRemover
    {
        /// <summary>
        ///     Cleans an image.
        /// </summary>
        void CleanImage();

        /// <summary>
        ///     Saves an image under a new file path.
        /// </summary>
        /// <param name="newFilePath">The file path to save the clean image.</param>
        void SaveImage(string newFilePath);

        /// <summary>
        ///     Saves the image.
        /// </summary>
        /// <param name="stream">The stream.</param>
        void SaveImage(Stream stream);

        /// <summary>
        ///     GetImagePath gets the current image path on the filesystem.
        /// </summary>
        /// <returns>A string representing the absolute path.</returns>
        string GetImagePath();
    }
}