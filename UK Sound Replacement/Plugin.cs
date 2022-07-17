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
                    jValues.TryGetValue("rev", out string rev);
                    jValues.TryGetValue("sg", out string sg);
                    jValues.TryGetValue("ng", out string ng);
                    jValues.TryGetValue("rc", out string rc);
                    Debug.Log("Found a .uksf save file, setting sound pack to " + name);
                    SoundPackController.SetCurrentSoundPack(rev, SoundPackController.SoundPackType.Revolver);
                    SoundPackController.SetCurrentSoundPack(sg, SoundPackController.SoundPackType.Shotgun);
                    SoundPackController.SetCurrentSoundPack(ng, SoundPackController.SoundPackType.Nailgun);
                    SoundPackController.SetCurrentSoundPack(rc, SoundPackController.SoundPackType.Railcannon);
                    jFile.Close();
                }
            }

            patched = true;
        }
    }

    public void OnApplicationQuit()
    {
        using (StreamReader jFile = saveFileInfo.OpenText())
        {
            Dictionary<string, string> jValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(jFile.ReadToEnd());
            if (jValues.ContainsKey("rev"))
                jValues["rev"] = SoundPackController.revolverSoundPack.name;
            if (jValues.ContainsKey("sg"))
                jValues["sg"] = SoundPackController.shotgunSoundPack.name;
            if (jValues.ContainsKey("ng"))
                jValues["ng"] = SoundPackController.nailgunSoundPack.name;
            if (jValues.ContainsKey("rc"))
                jValues["rc"] = SoundPackController.railcannonSoundPack.name;
            jFile.Close();
            File.WriteAllText(saveFileInfo.FullName, JsonConvert.SerializeObject(jValues));
        }
    }
}

