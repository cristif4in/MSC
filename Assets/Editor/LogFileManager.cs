using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class LogFileManager : EditorWindow
{
    [MenuItem("Tools/Log File Manager")]
    public static void ShowWindow()
    {
        GetWindow<LogFileManager>("Log File Manager");
    }

    private void OnEnable()
    {
        // Auto-clear and delete .log files on play mode state change
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    private void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode || state == PlayModeStateChange.ExitingPlayMode)
        {
            // Auto-clear old .log files
            ClearOldLogFiles();
            
            // Auto-delete .log files if found
            DeleteLogFiles();
        }
    }

    private void ClearOldLogFiles()
    {
        string projectPath = Application.dataPath.Replace("/Assets", "");
        string[] logFiles = Directory.GetFiles(projectPath, "*.log", SearchOption.AllDirectories);

        foreach (string file in logFiles)
        {
            // Get the file's last write time
            FileInfo fileInfo = new FileInfo(file);
            
            // If file is older than 7 days, delete it (or you can use your own logic)
            if (fileInfo.LastWriteTime < DateTime.Now.AddDays(-7))
            {
                try
                {
                    File.Delete(file);
                    Debug.Log($"Deleted old log file: {file}");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Failed to delete log file {file}: {ex.Message}");
                }
            }
        }
    }

    private void DeleteLogFiles()
    {
        string projectPath = Application.dataPath.Replace("/Assets", "");
        string[] logFiles = Directory.GetFiles(projectPath, "*.log", SearchOption.AllDirectories);

        foreach (string file in logFiles)
        {
            try
            {
                File.Delete(file);
                Debug.Log($"Deleted log file: {file}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to delete log file {file}: {ex.Message}");
            }
        }
    }

    [MenuItem("Tools/Disable Screenshot Generation")]
    public static void bDisableScreenshots()
    {
        // Find MonoScripts that might contain screenshot logic
        string[] guis = AssetDatabase.FindAssets("t:MonoScript", new[] { "Assets/Scripts" });
        
        foreach (string guid in guis)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
            
            if (script == null) continue;
            
            // Read the script text
            string scriptText = AssetDatabase.GetTextAsset(path)?.ToString();
            if (string.IsNullOrEmpty(scriptText)) continue;
            
            // Check for screenshot-related patterns
            if (Regex.IsMatch(scriptText, "ScreenCapture|TakeScreenshot|screenshot", RegexOptions.IgnoreCase))
            {
                Debug.Log($"Found potential screenshot script: {path}");
                // You can add logic here to disable or modify the script
            }
        }
        
        AssetDatabase.Refresh();
    }
}