using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuAnimator : MonoBehaviour
{
    public bool menuOpened = false;
    public Animator restart;
    public Animator theme;
    public Animator noAds;
    public Animator help;
    public Animator sound;
    public Animator next;
    public Animator prev;
    public Animator hint;
    public GameObject add;
    public Animator cameraSprite;
    public GameObject cameraObject;
    public bool clicked = false;
    float timer = 0.0f;

    public SaveSystem saveSystem = new SaveSystem();

    public GameData saveData = new GameData();
    // Start is called before the first frame update
    void Start()
    {
        saveData = SaveSystem.instance.LoadGame();
        if (saveData.premium == 1)
            Destroy(noAds.gameObject);

        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        Open();
 clicked = false;
    }


    public void Open()
    {

        if(!menuOpened)
        {

            openedMenu();
        }
        else
        {
            closedMenu();

        }
        menuOpened = !menuOpened;

        clicked = true;
    }

    public void openedMenu()
    {
        restart.Play("themeBOpen");
        theme.Play("themeBOpen");
        help.Play("themeBOpen");
        if (noAds)
            noAds.Play("themeBOpen");
        prev.Play("themeBOpen");
        sound.Play("themeBOpen");
        next.Play("themeBOpen");
        hint.Play("themeBOpen");
        if (cameraSprite)
            cameraSprite.Play("themeBOpen");

        restart.GetComponent<Button>().interactable = true;
        theme.GetComponent<Button>().interactable = true;
        if (noAds)
            noAds.GetComponent<Button>().interactable = true;
        help.GetComponent<Button>().interactable = true;
        prev.GetComponent<Button>().interactable = true;
        sound.GetComponent<Button>().interactable = true;
        next.GetComponent<Button>().interactable = true;
        hint.GetComponent<Button>().interactable = true;
    }

    void closedMenu()
    {
        restart.Play("themeBclose");
        theme.Play("themeBclose");
        if (noAds)
            noAds.Play("themeBclose");
        help.Play("themeBclose");
        prev.Play("themeBclose");
        sound.Play("themeBclose");
        next.Play("themeBclose");
        hint.Play("themeBclose");
        if(cameraSprite)
        cameraSprite.Play("themeBclose");

        restart.GetComponent<Button>().interactable = false;
        theme.GetComponent<Button>().interactable = false;
        if(noAds)
        noAds.GetComponent<Button>().interactable = false;
        help.GetComponent<Button>().interactable = false;
        prev.GetComponent<Button>().interactable = false;
        sound.GetComponent<Button>().interactable = false;
        next.GetComponent<Button>().interactable = false;
        hint.GetComponent<Button>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraObject.GetComponent<cameraScript>().GameOverB)
        {
            hint.GetComponent<Button>().interactable = false;
            add.GetComponent<Button>().interactable = false;
        }
        timer += Time.deltaTime;
        if (timer > 3.0f && !clicked)
            Open();
    }
}
