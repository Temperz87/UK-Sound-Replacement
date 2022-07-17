using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(Revolver), "Start")]
public static class Inject_RevolverSounds
{
    public static void Postfix(Revolver __instance)
    {
        SoundPackController.GetAllAudioClips("RevolverShootSounds" + __instance.gunVariation + __instance.altVersion.ToString(), ref __instance.gunShots);
        if (__instance.gunVariation == 0)
        {
            SoundPackController.GetAllAudioClips("RevolverSuperShootSounds" + __instance.gunVariation + __instance.altVersion.ToString(), ref __instance.superGunShots);
            SoundPackController.SetAudioClip(ref __instance.chargingSound, "ChargingUp" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioClip(ref __instance.chargedSound, "ChargeReady" + __instance.gunVariation + __instance.altVersion.ToString());
            if (__instance.gunBarrel == null)
            {
                foreach (AudioSource source in __instance.GetComponentsInChildren<AudioSource>(true))
                    if (source.name == "ChargeEffect")
                        SoundPackController.SetAudioSourceClip(source, "ChargeLoop" + __instance.gunVariation + __instance.altVersion.ToString(), true);
            }
            else
                SoundPackController.SetAudioSourceClip(__instance.gunBarrel.transform.GetChild(0).GetComponent<AudioSource>(), "ChargeLoop" + __instance.gunVariation + __instance.altVersion.ToString(), true);
            foreach (AudioSource source in __instance.GetComponentsInChildren<AudioSource>(true))
                if (source.name == "Bone_001")
                    SoundPackController.SetAudioSourceClip(source, "ClickCancel" + __instance.gunVariation + __instance.altVersion.ToString());
        }
        else
        {
            SoundPackController.SetAudioClip(ref __instance.twirlSound, "CoinTwirl" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioSourceClip(__instance.GetComponentInChildren<Canvas>().gameObject.GetComponent<AudioSource>(), "CoinReady" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioSourceClip(__instance.coin.GetComponent<AudioSource>(), "CoinFlip" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioSourceClip(__instance.coin.transform.GetChild(0).GetComponent<AudioSource>(), "CoinSpin" + __instance.gunVariation + __instance.altVersion.ToString());

            Coin coin = __instance.coin.GetComponent<Coin>();
            SoundPackController.SetAudioSourceClip(coin.flash.GetComponent<AudioSource>(), "CoinFlip" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioSourceClip(coin.coinBreak.GetComponent<AudioSource>(), "CoinBreak" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioSourceClip(coin.coinHitSound.GetComponents<AudioSource>()[0], "CoinRicochet" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioSourceClip(coin.coinHitSound.GetComponents<AudioSource>()[1], "CoinFlip" + __instance.gunVariation + __instance.altVersion.ToString());
            SoundPackController.SetAudioSourceClip(coin.chargeEffect.GetComponent<AudioSource>(), "CoinFlashLoop" + __instance.gunVariation + __instance.altVersion.ToString());
        }
    }
}

[HarmonyPatch(typeof(RevolverAnimationReceiver), "Click")]
public static class Inject_RevolverHammerSound
{
    public static bool Prefix(RevolverAnimationReceiver __instance)
    {
        SoundPackController.SetAudioSourceClip(__instance.click.GetComponent<AudioSource>(), "HammerClick" + ((Revolver)Traverse.Create(__instance).Field("rev").GetValue()).gunVariation); // I think that traverse is faster than unity's get component in children
        return true;
    }
}