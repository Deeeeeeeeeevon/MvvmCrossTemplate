using MvvmCrossTemplate.Core.Interfaces.Utils;

namespace MvvmCrossTemplate.Core.Utils
{
    public class LocalFolder : ILocalFolder
    {
        /// <summary>
        /// The name of the folder
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// The "full path" of the folder, which should uniquely identify it
        /// </summary>
        public string Path { get; }
        public LocalFolder(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}