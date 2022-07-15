using BepInEx;
using HarmonyLib;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[BepInPlugin("UKSoundReplacement", "UKSoundReplacement", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    public static bool patched = false;
    public static FileInfo saveFileInfo;

    public void Start()
    {
        if (!patched)
        {
            new Harmony("tempy.soundreplacement").PatchAll();
            DirectoryInfo info = new DirectoryInfo(Directory.GetCurrentDirectory());
            SoundPackController.CreateNewSoundPack("Stock");
            Debug.Log("Searching " + Directory.GetCurrentDirectory() + " for .uksr files");
            foreach (FileInfo file in info.GetFiles("*.uksr", SearchOption.AllDirectories))
            {
                using (StreamReader jFile = file.OpenText())
                {
                    Dictionary<string, string> jValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(jFile.ReadToEnd());
                    string name = "No Name";
                    if (jValues.ContainsKey("name"))
                        name = jValues["name"];
                    if (name != "Template")
                    {
                        Debug.Log("Found .uksr " + name + " at path " + file.FullName);
                        SoundPackController.SoundPack newPack = SoundPackController.CreateNewSoundPack(name);
                        StartCoroutine(newPack.LoadFromDirectory(file.Directory, this));
                    }
                    jFile.Close();
                }
            }

            FileInfo[] allFiles = info.GetFiles("*.uksf", SearchOption.AllDirectories);
            if (allFiles.Count() > 0)
            {
                saveFileInfo = allFiles[0];
                using (StreamReader jFile = saveFileInfo.OpenText())
                {
                    Dictionary<string, string> jValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(jFile.ReadToEnd());
                    string name = "No Name";
                    if (jValues.ContainsKey("name"))
                        name = jValues["name"];
                    Debug.Log("Found a .uksf save file, setting sound pack to " + name);
                    SoundPackController.SetCurrentSoundPack(name);
                    jFile.Close();
                }
            }

            patched = true;
        }
    }

    public static void Log(string message) => Debug.Log(message);

    public void OnApplicationQuit()
    {
        using (StreamReader jFile = saveFileInfo.OpenText())
        {
            Dictionary<string, string> jValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(jFile.ReadToEnd());
            if (jValues.ContainsKey("name"))
                jValues["name"] = SoundPackController.currentSoundPackName;
            jFile.Close();
            File.WriteAllText(saveFileInfo.FullName, JsonConvert.SerializeObject(jValues));
        }
    }
}

