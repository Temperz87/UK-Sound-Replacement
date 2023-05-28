using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(RocketLauncher), "Start")]
public static class Inject_RocketLauncherSounds
{
    public static void Postfix(RocketLauncher __instance, AudioSource ___chargeSound)
    {
        Traverse rocketTraverse = Traverse.Create(__instance);
        if (__instance.variation == 0)
        {
            SoundPackController.SetAudioSourceClip(rocketTraverse.Field("timerTickSound").GetValue() as AudioSource, "RocketLauncherTickSounds", SoundPackController.SoundPackType.RocketLauncher);
            SoundPackController.SetAudioSourceClip(rocketTraverse.Field("timerWindupSound").GetValue() as AudioSource, "RocketLauncherWindUpSounds", SoundPackController.SoundPackType.RocketLauncher);
        }
        else if (__instance.variation == 1)
        {
            SoundPackController.SetAudioSourceClip(___chargeSound, "RocketLauncherCannonballTimerWindUp", SoundPackController.SoundPackType.RocketLauncher);
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


[HarmonyPatch(typeof(RocketLauncher), "Update")]
public static class Inject_UpdateSounds
{
    public static void Prefix(RocketLauncher __instance, ref float ___cooldown, ref AudioSource ___chargeSound, ref AudioSource ___aud, ref float ___lastKnownTimerAmount, ref WeaponIdentifier ___wid)
    {
        if (__instance.variation == 1)
        {
            if (MonoSingleton<GunControl>.Instance.activated && !GameStateManager.Instance.PlayerInputLocked)
            {
                if (___lastKnownTimerAmount != MonoSingleton<WeaponCharges>.Instance.rocketCannonballCharge && (!___wid || ___wid.delay == 0f))
                {
                    SoundPackController.SetAudioSourceClip(___chargeSound, "RocketLauncherCannonballTimerWindUp", SoundPackController.SoundPackType.RocketLauncher);
                }
                if (___cooldown <= 0f)
                {
                    if (MonoSingleton<InputManager>.Instance.InputSource.Fire2.IsPressed && !MonoSingleton<InputManager>.Instance.InputSource.Fire1.WasPerformedThisFrame && MonoSingleton<WeaponCharges>.Instance.rocketCannonballCharge >= 1f)
                    {
                        SoundPackController.SetAudioSourceClip(___chargeSound, "RocketLauncherCannonballChargeUp", SoundPackController.SoundPackType.RocketLauncher);
                    }
                }
            }
        }
    }
}

[HarmonyPatch(typeof(RocketLauncher), "ShootCannonball")]
public static class Inject_ShootCannonballSounds
{
    public static void Prefix(RocketLauncher __instance, ref AudioSource ___aud)
    {
        SoundPackController.SetAudioSourceClip(___aud, "RocketLauncherCannonballShootSounds", SoundPackController.SoundPackType.RocketLauncher);
    }
}

[HarmonyPatch(typeof(Cannonball), "Bounce")]
public static class Inject_ShootCannonballBounceSounds
{
    public static void Prefix(Cannonball __instance)
    {
        if (__instance.sisy == null && __instance.bounceSound != null)
            SoundPackController.SetAudioSourceClip(__instance.bounceSound, "RocketLauncherCannonballBounce", SoundPackController.SoundPackType.RocketLauncher);
    }
}
