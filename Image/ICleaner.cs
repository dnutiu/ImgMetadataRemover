namespace Image
{
    public interface ICleaner
    {
        void CleanImage(string newFileName);
        void OptimizeImage(string fileName);
        public void RemoveExifData(string newFilename);
    }
}