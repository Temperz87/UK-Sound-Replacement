﻿using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(Railcannon), "Shoot")]
public static class Inject_RailcannonShootSounds
{
    public static bool Prefix(Railcannon __instance)
    {
        if (__instance.variation != 2)
            SoundPackController.SetAudioSourceClip(__instance.fireSound.GetComponent<AudioSource>(), "RailcannonShootSounds" + __instance.variation, SoundPackController.SoundPackType.Railcannon);
        else if (__instance.variation == 2)
        {
            AudioSource[] sources = __instance.fireSound.GetComponents<AudioSource>();
            SoundPackController.SetAudioSourceClip(sources[0], "RailcannonRedWindDownSounds2", SoundPackController.SoundPackType.Railcannon);
            SoundPackController.SetAudioSourceClip(sources[1], "RailcannonShootSounds2", SoundPackController.SoundPackType.Railcannon);
            SoundPackController.SetAudioSourceClip(sources[2], "RailcannonShootSounds2", SoundPackController.SoundPackType.Railcannon);
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
        SoundPackController.SetAudioSourceClip(fullAud, "RailcannonIdleSounds" + __instance.variation, SoundPackController.SoundPackType.Railcannon, true);
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
                SoundPackController.SetAudioSourceClip(source, "RailcannonWhirSounds" + ((Railcannon)Traverse.Create(__instance).Field("rc").GetValue()).variation, SoundPackController.SoundPackType.Railcannon, false);
            else
                SoundPackController.SetAudioSourceClip(source, "RailcannonClickSounds" + ((Railcannon)Traverse.Create(__instance).Field("rc").GetValue()).variation, SoundPackController.SoundPackType.Railcannon);
        }
    }
}

[HarmonyPatch(typeof(WeaponCharges), "PlayRailCharge")]
public static class Inject_RailcannonChargedSounds
{
    public static bool Prefix(WeaponCharges __instance)
    {
        SoundPackController.SetAudioSourceClip(__instance.railCannonFullChargeSound.GetComponent<AudioSource>(), "RailcannonChargedSounds", SoundPackController.SoundPackType.Railcannon);
        return true;
    }
}