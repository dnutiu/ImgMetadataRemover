namespace Image
{
    public class NullCompressor : ICompressor
    {
        public static readonly NullCompressor Instance = new NullCompressor();
        
        public void Compress(string fileName)
        {
        }
    }
}