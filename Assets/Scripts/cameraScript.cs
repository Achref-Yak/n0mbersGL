using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;
public class cameraScript : MonoBehaviour

{
    // Start is called before the first frame update
    public AudioSource audioData1;
    AudioSource rotateAudio;

    public string theme = "light";
    public List<GameObject> blockList = new List<GameObject>();
    public List<int> modifiedBlocksIndexes = new List<int>();
    public int[] numbersTemp = new int[4];
    Dictionary<int, List<int>> Pairs = new Dictionary<int, List<int>>();
    private float startPosX;
    private float startPosY;
    private bool isBeignHeld = false;
    public Transform block;
    public GameObject blockObject;
    public float timer = 0.0f;
    public bool canRotate = false;
    public int locked = 0;
    public int angle = 1;
    public GameObject Spawner;
    public GameObject otherBlock;
    public Color32 blockColor;
    public GameObject Selector;
    public int[] numbers = new int[4];
    private List<int> disableds;
    public bool release = false;
    public int[] currentNumbers = new int[4];
    public int order = 0;
    private float maxTime = 0.3f;
    public int scoreint = 0;
    float deltaX, deltaY;

    public float turnRatio = 0.0f;
    public bool drag = false;
    public GameObject lastBlock;
    public GameObject BlockToAdd;
    public GameObject GameOver;
    public GameObject itemsManager;
    public GameObject canvas;
    public bool GameOverB = false;
    public int kol = 0;
    Color cl;
    public int[] coms = new int[4];
    int step = -1;
    public bool canReturn = false;
    //ArrayList helpersT = new ArrayList() { 0, 1, 0, 4, 2, 6, 6, 5, 3, 7, 11, 15, 8, 12, 8, 9, 10, 14, 14, 13 };
    ArrayList helperAll = new ArrayList();
    public GameObject returnButton;
    public List<int> CorruptedLists = new List<int>();
    public GameObject menuAnim;
    public int finishedBlocks;
    public int finishedBlocksT=0;
    public SaveSystem saveSystem = new SaveSystem();
    public GameObject levelManager;
    public GameData saveData = new GameData();
    public GameObject Blocks;
    public GameObject dimmer;
    public GameObject won;
    public GameObject help;
    private bool isHelpShowing = false;
    public bool playerBegan = false;
    public GameObject soundConfig;
    public IDictionary<int, int> sumsByIndex = new Dictionary<int, int>();
    public GameObject removeAds;
    public GameObject rateus;
    public GameObject thanks;


    public int premium = 0;
 
    void Start()
    {

        Blocks = GameObject.FindGameObjectWithTag("Blocks");
        soundConfig = GameObject.FindGameObjectWithTag("SoundConfig");
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        returnButton.GetComponent<CanvasRenderer>().SetColor(new Color32(231, 231, 231, 100));
        help = GameObject.FindGameObjectWithTag("Help");
        help.SetActive(false);
        removeAds = GameObject.FindGameObjectWithTag("removeAds");
        removeAds.SetActive(false);
        blockList = Spawner.GetComponent<Spawner>().blockList;


        sumsByIndexInitializer();


        canvas = GameObject.FindGameObjectWithTag("Canvas");


        int index = -1;

        saveData = SaveSystem.instance.LoadGame();
        theme = saveData.theme;
        levelManager.GetComponent<levelManager>().theme = theme;


      
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Camera>().backgroundColor = themeSystem.getBackgroundTheme(theme);
        cl = themeSystem.getIdleTheme(theme);

        if (numberBlocks(blockList) == 1 && !GameOverB )
        {
            GameObject gm = (GameObject)Instantiate(won);
            gm.transform.SetParent(canvas.transform);
            gm.transform.localScale = new Vector3(1, 1, 1);
            gm.transform.eulerAngles = new Vector3(0, 0, 0);
            gm.transform.localPosition = new Vector3(0, 0, 0);
            GameOverB = true;
        }
    
        if (finishedBlocksT<finishedBlocks)
        {
            if(soundConfig.GetComponent<soudConfig>().soundState == 0)
            {
               
            audioData1 = GameObject.FindGameObjectWithTag("thisIsRight").GetComponent<AudioSource>();
            audioData1.Play(0);
            Debug.Log("started");
             }
            finishedBlocksT = finishedBlocks;
        }
         finishedBlocks = 0;
         disableds = getDisableds();
        foreach (GameObject block in blockList)
        {
            
            Pairs = block.GetComponent<Block>().Pairs;
            if (checkAdjInDisableds(Pairs[blockList.IndexOf(block)], disableds) &&!GameOverB && !CorruptedLists.Contains(blockList.IndexOf(block)))
            {
                finishedBlocks++;
                
                  
 
                if (theme=="dark")
                foreach (int ind in Pairs[blockList.IndexOf(block)])
                        blockList[ind].GetComponentInChildren<SpriteRenderer>().color = new Color32(70,70,70,255);
                else
                    foreach (int ind in Pairs[blockList.IndexOf(block)])
                        blockList[ind].GetComponentInChildren<SpriteRenderer>().color = new Color32(230, 230, 230, 255);

            }
            else
            block.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(block.GetComponentInChildren<SpriteRenderer>().color, cl, Time.deltaTime*2);

        }
        if (finishedBlocks == 16)
        {

            // won!

      
            GameOverB = true;
            for (int i = 0; i < 16; i++)
            {
                if (theme == "dark")
                    blockList[i].GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                else
                    blockList[i].GetComponentInChildren<SpriteRenderer>().color = new Color32(150, 150, 150, 255);
            }
            menuAnim.GetComponent<menuAnimator>().openedMenu();
            saveData.currentActiveLevel = levelManager.GetComponent<levelManager>().level + 1;
            levelManager.GetComponent<levelManager>().level++;
            SaveSystem.instance.SaveGame(saveData);
            GameObject rt;
            GameObject thnx;
            if (saveData.level == 5)
             rt = (GameObject)Instantiate(rateus);
            if (saveData.level == 20)
            {
                GameObject gm = (GameObject)Instantiate(won);
                gm.transform.SetParent(canvas.transform);
                gm.transform.localScale = new Vector3(1, 1, 1);
                gm.transform.eulerAngles = new Vector3(0, 0, 0);
                gm.transform.localPosition = new Vector3(0, 0, 0);
            }
            if (saveData.level == 20)
                thnx = (GameObject)Instantiate(thanks);

        }

        if (finishedBlocks==16)
        {
            // won!
            GameObject gm = (GameObject)Instantiate(won);
            gm.transform.SetParent(canvas.transform);
            gm.transform.localScale = new Vector3(1, 1, 1);
            gm.transform.eulerAngles = new Vector3(0, 0, 0);
            gm.transform.localPosition = new Vector3(0, 0, 0);
            GameOverB = true;
            for (int i =0; i < 16; i++)
            {
                if(theme == "dark")
                blockList[i].GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                else
                    blockList[i].GetComponentInChildren<SpriteRenderer>().color = new Color32(150, 150, 150, 255);
            }
           menuAnim.GetComponent<menuAnimator>().openedMenu();
            saveData.currentActiveLevel = levelManager.GetComponent<levelManager>().level+1;
            levelManager.GetComponent<levelManager>().level++;
            SaveSystem.instance.SaveGame(saveData);
            


        }


        BlockToAdd = GameObject.FindGameObjectWithTag("add");

        if (angle > 4)
            angle = 1;


        toggleReturn(canReturn);

        if (Input.touchCount > 0 && blockList[0].GetComponent<Block>().gameStarted && !GameOverB)
        {
            Touch touch = Input.GetTouch(0);



            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {

                case TouchPhase.Began:
            
                    turnRatio = 0.0f;
                    startPosY = touch.position.y;
                    timer = 0.0f;
                    if (canRotate && locked == 1)
                        release = true;
                    else
                        release = false;
                    RaycastHit2D hitP = Physics2D.Raycast(touchPosition, Vector2.zero);
                    if (hitP != null && hitP.collider != null && hitP.collider.tag == "Place")
                    {
                        try
                        {
                         
                            BlockToAdd.GetComponent<blockDragHandler>().target = hitP.transform.position;
                            BlockToAdd.GetComponent<blockDragHandler>().place = hitP.collider.gameObject;
                            BlockToAdd.GetComponent<blockDragHandler>().Drag = true;
                        }
                        catch (NullReferenceException e)
                        {

                        }


                    }
                    if (hitP != null && hitP.collider != null && hitP.collider.tag == "Block")
                    {

                      
                        
                        setBlockValues();



                    }




                    break;
                case TouchPhase.Stationary:
          
                    timer += Time.deltaTime;

                    if (canRotate && locked == 1)
                        maxTime = 0.3f;
                    else
                        maxTime = 0.001f;

                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
      
                    if (timer >= maxTime)
                    {

                        if (hit != null && hit.collider != null && hit.collider.tag == "Block" && (hit.collider.gameObject.transform == block || block == null))
                        {

                            if (!release)
                            {

                                block = hit.collider.transform;
                                lastBlock = hit.collider.transform.gameObject;
                                currentNumbers = block.GetComponent<Block>().numbers;

                                
                                if (!canRotate)
                                {
                                    GameObject selector = (GameObject)GameObject.FindGameObjectWithTag("Selector");
                                    if (selector == null)
                                    {
                                        GameObject selectorNew = (GameObject)Instantiate(Selector, block.transform.position + new Vector3(0, 0, -0.5f), transform.rotation);
                                        selectorNew.transform.localScale = block.GetComponent<Block>().blockSize;

                                    }

                                }

                                if (hit.transform.gameObject.GetComponent<Block>().NewBlock)
                                    hit.transform.gameObject.GetComponent<Block>().NewBlock = false;




                                timer = maxTime;

                                canRotate = true;
                                locked = 0;
                            }
                            else
                            {
                                canRotate = false;
                                locked = 0;


                            }





                        }



                    }


                    break;
                case TouchPhase.Moved:
                    float ypos = touch.deltaPosition.y;
            


                    turnRatio += Math.Abs(ypos);

                  

                    if (turnRatio > 150.0f)
                    {
                        if (block != null && (block.GetComponent<Block>().i1 + block.GetComponent<Block>().i2 + block.GetComponent<Block>().i3 + block.GetComponent<Block>().i4 !=0))
                        {
                            block.SendMessage("Flip");
                            playerBegan = true;
                            angle++;
                            int _column = block.gameObject.GetComponent<Block>().Column;
                            int _row = block.gameObject.GetComponent<Block>().Row;
                            block.GetComponentInChildren<Animator>().Play("blockRotate");

                            turnRatio = 0.0f;
                            if(soundConfig.GetComponent<soudConfig>().soundState == 0)
                                    {
                            rotateAudio = GameObject.FindGameObjectWithTag("rotateAudio").GetComponent<AudioSource>();
                            rotateAudio.Play(0);
                                }
                            try
                            {
                               // checkChangedAndAct(block.gameObject);
                            }
                            catch (System.Exception e)
                            {

                            }

                        }




                    }


                    break;
                case TouchPhase.Ended:
                    locked = 0;
                    



                    canRotate = false;
                    if (canRotate && locked == 1)
                    {
                        block.GetComponentInChildren<Animator>().Play("blockRotate");
               
                    }
           
                 
                    timer = 0.0f;
             
                    if (locked == 0 && !canRotate && block)
                    {

                        GameObject selector = (GameObject)GameObject.FindGameObjectWithTag("Selector");
                        if (selector != null)
                            Destroy(selector.gameObject);



                        try
                        {
                            otherBlock = Spawner.GetComponent<Spawner>().GetBlock(block.GetComponent<Block>().Column, block.GetComponent<Block>().Row - 1);
                            //TOP BLOCK

                            if (otherBlock != null && block.GetComponent<Block>().numbers[0] >= otherBlock.GetComponent<Block>().numbers[2])
                            {
                                if (otherBlock.GetComponent<Block>().numbers[2] != 0 && block.GetComponent<Block>().numbers[0] != 0)
                                {
                                    otherBlock.GetComponentInChildren<Animator>().Play("blockAnim");
                                    if(soundConfig.GetComponent<soudConfig>().soundState == 0)
                                    {
                                          rotateAudio = GameObject.FindGameObjectWithTag("blip1").GetComponent<AudioSource>();
                                    rotateAudio.Play(0);
                                    }
                                  
                                     if (!getSetByIndex(blockList.IndexOf(block.gameObject)).SequenceEqual(getSetByIndex(blockList.IndexOf(otherBlock.gameObject))))
                                    {
                                        addtoCorruptList(blockList.IndexOf(block.gameObject));
                                        addtoCorruptList(blockList.IndexOf(otherBlock.gameObject));
                                    } 
                                }

                                // SETTING ADJACENTS CONST

                                int tempScore = otherBlock.GetComponent<Block>().numbers[2];
                                block.GetComponent<Block>().numbers[0] = block.GetComponent<Block>().numbers[0] - otherBlock.GetComponent<Block>().numbers[2];
                                scoreint += tempScore;




                                block.SendMessage("UpdateNum");

                                otherBlock.GetComponent<Block>().numbers[2] = 0;
                                otherBlock.SendMessage("UpdateNum");

                                block.GetComponentInChildren<Animator>().Play("blockAnim");





                            }

                        }

                        catch (Exception e)
                        {

                        }

                        //LEFT BLOCK
                        try
                        {
                            otherBlock = Spawner.GetComponent<Spawner>().GetBlock(block.GetComponent<Block>().Column - 1, block.GetComponent<Block>().Row);
                            if (otherBlock != null && block.GetComponent<Block>().numbers[1] >= otherBlock.GetComponent<Block>().numbers[3])
                            {
                                if (otherBlock.GetComponent<Block>().numbers[3] != 0 && block.GetComponent<Block>().numbers[1] != 0)
                                {
                                    otherBlock.GetComponentInChildren<Animator>().Play("blockAnim");
                                      if(soundConfig.GetComponent<soudConfig>().soundState == 0)
                                    {
                                    rotateAudio = GameObject.FindGameObjectWithTag("blip1").GetComponent<AudioSource>();
                                    rotateAudio.Play(0);
                                        }
                                     if (!getSetByIndex(blockList.IndexOf(block.gameObject)).SequenceEqual(getSetByIndex(blockList.IndexOf(otherBlock.gameObject))))
                                    {
                                        addtoCorruptList(blockList.IndexOf(block.gameObject));
                                        addtoCorruptList(blockList.IndexOf(otherBlock.gameObject));
                                    } 
                                }


                                int tempScore = otherBlock.GetComponent<Block>().numbers[3];
                                block.GetComponent<Block>().numbers[1] = block.GetComponent<Block>().numbers[1] - otherBlock.GetComponent<Block>().numbers[3];
                                scoreint += tempScore;
                                // SETTING ADJACENTS CONST



                                block.SendMessage("UpdateNum");

                                otherBlock.GetComponent<Block>().numbers[3] = 0;
                                otherBlock.SendMessage("UpdateNum");

                                block.GetComponentInChildren<Animator>().Play("blockAnim");



                            }

                        }
                        catch (Exception e)
                        {

                        }
                        //BOTTOM BLOCK
                        try
                        {
                            otherBlock = Spawner.GetComponent<Spawner>().GetBlock(block.GetComponent<Block>().Column, block.GetComponent<Block>().Row + 1);
                            if (otherBlock != null && block.GetComponent<Block>().numbers[2] >= otherBlock.GetComponent<Block>().numbers[0])
                            {
                                if (otherBlock.GetComponent<Block>().numbers[0] != 0 && block.GetComponent<Block>().numbers[2] != 0)
                                {
                                    otherBlock.GetComponentInChildren<Animator>().Play("blockAnim");
                                    if(soundConfig.GetComponent<soudConfig>().soundState == 0)
                                    {
                                    rotateAudio = GameObject.FindGameObjectWithTag("blip1").GetComponent<AudioSource>();
                                    rotateAudio.Play(0);
                                        }
                                     if (!getSetByIndex(blockList.IndexOf(block.gameObject)).SequenceEqual(getSetByIndex(blockList.IndexOf(otherBlock.gameObject))))
                                    {
                                        addtoCorruptList(blockList.IndexOf(block.gameObject));
                                        addtoCorruptList(blockList.IndexOf(otherBlock.gameObject));
                                    } 
                                }


                                int tempScore = otherBlock.GetComponent<Block>().numbers[0];
                                block.GetComponent<Block>().numbers[2] = block.GetComponent<Block>().numbers[2] - otherBlock.GetComponent<Block>().numbers[0];
                                scoreint += tempScore;
                                // SETTING ADJACENTS CONST


                                block.SendMessage("UpdateNum");

                                otherBlock.GetComponent<Block>().numbers[0] = 0;
                                otherBlock.SendMessage("UpdateNum");

                                block.GetComponentInChildren<Animator>().Play("blockAnim");



                            }


                        }
                        catch (Exception e)
                        {

                        }
                        //RIGHT BLOCK
                        try
                        {
                            otherBlock = Spawner.GetComponent<Spawner>().GetBlock(block.GetComponent<Block>().Column + 1, block.GetComponent<Block>().Row);
                            if (otherBlock != null && block.GetComponent<Block>().numbers[3] >= otherBlock.GetComponent<Block>().numbers[1])
                            {
                                if (otherBlock.GetComponent<Block>().numbers[1] != 0 && block.GetComponent<Block>().numbers[3] != 0)
                                {
                                    otherBlock.GetComponentInChildren<Animator>().Play("blockAnim");
                                    if(soundConfig.GetComponent<soudConfig>().soundState == 0)
                                    {
                                    rotateAudio = GameObject.FindGameObjectWithTag("blip1").GetComponent<AudioSource>();
                                    rotateAudio.Play(0);
                                        }
                                    if (!getSetByIndex(blockList.IndexOf(block.gameObject)).SequenceEqual(getSetByIndex(blockList.IndexOf(otherBlock.gameObject))))
                                    {
                                        addtoCorruptList(blockList.IndexOf(block.gameObject));
                                        addtoCorruptList(blockList.IndexOf(otherBlock.gameObject));
                                    } 
                                }

                                int tempScore = otherBlock.GetComponent<Block>().numbers[1];

                                block.GetComponent<Block>().numbers[3] = block.GetComponent<Block>().numbers[3] - otherBlock.GetComponent<Block>().numbers[1];
                                scoreint += tempScore;
                                // SETTING ADJACENTS CONST

                                block.SendMessage("UpdateNum");

                                otherBlock.GetComponent<Block>().numbers[1] = 0;
                                otherBlock.SendMessage("UpdateNum");

                                block.GetComponentInChildren<Animator>().Play("blockAnim");



                            }

                        }
                        catch (Exception e)
                        {

                        }







                    }
                    if (canRotate)
                        locked = 1;

                    

                    for (int i = 0; i < Spawner.GetComponent<Spawner>().blockList.Count; i++)
                    {
                        if (Spawner.GetComponent<Spawner>().blockList[i] != null)
                            Spawner.GetComponent<Spawner>().blockList[i].GetComponent<Block>().setColor();
                    }

                    if (locked == 0)
                    {

                        checkChangedAndAct(lastBlock);

                        // check if last block is among finished sets
                        if (lastBlock)
                        {
                            List<int> disableds = getDisableds();
                            Pairs = lastBlock.GetComponent<Block>().Pairs;
                            if (checkAdjInDisableds(Pairs[blockList.IndexOf(lastBlock)], disableds))
                            {
                                canReturn = false;

                              
                            }

                        }



                       // checkSetHealth();

          
                        block = null;
                    
                      
                    }



                    break;

            }

        }

    

         

    }
    List<int> getSetByIndex(int index)
    {
        Dictionary<int, List<int>> PairsT = new Dictionary<int, List<int>>();
      
            PairsT = blockList[index].GetComponent<Block>().Pairs;
            return PairsT[index];
      


    }


    int numberBlocks(List<GameObject> blockList)
    {
        int c = 0;
        foreach (GameObject block in blockList)
        {
            if (block != null)
                c++;
        }
        return c;
    }


    private int getSum(List<int> numbersT)
    {
        int sum = 0;
        foreach(int number in numbersT)
        {
            sum++;
        }
        return sum;
    }

    private void sumsByIndexInitializer()
    {
        foreach (GameObject block in blockList)
        {
            sumsByIndex.Add(new KeyValuePair<int, int>(blockList.IndexOf(block), getSum(getSetByIndex(blockList.IndexOf(block)))));
            Debug.Log("5"+blockList.IndexOf(block));
             
        }


    }

    private void checkSetHealth()
    {
      
        foreach (GameObject block in blockList)
        {
            if (getSum(getSetByIndex(blockList.IndexOf(block))) != sumsByIndex[blockList.IndexOf(block)])
            {
                addtoCorruptList(blockList.IndexOf(block));

            }
        }
        
     
    }
    private void toggleReturn(bool can)
    {
        if(can)
        {
            if(theme=="dark")
            returnButton.GetComponent<CanvasRenderer>().SetColor(new Color32(231, 231, 231, 255));
            else
                returnButton.GetComponent<CanvasRenderer>().SetColor(new Color32(200, 200, 200, 255));
        }

        else
        {
            if (theme == "dark")
                returnButton.GetComponent<CanvasRenderer>().SetColor(new Color32(231, 231, 231, 170));
            else
                returnButton.GetComponent<CanvasRenderer>().SetColor(new Color32(200, 200, 200, 170));
        }
            

    }

    public bool checkChangedAndAct(GameObject block)
    {
        // check if we can return values
        if(block)
        {
             if (checkBlockChanged(block))
        {
           
            canReturn = true;

        }

        else
        {
            canReturn = false;
           
        }
        }
       

        return canReturn;
    }
    public bool checkBlockChanged(GameObject block)
    {
        if (block.GetComponent<Block>().i1 != block.GetComponent<Block>().numbers[0])
            return true;
        if (block.GetComponent<Block>().i2 != block.GetComponent<Block>().numbers[1])
            return true;
        if (block.GetComponent<Block>().i3 != block.GetComponent<Block>().numbers[2])
            return true;
        if (block.GetComponent<Block>().i4 != block.GetComponent<Block>().numbers[3])
            return true;
        return false;
    }


    void setCorrectSetBlock()
    {
        for (int j = 0; j < 16; j++)
        {

            Dictionary<int, List<int>> Pairs = new Dictionary<int, List<int>>();
            Pairs = blockList[j].GetComponent<Block>().Pairs;

        }
    }
 
  


    public struct blockPosition
    {
        public int xpos;
        public int ypos;
        public void setPosition(int x, int y)
        {
            xpos = x;
            ypos = y;
          
        }
    }


    public void helper()
    {

        // substract from hint coins

     
            List<int> disableds = getDisableds();
            // remove dulplicate sets and add them to blockAll

            int index = -1;

            List<List<int>> helperAll = new List<List<int>>();
            foreach (GameObject block in blockList)
                try
                {


                    // check if set is not finished then add it to helperAll

                    index = blockList.IndexOf(block);
                    Pairs = block.GetComponent<Block>().Pairs;
                    if (!checkAdjInDisableds(Pairs[index], disableds))
                    {

                        if (index == 0)
                            helperAll.Add(Pairs[index]);
                        else if (!checkPrevs(Pairs, Pairs[index], index))
                            helperAll.Add(Pairs[index]);

                    }
                    else
                        index++;


                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }

            // get sets from helperAll and indicate them 

            step++;
            if (step > helperAll.Count - 1)
                step = 0;

 




            foreach (int val in helperAll[step])
            {

                if(theme=="dark")
            {
                blockList[val].GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, cl, Time.deltaTime);
                blockList[val].GetComponentInChildren<Animator>().Play("blockAnimNewest");
            }
                else if (theme=="light")
            {
                blockList[val].GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.grey, cl, Time.deltaTime);
                blockList[val].GetComponentInChildren<Animator>().Play("blockAnimNewest");
            }
                
            /*for (int i = 0; i < 4; i++)
            {
                blockList[val].GetComponent<Block>().numbers[i] = 0;
                    

            }*/

            }
    


        
    }

    public List<int>  Shuffle( List<int> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r =  Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }

        return ts;
    }
    private bool checkPrevs(Dictionary<int, List<int>> Pairs, List<int> cPairs, int cindex)
    {


        for (int i = 0; i < cindex; i++)

            foreach (int val in Pairs[i])
                if (val == cindex)
                    return true;
 

        return false;
    }

    public void shuffle()
    {

        for (int i = 0; i < 16; i++)
        {
            blockList[i].GetComponent<Block>().coms = blockList[i].GetComponent<Block>().setComb(blockList[i].GetComponent<Block>().Blocknum);
            blockList[i].GetComponent<Block>().numbers = blockList[i].GetComponent<Block>().coms;
        }
    }



    public void setBlockValues()
    {
        kol++;
        for (int i = 0; i < 16; i++)
        {


            if (blockList[i] != null)
            {
                blockList[i].GetComponent<Block>().i1 = blockList[i].GetComponent<Block>().numbers[0];
                blockList[i].GetComponent<Block>().i2 = blockList[i].GetComponent<Block>().numbers[1];
                blockList[i].GetComponent<Block>().i3 = blockList[i].GetComponent<Block>().numbers[2];
                blockList[i].GetComponent<Block>().i4 = blockList[i].GetComponent<Block>().numbers[3];

                if (blockList[i].GetComponent<Block>().numbers[0] == 0 && blockList[i].GetComponent<Block>().numbers[1] == 0 && blockList[i].GetComponent<Block>().numbers[2] == 0 && blockList[i].GetComponent<Block>().numbers[3] == 0)
                    blockList[i].GetComponent<Block>().deactivateBlock();
            }

        }


    }


    void addtoCorruptList(int index)
    {
      

        foreach (int val in blockList[index].GetComponent<Block>().Pairs[index])
            if(!CorruptedLists.Contains(val))
            CorruptedLists.Add(val);
    }


     
    public void returnValues()
    {
           


        List<int> disableds = getDisableds();

     

        if (canReturn&& playerBegan)
        {
            // substract from hint coins

            if (itemsManager.GetComponent<itemsManager>().backs > 0)
            {
                itemsManager.GetComponent<itemsManager>().backs -= 1;
            }

 

                for (int i = 0; i < 16; i++)
            {

                try
                {
           
                    Dictionary<int, List<int>> Pairs = new Dictionary<int, List<int>>();
                    Pairs = blockList[i].GetComponent<Block>().Pairs;

                    if (!checkAdjInDisableds(Pairs[i], disableds) ||  CorruptedLists.Contains(i))
                    {
                        // block is not among finished sets

                        blockList[i].GetComponent<BoxCollider2D>().enabled = true;
                        blockList[i].GetComponent<Block>().enabled = true;



                        blockList[i].GetComponent<Block>().reactivateBlock();


                        blockList[i].GetComponent<Block>().numbers[0] = blockList[i].GetComponent<Block>().i1;
                        blockList[i].GetComponent<Block>().numbers[1] = blockList[i].GetComponent<Block>().i2;
                        blockList[i].GetComponent<Block>().numbers[2] = blockList[i].GetComponent<Block>().i3;
                        blockList[i].GetComponent<Block>().numbers[3] = blockList[i].GetComponent<Block>().i4;

                        if (blockList[i].GetComponent<Block>().NewBlock)
                        {
                            blockList[i].GetComponent<Block>().numbers[0] = blockList[i].GetComponent<Block>().i1;
                            blockList[i].GetComponent<Block>().numbers[1] = blockList[i].GetComponent<Block>().i2;
                            blockList[i].GetComponent<Block>().numbers[2] = blockList[i].GetComponent<Block>().i3;
                            blockList[i].GetComponent<Block>().numbers[3] = blockList[i].GetComponent<Block>().i4;
                        }

                    }


                    

                }
                catch (MissingReferenceException)
                {
                    Debug.Log("ret " + i);
                }

            }

    }
      



       
     
           

    }
    private List<int> getDisableds()
    {
        // check if set is not finished then return values

        List<int> disableds = new List<int>();
        disableds.Clear();

        foreach (GameObject block in blockList)
        {
            if (!block.GetComponent<Block>().enabled)
            { 
                disableds.Add(blockList.IndexOf(block));
                
            }

        }

        return disableds;
    }

    public void themeToggle()
    { saveData = SaveSystem.instance.LoadGame();
        if (menuAnim.GetComponent<menuAnimator>().restart.GetComponent<Button>().interactable)
        if (theme == "dark")
            theme = "light";
        else
            theme="dark";

        saveData.theme = theme;
        levelManager.GetComponent<levelManager>().theme = theme;
        SaveSystem.instance.SaveGame(saveData);
    }

   
    public void restart()
    {
        if (menuAnim.GetComponent<menuAnimator>().restart.GetComponent<Button>().interactable)
            Application.LoadLevel(Application.loadedLevel);
    }

    public void premiumPurchase()
    {
        premium = 1;
        saveData.premium = premium;
        SaveSystem.instance.SaveGame(saveData);
    }

    private bool checkAdjInDisableds(List<int> vals, List<int> disableds)
    {

        foreach (int val in vals)
        {
            //  Debug.Log("ch"+val+"="+)
            if (!disableds.Contains(val))
                // block sets are not modified, we need to reset them.

                return false;
        }

        // block sets are all deleted correctly, we can't reset this set.
        return true;

    }
    public void removeAdsFun()
    {
        removeAds.SetActive(true);
    }
    public void removeAdsClose()
    {
        removeAds.SetActive(false);
    }

    public void rateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.tocaboca.tocalifeworld");
    
    }

    public void toggleHelper()
    {
        isHelpShowing = !isHelpShowing;
        help.SetActive(isHelpShowing);
    }

    public void goHome()
    {
        SceneManager.LoadScene(0);
    }
}

