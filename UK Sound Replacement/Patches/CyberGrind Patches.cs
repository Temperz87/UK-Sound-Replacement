using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

[HarmonyPatch(typeof(MusicChanger), "OnEnable")]
public static class Inject_CyberGrindMusic
{
    public static bool Prefix(MusicChanger __instance)
    {
        if (SceneManager.GetActiveScene().name == "Endless")
        {
            SoundPackController.GetCurrentCybergrindSong(ref __instance.clean, ref __instance.battle, ref __instance.boss);
        }
        return true;
    }
}

[HarmonyPatch(typeof(ActivateOnSoundEnd), "Start")]
public static class Inject_CyberGrindIntroMusic
{
    public static bool Prefix(ActivateOnSoundEnd __instance)
    {
        if (SceneManager.GetActiveScene().name == "Endless")
        {
            AudioSource source = __instance.GetComponent<AudioSource>();
            SoundPackController.GetCurrentCybergrindIntroSong(source);
            source.Play();
        }
        return true;
    }
}