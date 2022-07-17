using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(Shotgun), "Start")]
public static class Inject_ShotgunSounds
{
    public static void Postfix(Shotgun __instance)
    {
        SoundPackController.SetAudioSourceClip(__instance.pumpChargeSound.GetComponent<AudioSource>(), "ShotgunCharge", SoundPackController.SoundPackType.Shotgun);
        SoundPackController.SetAudioSourceClip(__instance.warningBeep.GetComponent<AudioSource>(), "OverCharged", SoundPackController.SoundPackType.Shotgun);
        SoundPackController.SetAudioSourceClip(__instance.chargeSoundBubble.GetComponent<AudioSource>(), "ShotgunChargeLoop", SoundPackController.SoundPackType.Shotgun);
        AudioSource heatSinkAud = (AudioSource)Traverse.Create(__instance).Field("heatSinkAud").GetValue();
        if (heatSinkAud == null)
            heatSinkAud = __instance.heatSinkSMR.GetComponent<AudioSource>();
        SoundPackController.SetAudioSourceClip(heatSinkAud, "HeatHiss", SoundPackController.SoundPackType.Shotgun);
        SoundPackController.SetAudioClip(ref __instance.clickChargeSound, "CoreEjectFlick", SoundPackController.SoundPackType.Shotgun);
        SoundPackController.SetAudioClip(ref __instance.smackSound, "CoreEjectReload", SoundPackController.SoundPackType.Shotgun);
        SoundPackController.SetAudioClip(ref __instance.clickSound, "MainShotReload", SoundPackController.SoundPackType.Shotgun);
    }
}

[HarmonyPatch(typeof(Shotgun), "Shoot")]
public static class Inject_ShotgunShootSounds
{
    public static bool Prefix(Shotgun __instance)
    {
        SoundPackController.SetAudioClip(ref __instance.shootSound, "ShotgunShootSounds" + __instance.variation, SoundPackController.SoundPackType.Shotgun);
        return true;
    }
}

[HarmonyPatch(typeof(Shotgun), "ShootSinks")]
public static class Inject_ShotgunShootHeatSinkSounds
{
    public static bool Prefix(Shotgun __instance)
    {
        SoundPackController.SetAudioSourceClip(__instance.grenadeSoundBubble.GetComponent<AudioSource>(), "CoreEject", SoundPackController.SoundPackType.Shotgun);
        SoundPackController.SetAudioClip(ref __instance.shootSound, "ShotgunShootSounds" + __instance.variation, SoundPackController.SoundPackType.Shotgun);
        return true;
    }
}

[HarmonyPatch(typeof(Shotgun), "Pump1Sound")]
public static class Inject_ShotgunPump1Sounds
{
    public static bool Prefix(Shotgun __instance)
    {
        SoundPackController.SetAudioClip(ref __instance.pump1sound, "ShotgunPump1", SoundPackController.SoundPackType.Shotgun);
        return true;
    }
}

[HarmonyPatch(typeof(Shotgun), "Pump2Sound")]
public static class Inject_ShotgunPump2Sounds
{
    public static bool Prefix(Shotgun __instance)
    {
        SoundPackController.SetAudioClip(ref __instance.pump2sound, "ShotgunPump2", SoundPackController.SoundPackType.Shotgun);
        return true;
    }
}