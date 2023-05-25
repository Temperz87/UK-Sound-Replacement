using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(Revolver), "Start")]
public static class Inject_RevolverSounds
{
    public static void Postfix(Revolver __instance)
    {
        SoundPackController.GetAllAudioClips(ref __instance.gunShots, "RevolverShootSounds" + __instance.gunVariation + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver);
        if (__instance.gunVariation == 0)
        {
            SoundPackController.GetAllAudioClips(ref __instance.superGunShots, "RevolverSuperShootSounds" + __instance.gunVariation + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver);
            SoundPackController.SetAudioClip(ref __instance.chargingSound, "ChargingUp" + __instance.gunVariation + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver);
            SoundPackController.SetAudioClip(ref __instance.chargedSound, "ChargeReady" + __instance.gunVariation + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver);
            if (__instance.gunBarrel == null)
            {
                foreach (AudioSource source in __instance.GetComponentsInChildren<AudioSource>(true))
                    if (source.name == "ChargeEffect")
                        SoundPackController.SetAudioSourceClip(source, "ChargeLoop" + __instance.gunVariation + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver, true);
            }
            else
                SoundPackController.SetAudioSourceClip(__instance.gunBarrel.transform.GetChild(0).GetComponent<AudioSource>(), "ChargeLoop" + __instance.gunVariation + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver, true);
            foreach (AudioSource source in __instance.GetComponentsInChildren<AudioSource>(true))
                if (source.name == "Bone_001")
                    SoundPackController.SetAudioSourceClip(source, "ClickCancel" + __instance.gunVariation + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver);
        }
        else if (__instance.gunVariation == 1)
        {
            //SoundPackController.SetAudioClip(ref __instance.twirlSound, "CoinTwirl" + __instance.gunVariation + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver);
            SoundPackController.SetAudioSourceClip(__instance.GetComponentInChildren<Canvas>().gameObject.GetComponent<AudioSource>(), "CoinReady" + __instance.gunVariation + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver);
            SoundPackController.SetAudioSourceClip(__instance.coin.GetComponent<AudioSource>(), "CoinFlip" + __instance.gunVariation + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver);
            SoundPackController.SetAudioSourceClip(__instance.coin.transform.GetChild(0).GetComponent<AudioSource>(), "CoinSpin" + __instance.gunVariation + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver);

            Coin coin = __instance.coin.GetComponent<Coin>();
            SoundPackController.SetAudioSourceClip(coin.flash.GetComponent<AudioSource>(), "CoinFlip" + __instance.gunVariation + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver);
            SoundPackController.SetAudioSourceClip(coin.coinBreak.GetComponent<AudioSource>(), "CoinBreak" + __instance.gunVariation + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver);
            SoundPackController.SetAudioSourceClip(coin.coinHitSound.GetComponents<AudioSource>()[0], "CoinRicochet" + __instance.gunVariation + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver);
            SoundPackController.SetAudioSourceClip(coin.coinHitSound.GetComponents<AudioSource>()[1], "CoinFlip" + __instance.gunVariation + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver);
            SoundPackController.SetAudioSourceClip(coin.chargeEffect.GetComponent<AudioSource>(), "CoinFlashLoop" + __instance.gunVariation + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver);
        }
        else if (__instance.gunVariation == 2)
        {
            SoundPackController.GetAllAudioClips(ref __instance.superGunShots, "RevolverSuperShootSounds" + __instance.gunVariation + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver);
            SoundPackController.SetAudioSourceClip(__instance.chargeEffect.GetComponent<AudioSource>(), "TwirlSound" + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver);
        }
    }
}

[HarmonyPatch(typeof(Revolver), "CheckCoinCharges")]
public static class Inject_CoinCHargedSound
{
    public static bool Prefix(Revolver __instance, ref AudioSource ___screenAud)
    {
        if (__instance.gunVariation == 2)
        {
            if (!___screenAud)
                ___screenAud = __instance.GetComponentInChildren<Canvas>().GetComponent<AudioSource>();
            SoundPackController.SetAudioSourceClip(___screenAud, "TwirlCharged" + __instance.altVersion.ToString(), SoundPackController.SoundPackType.Revolver);
        }
        return true;
    }
}

[HarmonyPatch(typeof(RevolverAnimationReceiver), "Click")]
public static class Inject_RevolverHammerSound
{
    public static bool Prefix(RevolverAnimationReceiver __instance, Revolver ___rev)
    {
        SoundPackController.SetAudioSourceClip(__instance.click.GetComponent<AudioSource>(), "HammerClick" + ___rev.gunVariation, SoundPackController.SoundPackType.Revolver);
        return true;
    }
}
