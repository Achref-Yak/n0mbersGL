using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class Block : MonoBehaviour
{
    public AudioSource audioData1;
    public GameObject cameraObject;
    public GameObject spawner;
    public int[] numbers = new int[4];
    public int i1;
    public int i2;
    public int i3;
    public int i4;
    public Dictionary<int, List<int>> Pairs = new Dictionary<int, List<int>>();
    public int Column;
    public int Row;
    public int C;
    public GameObject[,] blockListMatrix = new GameObject[4, 4];
    public Text num;
    public Image point;
    public Image pointTop;
    public Image pointLeft;
    public Image pointBottom;
    public Image pointRight;

    public Sprite pointRed;
    public Sprite pointYellow;
    public Sprite pointBlue;
    public Sprite zero;
    // current block numbers
    public int[] coms = new int[4];
    // initial block numbers
 
    public GameObject canvas;
    public Image imageTop;
    public Text numTop;
    public Text numBottom;
    public Text numLeft;
    public Text numRight;
    public int counter = 0;
    public int Blocknum;
    public int sum = 0;
    public GameObject shadow;
    public GameObject place;
    public GameObject Cross;
    public GameObject shadowIns;
    public Vector3 fallPosition;
    public bool gameStarted = false;
    float timer = 0.0f;
    public float startTime = 1.0f;
    public bool lost = false;
    Vector3 target = Vector3.zero;
    public float blockMargin = 1.06f;
    public bool NewBlock = false;
    public Vector3 newTargetPosition;
    float offset;
    public AnimationClip clip;
    public Vector3 blockSize;
    float betweenOfset;
    public bool blocked = false;
    public bool blockDes = false;
    public GameObject dimmer;
    public GameObject gameOver;
    public GameObject Blocks;
    public bool finishedBlock = false;
    Color32 circleZero;
    Color32 circlePostitive;
    Color32 circleDead;
    Color32 pointRedColor = new Color32(0, 88, 122, 255);
    public string theme = "light";

    public SaveSystem saveSystem = new SaveSystem();
    public GameData saveData = new GameData();


    // Start is called before the first frame update


    private void setResponsive()
    {


        float width = Screen.width;
        float height = Screen.height;
        if (width == 1080 && height == 2340)
        {
            blockSize = new Vector3(0.077f, 0.077f, 1);
            transform.localScale = blockSize;
            offset = Camera.main.ViewportToScreenPoint(this.transform.localScale).x - 9.0f;
        }

        if (width == 1080 && height == 2160)
        {
            blockSize = new Vector3(0.084f, 0.0795f, 1);
            transform.localScale = blockSize;
            offset = Camera.main.ViewportToScreenPoint(this.transform.localScale).x - 18.9f;
        }

        if (width == 720 && height == 1440)
        {
            blockSize = new Vector3(0.084f, 0.0795f, 1);
            transform.localScale = blockSize;
            offset = Camera.main.ViewportToScreenPoint(this.transform.localScale).x - 18.9f;
        }
        if (width == 720 && height == 1560)
        {
            blockSize = new Vector3(0.082f, 0.077f, 1);
            transform.localScale = blockSize;
            offset = Camera.main.ViewportToScreenPoint(this.transform.localScale).x - 18.9f;
        }
        if (width == 720 && height == 1600)
        {
            blockSize = new Vector3(0.082f, 0.077f, 1);
            transform.localScale = blockSize;
            offset = Camera.main.ViewportToScreenPoint(this.transform.localScale).x - 14.9f;
        }

        if (width == 1080 && height == 1920)
        {
            blockSize = new Vector3(0.099f, 0.082f, 1);
            transform.localScale = blockSize;
            offset = Camera.main.ViewportToScreenPoint(this.transform.localScale).x - 38.6f;
        }

        if (width == 1440 && height == 2560)
        {
            blockSize = new Vector3(0.099f, 0.090f, 1);
            transform.localScale = blockSize;
            offset = Camera.main.ViewportToScreenPoint(this.transform.localScale).x - 47.6f;
        }
        if (width == 1080 && height == 2280)
        {
            blockSize = new Vector3(0.085f, 0.080f, 1);
            transform.localScale = blockSize;
            offset = Camera.main.ViewportToScreenPoint(this.transform.localScale).x - 27.0f;
        }

        if (width == 1080 && height == 2246)
        {
            blockSize = new Vector3(0.085f, 0.080f, 1);
            transform.localScale = blockSize;
            offset = Camera.main.ViewportToScreenPoint(this.transform.localScale).x - 26.6f;
        }
        if (width == 1440 && height == 3040)
        {
     
            offset = Camera.main.ViewportToScreenPoint(this.transform.localScale).x - 30f;
        }
        if (width == 1440 && height == 2960)
        {

            offset = Camera.main.ViewportToScreenPoint(this.transform.localScale).x - 30f;
        }
       

    }

    private void setPointResponsive(Image point, Text num, int res)
    {
        if(res == (1440*2560))
        {
            point.transform.localScale = new Vector3(point.transform.localScale.x + 0.2f, point.transform.localScale.y+0.2f, point.transform.localScale.z + 0.2f);
            num.fontSize = num.font.fontSize + 1;
        }
        if (res == (1440 * 2960))
        {
            num.resizeTextForBestFit = false;
            point.transform.localScale = new Vector3(point.transform.localScale.x + 0.2f, point.transform.localScale.y + 0.2f, point.transform.localScale.z + 0.2f);
            num.fontSize = 70;
        }
        if (res == (1080 * 2280))
        {
            point.transform.localScale = new Vector3(point.transform.localScale.x + 0.05f, point.transform.localScale.y + 0.05f, point.transform.localScale.z + 0.2f);
            num.fontSize = num.font.fontSize + 1;
        }
        if (res == (1080 * 2246))
        {
            point.transform.localScale = new Vector3(point.transform.localScale.x + 0.05f, point.transform.localScale.y + 0.05f, point.transform.localScale.z + 0.2f);
            num.fontSize = num.font.fontSize + 1;
        }
        if (res == (720 * 1440))
        {
            point.transform.localScale = new Vector3(point.transform.localScale.x - 0.3f, point.transform.localScale.y - 0.3f, point.transform.localScale.z - 0.3f);
            num.resizeTextForBestFit = false;
            num.fontSize = 40 ;
        }
        if (res == (720 * 1600))
        {
            point.transform.localScale = new Vector3(point.transform.localScale.x - 0.3f, point.transform.localScale.y - 0.3f, point.transform.localScale.z - 0.3f);
            num.resizeTextForBestFit = false;
            num.fontSize = 40;

        }
            if (res == (720 * 1560))
        {
            point.transform.localScale = new Vector3(point.transform.localScale.x - 0.3f, point.transform.localScale.y - 0.3f, point.transform.localScale.z - 0.3f);
            num.resizeTextForBestFit = false;
            num.fontSize = 40;
        }
        if (res == (1440 * 3040))
        {
            point.transform.localScale = new Vector3(point.transform.localScale.x + 0.2f, point.transform.localScale.y + 0.2f, point.transform.localScale.z + 0.2f);
            num.fontSize = num.font.fontSize + 1;
        }

    }
    void Start()
    {
 


      


        Blocks = GameObject.FindGameObjectWithTag("Blocks");
        spawner = GameObject.FindGameObjectWithTag("Spawner");
        betweenOfset = 0.0f;

        setResponsive();


        //transform.GetComponent<BoxCollider2D>().size.x = new Vector3(convertoUnits(screenWidth), convertoUnits(screenHeight), 1);
 
        spawner = GameObject.FindGameObjectWithTag("Spawner");
        blockListMatrix = spawner.GetComponent<Spawner>().blockListMatrix;
        if (!NewBlock)
            transform.position = transform.position + new Vector3(0, 1, 0);
        else
            transform.position = newTargetPosition;
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        if (!NewBlock)
            coms = setComb(Blocknum);
        numbers = coms;

        shadowIns = (GameObject)Instantiate(shadow, new Vector3(transform.position.x, transform.position.y - 0.04f, transform.position.z), transform.rotation);
        shadowIns.transform.localScale = blockSize;

 



        // this sets the 4 numbers of each block with a random number from a combination;

        for (int i = 0; i < numbers.Length; i++)
        {
            int num = coms[i];


            numbers[i] = num;

        }

    


        canvas = GameObject.FindGameObjectWithTag("Canvas");
        pointTop = (Image)Instantiate(point, Blocks.transform.position, transform.rotation);
        float offset = Camera.main.ViewportToScreenPoint(this.transform.localScale).x;
        Vector3 namePose = Camera.main.WorldToScreenPoint(this.transform.position);
        numTop = (Text)Instantiate(num, transform.position, transform.rotation);
        setPointResponsive(pointTop, numTop, Screen.width * Screen.height);
        numTop.text = numbers[0].ToString();

        pointTop.transform.SetParent(Blocks.transform);
        pointTop.transform.position = namePose + new Vector3(0, offset, 0);
        if (numTop.text != "0")
        {
            
            pointTop.GetComponent<Image>().color = pointRedColor;
        }

        numTop.transform.SetParent(Blocks.transform);
        numTop.transform.position = namePose + new Vector3(0, offset, 0);




        pointBottom = (Image)Instantiate(point, Blocks.transform.position, transform.rotation);
        numBottom = (Text)Instantiate(num, Blocks.transform.position, transform.rotation);

        setPointResponsive(pointBottom, numBottom, Screen.width * Screen.height); 


        numBottom.text = numbers[2].ToString();
        if (numBottom.text != "0")
            pointBottom.GetComponent<Image>().color = pointRedColor;

        pointBottom.transform.SetParent(Blocks.transform);
        pointBottom.transform.position = namePose + new Vector3(0, -offset, 0);

        numBottom.transform.SetParent(Blocks.transform);
        numBottom.transform.position = namePose + new Vector3(0, -offset, 0);

         
        pointLeft = (Image)Instantiate(point, Blocks.transform.position, transform.rotation);
        numLeft = (Text)Instantiate(num, Blocks.transform.position, transform.rotation);

        setPointResponsive(pointLeft, numLeft, Screen.width * Screen.height);
        numLeft.text = numbers[1].ToString();

        if (numLeft.text != "0")
            pointLeft.GetComponent<Image>().color = pointRedColor;
        pointLeft.transform.SetParent(Blocks.transform);
        pointLeft.transform.position = namePose + new Vector3(-offset, 0, 0);


        numLeft.transform.SetParent(Blocks.transform);
        numLeft.transform.position = namePose + new Vector3(-offset, 0, 0);



        pointRight = (Image)Instantiate(point, Blocks.transform.position, transform.rotation);
        numRight = (Text)Instantiate(num, Blocks.transform.position, transform.rotation);
        setPointResponsive(pointRight, numRight, Screen.width * Screen.height);
        numRight.text = numbers[3].ToString();
        
            
        pointRight.transform.SetParent(Blocks.transform);
        pointRight.transform.position = namePose + new Vector3(offset, 0, 0);
        numRight.transform.SetParent(Blocks.transform);
        if (numRight.text != "0")
            pointRight.GetComponent<Image>().color =pointRedColor;
        numRight.transform.position = namePose + new Vector3(offset, 0, 0);
        if (checkIfBlockIsEmpty(numbers))
        {
            numRight.color = new Color(0, 0, 0, 0);
            numLeft.color = new Color(0, 0, 0, 0);
            numBottom.color = new Color(0, 0, 0, 0);
            numTop.color = new Color(0, 0, 0, 0);
        }

        setColor();


        reactivateBlock();

    }

    bool checkIfBlockIsEmpty(int[] arr)
    {
        bool b = false;
        for(int i=0; i<arr.Length; i++)
        {
            if (arr[i] == 0)
                b = false;
            else
                b = true;
        }
        return b;
    }

    // Update is called once per frame
    void Update()
    {


        theme = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraScript>().theme;
        circleZero = themeSystem.getCircleZero(theme);
        circlePostitive = themeSystem.getCirclePositive(theme);

 
            circleDead = new Color32(150, 150, 150, 255);
 





        if (!lost)
            checkBlocks();

       
        if (shadowIns != null)
            shadowIns.transform.position = transform.position + new Vector3(0, -0.04f, 0);
        //updatePoint();

        updatePosition();
        setColor();
        UpdateNum();
        Fall();

    }

    void updatePosition()
    {


        Vector3 namePose = Camera.main.WorldToScreenPoint(this.transform.position);
        if (pointBottom)
            pointBottom.transform.position = namePose + new Vector3(0, -offset, 0);
        if (pointTop)
            pointTop.transform.position = namePose + new Vector3(0, offset, 0);
        if (pointLeft)
            pointLeft.transform.position = namePose + new Vector3(-offset, 0, 0);
        if (pointRight)
            pointRight.transform.position = namePose + new Vector3(offset, 0, 0);
        numTop.transform.position = namePose + new Vector3(0, offset, 0);
        numLeft.transform.position = namePose + new Vector3(-offset, 0, 0);
        numRight.transform.position = namePose + new Vector3(offset, 0, 0);
        numBottom.transform.position = namePose + new Vector3(0, -offset, 0);


    }
    float convertoUnits(float p)
    {
        float ortho = Camera.main.orthographicSize;
        float pixelH = Camera.main.pixelHeight;
        return (p * ortho * 2) / pixelH;
    }
    void setColumn(int v)
    {
        Column = v;
    }
    void setRow(int v)
    {
        Row = v;
    }

    public void Flip()
    {

        int temp = numbers[0];
        int[] numbersT = new int[4];


        numbers = shiftRight(numbers);

        numTop.text = numbers[0].ToString();
        numBottom.text = numbers[2].ToString();
        numLeft.text = numbers[1].ToString();
        numRight.text = numbers[3].ToString();



    }

    public int[] shiftRight(int[] arr)
    {
        int[] demo = new int[arr.Length];

        for (int i = 1; i < arr.Length; i++)
        {
            demo[i] = arr[i - 1];
        }

        demo[0] = arr[demo.Length - 1];

        return demo;
    }


    public void UpdateNum()
    {
        numTop.text = numbers[0].ToString();
        numBottom.text = numbers[2].ToString();
        numLeft.text = numbers[1].ToString();
        numRight.text = numbers[3].ToString();




    }


    public void setColor()
    {
        if (numTop.text != "0")
        {
            numTop.color = new Color32(230, 230, 230, 255);
            pointTop.GetComponent<Image>().color = new Color32(232, 74, 95, 255);
        }

        else
        {
            pointTop.GetComponent<Image>().color = circlePostitive;
            numTop.color = new Color32(111, 111, 111, 255);
        }

        if (numLeft.text != "0")
        {
            pointLeft.GetComponent<Image>().color = new Color32(232, 74, 95, 255);
            numLeft.color = new Color32(230, 230, 230, 255);
        }

        else
        {
            pointLeft.GetComponent<Image>().color = circlePostitive;
            numLeft.color = new Color32(111, 111, 111, 255);
        }

        if (numBottom.text != "0")
        {
            numBottom.color = new Color32(230, 230, 230, 255);
            pointBottom.GetComponent<Image>().color = new Color32(232, 74, 95, 255);
        }

        else
        {
            pointBottom.GetComponent<Image>().color = circlePostitive;
            numBottom.color = new Color32(111, 111, 111, 255);
        }

        if (numRight.text != "0")
        {
            numRight.color = new Color32(230, 230, 230, 255);
            pointRight.GetComponent<Image>().color = new Color32(232, 74, 95, 255);
        }

        else
        {

            numRight.color = new Color32(111, 111, 111, 255);
            pointRight.GetComponent<Image>().color = circlePostitive;
        }
       

        for (int i = 0; i < 4; i++)
        {
            if (numbers[i] == 0)
                counter++;




            if (counter == 1)
            {

                this.GetComponent<BoxCollider2D>().enabled = true;
                // this.gameObject.GetComponent<Renderer>().material.color = new Color32(231, 211, 159, 255);
                shadowIns.GetComponent<Renderer>().material.color = new Color32(231, 211, 159, 130);
                /*numTop.color = new Color32(255, 255, 255, 255);
                numBottom.color = new Color32(255, 255, 255, 255);
                numLeft.color = new Color32(255, 255, 255, 255);
                numRight.color = new Color32(255, 255, 255, 255);*/

            }

            else if (counter == 2)
            {


                this.GetComponent<BoxCollider2D>().enabled = true;
                //this.gameObject.GetComponent<Renderer>().material.color = new Color32(234, 144, 133, 255);
                /*numTop.color = new Color32(255, 255, 255, 255);
                numBottom.color = new Color32(255, 255, 255, 255);
                numLeft.color = new Color32(255, 255, 255, 255);
                numRight.color = new Color32(255, 255, 255, 255);*/
            }

            else if (counter == 3)
            {

                this.GetComponent<BoxCollider2D>().enabled = true;
                //this.gameObject.GetComponent<Renderer>().material.color = new Color32(212, 93, 121, 255);
                /*numTop.color = new Color32(212, 93, 121, 255;
                numBottom.color = new Color32(255, 255, 255, 255);
                numLeft.color = new Color32(255, 255, 255, 255);
                numRight.color = new Color32(255, 255, 255, 255);*/
            }
            else if (counter == 4)
            {

                // play 0 audio
                if(this.GetComponent<Block>().enabled)
                {
                    
                  
                }
      


                deactivateBlock();
            }

        }

        counter = 0;



    }

    public bool checkSetOfBlocksCorrect()
    {

        int index;


        index = cameraObject.GetComponent<cameraScript>().blockList.IndexOf(this.gameObject);


        Pairs = blockListMatrix[Column, Row].GetComponent<Block>().Pairs;
        List<int> vals;
        Debug.Log(index + "bb");
        try
        {
            vals = Pairs[index];


            if (checkBlockSetsDead(vals))
            {
               
                return true;
            }
               
            else
                return false;
             



            

        }
        catch (System.Exception e)
        {
         //   Debug.LogError(e + " : " + index);
        }

        return false;





    }

    private bool checkBlockSetsDead(List<int> vals)
    {

        foreach (int kvp in vals)
        {

            if (cameraObject.GetComponent<cameraScript>().blockList[kvp].GetComponent<Block>().enabled)
                // block sets are not modified, we need to reset them.
                return true;
        }

        // block sets are all deleted correctly, we can't reset this set.
        return false;

    }
    /*public void createPlaces(int Row, int Column)
    {
        bool x = true;
        GameObject[] places = GameObject.FindGameObjectsWithTag("Place");
        for (int i = 0; i < places.Length; i++)
        {
            if (places[i].GetComponent<Place>().column == Column && places[i].GetComponent<Place>().row == Row)
            {
                x = false;
                break;
            }

        }

        if (x)
        {
            GameObject placeN = (GameObject)Instantiate(place, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.9f), transform.rotation);

            placeN.transform.localScale = blockSize;
            placeN.GetComponent<Place>().column = Column;
            placeN.GetComponent<Place>().row = Row;
            placeN.GetComponent<Place>().i1 = i1;
            placeN.GetComponent<Place>().i2 = i2;
            placeN.GetComponent<Place>().i3 = i3;
            placeN.GetComponent<Place>().i4 = i4;

        }

    }*/
    public void deactivateBlock()
    {




        blockDes = true;

        numTop.GetComponent<Text>().enabled = true;
        numTop.text = "0";
        numLeft.GetComponent<Text>().enabled = true;
        numLeft.text = "0";
        numRight.GetComponent<Text>().enabled = true;
        numRight.text = "0";
        numBottom.GetComponent<Text>().enabled = true;
        numBottom.text = "0";
        pointRight.GetComponent<Image>().color = circleDead;
        pointTop.GetComponent<Image>().color = circleDead;
        pointBottom.GetComponent<Image>().color = circleDead;
        pointLeft.GetComponent<Image>().color = circleDead;
        if(numTop.color.a==0)
        {
            numTop.color = new Color32(111, 111, 111, 255);
            numBottom.color = new Color32(111, 111, 111, 255);
            numRight.color = new Color32(111, 111, 111, 255);
            numLeft.color = new Color32(111, 111, 111, 255);

     
        }

        if(blocked)
    
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<Block>().enabled = false;
    }
    public void reactivateBlock()
    {
        blockDes = false;
        GameObject[] places;
        places = GameObject.FindGameObjectsWithTag("Place");
        foreach (GameObject pl in places)
        {
            if (pl.GetComponent<Place>().column == Column && pl.GetComponent<Place>().row == Row)
                Destroy(pl);
        }

        numTop.GetComponent<Text>().enabled = true;
        numLeft.GetComponent<Text>().enabled = true;
        numRight.GetComponent<Text>().enabled = true;
        numBottom.GetComponent<Text>().enabled = true;



    }



    private int[] permutate(int[] arr, int num)
    {
        for (int i = 0; i < 4; i++)
        {

            int sum = sumFun(arr);
            if (sum > num)
                for (int j = i; j < 4; j++)
                {
                    if (arr[j] == 4)
                        arr[j] = 2;
                    else if (arr[j] == 2)
                        arr[j] = 0;
                    else if (arr[j] == 8)
                        arr[j] = 4;
                }

            else if (sum < num)
                for (int j = i; j < 4; j++)
                {
                    if (arr[j] == 0)
                        arr[j] = 2;
                    else if (arr[j] == 2)
                        arr[j] = 4;
                    else if (arr[j] == 4)
                        arr[j] = 8;




                }

            else if (sum == num)
            {
                break;
            }
        }

        if (sumFun(arr) == num)
            return arr;
        else
        {
            arr = permutate(arr, num);
            return arr;
        }


    }


    public int[] setComb(int num)
    {
        int[] numbersT = new int[4];
        try
        {
           



            for (int i = 0; i < 4; i++)
            {
                int[] nums = new int[3] { 2, 4, 8 };

                int numR = Random.Range(0, 3);
              
                    numR = 4;
               
 
                numbersT[i] = numR;

                numbersT = permutate(numbersT, num);

                return numbersT;

            }
        }
        catch(System.Exception e)
        {
            setComb(num);
        }


        return numbersT;






    }


    private int sumFun(int[] numbersT)
    {
        int sum = 0;
        for (int k = 0; k < 4; k++)
        {
            sum += numbersT[k];

        }
        return sum;
    }


    void setBlocknum(int x)
    {
        saveData = SaveSystem.instance.LoadGame();
        int level = saveData.level;

        Debug.Log(saveData.level);
        int[] numbersT = new int[16] { 2, 4, 2, 2, 2, 2, 2, 4, 6, 2, 2, 2, 2, 2, 6, 2 };
        int[] numbers2 = new int[16] { 4, 10, 4, 4, 2, 2, 4, 4, 2, 2, 4, 4, 2, 4, 2, 6 };
        int[] numbers3 = new int[16] { 4, 2, 4, 8, 2, 2, 4, 4, 2, 2, 4, 2, 4, 2, 4, 6 };
        int[] numbers4 = new int[16] { 4, 8, 2, 4, 2, 4, 4, 2, 2, 2, 4, 4, 2, 8, 4, 4 };
        int[] numbers5 = new int[16] { 10, 4, 2, 2, 6, 6, 6, 4, 6, 2, 2, 8, 4, 2, 2, 2 };
        int[] numbers6 = new int[16] { 2, 2, 6, 2, 4, 6, 2, 2, 2, 4, 2, 6, 4, 2, 4, 6 };
        int[] numbers7 = new int[16] { 4, 4, 4, 2, 8, 8, 2, 6, 4, 4, 6, 2, 6, 2, 10, 4 };
        int[] numbers8 = new int[16] { 2, 2, 10, 2, 2, 2, 6, 2, 2, 2, 2, 6, 10, 10, 2, 2 };
        int[] numbers9 = new int[16] { 2, 2, 6, 2, 10, 2, 2, 2, 6, 2, 2, 6, 10, 10, 2, 6 };
        int[] numbers10 = new int[16] { 10, 10, 6, 8, 2, 10, 10, 4, 6, 2, 2, 2, 2, 4, 10, 4 };
        int[] numbers11 = new int[16] { 2, 2, 10, 6, 6, 2, 4, 2, 2, 10, 4, 2, 2, 8, 2, 4 };
        int[] numbers12 = new int[16] { 4, 2, 2, 4, 8, 4, 4, 8, 2, 4, 4, 4, 4, 2, 2, 6 };
        int[] numbers13 = new int[16] { 4, 2, 6, 4, 10, 4, 4, 2, 2, 4, 2, 6, 4, 12, 4, 6 };
        int[] numbers14 = new int[16] { 8, 6, 2, 4, 4, 2, 2, 2, 4, 2, 8, 2, 6, 2, 4, 2 };
        int[] numbers15 = new int[16] { 8, 4, 2, 4, 4, 2, 6, 2, 10, 8, 4, 8, 4, 2, 2, 10 };
        int[] numbers16 = new int[16] { 2, 4, 10, 4, 10, 2, 8, 2, 6, 2, 4, 2, 2, 4, 2, 4 };
        int[] numbers17 = new int[16] { 8, 4, 2, 4, 4, 4, 4, 6, 2, 2, 2, 2, 4, 4, 6, 2 };
        int[] numbers18 = new int[16] { 2, 8, 6, 2, 4, 4, 2, 2, 8, 2, 4, 8, 2, 2, 2, 2 };
        int[] numbers19 = new int[16] { 6, 2, 4, 8, 10, 4, 8, 4, 10, 2, 4, 2, 2, 2, 6, 6 };
        int[] numbers20 = new int[16] { 10, 8, 4, 2, 2, 8, 10, 2, 12, 8, 8, 2, 4, 8, 18, 2 };
        int[] numbers21 = new int[16] { 6, 12, 2, 2, 2, 4, 2, 10, 10, 2, 4, 6, 6, 6, 12, 2 };

        // CORRECT BLOCKS FUNCTIONALITY
        // CREATE A DICTIONARY OF EACH INDEX AND ITS ADJACENT BLOCKS
        // THIS HAPPENS ONLY ONCE PER GAME SPAWN
        Dictionary<int, List<int>> lvl1 = new Dictionary<int, List<int>>();

        lvl1.Add(5, new List<int> { 5, 1, 0 });

        lvl1.Add(1, new List<int> { 5, 1, 0 });

        lvl1.Add(0, new List<int> { 5, 1, 0 });

        lvl1.Add(2, new List<int> { 2, 6 });

        lvl1.Add(6, new List<int> { 2, 6 });

        lvl1.Add(3, new List<int> { 3, 7, 11 });

        lvl1.Add(7, new List<int> { 3, 7, 11 });

        lvl1.Add(11, new List<int> { 3, 7, 11 });

        lvl1.Add(15, new List<int> { 15, 14, 13, 10 });

        lvl1.Add(14, new List<int> { 15, 14, 13, 10 });

        lvl1.Add(13, new List<int> { 15, 14, 13, 10 });

        lvl1.Add(10, new List<int> { 15, 14, 13, 10 });
        lvl1.Add(12, new List<int> { 12, 8, 9, 4 });

        lvl1.Add(8, new List<int> { 12, 8, 9, 4 });

        lvl1.Add(9, new List<int> { 12, 8, 9, 4 });

        lvl1.Add(4, new List<int> { 12, 8, 9, 4 });



        Dictionary<int, List<int>> lvl2 = new Dictionary<int, List<int>>();

        lvl2.Add(0, new List<int> { 0, 1, 2, 5 });

        lvl2.Add(1, new List<int> { 0, 1, 2, 5 });

        lvl2.Add(2, new List<int> { 0, 1, 2, 5 });
        lvl2.Add(5, new List<int> { 0, 1, 2, 5 });

        lvl2.Add(3, new List<int> { 3, 7 });

        lvl2.Add(7, new List<int> { 3, 7 });

        lvl2.Add(11, new List<int> { 11, 15, 14 });

        lvl2.Add(15, new List<int> { 11, 15, 14 });

        lvl2.Add(14, new List<int> { 11, 15, 14 });

        lvl2.Add(10, new List<int> { 10, 6 });

        lvl2.Add(6, new List<int> { 10, 6 });

        lvl2.Add(13, new List<int> { 13, 9, 12 });

        lvl2.Add(9, new List<int> { 13, 9, 12 });

        lvl2.Add(12, new List<int> { 13, 9, 12 });

        lvl2.Add(8, new List<int> { 8, 4 });

        lvl2.Add(4, new List<int> { 8, 4 });

        Dictionary<int, List<int>> lvl5 = new Dictionary<int, List<int>>();
        lvl5.Add(3, new List<int> { 3, 2 });

        lvl5.Add(2, new List<int> { 3, 2 });

        lvl5.Add(7, new List<int> { 7, 11, 15, 10 });

        lvl5.Add(11, new List<int> { 7, 11, 15, 10 });

        lvl5.Add(15, new List<int> { 7, 11, 15, 10 });

        lvl5.Add(10, new List<int> { 7, 11, 15, 10 });

        lvl5.Add(6, new List<int> { 6, 5 });

        lvl5.Add(5, new List<int> { 6, 5 });

        lvl5.Add(14, new List<int> { 14, 13 });

        lvl5.Add(13, new List<int> { 14, 13 });

        lvl5.Add(9, new List<int> { 9, 8, 12 });

lvl5.Add(8, new List<int> { 9, 8, 12 });

        lvl5.Add(12, new List<int> { 9, 8, 12 });

        lvl5.Add(4, new List<int> { 4, 0, 1 });

        lvl5.Add(0, new List<int> { 4, 0, 1 });

        lvl5.Add(1, new List<int> { 4, 0, 1 });



        if (level == 0)
        {
            Pairs = lvl1;
            Blocknum = numbersT[x];
        }
  
        if (level == 1)
        {
            Pairs = lvl2;
            Blocknum = numbers2[x];
        }

        if (level == 4)
        {
            Pairs = lvl5;
            Blocknum = numbers5[x];
        }


        Dictionary<int, List<int>> lvl4 = new Dictionary<int, List<int>>();

        lvl4.Add(0, new List<int> { 0, 1, 5 });
        lvl4.Add(1, new List<int> { 0, 1, 5 });

        lvl4.Add(5, new List<int> { 0, 1, 5 });

        lvl4.Add(2, new List<int> { 2, 3, 7 });

        lvl4.Add(3, new List<int> { 2, 3, 7 });

        lvl4.Add(7, new List<int> { 2, 3, 7 });

        lvl4.Add(11, new List<int> { 11, 15 });

        lvl4.Add(15, new List<int> { 11, 15 });

        lvl4.Add(6, new List<int> { 6, 10 });

        lvl4.Add(10, new List<int> { 6, 10 });

        lvl4.Add(14, new List<int> { 14, 13, 9, 12 });

        lvl4.Add(13, new List<int> { 14, 13, 9, 12 });

        lvl4.Add(9, new List<int> { 14, 13, 9, 12 });

        lvl4.Add(12, new List<int> { 14, 13, 9, 12 });

        lvl4.Add(8, new List<int> { 8, 4 });

        lvl4.Add(4, new List<int> { 8, 4 });

        if (level == 3) { Pairs = lvl4; Blocknum = numbers4[x]; }

        Dictionary<int, List<int>> lvl3 = new Dictionary<int, List<int>>();

        lvl3.Add(4, new List<int> { 4, 0, 1 });

        lvl3.Add(0, new List<int> { 4, 0, 1 });

        lvl3.Add(1, new List<int> { 4, 0, 1 });

        lvl3.Add(2, new List<int> { 2, 3, 7 });

        lvl3.Add(3, new List<int> { 2, 3, 7 });

        lvl3.Add(7, new List<int> { 2, 3, 7 });

        lvl3.Add(5, new List<int> { 5, 9 });

        lvl3.Add(9, new List<int> { 5, 9 });

        lvl3.Add(6, new List<int> { 6, 10 });

        lvl3.Add(10, new List<int> { 6, 10 });

        lvl3.Add(11, new List<int> { 11, 15, 14 });

        lvl3.Add(15, new List<int> { 11, 15, 14 });

        lvl3.Add(14, new List<int> { 11, 15, 14 });

        lvl3.Add(13, new List<int> { 13, 12, 8 });

        lvl3.Add(12, new List<int> { 13, 12, 8 });

        lvl3.Add(8, new List<int> { 13, 12, 8 });

        if (level == 2) { Pairs = lvl3; Blocknum = numbers3[x]; }



        Dictionary<int, List<int>> lvl6 = new Dictionary<int, List<int>>();

        lvl6.Add(11, new List<int> { 11, 15, 10, 14, 7 });

        lvl6.Add(15, new List<int> { 11, 15, 10, 14, 7 });

        lvl6.Add(10, new List<int> { 11, 15, 10, 14, 7 });

        lvl6.Add(14, new List<int> { 11, 15, 10, 14, 7 });

        lvl6.Add(7, new List<int> { 11, 15, 10, 14, 7 });

        lvl6.Add(3, new List<int> { 3, 6, 1, 2 });
        lvl6.Add(6, new List<int> { 3, 6, 1, 2 });

        lvl6.Add(1, new List<int> { 3, 6, 1, 2 });

        lvl6.Add(2, new List<int> { 3, 6, 1, 2 });

        lvl6.Add(0, new List<int> { 0, 4, 5, 9 });

        lvl6.Add(4, new List<int> { 0, 4, 5, 9 });

        lvl6.Add(5, new List<int> { 0, 4, 5, 9 });

        lvl6.Add(9, new List<int> { 0, 4, 5, 9 });

        lvl6.Add(8, new List<int> { 8, 13, 12 });

        lvl6.Add(13, new List<int> { 8, 13, 12 });

        lvl6.Add(12, new List<int> { 8, 13, 12 });

        if (level == 5) { Pairs = lvl6; Blocknum = numbers6[x]; }

        Dictionary<int, List<int>> lvl7 = new Dictionary<int, List<int>>();

lvl7.Add(8, new List<int> { 8, 12, 13 });

lvl7.Add(12, new List<int> { 8, 12, 13 });

lvl7.Add(13, new List<int> { 8, 12, 13 });

lvl7.Add(10, new List<int> { 10, 14, 15 });

lvl7.Add(14, new List<int> { 10, 14, 15 });

lvl7.Add(15, new List<int> { 10, 14, 15 });

lvl7.Add(9, new List<int> { 9, 5, 0, 4 });

lvl7.Add(5, new List<int> { 9, 5, 0, 4 });

lvl7.Add(0, new List<int> { 9, 5, 0, 4 });

lvl7.Add(4, new List<int> { 9, 5, 0, 4 });

lvl7.Add(1, new List<int> { 1, 2 });

lvl7.Add(2, new List<int> { 1, 2 });

lvl7.Add(7, new List<int> { 7, 3, 6, 11 });

lvl7.Add(3, new List<int> { 7, 3, 6, 11 });

lvl7.Add(6, new List<int> { 7, 3, 6, 11 });

lvl7.Add(11, new List<int> { 7, 3, 6, 11 });

if (level == 6) { Pairs = lvl7; Blocknum = numbers7[x]; }

        Dictionary<int, List<int>> lvl8 = new Dictionary<int, List<int>>();

lvl8.Add(11, new List<int> { 11, 7, 10, 15 });

lvl8.Add(7, new List<int> { 11, 7, 10, 15 });

lvl8.Add(10, new List<int> { 11, 7, 10, 15 });

lvl8.Add(15, new List<int> { 11, 7, 10, 15 });

lvl8.Add(6, new List<int> { 6, 2, 3, 1 });

lvl8.Add(2, new List<int> { 6, 2, 3, 1 });

lvl8.Add(3, new List<int> { 6, 2, 3, 1 });

lvl8.Add(1, new List<int> { 6, 2, 3, 1 });

lvl8.Add(5, new List<int> { 5, 9 });

lvl8.Add(9, new List<int> { 5, 9 });

lvl8.Add(12, new List<int> { 12, 8, 13, 14 });

lvl8.Add(8, new List<int> { 12, 8, 13, 14 });

lvl8.Add(13, new List<int> { 12, 8, 13, 14 });

lvl8.Add(14, new List<int> { 12, 8, 13, 14 });

lvl8.Add(0, new List<int> { 0, 4 });

lvl8.Add(4, new List<int> { 0, 4 });

if (level == 7) { Pairs = lvl8; Blocknum = numbers8[x]; }


        Dictionary<int, List<int>> lvl9 = new Dictionary<int, List<int>>();

       

lvl9.Add(2, new List<int> { 2, 6, 3, 1 });

lvl9.Add(6, new List<int> { 2, 6, 3, 1 });

lvl9.Add(3, new List<int> { 2, 6, 3, 1 });

lvl9.Add(1, new List<int> { 2, 6, 3, 1 });

lvl9.Add(15, new List<int> { 15, 11, 7, 14 });

lvl9.Add(11, new List<int> { 15, 11, 7, 14 });

lvl9.Add(7, new List<int> { 15, 11, 7, 14 });

lvl9.Add(14, new List<int> { 15, 11, 7, 14 });

lvl9.Add(10, new List<int> { 10, 9 });

lvl9.Add(9, new List<int> { 10, 9 });

lvl9.Add(4, new List<int> { 4, 0, 5, 8 });

lvl9.Add(0, new List<int> { 4, 0, 5, 8 });

lvl9.Add(5, new List<int> { 4, 0, 5, 8 });

lvl9.Add(8, new List<int> { 4, 0, 5, 8 });

lvl9.Add(13, new List<int> { 13, 12 });

lvl9.Add(12, new List<int> { 13, 12 });



        if (level == 8) { Pairs = lvl9; Blocknum = numbers9[x]; }


        Dictionary<int, List<int>> lvl10 = new Dictionary<int, List<int>>();
        lvl10.Add(5, new List<int> { 5, 6 });



lvl10.Add(6, new List<int> { 5, 6 });

lvl10.Add(3, new List<int> { 3, 2, 7, 11 });

lvl10.Add(2, new List<int> { 3, 2, 7, 11 });

lvl10.Add(7, new List<int> { 3, 2, 7, 11 });

lvl10.Add(11, new List<int> { 3, 2, 7, 11 });

lvl10.Add(14, new List<int> { 14, 10, 13, 15 });

lvl10.Add(10, new List<int> { 14, 10, 13, 15 });

lvl10.Add(13, new List<int> { 14, 10, 13, 15 });

lvl10.Add(15, new List<int> { 14, 10, 13, 15 });

lvl10.Add(8, new List<int> { 8, 9, 12, 4 });

lvl10.Add(9, new List<int> { 8, 9, 12, 4 });

lvl10.Add(12, new List<int> { 8, 9, 12, 4 });

lvl10.Add(4, new List<int> { 8, 9, 12, 4 });

lvl10.Add(0, new List<int> { 0, 1 });

lvl10.Add(1, new List<int> { 0, 1 });

if (level == 9) { Pairs = lvl10; Blocknum = numbers10[x]; }
        Dictionary<int, List<int>> lvl12 = new Dictionary<int, List<int>>();

lvl12.Add(14, new List<int> { 14, 15, 11 });

lvl12.Add(15, new List<int> { 14, 15, 11 });

lvl12.Add(11, new List<int> { 14, 15, 11 });

lvl12.Add(13, new List<int> { 13, 12, 8 });

lvl12.Add(12, new List<int> { 13, 12, 8 });

lvl12.Add(8, new List<int> { 13, 12, 8 });

lvl12.Add(4, new List<int> { 4, 0, 5 });

lvl12.Add(0, new List<int> { 4, 0, 5 });

lvl12.Add(5, new List<int> { 4, 0, 5 });

lvl12.Add(9, new List<int> { 9, 10 });

lvl12.Add(10, new List<int> { 9, 10 });

lvl12.Add(6, new List<int> { 6, 7, 3 });

lvl12.Add(7, new List<int> { 6, 7, 3 });

lvl12.Add(3, new List<int> { 6, 7, 3 });

lvl12.Add(1, new List<int> { 1, 2 });

lvl12.Add(2, new List<int> { 1, 2 });

if (level == 11) { Pairs = lvl12; Blocknum = numbers12[x]; }


        Dictionary<int, List<int>> lvl11 = new Dictionary<int, List<int>>();


lvl11.Add(3, new List<int> { 7, 3, 2, 6, 1 });

lvl11.Add(2, new List<int> { 7, 3, 2, 6, 1 });

lvl11.Add(6, new List<int> { 7, 3, 2, 6, 1 });

lvl11.Add(1, new List<int> { 7, 3, 2, 6, 1 });


        lvl11.Add(7, new List<int> { 7, 3, 2, 6, 1 });

        lvl11.Add(9, new List<int> { 9, 10, 12, 13 });
        lvl11.Add(10, new List<int> { 9, 10, 12, 13 });
        lvl11.Add(12, new List<int> { 9, 10, 12, 13 });
        lvl11.Add(13, new List<int> { 9, 10, 12, 13 });

        lvl11.Add(0, new List<int> { 5, 4, 0, 8 });
        lvl11.Add(5, new List<int> { 5, 4, 0, 8 });
        lvl11.Add(4, new List<int> { 5, 4, 0, 8 });
        lvl11.Add(8, new List<int> { 5, 4, 0, 8 });

lvl11.Add(11, new List<int> { 11, 15, 14 });

lvl11.Add(15, new List<int> { 11, 15, 14 });

lvl11.Add(14, new List<int> { 11, 15, 14 });

if (level == 10) { Pairs = lvl11; Blocknum = numbers11[x]; }
        //  foreach (var kvp in Pairs)
        //   Debug.Log("Key: {0}, Value: {1}" + kvp.Key + kvp.Value);



        Dictionary<int, List<int>> lvl13 = new Dictionary<int, List<int>>();

lvl13.Add(2, new List<int> { 2, 1, 6, 10, 3, 7 });

lvl13.Add(1, new List<int> { 2, 1, 6, 10, 3, 7 });

lvl13.Add(6, new List<int> { 2, 1, 6, 10, 3, 7 });

lvl13.Add(10, new List<int> { 2, 1, 6, 10, 3, 7 });

lvl13.Add(3, new List<int> { 2, 1, 6, 10, 3, 7 });

lvl13.Add(7, new List<int> { 2, 1, 6, 10, 3, 7 });

lvl13.Add(4, new List<int> { 4, 5, 0, 8 });

lvl13.Add(5, new List<int> { 4, 5, 0, 8 });

        lvl13.Add(12, new List<int> { 9, 14, 13, 12 });
        lvl13.Add(13, new List<int> { 9, 14, 13, 12 });
        lvl13.Add(14, new List<int> { 9, 14, 13, 12 });
        lvl13.Add(9, new List<int> { 9, 14, 13, 12 });

        lvl13.Add(0, new List<int> { 4, 5, 0, 8 });

lvl13.Add(8, new List<int> { 4, 5, 0, 8 });

lvl13.Add(11, new List<int> { 11, 15 });

lvl13.Add(15, new List<int> { 11, 15 });

if (level == 12) { Pairs = lvl13; Blocknum = numbers13[x]; }

        Dictionary<int, List<int>> lvl14 = new Dictionary<int, List<int>>();

lvl14.Add(1, new List<int> { 1, 0, 4, 5 });

        lvl14.Add(0, new List<int> { 1, 0, 4, 5 });

        lvl14.Add(4, new List<int> { 1, 0, 4, 5 });

        lvl14.Add(5, new List<int> { 1, 0, 4, 5 });

        lvl14.Add(3, new List<int> { 3, 7, 2 });

        lvl14.Add(7, new List<int> { 3, 7, 2 });

        lvl14.Add(2, new List<int> { 3, 7, 2 });

        lvl14.Add(12, new List<int> { 12, 13, 8 });

        lvl14.Add(13, new List<int> { 12, 13, 8 });

        lvl14.Add(8, new List<int> { 12, 13, 8 });

        lvl14.Add(10, new List<int> { 10, 9, 6, 11, 14, 15 });

        lvl14.Add(9, new List<int> { 10, 9, 6, 11, 14, 15 });

        lvl14.Add(6, new List<int> { 10, 9, 6, 11, 14, 15 });

        lvl14.Add(11, new List<int> { 10, 9, 6, 11, 14, 15 });

        lvl14.Add(14, new List<int> { 10, 9, 6, 11, 14, 15 });

        lvl14.Add(15, new List<int> { 10, 9, 6, 11, 14, 15 });

if (level == 13) { Pairs = lvl14; Blocknum = numbers14[x]; }


        Dictionary<int, List<int>> lvl15 = new Dictionary<int, List<int>>();

lvl15.Add(13, new List<int> { 13, 9, 8, 12 });

lvl15.Add(9, new List<int> { 13, 9, 8, 12 });

lvl15.Add(8, new List<int> { 13, 9, 8, 12 });

lvl15.Add(12, new List<int> { 13, 9, 8, 12 });

lvl15.Add(14, new List<int> { 14, 15, 11 });

lvl15.Add(15, new List<int> { 14, 15, 11 });

lvl15.Add(11, new List<int> { 14, 15, 11 });

lvl15.Add(10, new List<int> { 10, 6, 5 });

lvl15.Add(6, new List<int> { 10, 6, 5 });

lvl15.Add(5, new List<int> { 10, 6, 5 });

lvl15.Add(7, new List<int> { 7, 3, 2 });

lvl15.Add(3, new List<int> { 7, 3, 2 });

lvl15.Add(2, new List<int> { 7, 3, 2 });

lvl15.Add(1, new List<int> { 1, 0, 4 });

lvl15.Add(0, new List<int> { 1, 0, 4 });

lvl15.Add(4, new List<int> { 1, 0, 4 });

if (level == 14) { Pairs = lvl15; Blocknum = numbers15[x]; }



        Dictionary<int, List<int>> lvl16 = new Dictionary<int, List<int>>();

        lvl16.Add(9, new List<int> { 9,12,13 });
        lvl16.Add(12, new List<int> { 9,12,13});
        lvl16.Add(13, new List<int> { 9,12,13 });
        lvl16.Add(4, new List<int> { 4, 0, 5, 8 });

lvl16.Add(0, new List<int> { 4, 0, 5, 8 });

lvl16.Add(5, new List<int> { 4, 0, 5, 8 });

lvl16.Add(8, new List<int> { 4, 0, 5, 8 });

lvl16.Add(2, new List<int> { 2, 1, 3, 6, 10, 7 });

lvl16.Add(1, new List<int> { 2, 1, 3, 6, 10, 7 });

lvl16.Add(3, new List<int> { 2, 1, 3, 6, 10, 7 });

lvl16.Add(6, new List<int> { 2, 1, 3, 6, 10, 7 });

lvl16.Add(10, new List<int> { 2, 1, 3, 6, 10, 7 });

lvl16.Add(7, new List<int> { 2, 1, 3, 6, 10, 7 });

lvl16.Add(15, new List<int> { 15, 11, 14 });

lvl16.Add(11, new List<int> { 15, 11, 14 });

lvl16.Add(14, new List<int> { 15, 11, 14 });

if (level == 15) { Pairs = lvl16; Blocknum = numbers16[x]; }





        Dictionary<int, List<int>> lvl17 = new Dictionary<int, List<int>>();

lvl17.Add(0, new List<int> { 0, 4, 1 });

lvl17.Add(4, new List<int> { 0, 4, 1 });

lvl17.Add(1, new List<int> { 0, 4, 1 });

lvl17.Add(2, new List<int> { 2, 3, 7, 11, 9, 5, 6 });

lvl17.Add(3, new List<int> { 2, 3, 7, 11, 9, 5, 6 });

lvl17.Add(7, new List<int> { 2, 3, 7, 11, 9, 5, 6 });

lvl17.Add(11, new List<int> { 2, 3, 7, 11, 9, 5, 6 });

lvl17.Add(9, new List<int> { 2, 3, 7, 11, 9, 5, 6 });

lvl17.Add(5, new List<int> { 2, 3, 7, 11, 9, 5, 6 });

lvl17.Add(6, new List<int> { 2, 3, 7, 11, 9, 5, 6 });

lvl17.Add(15, new List<int> { 15, 10, 14, 13, 8, 12 });

lvl17.Add(10, new List<int> { 15, 10, 14, 13, 8, 12 });

lvl17.Add(14, new List<int> { 15, 10, 14, 13, 8, 12 });

lvl17.Add(13, new List<int> { 15, 10, 14, 13, 8, 12 });

lvl17.Add(8, new List<int> { 15, 10, 14, 13, 8, 12 });

lvl17.Add(12, new List<int> { 15, 10, 14, 13, 8, 12 });

if (level == 16) { Pairs = lvl17; Blocknum = numbers17[x]; }


 Dictionary<int, List<int>> lvl18 = new Dictionary<int, List<int>>();

lvl18.Add(6, new List<int> {  6,5,2,1,3,0 });

lvl18.Add(5, new List<int> {  6,5,2,1,3,0 });

lvl18.Add(2, new List<int> {  6,5,2,1,3,0 });

lvl18.Add(1, new List<int> {  6,5,2,1,3,0 });

lvl18.Add(3, new List<int> {  6,5,2,1,3,0 });

lvl18.Add(0, new List<int> {  6,5,2,1,3,0 });

lvl18.Add(4, new List<int> {  4,8,9,12 });

lvl18.Add(8, new List<int> {  4,8,9,12 });

lvl18.Add(9, new List<int> {  4,8,9,12 });

lvl18.Add(12, new List<int> {  4,8,9,12 });

lvl18.Add(7, new List<int> {  7,11,10,15 });

lvl18.Add(11, new List<int> {  7,11,10,15 });

lvl18.Add(10, new List<int> {  7,11,10,15 });

lvl18.Add(15, new List<int> {  7,11,10,15 });

        lvl18.Add(13, new List<int> {  13, 14 });
        lvl18.Add(14, new List<int> { 13, 14 });

        if (level == 17) { Pairs = lvl18; Blocknum = numbers18[x]; }


        Dictionary<int, List<int>> lvl19 = new Dictionary<int, List<int>>();

  

lvl19.Add(1, new List<int> { 1, 0, 4, 8, 9, 12 });

lvl19.Add(0, new List<int> { 1, 0, 4, 8, 9, 12 });

lvl19.Add(4, new List<int> { 1, 0, 4, 8, 9, 12 });

lvl19.Add(8, new List<int> { 1, 0, 4, 8, 9, 12 });

lvl19.Add(9, new List<int> { 1, 0, 4, 8, 9, 12 });

lvl19.Add(12, new List<int> { 1, 0, 4, 8, 9, 12 });

lvl19.Add(10, new List<int> { 10, 6, 5 });

lvl19.Add(6, new List<int> { 10, 6, 5 });

lvl19.Add(5, new List<int> { 10, 6, 5 });

lvl19.Add(2, new List<int> { 2, 3, 7 });

lvl19.Add(3, new List<int> { 2, 3, 7 });

lvl19.Add(7, new List<int> { 2, 3, 7 });

lvl19.Add(13, new List<int> { 13, 14, 15, 11 });

lvl19.Add(14, new List<int> { 13, 14, 15, 11 });

lvl19.Add(15, new List<int> { 13, 14, 15, 11 });

lvl19.Add(11, new List<int> { 13, 14, 15, 11 });

if (level == 18) { Pairs = lvl19; Blocknum = numbers19[x]; }


        Dictionary<int, List<int>> lvl20 = new Dictionary<int, List<int>>();

lvl20.Add(6, new List<int> { 6, 5, 2, 3 });

lvl20.Add(5, new List<int> { 6, 5, 2, 3 });

lvl20.Add(2, new List<int> { 6, 5, 2, 3 });

lvl20.Add(3, new List<int> { 6, 5, 2, 3 });

lvl20.Add(1, new List<int> { 1, 4, 0 });

lvl20.Add(4, new List<int> { 1, 4, 0 });

lvl20.Add(0, new List<int> { 1, 4, 0 });

lvl20.Add(9, new List<int> { 9, 8, 12 });

lvl20.Add(8, new List<int> { 9, 8, 12 });

lvl20.Add(12, new List<int> { 9, 8, 12 });

lvl20.Add(13, new List<int> { 13, 10, 14, 15 });

lvl20.Add(10, new List<int> { 13, 10, 14, 15 });

lvl20.Add(14, new List<int> { 13, 10, 14, 15 });

lvl20.Add(15, new List<int> { 13, 10, 14, 15 });

lvl20.Add(11, new List<int> { 11, 7 });

lvl20.Add(7, new List<int> { 11, 7 });

if (level == 19) { Pairs = lvl20; Blocknum = numbers20[x]; }



        Dictionary<int, List<int>> lvl21 = new Dictionary<int, List<int>>();

lvl21.Add(7, new List<int> { 7, 3, 11, 6 });

lvl21.Add(3, new List<int> { 7, 3, 11, 6 });

lvl21.Add(11, new List<int> { 7, 3, 11, 6 });

lvl21.Add(6, new List<int> { 7, 3, 11, 6 });

lvl21.Add(14, new List<int> { 14, 15, 13, 10 });

lvl21.Add(15, new List<int> { 14, 15, 13, 10 });

lvl21.Add(13, new List<int> { 14, 15, 13, 10 });

lvl21.Add(10, new List<int> { 14, 15, 13, 10 });


        lvl21.Add(0, new List<int> { 0, 1, 2, 5 });
        lvl21.Add(1, new List<int> { 0, 1, 2, 5 });
        lvl21.Add(2, new List<int> { 0, 1, 2, 5 });
        lvl21.Add(5, new List<int> { 0, 1, 2, 5 });

        lvl21.Add(9, new List<int> { 9, 8, 12, 4 });

lvl21.Add(8, new List<int> { 9, 8, 12, 4 });

lvl21.Add(12, new List<int> { 9, 8, 12, 4 });

lvl21.Add(4, new List<int> { 9, 8, 12, 4 });

if (level == 20) { Pairs = lvl21; Blocknum = numbers21[x]; }
    }






    void updatePoint()
    {
        if (numbers[2] == 2)
        {
            pointBottom.sprite = pointYellow;
        }

        else if (numbers[2] == 0)
        {
            pointBottom.sprite = zero;
        }
        else if (numbers[2] == 4)
        {
            pointBottom.sprite = pointBlue;
        }
        else if (numbers[2] == 8)
        {
            pointBottom.sprite = pointRed;
        }
        if (numbers[0] == 2)
        {
            pointTop.sprite = pointYellow;
        }
        if (numbers[0] == 4)
        {
            pointTop.sprite = pointBlue;
        }
        else if (numbers[0] == 8)
        {
            pointTop.sprite = pointRed;
        }
        else if (numbers[0] == 0)
        {
            pointTop.sprite = zero;
        }
        else if (numbers[0] == 4)
        {
            pointTop.sprite = pointBlue;
        }


        if (numbers[1] == 2)
        {
            pointLeft.sprite = pointYellow;
        }
        else if (numbers[1] == 8)
        {
            pointLeft.sprite = pointRed;
        }
        else if (numbers[1] == 0)
        {
            pointLeft.sprite = zero;
        }
        else if (numbers[1] == 4)
        {
            pointLeft.sprite = pointBlue;
        }


        if (numbers[3] == 2)
        {
            pointRight.sprite = pointYellow;
        }
        else if (numbers[3] == 8)
        {
            pointRight.sprite = pointRed;
        }
        else if (numbers[3] == 0)
        {
            pointRight.sprite = zero;
        }
        else if (numbers[3] == 4)
        {
            pointRight.sprite = pointBlue;
        }
    }

    public void Fall()
    {
        
      
       /* RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.9f, 0), -Vector2.up, 3.0f);
        Debug.DrawRay(transform.position + new Vector3(0, -1.0f, 0), -Vector2.up, Color.green);
        if (hit.collider == null && !cameraObject.GetComponent<cameraScript>().canRotate)
        {
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -120.0f, 0));
        }
        else
        {


            if (hit.transform.tag == "Block")
            {
                target = hit.transform.position + new Vector3(0, blockMargin, 0);
                transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

                transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 10);
            }
            else
            {
                target = new Vector3(transform.position.x, hit.transform.position.y, hit.transform.position.z) + new Vector3(0, blockMargin, 0);
                transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

                transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 10);
            }

        }*/
 
        timer += Time.deltaTime*1.4f;
        if (timer >= startTime)
        {
            //game begins


            gameStarted = true;
         
        }



    }


    public void checkBlocks()
    {
        if (Row == 3 && Column == 0)
        {
            if (blockListMatrix[Column, Row - 1].GetComponent<Block>().blockDes && blockListMatrix[Column + 1, Row].GetComponent<Block>().blockDes)
            {
                GameObject cross = (GameObject)Instantiate(Cross, new Vector3(transform.position.x + betweenOfset, transform.position.y, transform.position.z), transform.rotation);
        
                cross.transform.localScale = blockSize;
 
    
                gameOverFun();
           
            }

        }

        else if (Row == 0 && Column == 3)
        {
            if (blockListMatrix[Column, Row + 1].GetComponent<Block>().blockDes && blockListMatrix[Column - 1, Row].GetComponent<Block>().blockDes)
            {
                GameObject cross = (GameObject)Instantiate(Cross, new Vector3(transform.position.x - betweenOfset, transform.position.y, transform.position.z), transform.rotation);
 
                cross.transform.localScale = blockSize;
 
    
                gameOverFun();
              
            }
        }

        else if (Row == 3 && Column == 3)
        {
            if (blockListMatrix[Column, Row - 1].GetComponent<Block>().blockDes && blockListMatrix[Column - 1, Row].GetComponent<Block>().blockDes)
            {
                GameObject cross = (GameObject)Instantiate(Cross, new Vector3(transform.position.x - betweenOfset, transform.position.y, transform.position.z), transform.rotation);
    
                cross.transform.localScale = blockSize;
      
             
                gameOverFun();
          
            }

        }

        else if (Row == 0 && Column == 0)
        {
            if (blockListMatrix[Column, Row + 1].GetComponent<Block>().blockDes && blockListMatrix[Column + 1, Row].GetComponent<Block>().blockDes)
            {
                GameObject cross = (GameObject)Instantiate(Cross, new Vector3(transform.position.x + betweenOfset, transform.position.y, transform.position.z), transform.rotation);
 
                cross.transform.localScale = blockSize;
 
                
                gameOverFun();
            
            }

        }
        else if (Row != 0 && Column != 0 && Row != 3 && Column != 3 && Row != 3 && Column != 3 && Row != 3 && Column != 0)

        {
            if (blockListMatrix[Column, Row + 1].GetComponent<Block>().blockDes && blockListMatrix[Column + 1, Row].GetComponent<Block>().blockDes && blockListMatrix[Column - 1, Row].GetComponent<Block>().blockDes && blockListMatrix[Column, Row - 1].GetComponent<Block>().blockDes && blockListMatrix[Column, Row + 1].GetComponent<Block>().blockDes)
            {
                GameObject cross = (GameObject)Instantiate(Cross, new Vector3(transform.position.x, transform.position.y + betweenOfset, transform.position.z), transform.rotation);
 
                cross.transform.localScale = blockSize;
         
            
                gameOverFun();
            
            }

        }

        else if (Column == 0 && Row != 0 && Row != 3)

        {
            if (blockListMatrix[Column, Row + 1].GetComponent<Block>().blockDes && blockListMatrix[Column + 1, Row].GetComponent<Block>().blockDes && blockListMatrix[Column, Row - 1].GetComponent<Block>().blockDes)
            {
                GameObject cross = (GameObject)Instantiate(Cross, new Vector3(transform.position.x, transform.position.y + betweenOfset, transform.position.z), transform.rotation);
                GameObject cross1 = (GameObject)Instantiate(Cross, new Vector3(transform.position.x, transform.position.y - betweenOfset, transform.position.z), transform.rotation);

                GameObject cross3 = (GameObject)Instantiate(Cross, new Vector3(transform.position.x + betweenOfset, transform.position.y, transform.position.z), transform.rotation);
                cross.transform.localScale = blockSize;
                cross1.transform.localScale = blockSize;
                cross3.transform.localScale = blockSize;

             
                gameOverFun();
             
            }

        }

        else if (Column == 3 && Row != 0 && Row != 3)

        {
            if (blockListMatrix[Column, Row + 1].GetComponent<Block>().blockDes && blockListMatrix[Column - 1, Row].GetComponent<Block>().blockDes && blockListMatrix[Column, Row - 1].GetComponent<Block>().blockDes)
            {
                GameObject cross = (GameObject)Instantiate(Cross, new Vector3(transform.position.x, transform.position.y + betweenOfset, transform.position.z), transform.rotation);
       
                cross.transform.localScale = blockSize;
       
                
                gameOverFun();
               
            }

        }
        else if (Row == 0 && Column != 0 && Column != 3)

        {
            if (blockListMatrix[Column - 1, Row].GetComponent<Block>().blockDes && blockListMatrix[Column + 1, Row].GetComponent<Block>().blockDes && blockListMatrix[Column, Row + 1].GetComponent<Block>().blockDes)
            {
                GameObject cross = (GameObject)Instantiate(Cross, new Vector3(transform.position.x, transform.position.y - betweenOfset, transform.position.z), transform.rotation);
           
                cross.transform.localScale = blockSize;
             
             
                gameOverFun();
            
            }

        }

        else if (Row == 3 && Column != 0 && Column != 3)

        {
            if (blockListMatrix[Column - 1, Row].GetComponent<Block>().blockDes && blockListMatrix[Column + 1, Row].GetComponent<Block>().blockDes && blockListMatrix[Column, Row - 1].GetComponent<Block>().blockDes)
            {
                GameObject cross = (GameObject)Instantiate(Cross, new Vector3(transform.position.x, transform.position.y + betweenOfset, transform.position.z), transform.rotation);
         
                cross.transform.localScale = blockSize;
   
           
                gameOverFun();
            
            }

        }


    }

    private void gameOverFun()
    {
        lost = true;
        GameObject dim = (GameObject)Instantiate(dimmer, Blocks.transform.position, transform.rotation);
        dim.transform.parent = Blocks.transform;
        GameObject gam = (GameObject)Instantiate(gameOver, Blocks.transform.position, transform.rotation);
        gam.transform.parent = Blocks.transform;
        cameraObject.GetComponent<cameraScript>().GameOverB = true;
    }

    public void resetInits()
    {
        try
        {

            if (!NewBlock)
                coms = setComb(Blocknum);
            numbers = coms;


            for (int i = 0; i < numbers.Length; i++)
            {
                int num = coms[i];


                numbers[i] = num;

            }

       
        }
        catch (System.Exception e)
        {
            Debug.LogError("ababab");
        }

    }
}
