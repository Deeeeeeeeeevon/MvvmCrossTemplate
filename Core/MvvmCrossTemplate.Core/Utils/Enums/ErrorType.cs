namespace MvvmCrossTemplate.Core.Utils.Enums
{
    public enum ErrorType
    {
        //Generic
        Unspecified,
        Cancelled,
        ServerError,
        PermissionDenied,
        ConvertingData,
        Crash,

        //Networking
        Offline,
        ConnectionCheckFailed,
        HostNotReachable,
        DownloadingData,
        UploadingData,
        ProcessingDownloadedData,

        //Database
        ConnectToDatabase,
        CreateDatabaseTable,
        SaveEntityToDatabase,
        QueryDatabase,
        ExecuteSql,
        DeleteFromDatabase,
        UpdateDatabase,
        DuplicateResults,
        NotFound,

        //File Storage
        FileExists,
        FileWrite,
        FileRead,
        FileDelete,
        FolderWrite,
        FolderRead,
        FolderDelete
    }
}