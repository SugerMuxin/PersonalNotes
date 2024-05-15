using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public enum SearchStyle {
    Normal,
    Strict
}

public enum ComStyle {
    Default,
    Red,
    Green,
    Purple,
    Sky,
    Brown,
    Background1,
}

public enum FileType {
    Code,
    Json,
    Asset,
    Xml,
    Prefab,
    Material,
    ImagePng,
    ImageJpg,
    All,
    None = 64
}



public class CommonGUIStyle {
    public ComStyle Style { get; private set; }
    public Color BackGroundColor { get; set; }

    public Color Color { get; set; }

    public CommonGUIStyle(ComStyle style) {
        this.Style = style;
    }
}

public class GUIEditorStyleManager {

    private static GUIEditorStyleManager _instance;

    Dictionary<ComStyle, CommonGUIStyle> CommonStyles = new Dictionary<ComStyle, CommonGUIStyle>();

    public static GUIEditorStyleManager Instance {
        get {
            if (_instance == null) {
                _instance = new GUIEditorStyleManager();
                _instance.Initilize();
            }
            return _instance;
        }
    }

    public void Initilize() {
        CommonGUIStyle style1 = new CommonGUIStyle(ComStyle.Default);
        style1.BackGroundColor = GUI.backgroundColor;
        style1.Color = GUI.color;
        CommonStyles.Add(ComStyle.Default, style1);
        CommonGUIStyle ground1 = new CommonGUIStyle(ComStyle.Background1);
        ground1.BackGroundColor = Color.cyan; 
        ground1.Color = GUI.color;
        CommonStyles.Add(ComStyle.Background1, ground1);
        CommonGUIStyle style2 = new CommonGUIStyle(ComStyle.Red);
        style2.BackGroundColor = GUI.backgroundColor;
        style2.Color = Color.red;
        CommonStyles.Add(ComStyle.Red, style2);
        CommonGUIStyle style3 = new CommonGUIStyle(ComStyle.Green);
        style3.BackGroundColor = GUI.backgroundColor;
        style3.Color = Color.green;
        CommonStyles.Add(ComStyle.Green, style3);
        CommonGUIStyle style4 = new CommonGUIStyle(ComStyle.Purple);
        style4.BackGroundColor = GUI.backgroundColor;
        style4.Color = new Color(139,0,139);
        CommonStyles.Add(ComStyle.Purple, style4);
        CommonGUIStyle sky = new CommonGUIStyle(ComStyle.Sky);
        sky.BackGroundColor = GUI.backgroundColor;
        sky.Color = new Color(139, 58, 98);
        CommonStyles.Add(ComStyle.Sky, sky);
        CommonGUIStyle brown = new CommonGUIStyle(ComStyle.Brown);
        brown.BackGroundColor = GUI.backgroundColor;
        brown.Color = Color.yellow;
        CommonStyles.Add(ComStyle.Brown, brown);

    }

    public void SetGUIStyle(ComStyle style) {
        GUI.backgroundColor = CommonStyles[style].BackGroundColor;
        GUI.color = CommonStyles[style].Color;
    }

    public void SetGUIStyle(FileType fileType) {
        ComStyle style = GetColorByFileType(fileType);
        SetGUIStyle(style);
    }

    private ComStyle GetColorByFileType(FileType fileType)
    {
        switch (fileType)
        {
            case FileType.Code:
                return ComStyle.Green;
            case FileType.Prefab:
                return ComStyle.Sky;
            case FileType.Asset:
            case FileType.Json:
            case FileType.Xml:
                return ComStyle.Brown;
        }
        return ComStyle.Green;
    }
}

/// <summary>
/// 一个资源在项目中的引用情况//
/// </summary>
public class AssetInfoRef
{
    public string Name { get; set; }
    public string SearchName { get; set; }
    public int RefCount { get; set; }
    //引用信息(引用文件名，行数)//
    public Dictionary<string, int> RefDict = new Dictionary<string, int>();

}

/// <summary>
/// 搜索结果//
/// </summary>
public class SearchResult
{
    public FileType fileType;
    public string ScriptName;
    public List<string> Lines = new List<string>();
    public bool ShowDetail { get; set; } = false; //是否展开//
    public string FileName { get; set; }
    public int RefCount { get; set; }
    //引用信息(引用文件名，行数等信息)
    public Dictionary<string, int> RefDict = new Dictionary<string, int>();
}



public class EditorSerachTools : EditorWindow
{
    public static readonly string PREF_FIX = "EditorPlugin_";
    private const byte toolsCount = 4;
    private Vector2[] scrollPos = new Vector2[toolsCount];
    private bool[] SearchTypes = new bool[] { true,true,true };
    private float maxHeight = 300;
    private float height = 0;
    /*---基础参数-----------*/
    private string[] serarchOptions = new string[] { "Code", "Json", "Asset", "Xml", "Prefab","Material","Png","Jpg","All" };
    private int selectedIndex = 0;//在什么类型的资源中搜索引用//
    private int lastSelecteIndex = -1;
    private FileType searchOption = FileType.Code;

    private string[] serarchResOptions = new string[] { "*.cs", "*.json", "*.asset", "*.xml", "*.prefab", "*.mat", "*.png", "*.jpg","*.*" };
    private int searchResourceIndex = 4;

    private string[] searchStyleOptions = new string[] { "Normal", "Strict" };
    private SearchStyle searchStyle = SearchStyle.Normal;
    private int searchModeIndex = 0; //搜索模式//


    /*---字符串搜索参数-----------*/
    private string testStr = "Input your string here...";
    private bool Searched = false;
    private List<SearchResult> Results;  //所有字符串的搜索结果//
    private int strResultCount = 0;
    private string[] ignoreFiles = new string[] {  /*这个表需要优化到本地的配置表中*/
        "StringContent",
        "WorldMapDragon",   /*服务器表*/
        "world"             /*服务器表*/
    };
    /*---文件夹搜索参数-----------*/
    string prefabFolderPath = "Assets/AssetBundles/DayZ/PlatformDefault/Default/Battle/Scene";
    private bool searchCompleted = false;   //是否搜索完成//
    private bool onlyUnused = false;        //只标记未使用的文件//
    private int exportIndex = 0;
    private string[] exportOptions = new string[] { "All", "Used","Unused" };
    List<AssetInfoRef> ShowSearchedAssets = new List<AssetInfoRef>();   //缓存当前搜索的信息//
    private string ExportFileName = "PrefabNameSearchResult";   //导出文件名称//

    private int ABCurPageInt{
        get{
            return EditorPrefs.GetInt(PREF_FIX + "CurrentPageInt", 0);
        }
        set{
            EditorPrefs.SetInt(PREF_FIX + "CurrentPageInt", value);
        }
    }

    public static void Init()
    {
        EditorWindow window = EditorWindow.GetWindow<EditorSerachTools>("EditorSerachTools");
        window.Show();
    }

    private void OnGUI()
    {
        Color bgcolor = GUI.backgroundColor;
        Color color = GUI.color;
        GUILayout.BeginVertical();
        SearchInfoBasic();
        SearchByNameString();
        SearchByFold();
        SearchText();
        GUILayout.EndVertical();
    }

    private bool UseStrictMode = false;
    private void SearchInfoBasic()
    {
        GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Background1);
        GUILayout.BeginVertical("HelpBox");
        GUILayout.BeginHorizontal();
        GUILayout.Label("\t\tTarget\t\t\tSearchMode");
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        /*格式化所有的json，方便查找时候看到行号*/
        GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Green);
        if (GUILayout.Button("JsonFormats", EditorStyles.miniButton))
        {
            FormationAllJson();
        }
        selectedIndex = EditorGUILayout.Popup("", selectedIndex, serarchOptions);
        searchOption = (FileType)selectedIndex;
        lastSelecteIndex = selectedIndex;
        searchModeIndex = EditorGUILayout.Popup("", searchModeIndex, searchStyleOptions);
        EditorSerachToolsManager.Instance.SetSearchStyle((SearchStyle)searchModeIndex);
        /*UseStrictMode = GUILayout.Toggle(UseStrictMode,"StrictMode");
        EditorSerachToolsManager.Instance.SetSearchStyle((SearchStyle)(UseStrictMode?1:0));*/
        GUILayout.FlexibleSpace();
        GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Red);
        if (GUILayout.Button("ClearMemory", EditorStyles.miniButton))
        {
            SearchInit();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    private void FormationAllJson() {
        string directoryPath = Application.dataPath+"/AATest";

        // 获取目录下的所有JSON文件
        string[] jsonFiles = Directory.GetFiles(directoryPath, "*.json", SearchOption.AllDirectories);
        int count = 0;
        float progress = 0;
        foreach (string filePath in jsonFiles)
        {
            // 读取JSON文件内容
            string jsonContent = File.ReadAllText(filePath);
            count++;
            progress = (float)count / jsonFiles.Length;
            EditorUtility.DisplayProgressBar("Formating", filePath, progress);
            try
            {
                // 使用Json.NET将JSON内容解析为JToken
                JToken token = JToken.Parse(jsonContent);
                // 将JToken格式化为可读的JSON字符串
                string formattedJson = token.ToString(Formatting.Indented);
                // 输出格式化后的JSON字符串到文件中
                //string formattedFilePath = Path.Combine(directoryPath, Path.GetFileNameWithoutExtension(filePath) + "_formatted.json");
                //File.WriteAllText(formattedFilePath, formattedJson);
                File.WriteAllText(filePath, formattedJson);
            }
            catch (JsonReaderException)
            {
                Console.WriteLine($"文件 {filePath} 不是有效的JSON文件，转换失败。");
            }
        }
        EditorUtility.ClearProgressBar();
    }

    /// <summary>
    /// 字符串搜索//
    /// </summary>
    private void SearchByNameString() {
        GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Background1);
        GUILayout.BeginVertical("HelpBox");
        GUILayout.BeginHorizontal();
        SearchTypes[0] = EditorGUILayout.Foldout(SearchTypes[0], "SearchByString(查找代码，客户端表，服务器表中是否有用到该信息)");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        if (SearchTypes[0]) {
            GUILayout.BeginHorizontal();
            testStr = EditorGUILayout.TextField(testStr, GUILayout.Width(550));
            GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Green);
            if (GUILayout.Button("Paste", EditorStyles.miniButton))
            {
                if (!string.IsNullOrEmpty(GUIUtility.systemCopyBuffer))
                {
                    testStr = GUIUtility.systemCopyBuffer;
                }
            }
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Search", EditorStyles.miniButton))
            {
                //Results = IsTheNameUsedInCode(testStr);
                if (!EditorSerachToolsManager.Instance.CheckConfigCorrect(searchOption))
                {
                    string tip = "你的目录配置不正确，可能造成结果不正确，确定继续吗?";
                    if (EditorUtility.DisplayDialog("提示", tip, "Yes", "No"))
                    {
                        Results = EditorSerachToolsManager.Instance.Search(testStr, searchOption);
                        Searched = true;
                    }
                }
                else
                {
                    Results = EditorSerachToolsManager.Instance.Search(testStr, searchOption);
                    Searched = true;
                }
            }
            GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Red);
            if (GUILayout.Button("Clear", EditorStyles.miniButton))
            {
                Results.Clear();
            }
            GUILayout.EndHorizontal();
            GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Default);
            if (Searched && Results != null)
            {
                scrollPos[0] = GUILayout.BeginScrollView(scrollPos[0], GUILayout.MaxHeight(CalculateLength()));
                for (int i = 0; i < Results.Count; i++)
                {
                    strResultCount++;
                    //GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Green);
                    GUIEditorStyleManager.Instance.SetGUIStyle(Results[i].fileType);
                    GUILayout.BeginHorizontal();
                    //GUILayout.Label(string.Format("{0}---{1}", Results[i].ScriptName, Results[i].Lines.Count));
                    GUILayout.EndHorizontal();
                    Results[i].ShowDetail = EditorGUILayout.Foldout(Results[i].ShowDetail, string.Format("{0}---{1}", Results[i].ScriptName, Results[i].Lines.Count));
                    if (Results[i].ShowDetail)
                    {
                        for (int j = 0; j < Results[i].Lines.Count; j++)
                        {
                            //GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Default);
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("\t" + Results[i].Lines[j]);
                            GUILayout.EndHorizontal();
                        }
                    }
                }
                GUILayout.EndScrollView();
            }
        }
        GUILayout.EndVertical();
    }

    private float CalculateLength() {
        strResultCount = 0;
        for (int i = 0; i < Results.Count; i++)
        {
            strResultCount++;
            if (Results[i].ShowDetail) {
                strResultCount += Results[i].Lines.Count;
            }
        }
        height = 20 * strResultCount;
        if (height > maxHeight) {
            height = maxHeight;
        }
        return height;
    }

    /// <summary>
    /// 文件夹查找//
    /// </summary>
    private void SearchByFold() {
        GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Background1);
        GUILayout.BeginVertical("HelpBox");
        GUILayout.BeginHorizontal();
        SearchTypes[1] = EditorGUILayout.Foldout(SearchTypes[1], "SearchByFold(查找文件夹下所有预制体的引用情况)");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        if (SearchTypes[1]) { 
            GUILayout.BeginHorizontal();
            prefabFolderPath = EditorGUILayout.TextField(prefabFolderPath, GUILayout.Width(550));
            GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Green);
            if (GUILayout.Button("Paste", EditorStyles.miniButton))
            {
                if (!string.IsNullOrEmpty(GUIUtility.systemCopyBuffer))
                {
                    testStr = GUIUtility.systemCopyBuffer;
                }
            }
            searchResourceIndex = EditorGUILayout.Popup("", searchResourceIndex, serarchResOptions, GUILayout.Width(100));
            GUILayout.FlexibleSpace();
            GUIEditorStyleManager.Instance.SetGUIStyle(onlyUnused?ComStyle.Red: ComStyle.Green);
            onlyUnused = GUILayout.Toggle(onlyUnused, "Unused");
            GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Green);
            if (GUILayout.Button("Search", EditorStyles.miniButton))
            {
                SearchRefs(prefabFolderPath);
            }
            GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Red);
            if (GUILayout.Button("Clear", EditorStyles.miniButton))
            {
                EditorSerachToolsManager.Instance.ClearSearchRecords(prefabFolderPath);
                ShowSearchedAssets.Clear();
            }
            GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Default);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            if (searchCompleted) {
                scrollPos[1] = GUILayout.BeginScrollView(scrollPos[1]);
                int signalPageNumber = 20;
                int assetCount = ShowSearchedAssets.Count;
                int maxPages = 1;
                if (assetCount != 0)
                {
                    maxPages = (assetCount / signalPageNumber) + (assetCount % signalPageNumber != 0 ? 1 : 0);
                }
                ABCurPageInt = Mathf.Clamp(ABCurPageInt, 1, maxPages);
                for (int i = (ABCurPageInt - 1) * signalPageNumber; i < ABCurPageInt * signalPageNumber && i < assetCount; i++) {
                    GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Default);
                    string temp = ShowSearchedAssets[i].Name;
                    if (onlyUnused && ShowSearchedAssets[i].RefCount > 0) {
                        continue;
                    }
                    GUILayout.Space(2);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(new GUIContent(temp, temp), GUILayout.MinWidth(100));
                    GUILayout.FlexibleSpace();
                    if (ShowSearchedAssets[i].RefCount > 0) {
                        GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Green);
                    }
                    else {
                        GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Red);
                    }
                    GUILayout.Label("res state:\t" + (ShowSearchedAssets[i].RefCount>0?"Used":"Unused"));
                    if (GUILayout.Button("Where", EditorStyles.miniButton)) {
                        testStr = ShowSearchedAssets[i].SearchName;
                        //Results = IsTheNameUsedInCode(testStr);
                        Results = EditorSerachToolsManager.Instance.Search(testStr,searchOption);
                        Searched = true;
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndScrollView();
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Default);
                if (GUILayout.Button("LeftPage <-", EditorStyles.miniButton))
                {
                    if (ABCurPageInt > 0)
                    {
                        ABCurPageInt--;
                    }
                    else
                    {
                        ABCurPageInt = maxPages;
                    }
                }
                GUILayout.Label(ABCurPageInt + "/" + maxPages + "(total:" + ShowSearchedAssets.Count + ")", GUILayout.MinWidth(80));
                if (GUILayout.Button("RightPage ->", EditorStyles.miniButton))
                {
                    if (ABCurPageInt < maxPages)
                    {
                        ABCurPageInt++;
                    }
                    else
                    {
                        ABCurPageInt = 0;
                    }
                }
                ExportFileName = EditorGUILayout.TextField(ExportFileName);
                GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Green);
                exportIndex = EditorGUILayout.Popup("ExportType", exportIndex, exportOptions);
                if (GUILayout.Button("ExportResults",EditorStyles.miniButton)){
                    ExportInfo();
                }
                GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Default);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        }
        GUILayout.EndVertical();
    }

    /// <summary>
    /// 清除所有缓存//
    /// </summary>
    private void SearchInit()
    {
        ShowSearchedAssets.Clear();
        EditorSerachToolsManager.Instance.Clear();
    }
   
    /// <summary>
    /// 导出信息//
    /// </summary>
    private void ExportInfo() {
        System.Text.StringBuilder searchResult = new System.Text.StringBuilder();
        foreach (AssetInfoRef asset in ShowSearchedAssets)
        {
            if (exportIndex == 0){
                searchResult.AppendLine(asset.Name + ": " + (asset.RefCount > 0 ? "Used" : "Not used"));
            }
            else if (exportIndex == 1){
                if (asset.RefCount >0){
                    searchResult.AppendLine(asset.Name + ": Used");
                }
            }
            else {
                if (asset.RefCount == 0){
                    searchResult.AppendLine(asset.Name + ": Not used");
                }
            }
        }
        string filePath = string.Format("{0}/{1}.txt", Application.dataPath, ExportFileName); 
        File.WriteAllText(filePath, searchResult.ToString());
    }


    private void SearchRefs(string fPath) {
        searchCompleted = false;
        FileType searchType = (FileType)searchResourceIndex;
        ShowSearchedAssets = EditorSerachToolsManager.Instance.SearchFoldByChildsName(fPath, searchType, searchOption);
        searchCompleted = true;
        int len = fPath.LastIndexOf("/");
        ExportFileName = fPath.Substring(len+1);

    }

    private string searchFile = "Your File";
    private void SearchText() {
        GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Background1);
        GUILayout.BeginVertical("HelpBox");
        GUILayout.BeginHorizontal();
        SearchTypes[2] = EditorGUILayout.Foldout(SearchTypes[2], "SearchText");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        if (SearchTypes[2]) {
            GUILayout.BeginHorizontal();
            searchFile = EditorGUILayout.TextField(searchFile, GUILayout.Width(550));
            GUILayout.FlexibleSpace();
            GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Green);
            if (GUILayout.Button("Search", EditorStyles.miniButton))
            {
                var bosses = DayZ.DataAtlasManager.Instance.GetDataWithType<GeneralBossPersonal>();
                Debug.LogError("Count - " + bosses.Count);
            }
            GUIEditorStyleManager.Instance.SetGUIStyle(ComStyle.Red);
            if (GUILayout.Button("Clear", EditorStyles.miniButton))
            {
                //Results.Clear();
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }

    /// <summary>
    /// 搜索文件中没有用到的key//
    /// </summary>
    /// <param name="file"></param>
    private void SearchUnUsedId(string file) {

    }


}

