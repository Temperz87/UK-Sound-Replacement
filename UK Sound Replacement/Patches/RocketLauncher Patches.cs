using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(RocketLauncher), "Start")]
public static class Inject_RocketLauncherSounds
{
    public static void Postfix(RocketLauncher __instance)
    {
        Traverse rocketTraverse = Traverse.Create(__instance);
        if (__instance.variation == 0)
        {
            SoundPackController.SetAudioSourceClip(rocketTraverse.Field("timerTickSound").GetValue() as AudioSource, "RocketLauncherTickSounds", SoundPackController.SoundPackType.RocketLauncher);
            SoundPackController.SetAudioSourceClip(rocketTraverse.Field("timerWindupSound").GetValue() as AudioSource, "RocketLauncherWindUpSounds", SoundPackController.SoundPackType.RocketLauncher);
        }
    }
}

[HarmonyPatch(typeof(RocketLauncher), "Clunk")]
public static class Inject_RocketLauncherClunkSounds
{
    public static void Prefix(RocketLauncher __instance)
    {
        SoundPackController.SetAudioSourceClip(__instance.clunkSound.GetComponent<AudioSource>(), "RocketLauncherClunkSounds" + __instance.variation, SoundPackController.SoundPackType.RocketLauncher);
    }
}


[HarmonyPatch(typeof(RocketLauncher), "Shoot")]
public static class Inject_RocketLauncherShootSounds
{
    public static void Prefix(RocketLauncher __instance)
    {
        SoundPackController.SetAudioSourceClip(__instance.GetComponent<AudioSource>(), "RocketLauncherShootSounds" + __instance.variation, SoundPackController.SoundPackType.RocketLauncher);
        SoundPackController.SetAudioSourceClip(__instance.rocket.GetComponent<AudioSource>(), "RocketLauncherExhaustSounds" + __instance.variation, SoundPackController.SoundPackType.RocketLauncher);
    }
}

[HarmonyPatch(typeof(RocketLauncher), "FreezeRockets")]
public static class Inject_RocketLauncherFreezeSounds
{
    public static void Prefix(RocketLauncher __instance)
    {
        Traverse rocketTraverse = Traverse.Create(__instance);
        SoundPackController.SetAudioSourceClip(rocketTraverse.Field("timerFreezeSound").GetValue() as AudioSource, "RocketLauncherFreezeSounds", SoundPackController.SoundPackType.RocketLauncher);
    }
}

[HarmonyPatch(typeof(RocketLauncher), "UnfreezeRockets")]
public static class Inject_RocketLauncherUnFreezeSounds
{
    public static void Prefix(RocketLauncher __instance)
    {
        Traverse rocketTraverse = Traverse.Create(__instance);
        SoundPackController.SetAudioSourceClip(rocketTraverse.Field("timerUnfreezeSound").GetValue() as AudioSource, "RocketLauncherUnfreezeSounds", SoundPackController.SoundPackType.RocketLauncher);
    }
}