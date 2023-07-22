using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public static class SoundPackController
{
    public static string persistentLoopName = "";
    public static string persistentIntroName = "";
    //public static List<ClipData> cgMusic { get; private set; } = new List<ClipData>();
    //public static List<ClipData> cgMusicIntro { get; private set; } = new List<ClipData>();
    public static List<SoundtrackSong> allCGSongs = new List<SoundtrackSong>();
    private static Dictionary<string, SoundPack> allSoundPacks = new Dictionary<string, SoundPack>();

    public static SoundPack revolverSoundPack = null;
    public static SoundPack shotgunSoundPack = null;
    public static SoundPack nailgunSoundPack = null;
    public static SoundPack railcannonSoundPack = null;
    public static SoundPack rocketLauncherSoundPack = null;

    private static List<string> stockLoadedAspects = new List<string>();
    private static List<string> stockLoadedClips = new List<string>();

    public static SoundPack CreateNewSoundPack(string name) // The new sound aspect system is just easier on me for adding and removing sounds, I could hardcode it but I don't really want to
    {
        SoundPack result = new SoundPack(name);

        // Railcannon
        result.AddAspect(new SoundAspect("RailcannonChargedSounds", "RailcannonSounds", true, SoundPackType.Railcannon));

        // Blue
        result.AddAspect(new SoundAspect("RailcannonClickSounds0", "RailcannonSounds/RailcannonBlue", false, SoundPackType.Railcannon));
        result.AddAspect(new SoundAspect("RailcannonIdleSounds0", "RailcannonSounds/RailcannonBlue", false, SoundPackType.Railcannon));
        result.AddAspect(new SoundAspect("RailcannonShootSounds0", "RailcannonSounds/RailcannonBlue", false, SoundPackType.Railcannon));
        result.AddAspect(new SoundAspect("RailcannonWhirSounds0", "RailcannonSounds/RailcannonBlue", false, SoundPackType.Railcannon));

        // Green
        result.AddAspect(new SoundAspect("RailcannonClickSounds1", "RailcannonSounds/RailcannonGreen", false, SoundPackType.Railcannon));
        result.AddAspect(new SoundAspect("RailcannonIdleSounds1", "RailcannonSounds/RailcannonGreen", false, SoundPackType.Railcannon));
        result.AddAspect(new SoundAspect("RailcannonShootSounds1", "RailcannonSounds/RailcannonGreen", false, SoundPackType.Railcannon));
        result.AddAspect(new SoundAspect("RailcannonWhirSounds1", "RailcannonSounds/RailcannonGreen", false, SoundPackType.Railcannon));

        // Red
        result.AddAspect(new SoundAspect("RailcannonClickSounds2", "RailcannonSounds/RailcannonRed", false, SoundPackType.Railcannon));
        result.AddAspect(new SoundAspect("RailcannonIdleSounds2", "RailcannonSounds/RailcannonRed", false, SoundPackType.Railcannon));
        result.AddAspect(new SoundAspect("RailcannonShootSounds2", "RailcannonSounds/RailcannonRed", false, SoundPackType.Railcannon));
        result.AddAspect(new SoundAspect("RailcannonRedWindDownSounds2", "RailcannonSounds/RailcannonRed", false, SoundPackType.Railcannon));
        result.AddAspect(new SoundAspect("RailcannonWhirSounds2", "RailcannonSounds/RailcannonRed", false, SoundPackType.Railcannon));

        // Revolver

        // Blue
        result.AddAspect(new SoundAspect("ChargeLoop0False", "ChargeLoop", "RevolverSounds/RevolverPiercer", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("ChargeReady0False", "ChargeReady", "RevolverSounds/RevolverPiercer", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("ChargingUp0False", "ChargingUp", "RevolverSounds/RevolverPiercer", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("ClickCancel0False", "ClickCancel", "RevolverSounds/RevolverPiercer", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("RevolverShootSounds0False", "ShootSounds", "RevolverSounds/RevolverPiercer", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("RevolverSuperShootSounds0False", "SuperShootSounds", "RevolverSounds/RevolverPiercer", SoundPackType.Revolver));

        // Blue alt
        result.AddAspect(new SoundAspect("ChargeLoop0True", "ChargeLoop", "RevolverSounds/RevolverPiercerAlt", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("ChargeReady0True", "ChargeReady", "RevolverSounds/RevolverPiercerAlt", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("ChargingUp0True", "ChargingUp", "RevolverSounds/RevolverPiercerAlt", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("ClickCancel0True", "ClickCancel", "RevolverSounds/RevolverPiercerAlt", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("RevolverShootSounds0True", "ShootSounds", "RevolverSounds/RevolverPiercerAlt", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("RevolverSuperShootSounds0True", "SuperShootSounds", "RevolverSounds/RevolverPiercerAlt", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("HammerClick0", "HammerClick", "RevolverSounds/RevolverPiercerAlt", SoundPackType.Revolver));

        // Green
        result.AddAspect(new SoundAspect("CoinBreak1False", "CoinTwirl", "RevolverSounds/RevolverMarksman", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("CoinFlashLoop1False", "CoinFlashLoop", "RevolverSounds/RevolverMarksman", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("CoinFlip1False", "CoinFlip", "RevolverSounds/RevolverMarksman", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("CoinReady1False", "CoinReady", "RevolverSounds/RevolverMarksman", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("CoinSpin1False", "CoinSpin", "RevolverSounds/RevolverMarksman", SoundPackType.Revolver));
        //result.AddAspect(new SoundAspect("CoinTwirl1False", "CoinTwirl", "RevolverSounds/RevolverMarksman"));
        result.AddAspect(new SoundAspect("CoinRicochet1False", "Ricochet", "RevolverSounds/RevolverMarksman", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("RevolverShootSounds1False", "ShootSounds", "RevolverSounds/RevolverMarksman", SoundPackType.Revolver));

        // Green alt
        result.AddAspect(new SoundAspect("CoinBreak1True", "CoinTwirl", "RevolverSounds/RevolverMarksmanAlt", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("CoinFlashLoop1True", "CoinFlashLoop", "RevolverSounds/RevolverMarksmanAlt", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("CoinFlip1True", "CoinFlip", "RevolverSounds/RevolverMarksmanAlt", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("CoinReady1True", "CoinReady", "RevolverSounds/RevolverMarksmanAlt", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("CoinSpin1True", "CoinSpin", "RevolverSounds/RevolverMarksmanAlt", SoundPackType.Revolver));
        //result.AddAspect(new SoundAspect("CoinTwirl1True", "CoinTwirl", "RevolverSounds/RevolverMarksmanAlt"));
        result.AddAspect(new SoundAspect("CoinRicochet1True", "Ricochet", "RevolverSounds/RevolverMarksmanAlt", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("RevolverShootSounds1True", "ShootSounds", "RevolverSounds/RevolverMarksmanAlt", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("HammerClick1", "HammerClick", "RevolverSounds/RevolverMarksmanAlt", SoundPackType.Revolver));

        // Red
        result.AddAspect(new SoundAspect("RevolverShootSounds2False", "ShootSounds", "RevolverSounds/RevolverSharpshooter", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("RevolverSuperShootSounds2False", "SuperShootSounds", "RevolverSounds/RevolverSharpshooter", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("TwirlChargedFalse", "TwirlCharged", "RevolverSounds/RevolverSharpshooter", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("TwirlSoundFalse", "TwirlSound", "RevolverSounds/RevolverSharpshooter", SoundPackType.Revolver));

        // Red alt
        result.AddAspect(new SoundAspect("RevolverShootSounds2True", "ShootSounds", "RevolverSounds/RevolverSharpshooterAlt", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("RevolverSuperShootSounds2True", "SuperShootSounds", "RevolverSounds/RevolverSharpshooterAlt", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("TwirlChargedTrue", "TwirlCharged", "RevolverSounds/RevolverSharpshooterAlt", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("TwirlSoundTrue", "TwirlSound", "RevolverSounds/RevolverSharpshooterAlt", SoundPackType.Revolver));
        result.AddAspect(new SoundAspect("HammerClick2", "HammerClick", "RevolverSounds/RevolverSharpshooterAlt", SoundPackType.Revolver));

        // Shotgun

        // Blue
        result.AddAspect(new SoundAspect("ShotgunShootSounds0", "ShotgunShootSounds", "ShotgunSounds/ShotgunBlue", SoundPackType.Shotgun));
        result.AddAspect(new SoundAspect("ShotgunChargeLoop", "ChargeLoop", "ShotgunSounds/ShotgunBlue", SoundPackType.Shotgun));
        result.AddAspect(new SoundAspect("CoreEject", "ShotgunSounds/ShotgunBlue", true, SoundPackType.Shotgun));
        result.AddAspect(new SoundAspect("CoreEjectFlick", "ShotgunSounds/ShotgunBlue", true, SoundPackType.Shotgun));
        result.AddAspect(new SoundAspect("CoreEjectReload", "ShotgunSounds/ShotgunBlue", true, SoundPackType.Shotgun));
        result.AddAspect(new SoundAspect("MainShotReload", "ShotgunSounds/ShotgunBlue", true, SoundPackType.Shotgun));
        result.AddAspect(new SoundAspect("HeatHiss", "ShotgunSounds/ShotgunBlue", true, SoundPackType.Shotgun));

        // Green
        result.AddAspect(new SoundAspect("ShotgunShootSounds1", "ShotgunShootSounds", "ShotgunSounds/ShotgunGreen", SoundPackType.Shotgun));
        result.AddAspect(new SoundAspect("ShotgunCharge", "ShotgunSounds/ShotgunGreen", true, SoundPackType.Shotgun));
        result.AddAspect(new SoundAspect("ShotgunPump1", "ShotgunSounds/ShotgunGreen", true, SoundPackType.Shotgun));
        result.AddAspect(new SoundAspect("ShotgunPump2", "ShotgunSounds/ShotgunGreen", true, SoundPackType.Shotgun));
        result.AddAspect(new SoundAspect("OverCharged", "ShotgunSounds/ShotgunGreen", true, SoundPackType.Shotgun));

        // Nailgun

        // Blue
        result.AddAspect(new SoundAspect("BarrelSpin1False", "BarrelSpin", "NailgunSounds/NailgunBlue", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NailgunLastShot1False", "NailgunLastShot", "NailgunSounds/NailgunBlue", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NailgunShot1False", "NailgunShot", "NailgunSounds/NailgunBlue", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NewCharge1False", "NewCharge", "NailgunSounds/NailgunBlue", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NoAmmoClick1False", "NoAmmoClick", "NailgunSounds/NailgunBlue", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("MagnetBeep1False", "MagnetBeep", "NailgunSounds/NailgunBlueAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("MagnetBreak1False", "MagnetBreak", "NailgunSounds/NailgunBlueAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("MagnetHit1False", "MagnetHit", "NailgunSounds/NailgunBlueAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("MagnetHitEnemy1False", "MagnetHitEnemy", "NailgunSounds/NailgunBlueAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NailZap1False", "NailZap", "NailgunSounds/NailgunBlue", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("HeatSteam1False", "HeatSteam", "NailgunSounds/NailgunBlue", SoundPackType.Nailgun));

        // Blue alt
        result.AddAspect(new SoundAspect("MagnetBeep1True", "MagnetBeep", "NailgunSounds/NailgunBlueAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("MagnetBreak1True", "MagnetBreak", "NailgunSounds/NailgunBlueAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("MagnetHit1True", "MagnetHit", "NailgunSounds/NailgunBlueAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("MagnetHitEnemy1True", "MagnetHitEnemy", "NailgunSounds/NailgunBlueAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("BarrelSpin1True", "BarrelSpin", "NailgunSounds/NailgunBlueAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NailgunLastShot1True", "NailgunLastShot", "NailgunSounds/NailgunBlueAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NailgunShot1True", "NailgunShot", "NailgunSounds/NailgunBlueAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NailgunSnap1True", "NailgunSnap", "NailgunSounds/NailgunBlueAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NewCharge1True", "NewCharge", "NailgunSounds/NailgunBlueAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NoAmmoClick1True", "NoAmmoClick", "NailgunSounds/NailgunBlueAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("SawBreak1True", "SawBreak", "NailgunSounds/NailgunBlueAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("SawBounce1True", "SawBounce", "NailgunSounds/NailgunBlueAlt", SoundPackType.Nailgun));

        // Green
        result.AddAspect(new SoundAspect("BarrelSpin0False", "BarrelSpin", "NailgunSounds/NailgunGreen", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NailgunShot0False", "NailgunShot", "NailgunSounds/NailgunGreen", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NewCharge0False", "NewCharge", "NailgunSounds/NailgunGreen", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NailgunShotOverheat0False", "NailgunShotOverheat", "NailgunSounds/NailgunGreen", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NailZap0False", "NailZap", "NailgunSounds/NailgunGreen", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("HeatSteam0False", "HeatSteam", "NailgunSounds/NailgunGreen", SoundPackType.Nailgun));

        // Green alt
        result.AddAspect(new SoundAspect("BarrelSpin0True", "BarrelSpin", "NailgunSounds/NailgunGreenAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NailgunShot0True", "NailgunShot", "NailgunSounds/NailgunGreenAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NailgunSnap0True", "NailgunSnap", "NailgunSounds/NailgunGreenAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NewCharge0True", "NewCharge", "NailgunSounds/NailgunGreenAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("NailgunShotOverheat0True", "NailgunShotOverheat", "NailgunSounds/NailgunGreenAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("SawBreak0True", "SawBreak", "NailgunSounds/NailgunGreenAlt", SoundPackType.Nailgun));
        result.AddAspect(new SoundAspect("SawBounce0True", "SawBounce", "NailgunSounds/NailgunGreenAlt", SoundPackType.Nailgun));


        // Rocket Launcher blue
        result.AddAspect(new SoundAspect("RocketLauncherClunkSounds0", "RocketLauncherClunkSounds", "RocketLauncherSounds/RocketLauncherBlue", SoundPackType.RocketLauncher));
        result.AddAspect(new SoundAspect("RocketLauncherExhaustSounds0", "RocketLauncherExhaustSounds", "RocketLauncherSounds/RocketLauncherBlue", SoundPackType.RocketLauncher));
        result.AddAspect(new SoundAspect("RocketLauncherFreezeSounds", "RocketLauncherFreezeSounds", "RocketLauncherSounds/RocketLauncherBlue", SoundPackType.RocketLauncher));
        result.AddAspect(new SoundAspect("RocketLauncherShootSounds0", "RocketLauncherShootSounds", "RocketLauncherSounds/RocketLauncherBlue", SoundPackType.RocketLauncher));
        result.AddAspect(new SoundAspect("RocketLauncherTickSounds", "RocketLauncherTickSounds", "RocketLauncherSounds/RocketLauncherBlue", SoundPackType.RocketLauncher));
        result.AddAspect(new SoundAspect("RocketLauncherUnfreezeSounds", "RocketLauncherUnfreezeSounds", "RocketLauncherSounds/RocketLauncherBlue", SoundPackType.RocketLauncher));
        result.AddAspect(new SoundAspect("RocketLauncherWindUpSounds", "RocketLauncherWindUpSounds", "RocketLauncherSounds/RocketLauncherBlue", SoundPackType.RocketLauncher));

        // Rocket Launcher Green
        result.AddAspect(new SoundAspect("RocketLauncherClunkSounds1", "RocketLauncherClunkSounds", "RocketLauncherSounds/RocketLauncherGreen", SoundPackType.RocketLauncher));
        result.AddAspect(new SoundAspect("RocketLauncherExhaustSounds1", "RocketLauncherExhaustSounds", "RocketLauncherSounds/RocketLauncherGreen", SoundPackType.RocketLauncher));
        result.AddAspect(new SoundAspect("RocketLauncherShootSounds1", "RocketLauncherShootSounds", "RocketLauncherSounds/RocketLauncherGreen", SoundPackType.RocketLauncher));
        result.AddAspect(new SoundAspect("RocketLauncherCannonballShootSounds", "RocketLauncherCannonballShootSounds", "RocketLauncherSounds/RocketLauncherGreen", SoundPackType.RocketLauncher));
        result.AddAspect(new SoundAspect("RocketLauncherCannonballChargeUp", "RocketLauncherCannonballChargeUp", "RocketLauncherSounds/RocketLauncherGreen", SoundPackType.RocketLauncher));
        result.AddAspect(new SoundAspect("RocketLauncherCannonballTimerWindUp", "RocketLauncherCannonballTimerWindUp", "RocketLauncherSounds/RocketLauncherGreen", SoundPackType.RocketLauncher));
        result.AddAspect(new SoundAspect("RocketLauncherCannonballBounce", "RocketLauncherCannonballBounce", "RocketLauncherSounds/RocketLauncherGreen", SoundPackType.RocketLauncher));


        allSoundPacks.Add(name, result);
        return result;
    }

    public static SoundPack[] GetAllSoundPacks()
    {
        return (SoundPack[])allSoundPacks.Values.ToArray().Clone();
    }

    public static IEnumerator LoadCgMusic(string path, MonoBehaviour caller)
    {
        DirectoryInfo info = new DirectoryInfo(path + "/CyberGrindMusic");
        if (!info.Exists)
        {
            Directory.CreateDirectory(info.FullName);
            yield break;
        }

        foreach (DirectoryInfo subdirectory in info.GetDirectories())
        {
            FileInfo fInfo = new FileInfo(subdirectory.FullName + "/song.json");
            if (!fInfo.Exists)
                continue;
            // convert fInfo json file into a dictionary

            string songName = "";
            string introClip = "";
            using (StreamReader jFile = fInfo.OpenText())
            {
                Dictionary<string, string> jValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(jFile.ReadToEnd());
                if (jValues.ContainsKey("Song Name"))
                    songName = jValues["Song name"];
                if (!jValues.ContainsKey("Intro"))
                    continue;
                introClip = jValues["Intro"];
            }

            SoundtrackSong song = new SoundtrackSong();

            foreach (FileInfo file in subdirectory.GetFiles("*.wav", SearchOption.AllDirectories))
                caller.StartCoroutine(StartNewWWWCg(file.FullName, AudioType.WAV, introClip, song));

            /*
            info = new DirectoryInfo(path + "/CyberGrindMusic/Loops");
            foreach (FileInfo file in info.GetFiles("*.wav", SearchOption.AllDirectories))
                caller.StartCoroutine(StartNewWWWCg(file.FullName, AudioType.WAV, true));
            foreach (FileInfo file in info.GetFiles("*.mp3", SearchOption.AllDirectories))
                caller.StartCoroutine(StartNewWWWCg(file.FullName, AudioType.MPEG, true));
            foreach (FileInfo file in info.GetFiles("*.ogg", SearchOption.AllDirectories))
                caller.StartCoroutine(StartNewWWWCg(file.FullName, AudioType.OGGVORBIS, true));

            info = new DirectoryInfo(path + "/CyberGrindMusic/Intros");
            foreach (FileInfo file in info.GetFiles("*.wav", SearchOption.AllDirectories))
                caller.StartCoroutine(StartNewWWWCg(file.FullName, AudioType.WAV, false));
            foreach (FileInfo file in info.GetFiles("*.mp3", SearchOption.AllDirectories))
                caller.StartCoroutine(StartNewWWWCg(file.FullName, AudioType.MPEG, false));
            foreach (FileInfo file in info.GetFiles("*.ogg", SearchOption.AllDirectories))
                caller.StartCoroutine(StartNewWWWCg(file.FullName, AudioType.OGGVORBIS, false));
            */
        }
    }
    private static IEnumerator StartNewWWWCg(string path, AudioType type, string introName, SoundtrackSong song)
    {
		// file:// is required for linux/mac - Ali
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, type))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log("couldn't load clip " + path);
                Debug.Log(www.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                if (clip.name == introName)
                    song.introClip = clip;
                else
                    song.clips.Add(clip);
            }
        }
    }

    public static void GetCurrentCybergrindIntroSong(AudioSource source)
    {
        /*if (cgMusicIntro.Count <= 0)
            return;
        if (persistentIntroName == "Random")
        {
            int random = Random.Range(0, cgMusicIntro.Count + 1);
            if (random != cgMusicIntro.Count)  / accounting for stock song
                source.clip = cgMusicIntro[random].clip;
        }
        else if (persistentIntroName == "Stock")
            return;
        else
        {
            foreach (ClipData data in cgMusicIntro)
            {
                if (data.title == persistentIntroName)
                {
                    source.clip = data.clip;
                    break;
                }
            }
        }*/
    }

    public static void GetCurrentCybergrindSong(ref AudioClip clean, ref AudioClip battle, ref AudioClip boss)
    {
        /*
        if (cgMusic.Count <= 0)
            return;
        AudioClip clip = null;
        if (persistentLoopName == "Random")
        {
            int random = (int)Random.Range(0, cgMusic.Count + 1);
            if (random != cgMusic.Count)
                clip = cgMusic[random].clip;
        }
        else if (persistentLoopName == "Stock")
            return;
        else
        {
            foreach (ClipData data in cgMusic)
            {
                if (data.title == persistentLoopName)
                {
                    clip = data.clip;
                    break;
                }
            }
        }
        if (clip == null)
            return;
        clean = clip;
        battle = clean;
        boss = clean;
        */
    }

    public static void SetCurrentSoundPack(string name, SoundPackType type, bool setPersistent = true)
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
                case SoundPackType.RocketLauncher:
                    rocketLauncherSoundPack = toSet;
                    break;
                case SoundPackType.All:
                    revolverSoundPack = toSet;
                    shotgunSoundPack = toSet;
                    nailgunSoundPack = toSet;
                    railcannonSoundPack = toSet;
                    rocketLauncherSoundPack = toSet;
                    break;
            }
        }
        else
        {
            Debug.Log("Tried to set current soundpack to " + name + " but it wasn't found!");
            if (name != "Stock")
                SetCurrentSoundPack("Stock", type);
            return;
        }
        if (setPersistent)
            Plugin.instance.SetSoundPackPersistent(name, type);
        foreach (Revolver r in Resources.FindObjectsOfTypeAll<Revolver>())
            Inject_RevolverSounds.Postfix(r);
        foreach (Railcannon r in Resources.FindObjectsOfTypeAll<Railcannon>())
            Inject_RailcannonSounds.Postfix(r);
        foreach (Shotgun s in Resources.FindObjectsOfTypeAll<Shotgun>())
            Inject_ShotgunSounds.Postfix(s);
        foreach (Nailgun n in Resources.FindObjectsOfTypeAll<Nailgun>())
            Inject_NailgunSounds.Postfix(n);
        foreach (RocketLauncher r in Resources.FindObjectsOfTypeAll<RocketLauncher>())
            Inject_RocketLauncherSounds.Postfix(r, Traverse.Create(r).Field("chargeSound").GetValue() as AudioSource);
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
            case SoundPackType.RocketLauncher:
                return rocketLauncherSoundPack;
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

        SoundPack pack = revolverSoundPack;
        if (type != SoundPackType.All)
            pack = RetrieveSoundPackByType(type);
        if (pack != null)
        {
            AudioClip clip = pack.GetRandomClipFromAspect(name, type);
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
        if (!stockLoadedClips.Contains(name) && clip != null)
        {
            stockLoadedClips.Add(clip.name);
            allSoundPacks["Stock"].GetAspect(name).allClips.Add(clip);
        }
        SoundPack pack = RetrieveSoundPackByType(type);
        if (pack != null)
        {
            AudioClip newClip = pack.GetRandomClipFromAspect(name, type);
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
                if (new DirectoryInfo(info.FullName + aspect.path).Exists)
                {
                    foreach (FileInfo file in new DirectoryInfo(info.FullName + aspect.path).GetFiles("*.wav", SearchOption.AllDirectories))
                        caller.StartCoroutine(StartNewWWW(aspect, file.FullName, AudioType.WAV));
                    foreach (FileInfo file in new DirectoryInfo(info.FullName + aspect.path).GetFiles("*.mp3", SearchOption.AllDirectories))
                        caller.StartCoroutine(StartNewWWW(aspect, file.FullName, AudioType.MPEG));
                    foreach (FileInfo file in new DirectoryInfo(info.FullName + aspect.path).GetFiles("*.ogg", SearchOption.AllDirectories))
                        caller.StartCoroutine(StartNewWWW(aspect, file.FullName, AudioType.OGGVORBIS));
                }
                else
                    Debug.Log("Couldn't find " + info.FullName + aspect.path);
            }

            if (File.Exists(info.FullName + "/preview.png"))
            {
            	// file:// is required for linux/mac - Ali
                using (UnityWebRequest www = UnityWebRequestTexture.GetTexture("file://" + info.FullName + "/preview.png"))
                {
                    yield return www.SendWebRequest();
                    if (www.isNetworkError)
                    {
                        Debug.Log("couldn't load preview image " + info.FullName + "/preview.png");
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
        	// file:// is required for linux/mac - Ali
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + soundUrl, type))
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

        public AudioClip GetRandomClipFromAspect(string name, SoundPackType type)
        {
            if (name == "Random")
            {
                SoundAspect randomAspect = null;
                IEnumerable<SoundAspect> selectedAspects = allAspects.Values.Where(x => x.allClips.Count > 0);
                if (type != SoundPackType.All)
                    selectedAspects = selectedAspects.Where(x => x.type == type);
                if (selectedAspects.Count() <= 0)
                    return null;
                randomAspect = selectedAspects.ElementAt(Random.Range(0, selectedAspects.Count() - 1));
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
            if (!allAspects.ContainsKey(name))
            {
                Debug.Log(name + " was requested to get all audio clips, however it wasn't found!");
                return null;
            }
            if (allAspects[name] == null)
            {
                Debug.Log(name + " was requested to get all audio clips, however it's null!");
                return null;
            }
            return (AudioClip[])allAspects[name].allClips.ToArray().Clone();
        }

        public SoundAspect GetAspect(string name)
        {
            if (!allAspects.ContainsKey(name))
            {
                Debug.Log(name + " was requested as a sound aspect but wasn't found!");
                return null;
            }
            return allAspects[name];
        }
    }

    public class SoundAspect
    {
        public string name;
        public string path;
        public List<AudioClip> allClips = new List<AudioClip>();
        public SoundPackType type;

        public SoundAspect(string name, string path, bool useSameName, SoundPackType type)
        {
            this.name = name;
            this.type = type;
            if (useSameName)
                this.path = "/" + path + "/" + name + "/";
            else
                this.path = "/" + path + "/" + name.Substring(0, name.Length - 1) + "/";
        }

        public SoundAspect(string name, string folderName, string path, SoundPackType type)
        {
            this.name = name;
            this.type = type;
            this.path = "/" + path + "/" + folderName + "/";
        }
    }

    public class ClipData
    {
        public string title;
        public AudioClip clip;
        public Texture2D image;
        public bool isLoop;

        public ClipData(FileInfo info, AudioClip clip, bool isLoop) // thanks https://answers.unity.com/questions/1\461776/how-can-i-access-metadata-from-mp3-such-as-artist.html
        {
            this.clip = clip;
            this.isLoop = isLoop;
            var tfile = TagLib.File.Create(info.FullName);
            //get metadata
            if (tfile.Tag != null)
            {
                if (string.IsNullOrEmpty(tfile.Tag.Title))
                    title = info.Name;
                else
                    title = tfile.Tag.Title;
                if (title.Length > 23)
                    title = title.Substring(0, 23);
                if (tfile.Tag.Pictures.Length > 0)
                {
                    TagLib.IPicture pic = tfile.Tag.Pictures[0];
                    MemoryStream ms = new MemoryStream(pic.Data.Data);
                    ms.Seek(0, SeekOrigin.Begin);
                    image = new Texture2D(2, 2);
                    image.LoadRawTextureData(ms.ToArray());
                }
            }
            else
            {
                title = info.Name;
                if (title.Length > 23)
                    title = title.Substring(0, 23);
            }
        }
    }

    public enum SoundPackType
    {
        Revolver,
        Shotgun,
        Nailgun,
        Railcannon,
        RocketLauncher,
        All
    }
}
