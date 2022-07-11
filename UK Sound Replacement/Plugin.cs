using BepInEx;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;

[BepInPlugin("UKSoundReplacement", "UKSoundReplacement", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    public static bool patched = false;

    public void Start()
    {
        if (!patched)
        {
            new Harmony("tempy.soundreplacement").PatchAll();
            DirectoryInfo info = new DirectoryInfo(Directory.GetCurrentDirectory());
            Debug.Log("Searching " + Directory.GetCurrentDirectory() + " for .uksr files");
            foreach (FileInfo file in info.GetFiles("*.uksr", SearchOption.AllDirectories))
            {
                using (StreamReader jFile = file.OpenText())
                {
                    Dictionary<string, string> jValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(jFile.ReadToEnd());
                    Debug.Log("Found .uksr " + jValues["name"] + " at path " + file.FullName);
                    SoundPackController.SoundPack newPack = SoundPackController.CreateNewSoundPack(jValues["name"]);
                    StartCoroutine(newPack.LoadFromDirectory(file.Directory));
                    jFile.Close();
                }
            }
            SoundPackController.SetCurrentSoundPack("BVSSIC");
            patched = true;
        }
    }
}

public static class SoundPackController
{
    private static Dictionary<string, SoundPack> allSoundPacks = new Dictionary<string, SoundPack>();
    private static SoundPack currentSoundPack = null;

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
        result.AddAspect(new SoundAspect("CoinRicochet1False", "Ricochet", "RevolverSounds\\RevolverMarksmanAlt"));
        result.AddAspect(new SoundAspect("RevolverShootSounds1False", "ShootSounds", "RevolverSounds\\RevolverMarksman"));

        result.AddAspect(new SoundAspect("CoinBreak1True", "CoinTwirl", "RevolverSounds\\RevolverMarksmanAlt"));
        result.AddAspect(new SoundAspect("CoinFlashLoop1True", "CoinFlashLoop", "RevolverSounds\\RevolverMarksmanAlt"));
        result.AddAspect(new SoundAspect("CoinFlip1True", "CoinFlip", "RevolverSounds\\RevolverMarksmanAlt"));
        result.AddAspect(new SoundAspect("CoinReady1True", "CoinReady", "RevolverSounds\\RevolverMarksmanAlt"));
        result.AddAspect(new SoundAspect("CoinSpin1True", "CoinSpin", "RevolverSounds\\RevolverMarksmanAlt"));
        result.AddAspect(new SoundAspect("CoinTwirl1True", "CoinTwirl", "RevolverSounds\\RevolverMarksmanAlt"));
        result.AddAspect(new SoundAspect("CoinRicochet1True", "Ricochet", "RevolverSounds\\RevolverMarksmanAlt"));
        result.AddAspect(new SoundAspect("RevolverShootSounds1True", "ShootSounds", "RevolverSounds\\RevolverMarksmanAlt"));

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

        //result.AddAspect(new SoundAspect("ShotgunChargingSounds", "ShotgunSounds"));
        //result.AddAspect(new SoundAspect("ShotgunChargingStartSounds", "ShotgunSounds"));
        //result.AddAspect(new SoundAspect("ShotgunEjectSounds", "ShotgunSounds"));
        //result.AddAspect(new SoundAspect("ShotgunHeatHissSounds", "ShotgunSounds"));
        //result.AddAspect(new SoundAspect("ShotgunOverchargedBeepSounds", "ShotgunSounds"));
        //result.AddAspect(new SoundAspect("ShotgunPumpChargeSounds", "ShotgunSounds"));
        //result.AddAspect(new SoundAspect("ShotgunPump1Sounds", "ShotgunSounds"));
        //result.AddAspect(new SoundAspect("ShotgunPump2Sounds", "ShotgunSounds"));
        //result.AddAspect(new SoundAspect("ShotgunShootSounds", "ShotgunSounds"));

        allSoundPacks.Add(name, result);
        return result;
    }

    public static void SetCurrentSoundPack(string name)
    {
        if (name == "STOCK")
            currentSoundPack = null;
        else if (allSoundPacks.ContainsKey(name))
            currentSoundPack = allSoundPacks[name];
        else
            Debug.Log("Tried to set current soundpack to " + name + " but it wasn't found!");
    }

    public static AudioClip[] GetAllAudioClips(string name)
    {
        if (currentSoundPack != null)
            return currentSoundPack.GetAllAudioClipsOfAspect(name);
        return null;
    }

    public static void SetAudioSourceClip(AudioSource source, string name)
    {
        if (source == null)
        {
            Debug.LogError("Got a null audiosource while handling " + name);
            return;
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
            yield break;
        }

        public void AddAspect(SoundAspect toAdd)
        {
            allAspects.Add(toAdd.name, toAdd);
        }

        public AudioClip GetRandomClipFromAspect(string name)
        {
            if (!allAspects.ContainsKey(name) || allAspects[name].allClips.Count <= 0)
                return null;
            List<AudioClip> allClips = allAspects[name].allClips;
            if (allClips.Count == 0)
                return allClips[0];
            return allAspects[name].allClips[Random.Range(0, allAspects[name].allClips.Count - 1)];
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

        public List<AudioClip> CoinHitSounds = new List<AudioClip>();
        public List<AudioClip> CoinThrownSounds = new List<AudioClip>();
        public List<AudioClip> RevolverChargeShootSounds = new List<AudioClip>();
        public List<AudioClip> RevolverChargeSounds = new List<AudioClip>();
        public List<AudioClip> RevolverCoinReadySounds = new List<AudioClip>();
        public List<AudioClip> RevolverShootSounds = new List<AudioClip>();
        public List<AudioClip> RevolverSlabChargeShootSounds = new List<AudioClip>();
        public List<AudioClip> RevolverSlabCockSounds = new List<AudioClip>();
        public List<AudioClip> RevolverSlabShootSounds = new List<AudioClip>();

        public List<AudioClip> ShotgunChargeSounds = new List<AudioClip>();
        public List<AudioClip> ShotgunEjectSounds = new List<AudioClip>();
        public List<AudioClip> ShotgunHeatHissSounds = new List<AudioClip>();
        public List<AudioClip> ShotgunOverchargedSounds = new List<AudioClip>();
        public List<AudioClip> ShotgunPumpSounds = new List<AudioClip>();
        public List<AudioClip> ShotgunShootSounds = new List<AudioClip>();

        public List<AudioClip> NailgunBarrelRotateSounds = new List<AudioClip>();
        public List<AudioClip> NailgunMagnetReadyounds = new List<AudioClip>();
        public List<AudioClip> NailgunOverheatSounds = new List<AudioClip>();
        public List<AudioClip> NailgunShootSawSounds = new List<AudioClip>();
        public List<AudioClip> NailgunShootSounds = new List<AudioClip>();
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
 
#region HARMONY_PATCHES
[HarmonyPatch(typeof(ShopZone), "Start")]
public static class Inject_SoundPackShops
{
    public static void Postfix(ShopMother __instance)
    {
        if (__instance.transform.Find("Canvas").Find("Weapons") != null) // just a sanity check that we're not messing with a testament
        {
            Transform enemies = GameObject.Instantiate(__instance.transform.Find("Canvas").Find("Enemies"), __instance.transform.Find("Canvas"));
            enemies.Find("BackButton (2)").gameObject.SetActive(false);
            Transform contentParent = enemies.Find("Panel").Find("Scroll View").Find("Viewport").Find("Content");
            Transform packTemplate = contentParent.Find("Enemy Button Template");
            packTemplate.gameObject.tag = "Body"; // it's only body cuz it's a builtin tag that shouldn't affect behaviour
            GameObject newText = GameObject.Instantiate(__instance.transform.Find("Canvas").Find("Weapons").Find("ArmButton").gameObject, packTemplate.transform.GetChild(0).GetChild(0).GetChild(0));
            GameObject.Destroy(newText.GetComponent<ShopButton>());
            newText.transform.localPosition = Vector3.zero;
            newText.transform.localScale = new Vector3(0.4167529f, 0.4167529f, 0.4167529f);
            EnemyInfoPage page = enemies.GetComponentInChildren<EnemyInfoPage>(true);

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
                __instance.transform.Find("Canvas").Find("Weapons").Find("RailcannonWindow").gameObject
            };
        }
    }
}

[HarmonyPatch(typeof(Revolver), "Start")]
public static class Inject_RevolverSounds
{
    public static bool Prefix(Revolver __instance)
    {
        AudioClip[] clips = SoundPackController.GetAllAudioClips("RevolverShootSounds" + __instance.gunVariation + __instance.altVersion.ToString());
        if (clips != null)
            __instance.gunShots = clips;
        if (__instance.gunVariation == 0)
        {
            clips = SoundPackController.GetAllAudioClips("RevolverSuperShootSounds" + __instance.gunVariation + __instance.altVersion.ToString());
            if (clips != null)
                __instance.superGunShots = clips;
            SoundPackController.SetAudioClip(ref __instance.chargingSound, "ChargingUp" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioClip(ref __instance.chargedSound, "ChargeReady" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioSourceClip(__instance.gunBarrel.transform.GetChild(0).gameObject.GetComponent<AudioSource>(), "ChargeLoop" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioSourceClip(__instance.GetComponentInChildren<Turn>().gameObject.GetComponent<AudioSource>(), "ClickCancel" + __instance.gunVariation + __instance.altVersion.ToString());
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
        return true;
    }
}

//[HarmonyPatch(typeof(Nailgun), "FixedUpdate")]
//public static class Inject_NailgunSounds
//{
//    public static void Prefix(Nailgun __instance)
//    {
//        //((AudioSource)(Traverse.Create(__instance).Field("aud").GetValue())).clip = Plugin.allClips[UnityEngine.Random.Range(0, Plugin.allClips.Count - 1)];
//        if (__instance.lastShotSound)
//            __instance.lastShotSound.GetComponent<AudioSource>().clip = Plugin.allClips[UnityEngine.Random.Range(0, Plugin.allClips.Count - 1)];
//    }
//}

[HarmonyPatch(typeof(Shotgun), "Shoot")]
public static class Inject_ShotgunShootSounds
{
    public static bool Prefix(Shotgun __instance)
    {
        SoundPackController.SetAudioClip(ref __instance.shootSound, "ShotgunShootSounds");
        return true;
    }
}

[HarmonyPatch(typeof(Shotgun), "Update")]
public static class Inject_ShotgunChargingSounds
{
    public static void Postfix(Shotgun __instance)
    {
        if (__instance.variation == 0)
        {
            AudioSource source = Traverse.Create(__instance).Field("tempChargeSound").GetValue() as AudioSource;
            if (source != null)
                SoundPackController.SetAudioSourceClip(source, "ShotgunChargingSounds"); 
        }
    }
}

[HarmonyPatch(typeof(Shotgun), "Pump1Sound")]
public static class Inject_ShotgunPump1Sounds
{
    public static bool Prefix(Shotgun __instance)
    {
        SoundPackController.SetAudioClip(ref __instance.pump1sound, "ShotgunPump1Sounds");
        return true;
    }
}

[HarmonyPatch(typeof(Shotgun), "Pump2Sound")]
public static class Inject_ShotgunPump2Sounds
{
    public static bool Prefix(Shotgun __instance)
    {
        SoundPackController.SetAudioClip(ref __instance.pump2sound, "ShotgunPump2Sounds");
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
            SoundPackController.SetAudioSourceClip(sources[0], "RailcannonRedWindDownSounds");
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
        SoundPackController.SetAudioSourceClip((AudioSource)Traverse.Create(__instance).Field("fullAud").GetValue(), "RailcannonIdleSounds" + __instance.variation);
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