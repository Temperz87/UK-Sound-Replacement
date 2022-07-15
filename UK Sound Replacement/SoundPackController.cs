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


        result.AddAspect(new SoundAspect("NailgunShoot", "NailgunSounds", true));

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
            Plugin.Log("Tried to set current soundpack to " + name + " but it wasn't found!");
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
            Plugin.Log("Got a null audiosource while handling " + name);
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
                        Plugin.Log("couldn't load preview image " + info.FullName + "\\preview.png");
                        Plugin.Log(www.error);
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
                    Plugin.Log("couldn't load clip " + soundUrl);
                    Plugin.Log(www.error);
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