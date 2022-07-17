using BepInEx;
using HarmonyLib;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public static class SoundPackController
{
    //public static string currentSoundPackName = "Stock";

    private static Dictionary<string, SoundPack> allSoundPacks = new Dictionary<string, SoundPack>();

    //private static SoundPack currentSoundPack = null;

    public static SoundPack revolverSoundPack = null;
    public static SoundPack shotgunSoundPack = null;
    public static SoundPack nailgunSoundPack = null;
    public static SoundPack railcannonSoundPack = null;

    private static List<string> stockLoadedAspects = new List<string>();

    public static SoundPack CreateNewSoundPack(string name) // The new new sound aspect system is just easier on me for adding and removing sounds, I could hardcode it but I don't really want to
    {
        SoundPack result = new SoundPack(name);

        // Railcannon
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

        // Revolver
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

        // Shotgun
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

        // Nailgun
        result.AddAspect(new SoundAspect("BarrelSpin1False", "BarrelSpin", "NailgunSounds\\NailgunBlue"));
        result.AddAspect(new SoundAspect("NailgunLastShot1False", "NailgunLastShot", "NailgunSounds\\NailgunBlue"));
        result.AddAspect(new SoundAspect("NailgunShot1False", "NailgunShot", "NailgunSounds\\NailgunBlue"));
        result.AddAspect(new SoundAspect("NewCharge1False", "NewCharge", "NailgunSounds\\NailgunBlue"));
        result.AddAspect(new SoundAspect("NoAmmoClick1False", "NoAmmoClick", "NailgunSounds\\NailgunBlue"));

        result.AddAspect(new SoundAspect("MagnetBeep1False", "MagnetBeep", "NailgunSounds\\NailgunBlueAlt"));
        result.AddAspect(new SoundAspect("MagnetBreak1False", "MagnetBreak", "NailgunSounds\\NailgunBlueAlt"));
        result.AddAspect(new SoundAspect("MagnetHit1False", "MagnetHit", "NailgunSounds\\NailgunBlueAlt"));
        result.AddAspect(new SoundAspect("MagnetHitEnemy1False", "MagnetHitEnemy", "NailgunSounds\\NailgunBlueAlt"));

        result.AddAspect(new SoundAspect("MagnetBeep1True", "MagnetBeep", "NailgunSounds\\NailgunBlueAlt"));
        result.AddAspect(new SoundAspect("MagnetBreak1True", "MagnetBreak", "NailgunSounds\\NailgunBlueAlt"));
        result.AddAspect(new SoundAspect("MagnetHit1True", "MagnetHit", "NailgunSounds\\NailgunBlueAlt"));
        result.AddAspect(new SoundAspect("MagnetHitEnemy1True", "MagnetHitEnemy", "NailgunSounds\\NailgunBlueAlt"));

        result.AddAspect(new SoundAspect("BarrelSpin0False", "BarrelSpin", "NailgunSounds\\NailgunGreen"));
        result.AddAspect(new SoundAspect("NailgunShot0False", "NailgunShot", "NailgunSounds\\NailgunGreen"));
        result.AddAspect(new SoundAspect("NewCharge0False", "NewCharge", "NailgunSounds\\NailgunGreen"));

        result.AddAspect(new SoundAspect("BarrelSpin1True", "BarrelSpin", "NailgunSounds\\NailgunBlueAlt"));
        result.AddAspect(new SoundAspect("NailgunLastShot1True", "NailgunLastShot", "NailgunSounds\\NailgunBlueAlt"));
        result.AddAspect(new SoundAspect("NailgunShot1True", "NailgunShot", "NailgunSounds\\NailgunBlueAlt"));
        result.AddAspect(new SoundAspect("NailgunSnap1True", "NailgunSnap", "NailgunSounds\\NailgunBlueAlt"));
        result.AddAspect(new SoundAspect("NewCharge1True", "NewCharge", "NailgunSounds\\NailgunBlueAlt"));
        result.AddAspect(new SoundAspect("NoAmmoClick1True", "NoAmmoClick", "NailgunSounds\\NailgunBlueAlt"));

        result.AddAspect(new SoundAspect("BarrelSpin0True", "BarrelSpin", "NailgunSounds\\NailgunGreenAlt"));
        result.AddAspect(new SoundAspect("NailgunShot0True", "NailgunShot", "NailgunSounds\\NailgunGreenAlt"));
        result.AddAspect(new SoundAspect("NailgunSnap0True", "NailgunSnap", "NailgunSounds\\NailgunGreenAlt"));
        result.AddAspect(new SoundAspect("NewCharge0True", "NewCharge", "NailgunSounds\\NailgunGreenAlt"));

        result.AddAspect(new SoundAspect("NailgunShotOverheat0False", "NailgunShotOverheat", "NailgunSounds\\NailgunGreen"));
        result.AddAspect(new SoundAspect("NailgunShotOverheat0True", "NailgunShotOverheat", "NailgunSounds\\NailgunGreenAlt"));

        result.AddAspect(new SoundAspect("HeatSteam1False", "HeatSteam", "NailgunSounds\\NailgunBlue"));
        result.AddAspect(new SoundAspect("HeatSteam0False", "HeatSteam", "NailgunSounds\\NailgunGreen"));
        result.AddAspect(new SoundAspect("NailZap1False", "NailZap", "NailgunSounds\\NailgunBlue"));
        result.AddAspect(new SoundAspect("NailZap0False", "NailZap", "NailgunSounds\\NailgunGreen"));

        result.AddAspect(new SoundAspect("SawBreak1True", "SawBreak", "NailgunSounds\\NailgunBlueAlt"));
        result.AddAspect(new SoundAspect("SawBreak0True", "SawBreak", "NailgunSounds\\NailgunGreenAlt"));
        result.AddAspect(new SoundAspect("SawBounce1True", "SawBounce", "NailgunSounds\\NailgunBlueAlt"));
        result.AddAspect(new SoundAspect("SawBounce0True", "SawBounce", "NailgunSounds\\NailgunGreenAlt"));

        allSoundPacks.Add(name, result);
        return result;
    }

    public static SoundPack[] GetAllSoundPacks()
    {
        return (SoundPack[])allSoundPacks.Values.ToArray().Clone();
    }

    public static void SetCurrentSoundPack(string name, SoundPackType type)
    {
        if (name == "")
            name = "Stock";
        if (allSoundPacks.ContainsKey(name))
        {
            SoundPack toSet = allSoundPacks[name];
            //currentSoundPack = allSoundPacks[name];
            //currentSoundPackName = name;
            switch (type)
            {
                case SoundPackType.Revolver:
                    revolverSoundPack = toSet;
                    break;
                case SoundPackType.Shotgun:
                    shotgunSoundPack = toSet;
                    break;
                case SoundPackType.Nailgun:
                    nailgunSoundPack = toSet;
                    break;
                case SoundPackType.Railcannon:
                    railcannonSoundPack = toSet;
                    break;
                case SoundPackType.All:
                    revolverSoundPack = toSet;
                    shotgunSoundPack = toSet;
                    nailgunSoundPack = toSet;
                    railcannonSoundPack = toSet;
                    break;
            }
        }
        else
        {
            Debug.Log("Tried to set current soundpack to " + name + " but it wasn't found!");
            SetCurrentSoundPack("Stock", type);
            return;
        }
        foreach (Revolver r in Resources.FindObjectsOfTypeAll<Revolver>())
            Inject_RevolverSounds.Postfix(r);
        foreach (Railcannon r in Resources.FindObjectsOfTypeAll<Railcannon>())
            Inject_RailcannonSounds.Postfix(r);
        foreach (Shotgun s in Resources.FindObjectsOfTypeAll<Shotgun>())
            Inject_ShotgunSounds.Postfix(s);
        foreach (Nailgun n in Resources.FindObjectsOfTypeAll<Nailgun>())
            Inject_NailgunSounds.Postfix(n);
    }

    public static SoundPack RetrieveSoundPackByType(SoundPackType type)
    {
        switch (type)
        {
            case SoundPackType.Revolver:
                return revolverSoundPack;
            case SoundPackType.Shotgun:
                return shotgunSoundPack;
            case SoundPackType.Nailgun:
                return nailgunSoundPack;
            case SoundPackType.Railcannon:
                return railcannonSoundPack;
        }
        return null;
    }

    public static void GetAllAudioClips(ref AudioClip[] clips, string name, SoundPackType type)
    {
        if (!stockLoadedAspects.Contains(name))
        {
            stockLoadedAspects.Add(name);
            foreach (AudioClip clip in clips)
                allSoundPacks["Stock"].GetAspect(name).allClips.Add(clip);
        }
        SoundPack pack = RetrieveSoundPackByType(type);
        if (pack != null)
        {
            AudioClip[] allClips = pack.GetAllAudioClipsOfAspect(name);
            if (allClips != null)
                clips = allClips;
        }
    }

    public static void SetAudioSourceClip(AudioSource source, string name, SoundPackType type, bool playSource = false)
    {
        if (source == null)
        {
            Debug.Log("Got a null audiosource while handling " + name);
            return;
        }
        if (name != "Random" && !stockLoadedAspects.Contains(name) && source.clip != null)
        {
            stockLoadedAspects.Add(name);
            allSoundPacks["Stock"].GetAspect(name).allClips.Add(source.clip);
        }
        SoundPack pack = RetrieveSoundPackByType(type);
        if (pack != null)
        {
            AudioClip clip = pack.GetRandomClipFromAspect(name);
            if (clip != null)
            {
                source.clip = clip;
                if (playSource)
                    source.Play();
            }
        }
    }

    public static void SetAudioClip(ref AudioClip clip, string name, SoundPackType type)
    {
        if (!stockLoadedAspects.Contains(name) && clip != null)
        {
            stockLoadedAspects.Add(name);
            allSoundPacks["Stock"].GetAspect(name).allClips.Add(clip);
        }
        SoundPack pack = RetrieveSoundPackByType(type);
        if (pack != null)
        {
            AudioClip newClip = pack.GetRandomClipFromAspect(name);
            if (newClip != null)
                clip = newClip;
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

        public IEnumerator LoadFromDirectory(DirectoryInfo info, MonoBehaviour caller)
        {
            foreach (SoundAspect aspect in allAspects.Values)
            {
                foreach (FileInfo file in new DirectoryInfo(info.FullName + aspect.path).GetFiles("*.wav", SearchOption.AllDirectories))
                    caller.StartCoroutine(StartNewWWW(aspect, file.FullName, AudioType.WAV));
                foreach (FileInfo file in new DirectoryInfo(info.FullName + aspect.path).GetFiles("*.mp3", SearchOption.AllDirectories))
                    caller.StartCoroutine(StartNewWWW(aspect, file.FullName, AudioType.MPEG));
                foreach (FileInfo file in new DirectoryInfo(info.FullName + aspect.path).GetFiles("*.ogg", SearchOption.AllDirectories))
                    caller.StartCoroutine(StartNewWWW(aspect, file.FullName, AudioType.OGGVORBIS));
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

        private IEnumerator StartNewWWW(SoundAspect aspect, string soundUrl, AudioType type)
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(soundUrl, type))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError)
                {
                    Debug.Log("couldn't load clip " + soundUrl);
                    Debug.Log(www.error);
                }
                else
                {
                    aspect.allClips.Add(DownloadHandlerAudioClip.GetContent(www));
                }
            }
        }

        public void AddAspect(SoundAspect toAdd)
        {
            allAspects.Add(toAdd.name, toAdd);
        }

        public AudioClip GetRandomClipFromAspect(string name)
        {
            if (name == "Random")
            {
                SoundAspect randomAspect = allAspects.Values.ToList()[Random.Range(0, allAspects.Values.Count - 1)];
                while (randomAspect.allClips.Count <= 0)
                    randomAspect = allAspects.Values.ToList()[Random.Range(0, allAspects.Values.Count - 1)];
                if (randomAspect.allClips.Count == 1)
                    return randomAspect.allClips[0];
                return randomAspect.allClips[Random.Range(0, randomAspect.allClips.Count - 1)];
            }
            if (!allAspects.ContainsKey(name) || allAspects[name].allClips.Count <= 0)
                return null;
            if (!allAspects.ContainsKey(name))
            {
                Debug.LogError("Could not get aspect " + name);
                return null;
            }
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

    public enum SoundPackType
    {
        Revolver,
        Shotgun,
        Nailgun,
        Railcannon,
        All
    }
}