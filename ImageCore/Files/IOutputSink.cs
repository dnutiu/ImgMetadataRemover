namespace Image.Files
{
    /// <summary>
    ///     IOutputSink is an interface for generating saving the generated files..
    /// </summary>
    public interface IOutputSink
    {
        /// <summary>
        ///     Generates an absolute output path given the initial absolute file path.
        /// </summary>
        /// <param name="initialFilePath">The initial file path.</param>
        /// <returns>The formatted absolute output path.</returns>
        string GetOutputPath(string initialFilePath);
    }
}