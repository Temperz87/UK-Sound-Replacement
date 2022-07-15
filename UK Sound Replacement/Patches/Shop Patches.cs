using HarmonyLib;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[HarmonyPatch(typeof(ShopZone), "Start")]
public static class Inject_SoundPackShops
{
    public static void Prefix(ShopZone __instance)
    {
        if (__instance.transform.Find("Canvas").Find("Weapons") != null) // just a sanity check that we're not messing with a testament
        {
            Transform enemies = GameObject.Instantiate(__instance.transform.Find("Canvas").Find("Enemies"), __instance.transform.Find("Canvas").Find("Weapons"));
            enemies.Find("BackButton (2)").gameObject.SetActive(false);
            Transform contentParent = enemies.Find("Panel").Find("Scroll View").Find("Viewport").Find("Content");
            GameObject packTemplate = contentParent.Find("Enemy Button Template").gameObject;
            for (int i = 0; i < contentParent.childCount; i++)
                contentParent.GetChild(i).gameObject.SetActive(false);
            packTemplate.gameObject.SetActive(true);
            foreach (SoundPackController.SoundPack pack in SoundPackController.GetAllSoundPacks())
            {
                GameObject newPack = GameObject.Instantiate(packTemplate, contentParent);
                newPack.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = Color.gray;
                if (pack.previewImage != null)
                {
                    GameObject.Destroy(newPack.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Image>());
                    IEnumerator setImageNextFrame() // So unity waits a frame before destroying a component, hence this
                    {
                        yield return null;
                        RawImage img = newPack.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.AddComponent<RawImage>();
                        img.texture = pack.previewImage;
                        img.raycastTarget = false;
                    }
                    __instance.StartCoroutine(setImageNextFrame());
                }
                else
                    newPack.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
                ShopButton sButton = newPack.GetComponent<ShopButton>();
                sButton.toActivate = new GameObject[] { };
                newPack.GetComponent<ShopButton>().toDeactivate = new GameObject[] { };
                newPack.GetComponent<Button>().onClick.AddListener(delegate
                {
                    SoundPackController.SetCurrentSoundPack(pack.name);
                    GameObject.Instantiate(sButton.clickSound);
                    AudioSource source = GameObject.Instantiate(sButton.clickSound).GetComponent<AudioSource>();
                    SoundPackController.SetAudioSourceClip(source, "Random");
                    source.volume = 1f;
                    source.Play();
                });
                GameObject newText = GameObject.Instantiate(__instance.transform.Find("Canvas").Find("Weapons").Find("ArmButton").gameObject, newPack.transform.GetChild(0).GetChild(0)); // yes I tried making a "prefab" out of this and instantiating it, didn't work
                GameObject.Destroy(newText.GetComponent<ShopButton>());
                newText.transform.localPosition = Vector3.zero;
                newText.transform.localScale = new Vector3(0.4167529f, 0.4167529f, 0.4167529f);
                newText.layer = 5;
                Text text = newText.GetComponentInChildren<Text>();
                text.text = pack.name;
                text.raycastTarget = false;
            }
            packTemplate.SetActive(false);

            Transform newArmsButton = GameObject.Instantiate(__instance.transform.Find("Canvas").Find("Weapons").Find("ArmButton").gameObject, __instance.transform.Find("Canvas").Find("Weapons")).transform;
            newArmsButton.localPosition = new Vector3(-180f, -144.7f, -45.00326f);
            newArmsButton.gameObject.GetComponentInChildren<Text>(true).text = "SOUND PACKS";
            ShopButton button = newArmsButton.GetComponent<ShopButton>();
            button.toActivate = new GameObject[] { enemies.gameObject };
            button.toDeactivate = new GameObject[]
            {
                __instance.transform.Find("Canvas").Find("Weapons").Find("RevolverWindow").gameObject,
                __instance.transform.Find("Canvas").Find("Weapons").Find("ShotgunWindow").gameObject,
                __instance.transform.Find("Canvas").Find("Weapons").Find("NailgunWindow").gameObject,
                __instance.transform.Find("Canvas").Find("Weapons").Find("RailcannonWindow").gameObject,
                __instance.transform.Find("Canvas").Find("Weapons").Find("ArmWindow").gameObject
            };

            __instance.transform.Find("Canvas").Find("Weapons").Find("BackButton (1)").gameObject.GetComponent<ShopButton>().toDeactivate = __instance.transform.Find("Canvas").Find("Weapons").Find("BackButton (1)").gameObject.GetComponent<ShopButton>().toDeactivate.AddToArray(enemies.gameObject);
            __instance.transform.Find("Canvas").Find("Weapons").Find("RevolverButton").gameObject.GetComponent<ShopButton>().toDeactivate = __instance.transform.Find("Canvas").Find("Weapons").Find("RevolverButton").gameObject.GetComponent<ShopButton>().toDeactivate.AddToArray(enemies.gameObject);
            __instance.transform.Find("Canvas").Find("Weapons").Find("ShotgunButton").gameObject.GetComponent<ShopButton>().toDeactivate = __instance.transform.Find("Canvas").Find("Weapons").Find("ShotgunButton").gameObject.GetComponent<ShopButton>().toDeactivate.AddToArray(enemies.gameObject);
            __instance.transform.Find("Canvas").Find("Weapons").Find("NailgunButton").gameObject.GetComponent<ShopButton>().toDeactivate = __instance.transform.Find("Canvas").Find("Weapons").Find("NailgunButton").gameObject.GetComponent<ShopButton>().toDeactivate.AddToArray(enemies.gameObject);
            __instance.transform.Find("Canvas").Find("Weapons").Find("RailcannonButton").gameObject.GetComponent<ShopButton>().toDeactivate = __instance.transform.Find("Canvas").Find("Weapons").Find("RailcannonButton").gameObject.GetComponent<ShopButton>().toDeactivate.AddToArray(enemies.gameObject);
            __instance.transform.Find("Canvas").Find("Weapons").Find("ArmButton").gameObject.GetComponent<ShopButton>().toDeactivate = __instance.transform.Find("Canvas").Find("Weapons").Find("ArmButton").gameObject.GetComponent<ShopButton>().toDeactivate.AddToArray(enemies.gameObject);
        }
    }
}