using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;


public class TempFileInfo
{
    public string[] lines;
    public string fileName;
}
public class TempStyleFileInfo
{
    public FileType fileType { get; set; }

    public List<TempFileInfo> TempFiles { get; set; } = new List<TempFileInfo>();

    public void Add(TempFileInfo file) {
        if (file != null) {
            TempFiles.Add(file);
        }
    }

}

public class FileSearchInfo {
    public FileType SearchOption;  //文件类型//
    public string extension;     //拓展名称//
    public string NotFoundTip;   //找不到的提示//
    public string FileTypeName;    
}


public class EditorSerachToolsManager 
{
    private Dictionary<FileType, string> SearchFolds = new Dictionary<FileType, string>();  //搜索源配置//
    private Dictionary<FileType, string> extensionDict = new Dictionary<FileType, string>() {
        { FileType.Code,"*.cs"},
        { FileType.Asset,"*.asset"},
        { FileType.Json,"*.json"},
        { FileType.Xml,"*.xml"},
        { FileType.Prefab,"*.prefab"},
        { FileType.Material,"*.mat"},
        { FileType.ImagePng,"*.png"},
        { FileType.ImageJpg,"*.jpg"},
    };
    private Dictionary<FileType, string> tipDict = new Dictionary<FileType, string>() {
        { FileType.Code,"Not Found In Code!"},
        { FileType.Asset,"Not Found In Asset!"},
        { FileType.Json,"Not Found In Json!"},
        { FileType.Xml,"Not Found In XML!"},
        { FileType.Prefab,"Not Found In Prefab!"},
        { FileType.Material,"Not Found In Material!"},
    };
    private string[] ignoreFiles = new string[] {
        "StringContent",
        "WorldMapDragon",   /*服务器表*/
        "world"             /*服务器表*/
    };
    public Dictionary<FileType, TempStyleFileInfo> AllTempFiles = new Dictionary<FileType, TempStyleFileInfo>();
    //List<string> AllFiles = new List<string>(8192 * 4);    //保存所有的文件名称//
    public bool TempComplete { get; private set; }
    private static EditorSerachToolsManager instance;
    public static EditorSerachToolsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EditorSerachToolsManager();
                instance.Initialize();
            }
            return instance;
        }
    }

    private void Initialize() {
        SearchFolds = new Dictionary<FileType, string>() {
                { FileType.Code,Application.dataPath},
                { FileType.Json,Application.dataPath+"/AssetBundles/DayZ/PlatformDefault/Default"},
                { FileType.Asset,Application.dataPath+"/AssetBundles/DayZ/PlatformDefault/Default"},
                { FileType.Xml,Application.dataPath+"/ServerConfig"},
                { FileType.Prefab,Application.dataPath+"/AssetBundles"},
                { FileType.Material,Application.dataPath+"/AssetBundles"},
        };
    }

    public bool CheckConfigCorrect(FileType option) {
        if (option == FileType.All)
        {
            bool Correct = true;
            for (int i = 0; i < (int)FileType.All; i++)
            {
                Correct = CheckConfigCorrect((FileType)i);
                if (!Correct) return Correct;
            }
            return Correct;
        }
        else {
            return Directory.Exists(SearchFolds[option]);
        }
    }


    public bool Contain(FileType option)
    {
        return AllTempFiles.ContainsKey(option);
    }

    public void Clear(FileType option)
    {
        AllTempFiles.Remove(option);
    }

    public void Clear()
    {
        AllTempFiles.Clear();
        AllSearchedFolds.Clear();
    }

    public void InitAll()
    {
        var iter = SearchFolds.GetEnumerator();
        while (iter.MoveNext()) {
            Init(iter.Current.Key);
        }
    }

    float progress = 0;
    int count = 0;
    public void Init(FileType option)
    {
        if (option == FileType.All)
        {
            InitAll();
        }
        else {
            if (!Contain(option))
            {
                TempComplete = false;
                count = 0;
                progress = 0;
                TempStyleFileInfo tempStyle = new TempStyleFileInfo();
                tempStyle.fileType = (FileType)(int)option;
                string extension = extensionDict[option];
                string searchPath = SearchFolds[option];
                if (Directory.Exists(searchPath))
                {
                    string[] extFiles = Directory.GetFiles(searchPath, extension, SearchOption.AllDirectories);
                    for (int i = 0; i < extFiles.Length; i++)
                    {
                        tempStyle.Add(ReadFile(extFiles[i]));
                        count++;
                        progress = (float)count / extFiles.Length;
                        EditorUtility.DisplayProgressBar("Wait", string.Format("正在缓存{0}s...", option.ToString()), progress);
                    }
                }
                else
                {
                    Debug.LogError("你尚未配置路径,搜索结果可能不准确！  " + searchPath);
                }
                AllTempFiles.Add(option, tempStyle);
                EditorUtility.ClearProgressBar();
                TempComplete = true;
            }
        }
    }

    private TempFileInfo ReadFile(string filePath) {
        if (CheckIgnore(filePath))
            return null;
        else {
            TempFileInfo tempFile = new TempFileInfo();
            tempFile.lines = File.ReadAllLines(filePath);
            tempFile.fileName  = Path.GetFileNameWithoutExtension(filePath);
            return tempFile;
        }
    }

    private bool CheckIgnore(string fileName)
    {
        foreach (var item in ignoreFiles)
        {
            if (fileName.Contains(item))
            {
                return true;
            }
        }
        return false;
    }

    public List<SearchResult> Search(string str, FileType option) {
        Init(option);
        List<SearchResult> results = new List<SearchResult>();
        if (option == FileType.All)
        {
            results = SearchAll(str);
            return results;
        }
        else {
            int lineCount = 0;
            count = 0;
            TempStyleFileInfo StyleFile = AllTempFiles[option];
            TempFileInfo tempInfo = null;
            for (int i = 0; i < StyleFile.TempFiles.Count; i++)
            {
                tempInfo = StyleFile.TempFiles[i];
                SearchResult result = null;
                lineCount = 0;
                if (option == FileType.Prefab && tempInfo.fileName == str)
                {
                    continue;
                }
                string[] lines = tempInfo.lines;
                foreach (string line in lines) {
                    lineCount++;
                    if (SearchWord(str, line)) {
                        if (result == null)
                        {
                            result = new SearchResult();
                            result.ScriptName = StyleFile.TempFiles[i].fileName;
                            result.FileName = StyleFile.TempFiles[i].fileName;
                            result.fileType = StyleFile.fileType;
                        }
                        result.Lines.Add(string.Format("{0}-{1}", lineCount, line));     
                    }
                }
                if (result != null)
                {
                    results.Add(result);
                }
                count++;
                progress = (float)count / StyleFile.TempFiles.Count;
                EditorUtility.DisplayProgressBar(string.Format("Searching form {0}s ...", option.ToString()), tempInfo.fileName, progress);
            }
            EditorUtility.ClearProgressBar();
            if (results.Count == 0)
            {
                Searched = false;
                SearchResult defau = new SearchResult();
                defau.ScriptName = tipDict[option];
                results.Add(defau);
            }
            else {
                Searched = true;
            }
            return results;
        }
        
    }

    private bool Searched = false; //是否查询到//

    private List<SearchResult> SearchAll(string str) {
        List<SearchResult> results = new List<SearchResult>();
        var iter = SearchFolds.GetEnumerator();
        while (iter.MoveNext())
        {
            List<SearchResult> temp =Search(str, iter.Current.Key);
            if (Searched) {
                results.AddRange(temp);
            }
        }
        if (results.Count == 0)
        {
            Searched = false;
            SearchResult defau = new SearchResult();
            defau.ScriptName = "Not found in all Files!";
            results.Add(defau);
        }
        return results;
    }

    private Dictionary<string, List<AssetInfoRef>> AllSearchedFolds = new Dictionary<string, List<AssetInfoRef>>(); //缓存所有搜索信息//
    /// <summary>
    /// 查找一个文件加下所有文件在项目中的使用情况（根据文件名称查找）///
    /// </summary>
    /// <param name="foldPath">文件夹目录</param>
    /// <param name="option">搜索文件夹下哪一种类型的文件</param>
    /// <param name="searchOption">在哪些类型的文件中搜索</param>
    /// <returns></returns>
    public List<AssetInfoRef> SearchFoldByChildsName(string foldPath, FileType option, FileType searchOption) {
        FileType sourceOption = option;
        string key = foldPath + sourceOption;
        if (sourceOption == FileType.None) {
            return new List<AssetInfoRef>();
        }
        if (sourceOption == FileType.All)
        {
            List<AssetInfoRef> allRef = new List<AssetInfoRef>();
            for (int i = 0; i < (int)FileType.All; i++)
            {
                sourceOption = (FileType)i;
                allRef.AddRange(SearchFoldByChildsName(foldPath, sourceOption, searchOption));
            }
            key = foldPath + FileType.All;
            AllSearchedFolds.Add(key, allRef);
        }
        else {
            progress = 0;
            int count = 0;
            Init(searchOption);
            if (!AllSearchedFolds.ContainsKey(key))
            {
                if (sourceOption == FileType.None || sourceOption == FileType.All)
                {
                    return SearchFoldAll(foldPath, searchOption);
                }
                else
                {
                    List<AssetInfoRef> foldList = new List<AssetInfoRef>();
                    string[] assets = Directory.GetFiles(foldPath, extensionDict[sourceOption], SearchOption.AllDirectories);
                    Debug.Log("All need be search files count :" + assets.Length);
                    foreach (string asset in assets)
                    {
                        AssetInfoRef info = new AssetInfoRef();
                        string fileName = Path.GetFileNameWithoutExtension(asset);
                        bool isUsed = IsThePrefabUsedInCode(fileName, searchOption);
                        info.Name = asset;
                        info.SearchName = fileName;
                        if (isUsed)
                        {
                            info.RefCount = info.RefCount + 1;
                        }
                        foldList.Add(info);
                        count++;
                        progress = (float)count / assets.Length;
                        EditorUtility.DisplayProgressBar("Searching", fileName, progress);
                    }
                    AllSearchedFolds.Add(key, foldList);
                    EditorUtility.ClearProgressBar();
                }
            }
        }
        return AllSearchedFolds[key];
    }

    public void ClearSearchRecords(string foldPath) {
        AllSearchedFolds.Remove(foldPath);
    }

    private List<AssetInfoRef> SearchFoldAll(string foldPath, FileType searchOption) {
        List<AssetInfoRef> results = new List<AssetInfoRef>();
        foreach (var item in extensionDict)
        {
            results.AddRange(SearchFoldByChildsName(foldPath, item.Key, searchOption));
        }
        return results;
    }

    /// <summary>
    /// 查找某个对象在资源中的引用//
    /// </summary>
    /// <param name="str">查找对象</param>
    /// <param name="searchOption">查找集合的类型</param>
    /// <returns></returns>
    bool IsThePrefabUsedInCode(string str, FileType searchOption)
    {
        if (searchOption == FileType.All)
        {
            /*查找所有目录*/
            bool searched = false;
            var iter = AllTempFiles.GetEnumerator();
            while (iter.MoveNext()) {
                searched = IsThePrefabUsedInCode(str, iter.Current.Key);
                if (searched)
                    break;
            }
            return searched;
        }
        else {
            Init(searchOption);
            var iter = AllTempFiles[searchOption].TempFiles.GetEnumerator();
            while (iter.MoveNext())
            {
                if (searchOption == FileType.Prefab) {
                    if (str == iter.Current.fileName)
                    {
                        continue;
                    }
                }
                string[] lines = iter.Current.lines;
                foreach (string line in lines)
                {
                    if (SearchWord(str,line))
                    {
                        Debug.LogError(str+" -- "+iter.Current.fileName + ":" + line);
                        return true;
                    }
                }
            }
            return false;
        }

    }

    public SearchStyle searchStyle { get; set; } = SearchStyle.Normal;
    bool SearchWord(string word, string line)
    {
        if (searchStyle == SearchStyle.Normal)
            return line.Contains(word);
        else
            return Regex.IsMatch(line, @"\b" + Regex.Escape(word) + @"\b");
    }

    public void SetSearchStyle(SearchStyle style) {
        searchStyle = style;
    }

}
