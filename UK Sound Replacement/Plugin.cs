using BepInEx;
using HarmonyLib;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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
                        StartCoroutine(newPack.LoadFromDirectory(file.Directory));
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

public static class SoundPackController
{
    public static string currentSoundPackName = "Stock";
    private static Dictionary<string, SoundPack> allSoundPacks = new Dictionary<string, SoundPack>();
    private static SoundPack currentSoundPack = null;
    private static List<string> stockLoadedAspects = new List<string>();

    public static SoundPack CreateNewSoundPack(string name)
    {
        SoundPack result = new SoundPack(name);

        result.AddAspect(new SoundAspect("RailcannonChargedSounds", "RailcannonSounds", true));

        result.AddAspect(new SoundAspect("RailcannonClickSounds0", "RailcannonSounds\\RailcannonBlue", false));
        result.AddAspect(new SoundAspect("RailcannonIdleSounds0", "RailcannonSounds\\RailcannonBlue", false));
        result.AddAspect(new SoundAspect("RailcannonShootSounds0", "RailcannonSounds\\RailcannonBlue", false));
        result.AddAspect(new SoundAspect("RailcannonWhirSounds0", "RailcannonSounds\\RailcannonBlue", false));

        result.AddAspect(new SoundAspect("RailcannonClickSounds1", "RailcannonSounds\\RailcannonGreen", false));
        result.AddAspect(new SoundAspect("RailcannonIdleSounds1", "RailcannonSounds\\RailcannonGreen", false));
        result.AddAspect(new SoundAspect("RailcannonShootSounds1", "RailcannonSounds\\RailcannonGreen", false));
        result.AddAspect(new SoundAspect("RailcannonWhirSounds1", "RailcannonSounds\\RailcannonGreen", false));

        result.AddAspect(new SoundAspect("RailcannonClickSounds2", "RailcannonSounds\\RailcannonRed", false));
        result.AddAspect(new SoundAspect("RailcannonIdleSounds2", "RailcannonSounds\\RailcannonRed", false));
        result.AddAspect(new SoundAspect("RailcannonShootSounds2", "RailcannonSounds\\RailcannonRed", false));
        result.AddAspect(new SoundAspect("RailcannonRedWindDownSounds2", "RailcannonSounds\\RailcannonRed", false));
        result.AddAspect(new SoundAspect("RailcannonWhirSounds2", "RailcannonSounds\\RailcannonRed", false));

        result.AddAspect(new SoundAspect("CoinBreak1False", "CoinTwirl", "RevolverSounds\\RevolverMarksman"));
        result.AddAspect(new SoundAspect("CoinFlashLoop1False", "CoinFlashLoop", "RevolverSounds\\RevolverMarksman"));
        result.AddAspect(new SoundAspect("CoinFlip1False", "CoinFlip", "RevolverSounds\\RevolverMarksman"));
        result.AddAspect(new SoundAspect("CoinReady1False", "CoinReady", "RevolverSounds\\RevolverMarksman"));
        result.AddAspect(new SoundAspect("CoinSpin1False", "CoinSpin", "RevolverSounds\\RevolverMarksman"));
        result.AddAspect(new SoundAspect("CoinTwirl1False", "CoinTwirl", "RevolverSounds\\RevolverMarksman"));
        result.AddAspect(new SoundAspect("CoinRicochet1False", "Ricochet", "RevolverSounds\\RevolverMarksman"));
        result.AddAspect(new SoundAspect("RevolverShootSounds1False", "ShootSounds", "RevolverSounds\\RevolverMarksman"));

        result.AddAspect(new SoundAspect("CoinBreak1True", "CoinTwirl", "RevolverSounds\\RevolverMarksmanAlt"));
        result.AddAspect(new SoundAspect("CoinFlashLoop1True", "CoinFlashLoop", "RevolverSounds\\RevolverMarksmanAlt"));
        result.AddAspect(new SoundAspect("CoinFlip1True", "CoinFlip", "RevolverSounds\\RevolverMarksmanAlt"));
        result.AddAspect(new SoundAspect("CoinReady1True", "CoinReady", "RevolverSounds\\RevolverMarksmanAlt"));
        result.AddAspect(new SoundAspect("CoinSpin1True", "CoinSpin", "RevolverSounds\\RevolverMarksmanAlt"));
        result.AddAspect(new SoundAspect("CoinTwirl1True", "CoinTwirl", "RevolverSounds\\RevolverMarksmanAlt"));
        result.AddAspect(new SoundAspect("CoinRicochet1True", "Ricochet", "RevolverSounds\\RevolverMarksmanAlt"));
        result.AddAspect(new SoundAspect("RevolverShootSounds1True", "ShootSounds", "RevolverSounds\\RevolverMarksmanAlt"));
        result.AddAspect(new SoundAspect("HammerClick1", "HammerClick", "RevolverSounds\\RevolverMarksmanAlt"));

        result.AddAspect(new SoundAspect("ChargeLoop0False", "ChargeLoop", "RevolverSounds\\RevolverPiercer"));
        result.AddAspect(new SoundAspect("ChargeReady0False", "ChargeReady", "RevolverSounds\\RevolverPiercer"));
        result.AddAspect(new SoundAspect("ChargingUp0False", "ChargingUp", "RevolverSounds\\RevolverPiercer"));
        result.AddAspect(new SoundAspect("ClickCancel0False", "ClickCancel", "RevolverSounds\\RevolverPiercer"));
        result.AddAspect(new SoundAspect("RevolverShootSounds0False", "ShootSounds", "RevolverSounds\\RevolverPiercer"));
        result.AddAspect(new SoundAspect("RevolverSuperShootSounds0False", "SuperShootSounds", "RevolverSounds\\RevolverPiercer"));

        result.AddAspect(new SoundAspect("ChargeLoop0True", "ChargeLoop", "RevolverSounds\\RevolverPiercerAlt"));
        result.AddAspect(new SoundAspect("ChargeReady0True", "ChargeReady", "RevolverSounds\\RevolverPiercerAlt"));
        result.AddAspect(new SoundAspect("ChargingUp0True", "ChargingUp", "RevolverSounds\\RevolverPiercerAlt"));
        result.AddAspect(new SoundAspect("ClickCancel0True", "ClickCancel", "RevolverSounds\\RevolverPiercerAlt"));
        result.AddAspect(new SoundAspect("RevolverShootSounds0True", "ShootSounds", "RevolverSounds\\RevolverPiercerAlt"));
        result.AddAspect(new SoundAspect("RevolverSuperShootSounds0True", "SuperShootSounds", "RevolverSounds\\RevolverPiercerAlt"));
        result.AddAspect(new SoundAspect("HammerClick0", "HammerClick", "RevolverSounds\\RevolverPiercerAlt"));

        result.AddAspect(new SoundAspect("ShotgunShootSounds0", "ShotgunShootSounds", "ShotgunSounds\\ShotgunBlue"));
        result.AddAspect(new SoundAspect("ShotgunChargeLoop", "ChargeLoop", "ShotgunSounds\\ShotgunBlue"));
        result.AddAspect(new SoundAspect("CoreEject", "ShotgunSounds\\ShotgunBlue", true));
        result.AddAspect(new SoundAspect("CoreEjectFlick", "ShotgunSounds\\ShotgunBlue", true));
        result.AddAspect(new SoundAspect("CoreEjectReload", "ShotgunSounds\\ShotgunBlue", true));
        result.AddAspect(new SoundAspect("MainShotReload", "ShotgunSounds\\ShotgunBlue", true));
        result.AddAspect(new SoundAspect("HeatHiss", "ShotgunSounds\\ShotgunBlue", true));

        result.AddAspect(new SoundAspect("ShotgunShootSounds1", "ShotgunShootSounds", "ShotgunSounds\\ShotgunGreen"));
        result.AddAspect(new SoundAspect("ShotgunCharge", "ShotgunSounds\\ShotgunGreen", true));
        result.AddAspect(new SoundAspect("ShotgunPump1", "ShotgunSounds\\ShotgunGreen", true));
        result.AddAspect(new SoundAspect("ShotgunPump2", "ShotgunSounds\\ShotgunGreen", true));
        result.AddAspect(new SoundAspect("OverCharged", "ShotgunSounds\\ShotgunGreen", true));

        allSoundPacks.Add(name, result);
        return result;
    }

    public static SoundPack[] GetAllSoundPacks()
    {
        return (SoundPack[])allSoundPacks.Values.ToArray().Clone();
    }

    public static void SetCurrentSoundPack(string name)
    {
        if (allSoundPacks.ContainsKey(name))
        {
            currentSoundPack = allSoundPacks[name];
            currentSoundPackName = name;
        }
        else
            Debug.Log("Tried to set current soundpack to " + name + " but it wasn't found!");
        foreach (Revolver r in Resources.FindObjectsOfTypeAll<Revolver>())
        {
            Inject_RevolverSounds.Postfix(r);
        }
        foreach (Railcannon r in Resources.FindObjectsOfTypeAll<Railcannon>())
        {
            Inject_RailcannonSounds.Postfix(r);
        }
        foreach (Shotgun s in Resources.FindObjectsOfTypeAll<Shotgun>())
        {
            Inject_ShotgunSounds.Postfix(s);
        }
    }

    public static void GetAllAudioClips(string name, ref AudioClip[] clips)
    {
        if (!stockLoadedAspects.Contains(name))
        {
            stockLoadedAspects.Add(name);
            foreach (AudioClip clip in clips)
                allSoundPacks["Stock"].GetAspect(name).allClips.Add(clip);
        }
        if (currentSoundPack != null)
        {
            AudioClip[] allClips = currentSoundPack.GetAllAudioClipsOfAspect(name);
            if (allClips != null)
                clips = allClips;
        }
    }

    public static void SetAudioSourceClip(AudioSource source, string name)
    {
        if (source == null)
        {
            Debug.LogError("Got a null audiosource while handling " + name);
            return;
        }
        if (name != "Random" && !stockLoadedAspects.Contains(name) && source.clip != null)
        {
            stockLoadedAspects.Add(name);
            allSoundPacks["Stock"].GetAspect(name).allClips.Add(source.clip);
        }
        if (currentSoundPack != null)
        {
            AudioClip clip = currentSoundPack.GetRandomClipFromAspect(name);
            if (clip != null)
                source.clip = clip;
        }
    }

    public static void SetAudioClip(ref AudioClip clip, string name)
    {
        if (!stockLoadedAspects.Contains(name))
        {
            stockLoadedAspects.Add(name);
            allSoundPacks["Stock"].GetAspect(name).allClips.Add(clip);
        }
        if (currentSoundPack != null)
        {
            AudioClip newClip = currentSoundPack.GetRandomClipFromAspect(name);
            if (newClip != null)
            {
                clip = newClip;
            }
        }
    }

    public class SoundPack
    {
        public string name = "NOT ASSIGNED"; // I swear to god if I see this in game
        public Texture2D previewImage;
        private Dictionary<string, SoundAspect> allAspects = new Dictionary<string, SoundAspect>();

        public SoundPack(string name)
        {
            this.name = name;
        }

        public IEnumerator LoadFromDirectory(DirectoryInfo info)
        {
            foreach (SoundAspect aspect in allAspects.Values)
            {
                foreach (FileInfo file in new DirectoryInfo(info.FullName + aspect.path).GetFiles("*.wav", SearchOption.AllDirectories))
                {
                    using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(file.FullName, AudioType.WAV))
                    {
                        yield return www.SendWebRequest();

                        if (www.isNetworkError)
                        {
                            Debug.Log("couldn't load clip " + file.FullName);
                            Debug.Log(www.error);
                        }
                        else
                        {
                            aspect.allClips.Add(DownloadHandlerAudioClip.GetContent(www));
                        }
                    }
                }
                foreach (FileInfo file in new DirectoryInfo(info.FullName + aspect.path).GetFiles("*.mp3", SearchOption.AllDirectories))
                {
                    using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(file.FullName, AudioType.MPEG))
                    {
                        yield return www.SendWebRequest();

                        if (www.isNetworkError)
                        {
                            Debug.Log("couldn't load clip " + file.FullName);
                            Debug.Log(www.error);
                        }
                        else
                        {
                            aspect.allClips.Add(DownloadHandlerAudioClip.GetContent(www));
                        }
                    }
                }
                foreach (FileInfo file in new DirectoryInfo(info.FullName + aspect.path).GetFiles("*.ogg", SearchOption.AllDirectories))
                {
                    using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(file.FullName, AudioType.OGGVORBIS))
                    {
                        yield return www.SendWebRequest();

                        if (www.isNetworkError)
                        {
                            Debug.Log("couldn't load clip " + file.FullName);
                            Debug.Log(www.error);
                        }
                        else
                        {
                            aspect.allClips.Add(DownloadHandlerAudioClip.GetContent(www));
                        }
                    }
                }
            }
            if (File.Exists(info.FullName + "\\preview.png"))
            {
                using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(info.FullName + "\\preview.png"))
                {
                    yield return www.SendWebRequest();
                    if (www.isNetworkError)
                    {
                        Debug.Log("couldn't load preview image " + info.FullName + "\\preview.png");
                        Debug.Log(www.error);
                    }
                    else
                    {
                        previewImage = DownloadHandlerTexture.GetContent(www);
                    }
                }
            }
            yield break;
        }

        public void AddAspect(SoundAspect toAdd)
        {
            allAspects.Add(toAdd.name, toAdd);
        }

        public AudioClip GetRandomClipFromAspect(string name)
        {
            if (name == "Random")
            {
                SoundAspect randomAspect = allAspects.Values.ToList()[UnityEngine.Random.Range(0, allAspects.Values.Count - 1)];
                while (randomAspect.allClips.Count <= 0)
                    randomAspect = allAspects.Values.ToList()[UnityEngine.Random.Range(0, allAspects.Values.Count - 1)];
                if (randomAspect.allClips.Count == 1)
                    return randomAspect.allClips[0];
                return randomAspect.allClips[UnityEngine.Random.Range(0, randomAspect.allClips.Count - 1)];
            }
            if (!allAspects.ContainsKey(name) || allAspects[name].allClips.Count <= 0)
                return null;
            List<AudioClip> allClips = allAspects[name].allClips;
            if (allClips.Count == 1)
                return allClips[0];
            return allAspects[name].allClips[UnityEngine.Random.Range(0, allAspects[name].allClips.Count - 1)];
        }

        public AudioClip[] GetAllAudioClipsOfAspect(string name)
        {
            if (allAspects[name] == null)
                return null;
            return (AudioClip[])allAspects[name].allClips.ToArray().Clone();
        }

        public SoundAspect GetAspect(string name)
        {
            if (!allAspects.ContainsKey(name))
                return null;
            return allAspects[name];
        }
    }

    public class SoundAspect
    {
        public string name;
        public string path;
        public List<AudioClip> allClips = new List<AudioClip>();

        public SoundAspect(string name, string path, bool inRootDirectory)
        {
            this.name = name;
            if (inRootDirectory)
                this.path = "\\" + path + "\\" + name + "\\";
            else
                this.path = "\\" + path + "\\" + name.Substring(0, name.Length - 1) + "\\";
        }

        public SoundAspect(string name, string folderName, string path)
        {
            this.name = name;
            this.path = "\\" + path + "\\" + folderName + "\\";
        }
    }
}

#region HARMONY_PATCHES
[HarmonyPatch(typeof(ShopZone), "Start")]
public static class Inject_SoundPackShops
{
    public static void Prefix(ShopZone __instance)
    {
        if (__instance.transform.Find("Canvas").Find("Weapons") != null) // just a sanity check that we're not messing with a testament
        {
            Transform enemies = GameObject.Instantiate(__instance.transform.Find("Canvas").Find("Enemies"), __instance.transform.Find("Canvas").Find("Weapons"));
            enemies.Find("BackButton (2)").gameObject.SetActive(false);
            Transform contentParent = enemies.Find("Panel").Find("Scroll View").Find("Viewport").Find("Content");
            GameObject packTemplate = contentParent.Find("Enemy Button Template").gameObject;
            for (int i = 0; i < contentParent.childCount; i++)
                contentParent.GetChild(i).gameObject.SetActive(false);
            packTemplate.gameObject.SetActive(true);
            foreach (SoundPackController.SoundPack pack in SoundPackController.GetAllSoundPacks())
            {
                GameObject newPack = GameObject.Instantiate(packTemplate, contentParent);
                newPack.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = Color.gray;
                if (pack.previewImage != null)
                {
                    GameObject.Destroy(newPack.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Image>());
                    IEnumerator setImageNextFrame() // So unity waits a frame before destroying a component, hence this
                    {
                        yield return null;
                        RawImage img = newPack.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.AddComponent<RawImage>();
                        img.texture = pack.previewImage;
                        img.raycastTarget = false;
                    }
                    __instance.StartCoroutine(setImageNextFrame());
                }
                else
                    newPack.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
                ShopButton sButton = newPack.GetComponent<ShopButton>();
                sButton.toActivate = new GameObject[] { };
                newPack.GetComponent<ShopButton>().toDeactivate = new GameObject[] { };
                newPack.GetComponent<Button>().onClick.AddListener(delegate
                {
                    SoundPackController.SetCurrentSoundPack(pack.name);
                    GameObject.Instantiate(sButton.clickSound);
                    AudioSource source = GameObject.Instantiate(sButton.clickSound).GetComponent<AudioSource>();
                    SoundPackController.SetAudioSourceClip(source, "Random");
                    source.volume = 1f;
                    source.Play();
                });
                GameObject newText = GameObject.Instantiate(__instance.transform.Find("Canvas").Find("Weapons").Find("ArmButton").gameObject, newPack.transform.GetChild(0).GetChild(0)); // yes I tried making a "prefab" out of this and instantiating it, didn't work
                GameObject.Destroy(newText.GetComponent<ShopButton>());
                newText.transform.localPosition = Vector3.zero;
                newText.transform.localScale = new Vector3(0.4167529f, 0.4167529f, 0.4167529f);
                newText.layer = 5;
                Text text = newText.GetComponentInChildren<Text>();
                text.text = pack.name;
                text.raycastTarget = false;
            }
            packTemplate.SetActive(false);

            Transform newArmsButton = GameObject.Instantiate(__instance.transform.Find("Canvas").Find("Weapons").Find("ArmButton").gameObject, __instance.transform.Find("Canvas").Find("Weapons")).transform;
            newArmsButton.localPosition = new Vector3(-180f, -144.7f, -45.00326f);
            newArmsButton.gameObject.GetComponentInChildren<Text>(true).text = "SOUND PACKS";
            ShopButton button = newArmsButton.GetComponent<ShopButton>();
            button.toActivate = new GameObject[] { enemies.gameObject };
            button.toDeactivate = new GameObject[]
            {
                __instance.transform.Find("Canvas").Find("Weapons").Find("RevolverWindow").gameObject,
                __instance.transform.Find("Canvas").Find("Weapons").Find("ShotgunWindow").gameObject,
                __instance.transform.Find("Canvas").Find("Weapons").Find("NailgunWindow").gameObject,
                __instance.transform.Find("Canvas").Find("Weapons").Find("RailcannonWindow").gameObject,
                __instance.transform.Find("Canvas").Find("Weapons").Find("ArmWindow").gameObject
            };

            __instance.transform.Find("Canvas").Find("Weapons").Find("BackButton (1)").gameObject.GetComponent<ShopButton>().toDeactivate = __instance.transform.Find("Canvas").Find("Weapons").Find("BackButton (1)").gameObject.GetComponent<ShopButton>().toDeactivate.AddToArray(enemies.gameObject);
            __instance.transform.Find("Canvas").Find("Weapons").Find("RevolverButton").gameObject.GetComponent<ShopButton>().toDeactivate = __instance.transform.Find("Canvas").Find("Weapons").Find("RevolverButton").gameObject.GetComponent<ShopButton>().toDeactivate.AddToArray(enemies.gameObject);
            __instance.transform.Find("Canvas").Find("Weapons").Find("ShotgunButton").gameObject.GetComponent<ShopButton>().toDeactivate = __instance.transform.Find("Canvas").Find("Weapons").Find("ShotgunButton").gameObject.GetComponent<ShopButton>().toDeactivate.AddToArray(enemies.gameObject);
            __instance.transform.Find("Canvas").Find("Weapons").Find("NailgunButton").gameObject.GetComponent<ShopButton>().toDeactivate = __instance.transform.Find("Canvas").Find("Weapons").Find("NailgunButton").gameObject.GetComponent<ShopButton>().toDeactivate.AddToArray(enemies.gameObject);
            __instance.transform.Find("Canvas").Find("Weapons").Find("RailcannonButton").gameObject.GetComponent<ShopButton>().toDeactivate = __instance.transform.Find("Canvas").Find("Weapons").Find("RailcannonButton").gameObject.GetComponent<ShopButton>().toDeactivate.AddToArray(enemies.gameObject);
            __instance.transform.Find("Canvas").Find("Weapons").Find("ArmButton").gameObject.GetComponent<ShopButton>().toDeactivate = __instance.transform.Find("Canvas").Find("Weapons").Find("ArmButton").gameObject.GetComponent<ShopButton>().toDeactivate.AddToArray(enemies.gameObject);
        }
    }
}

[HarmonyPatch(typeof(Revolver), "Start")]
public static class Inject_RevolverSounds
{
    public static void Postfix(Revolver __instance)
    {
        SoundPackController.GetAllAudioClips("RevolverShootSounds" + __instance.gunVariation + __instance.altVersion.ToString(), ref __instance.gunShots);
        if (__instance.gunVariation == 0)
        {
            SoundPackController.GetAllAudioClips("RevolverSuperShootSounds" + __instance.gunVariation + __instance.altVersion.ToString(), ref __instance.superGunShots);
            SoundPackController.SetAudioClip(ref __instance.chargingSound, "ChargingUp" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioClip(ref __instance.chargedSound, "ChargeReady" + __instance.gunVariation + __instance.altVersion.ToString());
            if (__instance.gunBarrel == null)
            {
                foreach (AudioSource source in __instance.GetComponentsInChildren<AudioSource>(true))
                    if (source.name == "ChargeEffect")
                        SoundPackController.SetAudioSourceClip(source, "ChargeLoop" + __instance.gunVariation + __instance.altVersion.ToString());
            }
            else
                SoundPackController.SetAudioSourceClip(__instance.gunBarrel.transform.GetChild(0).GetComponent<AudioSource>(), "ChargeLoop" + __instance.gunVariation + __instance.altVersion.ToString());
            foreach (AudioSource source in __instance.GetComponentsInChildren<AudioSource>(true))
                if (source.name == "Bone_001")
                    SoundPackController.SetAudioSourceClip(source, "ClickCancel" + __instance.gunVariation + __instance.altVersion.ToString());
        }
        else
        {
            SoundPackController.SetAudioClip(ref __instance.twirlSound, "CoinTwirl" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioSourceClip(__instance.GetComponentInChildren<Canvas>().gameObject.GetComponent<AudioSource>(), "CoinReady" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioSourceClip(__instance.coin.GetComponent<AudioSource>(), "CoinFlip" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioSourceClip(__instance.coin.transform.GetChild(0).GetComponent<AudioSource>(), "CoinSpin" + __instance.gunVariation + __instance.altVersion.ToString());

            Coin coin = __instance.coin.GetComponent<Coin>();
            SoundPackController.SetAudioSourceClip(coin.flash.GetComponent<AudioSource>(), "CoinFlip" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioSourceClip(coin.coinBreak.GetComponent<AudioSource>(), "CoinBreak" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioSourceClip(coin.coinHitSound.GetComponents<AudioSource>()[0], "CoinRicochet" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioSourceClip(coin.coinHitSound.GetComponents<AudioSource>()[1], "CoinFlip" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioSourceClip(coin.chargeEffect.GetComponent<AudioSource>(), "CoinFlashLoop" + __instance.gunVariation + __instance.altVersion.ToString());
        }
    }
}

[HarmonyPatch(typeof(RevolverAnimationReceiver), "Click")]
public static class Inject_RevolverHammerSound
{
    public static bool Prefix(RevolverAnimationReceiver __instance)
    {
        SoundPackController.SetAudioSourceClip(__instance.click.GetComponent<AudioSource>(), "HammerClick" + ((Revolver)Traverse.Create(__instance).Field("rev").GetValue()).gunVariation); // I think that traverse is faster than unity's get component in children
        return true;
    }
}

[HarmonyPatch(typeof(Shotgun), "Start")]
public static class Inject_ShotgunSounds
{
    public static void Postfix(Shotgun __instance)
    {
        SoundPackController.SetAudioSourceClip(__instance.pumpChargeSound.GetComponent<AudioSource>(), "ShotgunCharge");
        SoundPackController.SetAudioSourceClip(__instance.warningBeep.GetComponent<AudioSource>(), "OverCharged");
        SoundPackController.SetAudioSourceClip(__instance.chargeSoundBubble.GetComponent<AudioSource>(), "ShotgunChargeLoop");
        AudioSource heatSinkAud = (AudioSource)Traverse.Create(__instance).Field("heatSinkAud").GetValue();
        if (heatSinkAud == null)
            heatSinkAud = __instance.heatSinkSMR.GetComponent<AudioSource>();
        SoundPackController.SetAudioSourceClip(heatSinkAud, "HeatHiss");
        SoundPackController.SetAudioClip(ref __instance.clickChargeSound, "CoreEjectFlick");
        SoundPackController.SetAudioClip(ref __instance.smackSound, "CoreEjectReload");
        SoundPackController.SetAudioClip(ref __instance.clickSound, "MainShotReload");
    }
}

[HarmonyPatch(typeof(Shotgun), "Shoot")]
public static class Inject_ShotgunShootSounds
{
    public static bool Prefix(Shotgun __instance)
    {
        SoundPackController.SetAudioClip(ref __instance.shootSound, "ShotgunShootSounds" + __instance.variation);
        return true;
    }
}

[HarmonyPatch(typeof(Shotgun), "ShootSinks")]
public static class Inject_ShotgunShootHeatSinkSounds
{
    public static bool Prefix(Shotgun __instance)
    {
        SoundPackController.SetAudioSourceClip(__instance.grenadeSoundBubble.GetComponent<AudioSource>(), "CoreEject");
        SoundPackController.SetAudioClip(ref __instance.shootSound, "ShotgunShootSounds" + __instance.variation);
        return true;
    }
}

[HarmonyPatch(typeof(Shotgun), "Pump1Sound")]
public static class Inject_ShotgunPump1Sounds
{
    public static bool Prefix(Shotgun __instance)
    {
        SoundPackController.SetAudioClip(ref __instance.pump1sound, "ShotgunPump1");
        return true;
    }
}

[HarmonyPatch(typeof(Shotgun), "Pump2Sound")]
public static class Inject_ShotgunPump2Sounds
{
    public static bool Prefix(Shotgun __instance)
    {
        SoundPackController.SetAudioClip(ref __instance.pump2sound, "ShotgunPump2");
        return true;
    }
}

[HarmonyPatch(typeof(Railcannon), "Shoot")]
public static class Inject_RailcannonShootSounds
{
    public static bool Prefix(Railcannon __instance)
    {
        if (__instance.variation != 2)
            SoundPackController.SetAudioSourceClip(__instance.fireSound.GetComponent<AudioSource>(), "RailcannonShootSounds" + __instance.variation);
        else if (__instance.variation == 2)
        {
            AudioSource[] sources = __instance.fireSound.GetComponents<AudioSource>();
            SoundPackController.SetAudioSourceClip(sources[0], "RailcannonRedWindDownSounds2");
            SoundPackController.SetAudioSourceClip(sources[1], "RailcannonShootSounds2");
            SoundPackController.SetAudioSourceClip(sources[2], "RailcannonShootSounds2");
        }
        return true;
    }
}

[HarmonyPatch(typeof(Railcannon), "GetStuff")]
public static class Inject_RailcannonSounds
{
    public static void Postfix(Railcannon __instance)
    {
        AudioSource fullAud = (AudioSource)Traverse.Create(__instance).Field("fullAud").GetValue();
        if (fullAud == null)
            fullAud = __instance.fullCharge.GetComponent<AudioSource>();
        SoundPackController.SetAudioSourceClip(fullAud, "RailcannonIdleSounds" + __instance.variation);
    }
}

[HarmonyPatch(typeof(RailCannonPip), "CheckSounds")]
public static class Inject_RailcannonIdleSounds
{
    public static void Postfix(RailCannonPip __instance)
    {
        foreach (AudioSource source in __instance.GetComponents<AudioSource>())
        {
            if (source.loop)
                SoundPackController.SetAudioSourceClip(source, "RailcannonWhirSounds" + ((Railcannon)Traverse.Create(__instance).Field("rc").GetValue()).variation);
            else
                SoundPackController.SetAudioSourceClip(source, "RailcannonClickSounds" + ((Railcannon)Traverse.Create(__instance).Field("rc").GetValue()).variation);
        }
    }
}

[HarmonyPatch(typeof(WeaponCharges), "PlayRailCharge")]
public static class Inject_RailcannonChargedSounds
{
    public static bool Prefix(WeaponCharges __instance)
    {
        SoundPackController.SetAudioSourceClip(__instance.railCannonFullChargeSound.GetComponent<AudioSource>(), "RailcannonChargedSounds");
        return true;
    }
}

#endregion