using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class homeMenu : MonoBehaviour
{


    public int level = 0;
    public GameObject levelManager;
    public GameObject menu;
    public Text levelText;
    public SaveSystem saveSystem = new SaveSystem();

    public GameData saveData = new GameData();
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        saveData = SaveSystem.instance.LoadGame();
        levelManager.GetComponent<levelManager>().level = saveData.level;
        Debug.Log(saveData.level);
        level = saveData.level;
       
        levelText.text = "#" + (level + 1);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    void Update()
    {
        Debug.Log(saveData.currentActiveLevel);
        Debug.Log(saveData.level);

        if ((saveData.level == saveData.currentActiveLevel -1 && this.gameObject.tag == "navB") || (saveData.level == 0 && saveData.currentActiveLevel ==0 && this.gameObject.tag == "navB") || (saveData.currentActiveLevel >= 21  && saveData.level ==20&& this.gameObject.tag == "navB"))
            this.gameObject.SetActive(false);
        if (level == 0 && this.gameObject.tag == "navPrev")
            this.gameObject.SetActive(false);
    }

    public void next()
    {


        

        if (level < saveData.currentActiveLevel && menu.GetComponent<menuAnimator>().menuOpened && level < 21)
        {
            saveData.theme = levelManager.GetComponent<levelManager>().theme;
            levelManager.GetComponent<levelManager>().level = saveData.nextLevel();
            SaveSystem.instance.SaveGame(saveData);
            SceneManager.LoadScene(0);
    
        }

    }

    public void prev()
    {
        if (level > 0 &&  menu.GetComponent<menuAnimator>().menuOpened)
        {
            saveData.theme = levelManager.GetComponent<levelManager>().theme;
            levelManager.GetComponent<levelManager>().level = saveData.prevLevel();
            SaveSystem.instance.SaveGame(saveData);
            SceneManager.LoadScene(0);
        }

    }


    public void chooseLevel1()
    {

        levelManager.GetComponent<levelManager>().level = 0;
        SceneManager.LoadScene(0);

    }
    public void chooseLevel2()
    {

        levelManager.GetComponent<levelManager>().level = 1;
        SceneManager.LoadScene(0);

    }
    public void chooseLevel3()
    {
        levelManager.GetComponent<levelManager>().level = 2;
        SceneManager.LoadScene(0);

    }
}
