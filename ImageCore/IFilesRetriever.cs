using System.Collections.Generic;

namespace Image
{
    /// <summary>
    ///     An to interface enabling implementation of filename retrievers.
    /// </summary>
    public interface IFilesRetriever
    {
        /// <summary>
        ///     Returns all filenames from given path.
        /// </summary>
        /// <param name="directoryPath">The path.</param>
        /// <returns>An enumerable containing all file names.</returns>
        IEnumerable<string> GetFilenamesFromPath(string directoryPath);
    }
}