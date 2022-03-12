using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BlockToAdd : MonoBehaviour
{
 
    public GameObject cameraObject;
    public GameObject spawner;
    public int[] numbers = new int[4];
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
    public int[] coms = new int[4];
    public GameObject canvas;
    public Image imageTop;
    public Text numTop;
    public Text numBottom;
    public Text numLeft;
    public Text numRight;
    public int counter = 0;
    public int Blocknum;
    public int sum = 0;
 
    public GameObject place;
    public Transform PointToFollow;
 
 
    float timer = 0.0f;
 
 
    Vector3 target = Vector3.zero;
    public float blockMargin = 1.06f;
    // Start is called before the first frame update
    void Start()
    {
     transform.position = Camera.main.ScreenToWorldPoint(new Vector3( PointToFollow.position.x, PointToFollow.position.y, 0));
        spawner = GameObject.FindGameObjectWithTag("Spawner");
        blockListMatrix = spawner.GetComponent<Spawner>().blockListMatrix;

        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");

        coms = setComb(2);
        numbers = coms;

     


        Debug.Log("Screen Width : " + this.gameObject.GetComponent<BoxCollider2D>().bounds.size.x);




        // this declares an integer array with 5 elements
        // and initializes all of them to their default value
        // which is zero


        for (int i = 0; i < numbers.Length; i++)
        {
            int num = coms[i];


            numbers[i] = num;

        }


           canvas = GameObject.FindGameObjectWithTag("Canvas");
        pointTop = (Image)Instantiate(point, transform.position, transform.rotation);
        float offset = Camera.main.ViewportToScreenPoint(this.transform.localScale).x;
        Vector3 namePose = Camera.main.WorldToScreenPoint(this.transform.position);
        numTop = (Text)Instantiate(num, transform.position, transform.rotation);

        numTop.text = numbers[0].ToString();

        pointTop.transform.SetParent(canvas.transform);
        pointTop.transform.position = namePose + new Vector3(0, offset, 0);
        if (numTop.text != "0")
            pointTop.GetComponent<Image>().color = new Color32(0, 88, 122, 255);

        numTop.transform.SetParent(canvas.transform);
        numTop.transform.position = namePose + new Vector3(0, offset, 0);
        /*pointTop = (Image)Instantiate(point, transform.position, transform.rotation);
        pointTop.transform.SetParent(canvas.transform);
        pointTop.transform.position = namePose + new Vector3(0, offset, 0);
        if (numbers[0]==2)
        {
            pointTop.sprite = pointRed;
        }
        else if (numbers[0] == 4)
        {
            pointTop.sprite = pointBlue;
        }
        else if (numbers[0]==1)
        {
            pointTop.sprite = pointYellow;
        }
        else if (numbers[0] == 0)
        {
            pointTop.sprite = zero;
        }*/


        pointBottom = (Image)Instantiate(point, transform.position, transform.rotation);
        numBottom = (Text)Instantiate(num, transform.position, transform.rotation);

        numBottom.text = numbers[2].ToString();
        if (numBottom.text != "0")
            pointBottom.GetComponent<Image>().color = new Color32(0, 88, 122, 255);

        pointBottom.transform.SetParent(canvas.transform);
        pointBottom.transform.position = namePose + new Vector3(0, -offset, 0);

        numBottom.transform.SetParent(canvas.transform);
        numBottom.transform.position = namePose + new Vector3(0, -offset, 0);

        /*pointBottom = (Image)Instantiate(point, transform.position, transform.rotation);
        pointBottom.transform.SetParent(canvas.transform);
        pointBottom.transform.position = namePose + new Vector3(0, -offset, 0);
        if (numbers[2] == 2)
        {
            pointBottom.sprite = pointRed;
        }
        else if (numbers[2] == 1)
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
        }*/
        pointLeft = (Image)Instantiate(point, transform.position, transform.rotation);
        numLeft = (Text)Instantiate(num, transform.position, transform.rotation);
        numLeft.text = numbers[1].ToString();

        if (numLeft.text != "0")
            pointLeft.GetComponent<Image>().color = new Color32(0, 88, 122, 255);
        pointLeft.transform.SetParent(canvas.transform);
        pointLeft.transform.position = namePose + new Vector3(-offset, 0, 0);


        numLeft.transform.SetParent(canvas.transform);
        numLeft.transform.position = namePose + new Vector3(-offset, 0, 0);


        /*pointLeft = (Image)Instantiate(point, transform.position, transform.rotation);
        pointLeft.transform.SetParent(canvas.transform);
        pointLeft.transform.position = namePose + new Vector3(-offset, 0 , 0);
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
        }*/

        pointRight = (Image)Instantiate(point, transform.position, transform.rotation);
        numRight = (Text)Instantiate(num, transform.position, transform.rotation);
        numRight.text = numbers[3].ToString();
        pointRight.transform.SetParent(canvas.transform);
        pointRight.transform.position = namePose + new Vector3(offset, 0, 0);
        numRight.transform.SetParent(canvas.transform);
        if (numRight.text != "0")
            pointRight.GetComponent<Image>().color = new Color32(0, 88, 122, 255);
        numRight.transform.position = namePose + new Vector3(offset, 0, 0);

        /*pointRight = (Image)Instantiate(point, transform.position, transform.rotation);
        pointRight.transform.SetParent(canvas.transform);
        pointRight.transform.position = namePose + new Vector3(offset, 0, 0);
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
        }*/

        setColor();



    }

    // Update is called once per frame
    void Update()
    {
 
 
        //updatePoint();

 
        setColor();
        UpdateNum();

    }

  

    void setColumn(int v)
    {
        Column = v;
    }
    void setRow(int v)
    {
        Row = v;
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

        for (int i = 0; i < 4; i++)
        {
            if (numbers[i] == 0)
                counter++;



 
     

        }

        counter = 0;



    }


    private int[] setComb(int num)
    {
        int[] numbersT = new int[4];



        for (int i = 0; i < 4; i++)
        {
            int[] nums = new int[3] { 2, 4, 8 };

            int numR = Random.Range(0, 3);
            if (numR == 0)
                numR = 2;
            else if (numR == 1)
                numR = 4;
            else if (numR == 2)
                numR = 8;
            Debug.Log("annd : " + numR);
            numbersT[i] = numR;

        }


        for (int i = 0; i < 4; i++)
        {
            int sum = sumFun(numbersT);
            if (sum > num)
                for (int j = i; j < 4; j++)
                {
                    if (numbersT[j] == 4)
                        numbersT[j] = 2;
                    else if (numbersT[j] == 2)
                        numbersT[j] = 0;
                    else if (numbersT[j] == 8)
                        numbersT[j] = 4;
                }

            else if (sum < num)
                for (int j = i; j < 4; j++)
                {
                    if (numbersT[j] == 0)
                        numbersT[j] = 2;
                    else if (numbersT[j] == 2)
                        numbersT[j] = 4;
                    else if (numbersT[j] == 4)
                        numbersT[j] = 8;



                }

            else if (sum == num)
            {
                break;
            }
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


    void setBlocknum( )
    {
        int randomBlockNum = Random.Range(1, 2);
        
        Blocknum =  randomBlockNum;
    }


 
 
 
}
