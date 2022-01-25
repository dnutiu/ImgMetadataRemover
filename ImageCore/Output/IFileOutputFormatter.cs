namespace Image.Output
{
    /// <summary>
    ///     IOutputFormatter is an interface for generating the output path and destination file name.
    /// </summary>
    public interface IFileOutputFormatter
    {
        /// <summary>
        ///     Generates an absolute output path given the initial absolute file path.
        /// </summary>
        /// <param name="initialFilePath">The initial file path.</param>
        /// <returns>The formatted absolute output path.</returns>
        string GetOutputPath(string initialFilePath);
    }
}