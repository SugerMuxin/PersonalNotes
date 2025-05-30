using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.Threading;
//mark:crj
//using ClassForDLL;
namespace  LTUnityPlugin {

/// <summary>
/// Zip Helper By Hxs1990 Use SharpZipLib.
/// For extract zip file.
///  Create 2015.06.20
///  Last Update 2015.06.22
/// </summary>

public class ZipHelperEvents {
    public delegate void ProcessExtractHandler(object sender, ProcessExtractArgs e);

    public class ProcessExtractArgs {
        bool completed;
        public bool Completed {
            get { return completed; }
        }

        long count, current;
        public long Current {
            get { return current; }
        }

        public long Count {
            get { return count; }
        }

        float progress;
        public float Progress {
            get { return progress; }
        }

        string file;
        public string File {
            get { return file; }
        }

        public ProcessExtractArgs(long _count, long _current, string _file) {
            count = _count;
            current =  _current;
            file = _file;
            progress = _count == 0 ? 0.0f : (float)_current / (float)_count;
            completed = (_current == _count);
        }
    }


    public ProcessExtractHandler ProcessExtract;
    public ProcessFileHandler ProcessFile;
    public CompletedFileHandler CompletedFile;
    public DirectoryFailureHandler DirectoryFailure;
    public FileFailureHandler FileFailure;

    public void OnProcessExtract(long count, long current, string file) {
        if(ProcessExtract != null) {
            ProcessExtractArgs args = new ProcessExtractArgs(count, current, file);
            ProcessExtract(this, args);
        }
    }

    public bool OnProcessFile(string file) {
        bool result = true;
        ProcessFileHandler handler = ProcessFile;

        if(handler != null) {
            ScanEventArgs args = new ScanEventArgs(file);
            handler(this, args);
            result = args.ContinueRunning;
        }
        return result;
    }

    public bool OnCompletedFile(string file) {
        bool result = true;
        CompletedFileHandler handler = CompletedFile;
        if(handler != null) {
            ScanEventArgs args = new ScanEventArgs(file);
            handler(this, args);
            result = args.ContinueRunning;
        }
        return result;
    }

    public bool OnDirectoryFailure(string directory, Exception e) {
        bool result = false;
        DirectoryFailureHandler handler = DirectoryFailure;

        if(handler != null) {
            ScanFailureEventArgs args = new ScanFailureEventArgs(directory, e);
            handler(this, args);
            result = args.ContinueRunning;
        }
        return result;
    }


    public bool OnFileFailure(string file, Exception e) {
        FileFailureHandler handler = FileFailure;
        bool result = (handler != null);

        if(result) {
            ScanFailureEventArgs args = new ScanFailureEventArgs(file, e);
            handler(this, args);
            result = args.ContinueRunning;
        }
        return result;
    }

}

public class ZipHelper {
    public enum Overwrite {
        Prompt,
        Never,
        Always
    }

    bool continueRunning;
    byte[] buffer;
    ZipFile zipFile;
    Overwrite overwrite;
    ConfirmOverwriteDelegate confirmDelegate;
    bool restoreDateTimeOnExtract;
    bool restoreAttributesOnExtract;
    bool createEmptyDirectories;
    ZipHelperEvents events;
    INameTransform extractNameTransform;

    public ZipHelper() {
    }


    public ZipHelper(ZipHelperEvents _events) {
        events = _events;
    }

    public bool CreateEmptyDirectories {
        get { return createEmptyDirectories; }
        set { createEmptyDirectories = value; }
    }

    public bool RestoreDateTimeOnExtract {
        get {
            return restoreDateTimeOnExtract;
        } set {
            restoreDateTimeOnExtract = value;
        }
    }

    public bool RestoreAttributesOnExtract {
        get { return restoreAttributesOnExtract; }
        set { restoreAttributesOnExtract = value; }
    }

    public delegate bool ConfirmOverwriteDelegate(string fileName);

    /// <summary>
    /// 解压ZIP文件
    /// </summary>
    /// <param name="_zipFileName">文件名</param>
    /// <param name="_targetDirectory">解压路径</param>
    public void ExtractZip(string _zipFileName, string _targetDirectory) {
        ExtractZip(_zipFileName, _targetDirectory, Overwrite.Always, null, true);
    }

    public void ExtractZip(string _zipFileName, string _targetDirectory,
                           Overwrite _overwrite, ConfirmOverwriteDelegate _confirmDelegate, bool _restoreDateTime) {
        Stream inputStream = File.Open(_zipFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        ExtractZip(_zipFileName, inputStream, _targetDirectory, _overwrite, _confirmDelegate, _restoreDateTime, true);
    }

    public void ExtractZip(string _zipFileName, Stream _inputStream, string _targetDirectory,
                           Overwrite _overwrite, ConfirmOverwriteDelegate _confirmDelegate,
                           bool _restoreDateTime, bool _isStreamOwner) {
        if((_overwrite == Overwrite.Prompt) && (_confirmDelegate == null)) {
            throw new ArgumentNullException("confirmDelegate");
        }
        ZipConstants.DefaultCodePage = 0;
        continueRunning = true;
        overwrite = _overwrite;
        confirmDelegate = _confirmDelegate;
        extractNameTransform = new WindowsNameTransform(_targetDirectory);
        restoreDateTimeOnExtract = _restoreDateTime;
        try {
            using(zipFile = new ZipFile(_inputStream)) {
                zipFile.IsStreamOwner = _isStreamOwner;
                System.Collections.IEnumerator enumerator = zipFile.GetEnumerator();
                long tmpIdx = 0;
                while(continueRunning && enumerator.MoveNext()) {
                    ZipEntry entry = (ZipEntry)enumerator.Current;
                    ExtractEntry(entry);
                    if(events != null) {
                        tmpIdx++;
                        events.OnProcessExtract(zipFile.Count, tmpIdx, entry.Name);
                    }
                }
            }
        } catch(System.Exception ex) {
            PluginLog.Err("Unzip File Error " + ex);
            if(events != null) {
                events.OnFileFailure(_zipFileName, ex);
            }
        }
    }

    void ExtractFileEntry(ZipEntry entry, string targetName) {
        bool proceed = true;
        if(overwrite != Overwrite.Always) {
            if(File.Exists(targetName)) {
                if((overwrite == Overwrite.Prompt) && (confirmDelegate != null)) {
                    proceed = confirmDelegate(targetName);
                } else {
                    proceed = false;
                }
            }
        }
        if(proceed) {
            if(events != null) {
                continueRunning = events.OnProcessFile(entry.Name);
            }
            if(continueRunning) {
                try {
                    using(FileStream outputStream = File.Create(targetName)) {
                        if(buffer == null) {
                            buffer = new byte[4096];
                        }
                        StreamUtils.Copy(zipFile.GetInputStream(entry), outputStream, buffer);
                        if(events != null) {
                            continueRunning = events.OnCompletedFile(entry.Name);
                        }
                    }
                    if(restoreDateTimeOnExtract) {
                        File.SetLastWriteTime(targetName, entry.DateTime);
                    }
                    if(RestoreAttributesOnExtract && entry.IsDOSEntry && (entry.ExternalFileAttributes != -1)) {
                        FileAttributes fileAttributes = (FileAttributes)entry.ExternalFileAttributes;
                        fileAttributes &= (FileAttributes.Archive | FileAttributes.Normal | FileAttributes.ReadOnly | FileAttributes.Hidden);
                        File.SetAttributes(targetName, fileAttributes);
                    }
                } catch(Exception ex) {
                    if(events != null) {
                        continueRunning = events.OnFileFailure(targetName, ex);
                    } else {
                        continueRunning = false;
                        throw;
                    }
                }
            }
        }
    }

    void ExtractEntry(ZipEntry entry) {
        bool doExtraction = entry.IsCompressionMethodSupported();
        string targetName = entry.Name;
        if(doExtraction) {
            if(entry.IsFile) {
                targetName = extractNameTransform.TransformFile(targetName);
            } else if(entry.IsDirectory) {
                targetName = extractNameTransform.TransformDirectory(targetName);
            }
            doExtraction = !((targetName == null) || (targetName.Length == 0));
        }
        string dirName = null;
        if(doExtraction) {
            if(entry.IsDirectory) {
                dirName = targetName;
            } else {
                dirName = Path.GetDirectoryName(Path.GetFullPath(targetName));
            }
        }
        if(doExtraction && !Directory.Exists(dirName)) {
            if(!entry.IsDirectory || CreateEmptyDirectories) {
                try {
                    Directory.CreateDirectory(dirName);
                } catch(Exception ex) {
                    doExtraction = false;
                    if(events != null) {
                        if(entry.IsDirectory) {
                            continueRunning = events.OnDirectoryFailure(targetName, ex);
                        } else {
                            continueRunning = events.OnFileFailure(targetName, ex);
                        }
                    } else {
                        continueRunning = false;
                        throw;
                    }
                }
            }
        }
        if(doExtraction && entry.IsFile) {
            ExtractFileEntry(entry, targetName);
        }
    }

    public static void UnzipFile(string src, string dst, ZipHelperEvents.ProcessExtractHandler proHdr, DirectoryFailureHandler dirFailHdr, FileFailureHandler ffailHdr) {
        ZipHelperEvents even = new ZipHelperEvents();
        even.ProcessExtract = proHdr;
        even.DirectoryFailure = dirFailHdr;
        even.FileFailure = ffailHdr;
        ZipHelper zipH = new ZipHelper(even);
        zipH.RestoreAttributesOnExtract = true;
        zipH.ExtractZip(src, dst);
    }

    public static void UnzipFileAsync(string src, string dst, ZipHelperEvents.ProcessExtractHandler proHdr, DirectoryFailureHandler dirFailHdr, FileFailureHandler ffailHdr) {
        new Thread(delegate() {
            UnzipFile(src, dst, proHdr, dirFailHdr, ffailHdr);
        }).Start();
    }

    public static byte[] ReadFormZIP(string zipFile, string path) {
        
//mark:crj        DebugUtil.Log(zipFile + " - " + path);
        ZipFile zipfile = new ZipFile(zipFile);
        ZipEntry item = zipfile.GetEntry(path);
        if(item == null) {
            return null;
        }
        byte[] datas = new byte[item.Size];
        Stream strm = zipfile.GetInputStream(item);
        int tmpsize = 1024 * 1024 * 3;
        byte[] tmpBuffer = new byte[tmpsize];
        bool reading = true;
        while(reading) {
            int bytesRead = strm.Read(tmpBuffer, 0, tmpsize);
            if(bytesRead > 0) {
                Array.Copy(tmpBuffer, datas, bytesRead);
            } else {
                reading = false;
            }
        }
        zipfile.Close();
        return datas;
    }
}

}
