using MvvmCrossTemplate.Core.Interfaces.Utils;

namespace MvvmCrossTemplate.Core.Utils
{
    public class LocalFile : ILocalFile
    {
        /// <summary>
        /// The name of the file
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// The "full path" of the file, which should uniquely identify
        /// </summary>
        public string Path { get; }
        public LocalFile(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}