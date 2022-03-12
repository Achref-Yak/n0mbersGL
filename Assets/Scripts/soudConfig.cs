using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class soudConfig : MonoBehaviour
{

    [SerializeField]
    private GameObject bulb;

    private SpriteRenderer bulbColor;

    public int soundState;

    [SerializeField]
    private Sprite[] switchSprites;

    private Image switchImage;
    public SaveSystem saveSystem = new SaveSystem();
    public GameData saveData = new GameData();
    // Start is called before the first frame update
    void Start()
    {
        saveData = SaveSystem.instance.LoadGame();
        Debug.Log(saveData.soundState);
         soundState = saveData.soundState;
        bulbColor = bulb.GetComponent<SpriteRenderer>();
          

        switchImage = GetComponent<Button>().image;
        switchImage.sprite = switchSprites[soundState];

       
    }

    public void TurnOnAndOff()
    {
        saveData = SaveSystem.instance.LoadGame();

        soundState = 1 - soundState;
        saveData.soundState = soundState;
        SaveSystem.instance.SaveGame(saveData);
        
 
    }

    // Update is called once per frame
    void Update()
    {
        switchImage.sprite = switchSprites[soundState];
    }
}
