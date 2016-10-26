using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Interfaces.Services;
using MvvmCrossTemplate.Core.Interfaces.Utils;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Enums;
using PCLStorage;

namespace MvvmCrossTemplate.Core.Services
{
    public class StorageService : IStorageService
    {
        private static IFileSystem FileSystem => PCLStorage.FileSystem.Current;

        #region Create File/Folder Methods

        /// <summary>
        /// Creates a file in the folder if there isn't one with the desiredName 
        /// else overrides the existing one with a new empty one
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="desiredName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<ILocalFile>> CreateOrOverrideFileAsync(ILocalFolder folder, string desiredName, CancellationToken cancellationToken = default(CancellationToken))
        {
            ILocalFile file;
            try
            {
                var folderToCreateIn = await FileSystem.GetFolderFromPathAsync(folder.Path, cancellationToken);
                if (folderToCreateIn == null) return Result.Fail<ILocalFile>(this, ErrorType.NotFound);
                var fileExists = await folderToCreateIn.CheckExistsAsync(desiredName, cancellationToken);
                if (fileExists == ExistenceCheckResult.FileExists)
                {
                    var foundFile = await folderToCreateIn.GetFileAsync(desiredName, cancellationToken);
                    file = new LocalFile(foundFile.Name, foundFile.Path);
                    return Result.Ok(file);
                }
                var iFile = await folderToCreateIn.CreateFileAsync(desiredName, CreationCollisionOption.ReplaceExisting, cancellationToken);
                file = new LocalFile(iFile.Name, iFile.Path);
            }
            catch (Exception e)
            {
                return Result.Fail<ILocalFile>(this, ErrorType.FileWrite, e);
            }
            return Result.Ok(file);
        }

        /// <summary>
        /// Creates a file in the folder if there isn't one with the desiredName 
        /// else fetches the existing one
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="desiredName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<ILocalFile>> CreateOrFetchFileAsync(ILocalFolder folder, string desiredName, CancellationToken cancellationToken = default(CancellationToken))
        {
            ILocalFile file;
            try
            {
                var folderToCreateIn = await FileSystem.GetFolderFromPathAsync(folder.Path, cancellationToken);
                if (folderToCreateIn == null) return Result.Fail<ILocalFile>(this, ErrorType.NotFound);
                var fileExists = await folderToCreateIn.CheckExistsAsync(desiredName, cancellationToken);
                if (fileExists == ExistenceCheckResult.FileExists)
                {
                    var foundFile = await folderToCreateIn.GetFileAsync(desiredName, cancellationToken);
                    file = new LocalFile(foundFile.Name, foundFile.Path);
                    return Result.Ok(file);
                }
                var iFile = await folderToCreateIn.CreateFileAsync(desiredName, CreationCollisionOption.FailIfExists, cancellationToken);
                file = new LocalFile(iFile.Name, iFile.Path);
            }
            catch (Exception e)
            {
                return Result.Fail<ILocalFile>(this, ErrorType.FileWrite, e);
            }
            return Result.Ok(file);
        }

        /// <summary>
        /// Creates a new subFolder in the folder if it doesn't exist or returns the found subFolder
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="desiredName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<ILocalFolder>> CreateOrFetchFolderAsync(ILocalFolder folder, string desiredName, CancellationToken cancellationToken = default(CancellationToken))
        {
            ILocalFolder subFolder;
            try
            {
                var folderToCreateIn = await FileSystem.GetFolderFromPathAsync(folder.Path, cancellationToken);
                if (folderToCreateIn == null) return Result.Fail<ILocalFolder>(this, ErrorType.NotFound);
                var folderExists = await folderToCreateIn.CheckExistsAsync(desiredName, cancellationToken);
                if (folderExists == ExistenceCheckResult.FolderExists)
                {
                    var foundFolder = await folderToCreateIn.GetFolderAsync(desiredName, cancellationToken);
                    subFolder = new LocalFolder(foundFolder.Name, foundFolder.Path);
                    return Result.Ok(subFolder);
                }
                var iSubFolder = await folderToCreateIn.CreateFolderAsync(desiredName, CreationCollisionOption.FailIfExists, cancellationToken);
                subFolder = new LocalFolder(iSubFolder.Name, iSubFolder.Path);
            }
            catch (Exception e)
            {
                return Result.Fail<ILocalFolder>(this, ErrorType.FolderWrite, e);
            }
            return Result.Ok(subFolder);
        }
        #endregion

        #region Read File/Folder Methods
        /// <summary>
        /// Reads the contents of the file as a string
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<Result<string>> ReadAllTextAsync(ILocalFile file)
        {
            string data;
            try
            {
                var fileToRead = await FileSystem.GetFileFromPathAsync(file.Path);
                if (fileToRead == null) return Result.Fail<string>(this, ErrorType.NotFound);
                data = await fileToRead.ReadAllTextAsync();
            }
            catch (Exception e)
            {
                return Result.Fail<string>(this, ErrorType.FileRead, e);
            }
            return Result.Ok(data);
        }
        /// <summary>
        /// Gets a file in the folder
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<ILocalFile>> GetFileAsync(ILocalFolder folder, string file, CancellationToken cancellationToken = default(CancellationToken))
        {
            ILocalFile value;
            try
            {
                var folderToSearch = await FileSystem.GetFolderFromPathAsync(folder.Path, cancellationToken);
                if (folderToSearch == null) return Result.Fail<ILocalFile>(this, ErrorType.NotFound);
                var iFolder = await folderToSearch.GetFileAsync(file, cancellationToken);
                value = new LocalFile(iFolder.Name, iFolder.Path);
            }
            catch (Exception e)
            {
                return Result.Fail<ILocalFile>(this, ErrorType.FolderRead, e);
            }
            return Result.Ok(value);
        }
        /// <summary>
        /// Gets a list of all files in the folder
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<List<ILocalFile>>> GetFilesAsync(ILocalFolder folder, CancellationToken cancellationToken = default(CancellationToken))
        {
            List<ILocalFile> value = new List<ILocalFile>();
            try
            {
                var folderToSearch = await FileSystem.GetFolderFromPathAsync(folder.Path, cancellationToken);
                if (folderToSearch == null) return Result.Fail<List<ILocalFile>>(this, ErrorType.NotFound);
                var iFolderList = await folderToSearch.GetFilesAsync(cancellationToken);
                value.AddRange(iFolderList.Select(item => new LocalFile(item.Name, item.Path)).ToList());
            }
            catch (Exception e)
            {
                return Result.Fail<List<ILocalFile>>(this, ErrorType.FolderRead, e);
            }
            return Result.Ok(value);
        }
        /// <summary>
        /// Gets subfolder in the folder
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="subFolderName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<ILocalFolder>> GetSubFolderAsync(ILocalFolder folder, string subFolderName,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            ILocalFolder subFolder;
            try
            {
                var folderToSearch = await FileSystem.GetFolderFromPathAsync(folder.Path, cancellationToken);
                if (folderToSearch == null) return Result.Fail<ILocalFolder>(this, ErrorType.NotFound);
                var iSubFolder = await folderToSearch.GetFolderAsync(subFolderName, cancellationToken);
                subFolder = new LocalFolder(iSubFolder.Name, iSubFolder.Path);
            }
            catch (Exception e)
            {
                return Result.Fail<ILocalFolder>(this, ErrorType.NotFound, e);
            }
            return Result.Ok(subFolder);
        }

        /// <summary>
        /// Checks whether a file exists at the given location
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<bool>> CheckFileExistsAsync(ILocalFolder folder, string name,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            bool checkResult;
            try
            {
                var folderToSearch = await FileSystem.GetFolderFromPathAsync(folder.Path, cancellationToken);
                if (folderToSearch == null) return Result.Fail<bool>(this, ErrorType.NotFound);
                checkResult = await folderToSearch.CheckExistsAsync(name, cancellationToken) == ExistenceCheckResult.FileExists;
            }
            catch (Exception e)
            {
                return Result.Fail<bool>(this, ErrorType.NotFound, e);
            }
            return Result.Ok(checkResult);
        }
        /// <summary>
        /// Checks whether a folder exists at the given location
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<bool>> CheckFolderExistsAsync(ILocalFolder folder, string name,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            bool checkResult;
            try
            {
                var folderToSearch = await FileSystem.GetFolderFromPathAsync(folder.Path, cancellationToken);
                if (folderToSearch == null) return Result.Fail<bool>(this, ErrorType.NotFound);
                checkResult = await folderToSearch.CheckExistsAsync(name, cancellationToken) == ExistenceCheckResult.FolderExists;
            }
            catch (Exception e)
            {
                return Result.Fail<bool>(this, ErrorType.NotFound, e);
            }
            return Result.Ok(checkResult);
        }
        /// <summary>
        /// Checks whether a path exists in the FileSystem
        /// </summary>
        /// <param name="path"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<bool>> CheckLocalPathExistsAsync(string path,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            bool checkResult;
            try
            {
                checkResult = await FileSystem.LocalStorage.CheckExistsAsync(path, cancellationToken) != ExistenceCheckResult.NotFound;
            }
            catch (Exception e)
            {
                return Result.Fail<bool>(this, ErrorType.NotFound, e);
            }
            return Result.Ok(checkResult);
        }
        /// <summary>
        /// Gets Application root folder
        /// </summary>
        /// <returns></returns>
        public ILocalFolder GetRootFolder()
        {
            return GetApplicationRootFolder();
        }
        /// <summary>
        /// Gets Application root folder
        /// </summary>
        /// <returns></returns>
        public static ILocalFolder GetApplicationRootFolder()
        {
            var folder = FileSystem.LocalStorage;
            return new LocalFolder(folder.Name, folder.Path);
        }
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
        public async Task<Result> OpenFileAsync(ILocalFile file, AccessRights fileAccess, Func<Stream, Task<Result>> toComplete,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Result result = Result.Ok();
            try
            {
                var fileToOpen = await FileSystem.GetFileFromPathAsync(file.Path, cancellationToken);
                if (fileToOpen == null) return Result.Fail(this, ErrorType.NotFound);
                using (Stream fileStream = await fileToOpen.OpenAsync((FileAccess)fileAccess, cancellationToken))
                {
                    result = await toComplete(fileStream);
                    await fileStream.FlushAsync(cancellationToken);
                }
            }
            catch (Exception e)
            {
                return result.IsSuccess ? Result.Fail(this, ErrorType.FileWrite, e) : Result.Fail(this, result);
            }
            return result.IsSuccess ? Result.Ok() : result;
        }
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
        public async Task<Result<T>> OpenFileAsync<T>(ILocalFile file, AccessRights fileAccess, Func<Stream, Task<Result<T>>> toComplete,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Result<T> result = Result.Ok(default(T));
            try
            {
                var fileToOpen = await FileSystem.GetFileFromPathAsync(file.Path, cancellationToken);
                if (fileToOpen == null) return Result.Fail<T>(this, ErrorType.NotFound);

                using (Stream fileStream = await fileToOpen.OpenAsync((FileAccess)fileAccess, cancellationToken))
                {
                    result = await toComplete(fileStream);
                    await fileStream.FlushAsync(cancellationToken);
                }
            }
            catch (Exception e)
            {
                return result.IsSuccess ? Result.Fail<T>(this, ErrorType.FileWrite, e) : Result.Fail<T>(this, result);
            }
            return result.IsSuccess ? result : Result.Fail<T>(this, result);
        }
        /// <summary>
        /// Renames a file without changing it's location
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <param name="option"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result> RenameAsync(ILocalFile file, string fileName,
            Collision option = Collision.FailIfExists,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var fileToRename = await FileSystem.GetFileFromPathAsync(file.Path, cancellationToken);
                if (fileToRename == null) return Result.Fail(this, ErrorType.NotFound);
                await fileToRename.RenameAsync(fileName, (NameCollisionOption)option, cancellationToken);
            }
            catch (Exception e)
            {
                return Result.Fail(this, ErrorType.FileWrite, e);
            }
            return Result.Ok();
        }
        /// <summary>
        /// Writes text to the file, overwriting any existing data
        /// </summary>
        /// <param name="file"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        public async Task<Result> WriteAllTextAsync(ILocalFile file, string contents)
        {
            try
            {
                var fileToWrite = await FileSystem.GetFileFromPathAsync(file.Path);
                if (fileToWrite == null) return Result.Fail(this, ErrorType.NotFound);
                await fileToWrite.WriteAllTextAsync(contents);
            }
            catch (Exception e)
            {
                return Result.Fail(this, ErrorType.FileWrite, e);
            }
            return Result.Ok();
        }
        #endregion

        #region Delete File/Folder Methods
        /// <summary>
        /// Deletes the File
        /// </summary>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result> DeleteAsync(ILocalFile file, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var fileToDelete = await FileSystem.GetFileFromPathAsync(file.Path, cancellationToken);
                if (fileToDelete == null) return Result.Fail(this, ErrorType.NotFound);
                await fileToDelete.DeleteAsync(cancellationToken);
            }
            catch (Exception e)
            {
                return Result.Fail(this, ErrorType.FileDelete, e);
            }
            return Result.Ok();
        }
        /// <summary>
        /// Deletes the folder and all it's contents
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result> DeleteAsync(ILocalFolder folder, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var folderToDelete = await FileSystem.GetFolderFromPathAsync(folder.Path, cancellationToken);
                if (folderToDelete == null) return Result.Fail(this, ErrorType.NotFound);
                await folderToDelete.DeleteAsync(cancellationToken);
            }
            catch (Exception e)
            {
                return Result.Fail(this, ErrorType.FolderDelete, e);
            }
            return Result.Ok();
        }
        #endregion

    }
}