using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(Nailgun), "Start")]
public static class Inject_NailgunSounds
{
    public static void Postfix(Nailgun __instance)
    {
        SoundPackController.SetAudioSourceClip(Traverse.Create(__instance).Field("barrelAud").GetValue() as AudioSource, "BarrelSpin" + __instance.variation + __instance.altVersion);
        SoundPackController.SetAudioSourceClip(Traverse.Create(__instance).Field("aud").GetValue() as AudioSource, "NewCharge" + __instance.variation + __instance.altVersion);
        if (__instance.variation == 1)
        {
            TimeBomb tb = __instance.magnetNail.GetComponent<TimeBomb>();
            SoundPackController.SetAudioSourceClip(tb.beepLight.GetComponent<AudioSource>(), "MagnetBeep" + __instance.variation + __instance.altVersion);
            Harpoon harpoon = __instance.magnetNail.GetComponentInChildren<Harpoon>();
            SoundPackController.SetAudioClip(ref harpoon.environmentHitSound, "MagnetHit" + __instance.variation + __instance.altVersion);
            SoundPackController.SetAudioClip(ref harpoon.enemyHitSound, "MagnetHitEnemy" + __instance.variation + __instance.altVersion);
            foreach (AudioSource source in tb.explosion.GetComponentsInChildren<AudioSource>(true))
                SoundPackController.SetAudioSourceClip(source, "MagnetBreak" + __instance.variation + __instance.altVersion);
            SoundPackController.SetAudioSourceClip(__instance.lastShotSound.GetComponent<AudioSource>(), "NailgunLastShot" + __instance.variation + __instance.altVersion);
            SoundPackController.SetAudioSourceClip(__instance.noAmmoSound.GetComponent<AudioSource>(), "NoAmmoClick" + __instance.variation + __instance.altVersion);
        }
        Nail nail = __instance.nail.GetComponent<Nail>();
        if (!__instance.altVersion)
        {
            SoundPackController.SetAudioSourceClip(Traverse.Create(__instance).Field("heatSteamAud").GetValue() as AudioSource, "HeatSteam" + __instance.variation + __instance.altVersion);
            SoundPackController.SetAudioSourceClip(nail.zapParticle.GetComponent<AudioSource>(), "NailZap" + __instance.variation + __instance.altVersion);
        }
        else
        {
            SoundPackController.SetAudioSourceClip((Traverse.Create(nail).Field("sawBreakEffect").GetValue() as GameObject).GetComponent<AudioSource>(), "SawBreak" + __instance.variation + __instance.altVersion);
            SoundPackController.SetAudioSourceClip((Traverse.Create(nail).Field("sawBounceEffect").GetValue() as GameObject).GetComponent<AudioSource>(), "SawBounce" + __instance.variation + __instance.altVersion);
        }
    }
}

[HarmonyPatch(typeof(Nailgun), "Shoot")]
public static class Inject_NailgunShootSounds
{
    public static bool Prefix(Nailgun __instance)
    {
        SoundPackController.SetAudioSourceClip(__instance.muzzleFlash.GetComponent<AudioSource>(), "NailgunShot" + __instance.variation + __instance.altVersion);
        if (__instance.variation == 0)
            SoundPackController.SetAudioSourceClip(__instance.muzzleFlash2.GetComponent<AudioSource>(), "NailgunShotOverheat" + __instance.variation + __instance.altVersion);
        return true;
    }
}

[HarmonyPatch(typeof(Nailgun), "SuperSaw")]
public static class Inject_NailgunSuperSawSounds
{
    public static bool Prefix(Nailgun __instance)
    {
        SoundPackController.SetAudioSourceClip(__instance.muzzleFlash2.GetComponent<AudioSource>(), "NailgunShotOverheat" + __instance.variation + __instance.altVersion);
        return true;
    }
}

[HarmonyPatch(typeof(Nailgun), "BurstFire")]
public static class Inject_NailgunBurstSounds
{
    public static bool Prefix(Nailgun __instance)
    {
        SoundPackController.SetAudioSourceClip(__instance.muzzleFlash2.GetComponent<AudioSource>(), "NailgunShotOverheat" + __instance.variation + __instance.altVersion);
        return true;
    }
}

[HarmonyPatch(typeof(Nailgun), "SnapSound")]
public static class Inject_NailgunSnapSounds
{
    public static bool Prefix(Nailgun __instance)
    {
        SoundPackController.SetAudioSourceClip(__instance.snapSound, "NailgunSnap" + __instance.variation + __instance.altVersion);
        return true;
    }
}

[HarmonyPatch(typeof(Nailgun), "ShootMagnet")]
public static class Inject_NailgunMagnetSounds
{
    public static bool Prefix(Nailgun __instance)
    {
        SoundPackController.SetAudioSourceClip(Traverse.Create(__instance).Field("barrelAud").GetValue() as AudioSource, "BarrelSpin" + __instance.variation + __instance.altVersion);
        return true;
    }
}