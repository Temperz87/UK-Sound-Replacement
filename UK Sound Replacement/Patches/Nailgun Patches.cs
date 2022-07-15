//using HarmonyLib;
//using UnityEngine;

//[HarmonyPatch(typeof(Nailgun), "Start")]
//public static class Inject_NailgunSounds
//{
//    public static void Postfix(Nailgun __instance)
//    {
//        SoundPackController.SetAudioSourceClip(__instance.noAmmoSound.GetComponent<AudioSource>(), "NailgunNoAmmo" + __instance.variation + __instance.altVersion);
//        SoundPackController.SetAudioSourceClip(__instance.lastShotSound.GetComponent<AudioSource>(), "NailgunLastShot" + __instance.variation + __instance.altVersion);
//    }
//}

//[HarmonyPatch(typeof(Nailgun), "Shoot")]
//public static class Inject_NailgunShootSounds
//{
//    public static bool Prefix(Nailgun __instance)
//    {
//        SoundPackController.SetAudioSourceClip(__instance.muzzleFlash.GetComponent<AudioSource>(), "NailgunShoot" + __instance.variation + __instance.altVersion);
//        SoundPackController.SetAudioSourceClip(__instance.muzzleFlash2.GetComponent<AudioSource>(), "NailgunShootBurntOut" + __instance.variation + __instance.altVersion);
//        return true;
//    }
//}

//[HarmonyPatch(typeof(Nailgun), "SuperSaw")]
//public static class Inject_NailgunSuperSawSounds
//{
//    public static bool Prefix(Nailgun __instance)
//    {
//        SoundPackController.SetAudioSourceClip(__instance.muzzleFlash2.GetComponent<AudioSource>(), "NailgunShootBurntOut" + __instance.variation + __instance.altVersion);
//        return true;
//    }
//}

//[HarmonyPatch(typeof(Nailgun), "SnapSound")]
//public static class Inject_NailgunSnapSounds
//{
//    public static bool Prefix(Nailgun __instance)
//    {
//        SoundPackController.SetAudioSourceClip(__instance.snapSound.GetComponent<AudioSource>(), "NailgunSnap" + __instance.variation + __instance.altVersion);
//        return true;
//    }
//}
