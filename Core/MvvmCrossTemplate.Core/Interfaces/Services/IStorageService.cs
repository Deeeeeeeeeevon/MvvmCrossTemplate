using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Interfaces.Utils;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Enums;

namespace MvvmCrossTemplate.Core.Interfaces.Services
{
    public interface IStorageService
    {
        #region Create File/Folder Methods

        /// <summary>
        /// Creates a file in the folder if there isn't one with the desiredName 
        /// else overrides the existing one with a new empty one
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="desiredName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<ILocalFile>> CreateOrOverrideFileAsync(ILocalFolder folder, string desiredName,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Creates a file in the folder if there isn't one with the desiredName 
        /// else fetches the existing one
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="desiredName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<ILocalFile>> CreateOrFetchFileAsync(ILocalFolder folder, string desiredName,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Creates a new subFolder in the folder if it doesn't exist or returns the found subFolder
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="desiredName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<ILocalFolder>> CreateOrFetchFolderAsync(ILocalFolder folder, string desiredName,
            CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region Read File/Folder Methods

        /// <summary>
        /// Reads the contents of the file as a string
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<Result<string>> ReadAllTextAsync(ILocalFile file);

        /// <summary>
        /// Gets a file in the folder
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<ILocalFile>> GetFileAsync(ILocalFolder folder, string file,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets a list of all files in the folder
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<List<ILocalFile>>> GetFilesAsync(ILocalFolder folder,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets subfolder in the folder
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="subFolderName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<ILocalFolder>> GetSubFolderAsync(ILocalFolder folder, string subFolderName,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Checks whether a file exists at the given location
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<bool>> CheckFileExistsAsync(ILocalFolder folder, string name,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Checks whether a folder or file exists at the given location
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<bool>> CheckFolderExistsAsync(ILocalFolder folder, string name,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Checks whether a path exists in the FileSystem
        /// </summary>
        /// <param name="path"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<bool>> CheckLocalPathExistsAsync(string path,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the Application's root folder
        /// </summary>
        /// <returns></returns>
        ILocalFolder GetRootFolder();

        #endregion

        #region Update File/Folder Methods

        /// <summary>
        /// Opens the file
        /// Performs a task asynchronously on the open FileStream
        /// Asynchronously clears all buffers for this stream, causes any buffered data to be written to the underlying device, and
        /// monitors cancellation requests
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileAccess"></param>
        /// <param name="toComplete"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The Result of the completed task</returns>
        Task<Result> OpenFileAsync(ILocalFile file, AccessRights fileAccess, Func<Stream, Task<Result>> toComplete,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Opens the file
        /// Performs a task asynchronously on the open FileStream
        /// Asynchronously clears all buffers for this stream, causes any buffered data to be written to the underlying device, and
        /// monitors cancellation requests
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileAccess"></param>
        /// <param name="toComplete"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The Result Value of the completed task</returns>
        Task<Result<T>> OpenFileAsync<T>(ILocalFile file, AccessRights fileAccess,
            Func<Stream, Task<Result<T>>> toComplete,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Renames a file without changing it's location
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <param name="option"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result> RenameAsync(ILocalFile file, string fileName,
            Collision option = Collision.FailIfExists,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Writes text to the file, overwriting any existing data
        /// </summary>
        /// <param name="file"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        Task<Result> WriteAllTextAsync(ILocalFile file, string contents);

        #endregion

        #region Delete File/Folder Methods

        /// <summary>
        /// Deletes the File
        /// </summary>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result> DeleteAsync(ILocalFile file, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes the folder and all it's contents
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result> DeleteAsync(ILocalFolder folder, CancellationToken cancellationToken = default(CancellationToken));

        #endregion
    }
}