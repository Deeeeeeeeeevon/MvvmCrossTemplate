namespace MvvmCrossTemplate.Core.Interfaces.Utils
{
    public interface ILocalFile
    {
        /// <summary>
        /// The name of the file
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The "full path" of the file, which should uniquely identify
        /// </summary>
        string Path { get; }
    }
}