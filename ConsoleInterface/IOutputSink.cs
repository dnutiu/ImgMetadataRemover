using Image.Core;

namespace Image
{
    /// <summary>
    ///     IOutputSink is an interface for generating saving the generated files..
    /// </summary>
    public interface IOutputSink
    {
        /// <summary>
        ///     Saves the image.
        /// </summary>
        /// <param name="metadataRemover">Metadata remover instance.</param>
        /// <returns>True if the image was saved successfully, false otherwise.</returns>
        bool Save(IMetadataRemover metadataRemover);
    }
}