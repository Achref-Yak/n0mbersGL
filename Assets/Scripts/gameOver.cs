using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    public GameObject menu;
    public GameObject levelManager;
    public SaveSystem saveSystem = new SaveSystem();
    public GameData saveData = new GameData();
    // Start is called before the first frame update
    void Start()
    {
        saveData = SaveSystem.instance.LoadGame();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
 
        menu = GameObject.FindGameObjectWithTag("menu");
 
            menu.GetComponent<menuAnimator>().openedMenu();
    }

    public void Next()
    {
        levelManager.GetComponent<levelManager>().level = saveData.nextLevel();
        SaveSystem.instance.SaveGame(saveData);
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
