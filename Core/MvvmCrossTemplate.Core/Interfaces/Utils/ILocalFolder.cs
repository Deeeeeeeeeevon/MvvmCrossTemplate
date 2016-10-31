namespace MvvmCrossTemplate.Core.Interfaces.Utils
{
    public interface ILocalFolder
    {
        /// <summary>
        /// The name of the folder
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The "full path" of the folder, which should uniquely identify it
        /// </summary>
        string Path { get; }
    }
}