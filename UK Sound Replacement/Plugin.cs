using HarmonyLib;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UMM;
using System.Reflection;

[UKPlugin("UKSoundReplacement", "1.2.3", "Replaces gun sounds and cybergrind music from sound packs", true, true)]
public class Plugin : UKMod
{   
    public static Plugin instance { get; private set; }
    private static Harmony harmony;

    public override void OnModLoaded()
    {
        instance = this;
        harmony = new Harmony("tempy.soundreplacement");
        //Assembly.Load(modFolder + "\\TagLibSharp.dll");
        harmony.PatchAll();
        DirectoryInfo info = new DirectoryInfo(Directory.GetCurrentDirectory());
        SoundPackController.CreateNewSoundPack("Stock");
        Debug.Log("Searching " + Directory.GetCurrentDirectory() + " for .uksr files");
        StartCoroutine(SoundPackController.LoadCgMusic(modFolder, this));
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

        object rev = RetrieveStringPersistentModData("rev");
        if (rev != null)
            SoundPackController.SetCurrentSoundPack(rev.ToString(), SoundPackController.SoundPackType.Revolver);
        object sg = RetrieveStringPersistentModData("sg");
        if (sg != null)
            SoundPackController.SetCurrentSoundPack(sg.ToString(), SoundPackController.SoundPackType.Shotgun);
        object ng = RetrieveStringPersistentModData("ng");
        if (ng != null)
            SoundPackController.SetCurrentSoundPack(ng.ToString(), SoundPackController.SoundPackType.Nailgun);
        object rc = RetrieveStringPersistentModData("rc");
        if (rc != null)
            SoundPackController.SetCurrentSoundPack(rc.ToString(), SoundPackController.SoundPackType.Railcannon);
        object rl = RetrieveStringPersistentModData("rl");
        if (rl != null)
            SoundPackController.SetCurrentSoundPack(rl.ToString(), SoundPackController.SoundPackType.RocketLauncher);
        object cgLoop = RetrieveStringPersistentModData("cgLoop");
        if (cgLoop != null)
            SoundPackController.persistentLoopName = cgLoop.ToString();
        object cgIntro = RetrieveStringPersistentModData("cgIntro");
        if (cgIntro != null)
            SoundPackController.persistentIntroName = cgIntro.ToString();

    }

    public void SetSoundPackPersistent(string name, SoundPackController.SoundPackType type)
    {
        Debug.Log("Setting persistent sound pack to " + name + " for type " + type);
        switch (type)
        {
            case SoundPackController.SoundPackType.Revolver:
                SetPersistentModData("rev", name);
                return;
            case SoundPackController.SoundPackType.Shotgun:
                SetPersistentModData("sg", name);
                return;
            case SoundPackController.SoundPackType.Nailgun:
                SetPersistentModData("ng", name);
                return;
            case SoundPackController.SoundPackType.Railcannon:
                SetPersistentModData("rc", name);
                return;
            case SoundPackController.SoundPackType.RocketLauncher:
                SetPersistentModData("rl", name);
                return;
            case SoundPackController.SoundPackType.All:
                SetPersistentModData("rev", name);
                SetPersistentModData("sg", name);
                SetPersistentModData("ng", name);
                SetPersistentModData("rc", name);
                SetPersistentModData("rl", name);
                return;
        }
    }

    public override void OnModUnload()
    {
        base.OnModUnload();
        SoundPackController.SetCurrentSoundPack("Stock", SoundPackController.SoundPackType.All, false);
        harmony.UnpatchSelf();
    }
}

