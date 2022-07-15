using HarmonyLib;
using UnityEngine;

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