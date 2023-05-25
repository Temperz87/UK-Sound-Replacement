using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[HarmonyPatch(typeof(ShopZone), "Start")]
public static class Inject_SoundPackShops
{
    public static void Prefix(ShopZone __instance) // This function's just spahgetti code, I'm sorry
    {
        if (__instance.transform.Find("Canvas").Find("Weapons") != null) // just a sanity check that we're not messing with a testament
        {
            // Grab parents and such
            Transform enemies = GameObject.Instantiate(__instance.transform.Find("Canvas").Find("Enemies"), __instance.transform.Find("Canvas").Find("Weapons"));
            enemies.Find("BackButton (2)").gameObject.SetActive(false);
            Text editingText = enemies.Find("Panel").Find("Title").GetComponent<Text>();
            editingText.text = "SOUND PACKS";
            editingText.transform.parent = enemies.parent;
            editingText.gameObject.SetActive(false);
            Transform contentParent = enemies.Find("Panel").Find("Scroll View").Find("Viewport").Find("Content");

            // Making the button that opens the sound packs page
            Transform newArmsButton = GameObject.Instantiate(__instance.transform.Find("Canvas").Find("Weapons").Find("ArmButton").gameObject, __instance.transform.Find("Canvas").Find("Weapons")).transform;
            newArmsButton.localPosition = new Vector3(-180f, -144.7f, -45.00326f);
            newArmsButton.gameObject.GetComponentInChildren<Text>(true).text = "SOUND PACKS";
            GameObject.Destroy(newArmsButton.GetComponent<ShopCategory>());
            ShopButton button = newArmsButton.GetComponent<ShopButton>();
            button.toActivate = new GameObject[]
            {
                enemies.gameObject,
                contentParent.gameObject,
                editingText.gameObject
            };
            button.toDeactivate = new GameObject[]
            {
                __instance.transform.Find("Canvas").Find("Weapons").Find("RevolverWindow").gameObject,
                __instance.transform.Find("Canvas").Find("Weapons").Find("ShotgunWindow").gameObject,
                __instance.transform.Find("Canvas").Find("Weapons").Find("NailgunWindow").gameObject,
                __instance.transform.Find("Canvas").Find("Weapons").Find("RailcannonWindow").gameObject,
                __instance.transform.Find("Canvas").Find("Weapons").Find("RocketLauncherWindow").gameObject,
                __instance.transform.Find("Canvas").Find("Weapons").Find("ArmWindow").gameObject,
            };
            newArmsButton.GetComponent<Button>().onClick.AddListener(delegate
            {
                editingText.text = "SOUND PACKS";
            });
            newArmsButton.SetAsFirstSibling();

            // Grabbing buttons
            Transform revButton = __instance.transform.Find("Canvas").Find("Weapons").Find("RevolverButton");
            ShopButton revShopButton = revButton.gameObject.GetComponent<ShopButton>();
            Transform sgButton = __instance.transform.Find("Canvas").Find("Weapons").Find("ShotgunButton");
            ShopButton sgShopButton = sgButton.gameObject.GetComponent<ShopButton>();
            Transform ngButton = __instance.transform.Find("Canvas").Find("Weapons").Find("NailgunButton");
            ShopButton ngShopButton = ngButton.gameObject.GetComponent<ShopButton>();
            Transform rcButton = __instance.transform.Find("Canvas").Find("Weapons").Find("RailcannonButton");
            ShopButton rcShopButton = rcButton.gameObject.GetComponent<ShopButton>();
            Transform rlButton = __instance.transform.Find("Canvas").Find("Weapons").Find("RocketLauncherButton");
            ShopButton rlShopButton = rlButton.gameObject.GetComponent<ShopButton>();
            ShopButton armShopButton = __instance.transform.Find("Canvas").Find("Weapons").Find("ArmButton").gameObject.GetComponent<ShopButton>();
            ShopButton backShopButton = __instance.transform.Find("Canvas").Find("Weapons").Find("BackButton (1)").gameObject.GetComponent<ShopButton>();
            SoundPackController.SoundPack selectedPack = null;
            GameObject newPageParent = GameObject.Instantiate(new GameObject(), revButton.parent);
            newPageParent.SetActive(false);
            newPageParent.transform.localPosition = new Vector3(90f, 0, 0);

            // Messing with preexisting buttons to close the custom pages
            revShopButton.toDeactivate = revShopButton.toDeactivate.AddToArray(newPageParent);
            revShopButton.toDeactivate = revShopButton.toDeactivate.AddToArray(editingText.gameObject);
            revShopButton.toDeactivate = revShopButton.toDeactivate.AddToArray(enemies.gameObject);

            sgShopButton.toDeactivate = sgShopButton.toDeactivate.AddToArray(newPageParent);
            sgShopButton.toDeactivate = sgShopButton.toDeactivate.AddToArray(editingText.gameObject);
            sgShopButton.toDeactivate = sgShopButton.toDeactivate.AddToArray(enemies.gameObject);

            ngShopButton.toDeactivate = ngShopButton.toDeactivate.AddToArray(newPageParent);
            ngShopButton.toDeactivate = ngShopButton.toDeactivate.AddToArray(editingText.gameObject);
            ngShopButton.toDeactivate = ngShopButton.toDeactivate.AddToArray(enemies.gameObject);

            rcShopButton.toDeactivate = rcShopButton.toDeactivate.AddToArray(newPageParent);
            rcShopButton.toDeactivate = rcShopButton.toDeactivate.AddToArray(editingText.gameObject);
            rcShopButton.toDeactivate = rcShopButton.toDeactivate.AddToArray(enemies.gameObject);

            rlShopButton.toDeactivate = rlShopButton.toDeactivate.AddToArray(newPageParent);
            rlShopButton.toDeactivate = rlShopButton.toDeactivate.AddToArray(editingText.gameObject);
            rlShopButton.toDeactivate = rlShopButton.toDeactivate.AddToArray(enemies.gameObject);

            armShopButton.toDeactivate = armShopButton.toDeactivate.AddToArray(newPageParent);
            armShopButton.toDeactivate = armShopButton.toDeactivate.AddToArray(editingText.gameObject);
            armShopButton.toDeactivate = armShopButton.toDeactivate.AddToArray(enemies.gameObject);

            backShopButton.toDeactivate = backShopButton.GetComponent<ShopButton>().toDeactivate.AddToArray(newPageParent);
            backShopButton.toDeactivate = backShopButton.toDeactivate.AddToArray(editingText.gameObject);
            backShopButton.toDeactivate = backShopButton.toDeactivate.AddToArray(enemies.gameObject);

            List<Image> toSet = new List<Image>(); // This is for the gamer rgb lights

            // Adding the buttons that actaully set the sounds
            Transform newRev = GameObject.Instantiate(revButton, newPageParent.transform);
            newRev.localPosition = new Vector3(0f, 65f, -45f);
            newRev.GetComponent<ShopButton>().toActivate = new GameObject[] { };
            newRev.GetComponent<ShopButton>().toDeactivate = new GameObject[] { };
            Image revImage = newRev.GetChild(0).GetComponent<Image>();
            newRev.GetComponent<Button>().onClick.AddListener(delegate
            {
                SoundPackController.SetCurrentSoundPack(selectedPack.name, SoundPackController.SoundPackType.Revolver);

                AudioSource source = GameObject.Instantiate(newRev.GetComponent<ShopButton>().clickSound).GetComponent<AudioSource>();
                source.volume = 1f;
                SoundPackController.SetAudioSourceClip(source, "Random", SoundPackController.SoundPackType.Revolver);
                source.Play();

                toSet.Add(revImage);
            });

            Transform newSg = GameObject.Instantiate(sgButton, newPageParent.transform);
            newSg.localPosition = new Vector3(0f, 35f, -45f);
            newSg.GetComponent<ShopButton>().toActivate = new GameObject[] { };
            newSg.GetComponent<ShopButton>().toDeactivate = new GameObject[] { };
            Image sgImage = newSg.GetChild(0).GetComponent<Image>();
            newSg.GetComponent<Button>().onClick.AddListener(delegate
            {
                SoundPackController.SetCurrentSoundPack(selectedPack.name, SoundPackController.SoundPackType.Shotgun);

                AudioSource source = GameObject.Instantiate(newSg.GetComponent<ShopButton>().clickSound).GetComponent<AudioSource>();
                source.volume = 1f;
                SoundPackController.SetAudioSourceClip(source, "Random", SoundPackController.SoundPackType.Shotgun);
                source.Play();

                toSet.Add(sgImage);
            });

            Transform newNg = GameObject.Instantiate(ngButton, newPageParent.transform);
            newNg.localPosition = new Vector3(0f, 5f, -45f);
            newNg.GetComponent<ShopButton>().toActivate = new GameObject[] { };
            newNg.GetComponent<ShopButton>().toDeactivate = new GameObject[] { };
            Image ngImage = newNg.GetChild(0).GetComponent<Image>();
            newNg.GetComponent<Button>().onClick.AddListener(delegate
            {
                SoundPackController.SetCurrentSoundPack(selectedPack.name, SoundPackController.SoundPackType.Nailgun);

                AudioSource source = GameObject.Instantiate(newNg.GetComponent<ShopButton>().clickSound).GetComponent<AudioSource>();
                source.volume = 1f;
                SoundPackController.SetAudioSourceClip(source, "Random", SoundPackController.SoundPackType.Nailgun);
                source.Play();

                toSet.Add(ngImage);
            });

            Transform newRc = GameObject.Instantiate(rcButton, newPageParent.transform);
            newRc.localPosition = new Vector3(0f, -25f, -45f);
            newRc.GetComponent<ShopButton>().toActivate = new GameObject[] { };
            newRc.GetComponent<ShopButton>().toDeactivate = new GameObject[] { };
            Image rcImage = newRc.GetChild(0).GetComponent<Image>();
            newRc.GetComponent<Button>().onClick.AddListener(delegate
            {
                SoundPackController.SetCurrentSoundPack(selectedPack.name, SoundPackController.SoundPackType.Railcannon);

                AudioSource source = GameObject.Instantiate(newRc.GetComponent<ShopButton>().clickSound).GetComponent<AudioSource>();
                source.volume = 1f;
                SoundPackController.SetAudioSourceClip(source, "Random", SoundPackController.SoundPackType.Railcannon);
                source.Play();

                toSet.Add(rcImage);
            });

            Transform newRl = GameObject.Instantiate(rlButton, newPageParent.transform);
            newRl.localPosition = new Vector3(0f, -55f, -45f);
            newRl.GetComponent<ShopButton>().toActivate = new GameObject[] { };
            newRl.GetComponent<ShopButton>().toDeactivate = new GameObject[] { };
            Image rlImage = newRl.GetChild(0).GetComponent<Image>();
            newRl.GetComponent<Button>().onClick.AddListener(delegate
            {
                SoundPackController.SetCurrentSoundPack(selectedPack.name, SoundPackController.SoundPackType.RocketLauncher);

                AudioSource source = GameObject.Instantiate(newRl.GetComponent<ShopButton>().clickSound).GetComponent<AudioSource>();
                source.volume = 1f;
                SoundPackController.SetAudioSourceClip(source, "Random", SoundPackController.SoundPackType.RocketLauncher);
                source.Play();

                toSet.Add(rlImage);
            });

            Transform newAll = GameObject.Instantiate(rcButton, newPageParent.transform);
            newAll.localPosition = new Vector3(0f, -85f, -45f);
            newAll.GetComponent<ShopButton>().toActivate = new GameObject[] { };
            newAll.GetComponent<ShopButton>().toDeactivate = new GameObject[] { };
            newAll.gameObject.transform.GetComponentInChildren<Text>().text = "ALL";
            newAll.GetComponent<Button>().onClick.AddListener(delegate
            {
                SoundPackController.SetCurrentSoundPack(selectedPack.name, SoundPackController.SoundPackType.All);
                toSet.Add(revImage);
                toSet.Add(sgImage);
                toSet.Add(ngImage);
                toSet.Add(rcImage);
                toSet.Add(rlImage);

                AudioSource source = GameObject.Instantiate(newRc.GetComponent<ShopButton>().clickSound).GetComponent<AudioSource>();
                source.volume = 1f;
                SoundPackController.SetAudioSourceClip(source, "Random", SoundPackController.SoundPackType.Railcannon); // The final argument doesn't really matter as it won't actually play a random sound from that specific type, just pack
                source.Play();
            });

            Transform newBack = GameObject.Instantiate(rcButton, newPageParent.transform);
            newBack.localPosition = new Vector3(0f, -115f, -45f);
            newBack.localScale = new Vector3(0.74757f, 0.74757f, 0.74757f);
            newBack.GetComponent<ShopButton>().toActivate = new GameObject[] { contentParent.gameObject };
            newBack.GetComponent<ShopButton>().toDeactivate = new GameObject[] { newPageParent };
            newBack.gameObject.GetComponentInChildren<Text>().text = "BACK";
            newBack.GetComponent<Button>().onClick.AddListener(delegate
            {
                editingText.text = "SOUND PACKS";
            });

            IEnumerator funnyColorRoutine() // The epic gamer RGB function
            {
                int r = 255;
                int g = 0;
                int b = 0;
                const float multiplier = 255f;

                while (true)
                {
                    if (r == 255)
                    {
                        while (g < 255)
                        {
                            g = (int)Mathf.Min(g + (Time.deltaTime * multiplier), 255);
                            foreach (Image image in toSet)
                                image.color = new Color32((byte)r, (byte)g, (byte)b, 255);
                            yield return null;
                        }
                        while (r > 0)
                        {
                            r = (int)Mathf.Max(r - (Time.deltaTime * multiplier), 0);
                            foreach (Image image in toSet)
                                image.color = new Color32((byte)r, (byte)g, (byte)b, 255);
                            yield return null;
                        }
                    }
                    else if (g == 255)
                    {
                        while (b < 255)
                        {
                            b = (int)Mathf.Min(b + (Time.deltaTime * multiplier), 255);
                            foreach (Image image in toSet)
                                image.color = new Color32((byte)r, (byte)g, (byte)b, 255);
                            yield return null;
                        }
                        while (g > 0)
                        {
                            g = (int)Mathf.Max(g - (Time.deltaTime * multiplier), 0);
                            foreach (Image image in toSet)
                                image.color = new Color32((byte)r, (byte)g, (byte)b, 255);
                            yield return null;
                        }
                    }
                    else if (b == 255)
                    {
                        while (r < 255)
                        {
                            r = (int)Mathf.Min(r + (Time.deltaTime * multiplier), 255);
                            foreach (Image image in toSet)
                                image.color = new Color32((byte)r, (byte)g, (byte)b, 255);
                            yield return null;
                        }
                        while (b > 0)
                        {
                            b = (int)Mathf.Max(b - (Time.deltaTime * multiplier), 0);
                            foreach (Image image in toSet)
                                image.color = new Color32((byte)r, (byte)g, (byte)b, 255);
                            yield return null;
                        }
                    }
                }
            }

            __instance.StartCoroutine(funnyColorRoutine());

            // hiding any existing content
            GameObject packTemplate = contentParent.Find("Enemy Button Template").gameObject;
            for (int i = 0; i < contentParent.childCount; i++)
                contentParent.GetChild(i).gameObject.SetActive(false);
            packTemplate.gameObject.SetActive(true);

            // Actually injecting the sound packs
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
                    selectedPack = pack;
                    newPageParent.SetActive(true);
                    GameObject.Instantiate(sButton.clickSound);
                    AudioSource source = GameObject.Instantiate(sButton.clickSound).GetComponent<AudioSource>();
                    source.Play();

                    editingText.text = "EDITING: " + pack.name;

                    toSet = new List<Image>();
                    if (SoundPackController.revolverSoundPack == pack)
                        toSet.Add(revImage);
                    else
                        revImage.color = new Color32(255, 255, 255, 255);
                    if (SoundPackController.shotgunSoundPack == pack)
                        toSet.Add(sgImage);
                    else
                        sgImage.color = new Color32(255, 255, 255, 255);
                    if (SoundPackController.nailgunSoundPack == pack)
                        toSet.Add(ngImage);
                    else
                        ngImage.color = new Color32(255, 255, 255, 255);
                    if (SoundPackController.railcannonSoundPack == pack)
                        toSet.Add(rcImage);
                    else
                        rcImage.color = new Color32(255, 255, 255, 255);
                    if (SoundPackController.rocketLauncherSoundPack == pack)
                        toSet.Add(rlImage);
                    else
                        rlImage.color = new Color32(255, 255, 255, 255);
                    contentParent.gameObject.SetActive(false);
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
        }
    }
}

[HarmonyPatch(typeof(CustomMusicSoundtrackBrowser), "Rebuild")]
public static class Inject_CustomCGMusic
{
    public static void Postfix(CustomMusicSoundtrackBrowser __instance, List<IDirectoryTree> ___currentDirectory)
    {
        
    }
}

[HarmonyPatch(typeof(WaveMenu), "Start")]
public static class Inject_CgMusicSelector
{
    public static void Prefix(ScreenZone __instance) 
    {

        
        /*
        if (__instance.transform.Find("Canvas").Find("Waves") != null)
        {
            GameObject newPage = GameObject.Instantiate(__instance.transform.Find("Canvas").Find("Patterns").gameObject, __instance.transform.Find("Canvas"));
            newPage.SetActive(false);
            newPage.transform.Find("Panel").Find("StateButton").gameObject.SetActive(false);

            Transform title = newPage.transform.Find("Panel").Find("Title");
            title.gameObject.SetActive(true);
            title.GetComponent<Text>().text = "--MUSIC--";
            title.transform.localPosition += new Vector3(60f, 0f, 0f);

            Transform customStuff = newPage.transform.Find("Panel").Find("CustomStuff");
            customStuff.transform.Find("PreviousButton").gameObject.SetActive(false);
            customStuff.transform.Find("NextButton").gameObject.SetActive(false);
            customStuff.transform.Find("RefreshButton").gameObject.SetActive(false);
            customStuff.transform.Find("EditorButton").gameObject.SetActive(false);
            customStuff.gameObject.SetActive(true);

            Transform gridParent = customStuff.Find("Grid");
            foreach (Transform tf in gridParent)
                tf.gameObject.SetActive(false);
            Transform introParent = GameObject.Instantiate(gridParent.gameObject, gridParent.transform.parent).transform;

            GameObject exampleButton = gridParent.Find("CustomButton").gameObject;
            exampleButton.SetActive(true);
            GameObject text = customStuff.Find("PageText").gameObject;
            text.SetActive(true);

            GameObject randomButton = GameObject.Instantiate(exampleButton, exampleButton.transform.parent);
            Button rButton = randomButton.GetComponent<Button>();
            rButton.onClick.AddListener(delegate
            {
                SoundPackController.persistentLoopName = "Random";
                Plugin.instance.SetPersistentModData("cgLoop", "Random");
            });
            rButton.targetGraphic.color = new Color32(125, 125, 125, 255);
            GameObject newRText = GameObject.Instantiate(text, randomButton.transform);
            newRText.transform.localPosition = new Vector3(40f, 10f, 0f);
            newRText.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
            newRText.transform.localScale = new Vector3(-1f, 1f, 1f);
            Text titleRText = newRText.GetComponent<Text>();
            titleRText.resizeTextForBestFit = true;
            titleRText.text = "Random";
            
            GameObject stockButton = GameObject.Instantiate(exampleButton, exampleButton.transform.parent);
            Button sButton = stockButton.GetComponent<Button>();
            sButton.onClick.AddListener(delegate
            {
                SoundPackController.persistentLoopName = "Stock";
                Plugin.instance.SetPersistentModData("cgLoop", "Stock");
            });
            sButton.targetGraphic.color = new Color32(125, 125, 125, 255);
            GameObject newSText = GameObject.Instantiate(text, sButton.transform);
            newSText.transform.localPosition = new Vector3(40f, 10f, 0f);
            newSText.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
            newSText.transform.localScale = new Vector3(-1f, 1f, 1f);
            Text titleSText = newSText.GetComponent<Text>();
            titleSText.resizeTextForBestFit = true;
            titleSText.text = "Stock";

            foreach (SoundPackController.ClipData clip in SoundPackController.cgMusic)
            {
                GameObject musicButton = GameObject.Instantiate(exampleButton, exampleButton.transform.parent);
                Button button = musicButton.GetComponent<Button>();
                button.onClick.AddListener(delegate
                {
                    SoundPackController.persistentLoopName = clip.title;
                    Plugin.instance.SetPersistentModData("cgLoop", clip.title);
                });
                button.targetGraphic.color = new Color32(125, 125, 125, 255);
                GameObject newText = GameObject.Instantiate(text, musicButton.transform);
                newText.transform.localPosition = new Vector3(40f, 10f, 0f);
                newText.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
                newText.transform.localScale = new Vector3(-1f, 1f, 1f);
                Text titleText = newText.GetComponent<Text>();
                titleText.resizeTextForBestFit = true;
                titleText.text = clip.title;
            }
            exampleButton.SetActive(false);
            text.SetActive(false);

            exampleButton = introParent.Find("CustomButton").gameObject;
            exampleButton.SetActive(true);
            text = customStuff.Find("PageText").gameObject;
            text.SetActive(true);

            GameObject randomIntroButton = GameObject.Instantiate(exampleButton, exampleButton.transform.parent);
            Button rIButton = randomIntroButton.GetComponent<Button>();
            rIButton.onClick.AddListener(delegate
            {
                SoundPackController.persistentIntroName = "Random";
                Plugin.instance.SetPersistentModData("cgIntro", "Random");
            });
            rIButton.targetGraphic.color = new Color32(125, 125, 125, 255);
            GameObject newRIText = GameObject.Instantiate(text, randomIntroButton.transform);
            newRIText.transform.localPosition = new Vector3(40f, 10f, 0f);
            newRIText.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
            newRIText.transform.localScale = new Vector3(-1f, 1f, 1f);
            Text titleRIText = newRIText.GetComponent<Text>();
            titleRIText.resizeTextForBestFit = true;
            titleRIText.text = "Random";

            GameObject stockIntroButton = GameObject.Instantiate(exampleButton, exampleButton.transform.parent);
            Button sIButton = stockIntroButton.GetComponent<Button>();
            sIButton.onClick.AddListener(delegate
            {
                SoundPackController.persistentIntroName = "Stock";
                Plugin.instance.SetPersistentModData("cgIntro", "Stock");
            });
            sIButton.targetGraphic.color = new Color32(125, 125, 125, 255);
            GameObject newSIText = GameObject.Instantiate(text, stockIntroButton.transform);
            newSIText.transform.localPosition = new Vector3(40f, 10f, 0f);
            newSIText.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
            newSIText.transform.localScale = new Vector3(-1f, 1f, 1f);
            Text titleSIText = newSIText.GetComponent<Text>();
            titleSIText.resizeTextForBestFit = true;
            titleSIText.text = "Stock";

            foreach (SoundPackController.ClipData clip in SoundPackController.cgMusicIntro)
            {
                GameObject musicButton = GameObject.Instantiate(exampleButton, exampleButton.transform.parent);
                Button button = musicButton.GetComponent<Button>();
                button.onClick.AddListener(delegate
                {
                    SoundPackController.persistentIntroName = clip.title;
                    Plugin.instance.SetPersistentModData("cgIntro", clip.title);
                });
                button.targetGraphic.color = new Color32(125, 125, 125, 255);
                GameObject newText = GameObject.Instantiate(text, musicButton.transform);
                newText.transform.localPosition = new Vector3(40f, 10f, 0f);
                newText.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
                newText.transform.localScale = new Vector3(-1f, 1f, 1f);
                Text titleText = newText.GetComponent<Text>();
                titleText.resizeTextForBestFit = true;
                titleText.text = clip.title;
            }
            exampleButton.SetActive(false);
            text.SetActive(false);

            introParent.gameObject.SetActive(false);
            introParent.gameObject.AddComponent<HudOpenEffect>();
            gridParent.gameObject.SetActive(false);
            gridParent.gameObject.AddComponent<HudOpenEffect>();

            GameObject buttonTemplate = __instance.transform.Find("Canvas").Find("Waves").Find("Panel").Find("0").gameObject;
            buttonTemplate.SetActive(false);

            GameObject loopSubpageButton = GameObject.Instantiate(buttonTemplate, title);
            loopSubpageButton.transform.localPosition = new Vector3(0f, -30, 0);
            WaveSetter loopSetter = loopSubpageButton.GetComponent<WaveSetter>();
            ShopButton newLoopButton = loopSubpageButton.AddComponent<ShopButton>();
            newLoopButton.clickSound = Traverse.Create(loopSetter).Field("buttonSuccess").GetValue() as GameObject;
            GameObject.Destroy(loopSetter);
            newLoopButton.toActivate = new GameObject[] { gridParent.gameObject };
            newLoopButton.toDeactivate = new GameObject[] { introParent.gameObject };
            loopSubpageButton.GetComponentInChildren<Text>().text = "Loops";
            loopSubpageButton.SetActive(true);

            GameObject introSubpageButton = GameObject.Instantiate(buttonTemplate, title);
            introSubpageButton.transform.localPosition = new Vector3(230f, -30, 0);
            WaveSetter introSetter = introSubpageButton.GetComponent<WaveSetter>();
            ShopButton newIntroButton = introSubpageButton.AddComponent<ShopButton>();
            newIntroButton.clickSound = Traverse.Create(introSetter).Field("buttonSuccess").GetValue() as GameObject;
            GameObject.Destroy(introSetter);
            newIntroButton.toActivate = new GameObject[] { introParent.gameObject };
            newIntroButton.toDeactivate = new GameObject[] { gridParent.gameObject };
            introSubpageButton.GetComponentInChildren<Text>().text = "Intros";
            introSubpageButton.SetActive(true);

            buttonTemplate.SetActive(true);

            GameObject newButton = GameObject.Instantiate(__instance.transform.Find("Canvas").Find("Main Menu").Find("WavesButton").gameObject, __instance.transform.Find("Canvas").Find("Main Menu"));
            newButton.transform.localPosition -= new Vector3(0f, 60f, 0f);
            newButton.GetComponentInChildren<Text>().text = "MUSIC";    
            ShopButton musicShopButton = newButton.GetComponent<ShopButton>();
            musicShopButton.toDeactivate = musicShopButton.toDeactivate.AddToArray(musicShopButton.toActivate[0]);
            musicShopButton.toActivate = new GameObject[] { newPage.gameObject };

            ShopButton themeButton = __instance.transform.Find("Canvas").Find("Main Menu").Find("ThemeButton").GetComponent<ShopButton>();
            themeButton.toDeactivate = themeButton.toDeactivate.AddToArray(newPage.gameObject);
            themeButton.toDeactivate = themeButton.toDeactivate.AddToArray(gridParent.gameObject);
            themeButton.toDeactivate = themeButton.toDeactivate.AddToArray(introParent.gameObject);
            ShopButton patternButton = __instance.transform.Find("Canvas").Find("Main Menu").Find("PatternsButton").GetComponent<ShopButton>();
            patternButton.toDeactivate = patternButton.toDeactivate.AddToArray(newPage.gameObject);
            patternButton.toDeactivate = patternButton.toDeactivate.AddToArray(gridParent.gameObject);
            patternButton.toDeactivate = patternButton.toDeactivate.AddToArray(introParent.gameObject);
            ShopButton wavesButton = __instance.transform.Find("Canvas").Find("Main Menu").Find("WavesButton").GetComponent<ShopButton>();
            wavesButton.toDeactivate = wavesButton.toDeactivate.AddToArray(newPage.gameObject);
            wavesButton.toDeactivate = wavesButton.toDeactivate.AddToArray(gridParent.gameObject);
            wavesButton.toDeactivate = wavesButton.toDeactivate.AddToArray(introParent.gameObject);
        }
        */
    }
}