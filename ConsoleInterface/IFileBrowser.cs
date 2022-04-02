using System.Collections.Generic;

namespace ConsoleInterface
{
    /// <summary>
    ///     An to interface enabling implementation of file browsers.
    /// </summary>
    public interface IFileBrowser
    {
        /// <summary>
        ///     Returns all filenames from given path.
        /// </summary>
        /// <param name="directoryPath">The path.</param>
        /// <returns>An enumerable containing all file names.</returns>
        IEnumerable<string> GetFilenamesFromPath(string directoryPath);
    }
}