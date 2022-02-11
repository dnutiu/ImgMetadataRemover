namespace Image.Core
{
    /// <summary>
    ///     Interface for implementing metadata removers.
    /// </summary>
    public interface IMetadataRemover
    {
        /// <summary>
        ///     CleanImage cleans an image and saves it..
        /// </summary>
        /// <param name="newFilePath">The file path to save the clean image.</param>
        void CleanImage(string newFilePath);
    }
}