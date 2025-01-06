using UnityEngine;
using System.Diagnostics;
using System.IO;

public class FileCopier : MonoBehaviour
{
    public string contentPath = "/path/to/your/images/folder";  // Absolute path of the source images folder (test on Meta Quest 3)

    void Start()
    {
        RunScript(GetCopyScript(), contentPath);
    }

    string GetCopyScript()
    {
        return @"
#!/bin/bash
SOURCE_FOLDER=$1
DEST_FOLDER=$2
mkdir -p $DEST_FOLDER
cp -r $SOURCE_FOLDER/* $DEST_FOLDER";
    }

    void RunScript(string scriptContent, string sourcePath)
    {
        try
        {
            string scriptPath = "/tmp/copy_images.sh";

            System.IO.File.WriteAllText(scriptPath, scriptContent);

            Process process = new Process();
            process.StartInfo.FileName = "/bin/bash";  // Assuming bash is available
            process.StartInfo.Arguments = scriptPath + " " + sourcePath + " " + Application.persistentDataPath;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();

            UnityEngine.Debug.Log("Images copied successfully.");
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError("Error running script: " + ex.Message);
        }
    }
}
