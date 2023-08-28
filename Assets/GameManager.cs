using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public variables
    public UIManager UIM;
    public GameObject CardPrefab;
    public Transform CardParent;
    public Transform Camera;
    public float WaitTime = 0.7f;
    public float EndWaitTime = 1f;

    //private variables
    Card[] CardsArray;
    Card OpenCard1;
    Card OpenCard2;
    float WaitTimer;
    bool StartWaitTime;
    float Timer;
    float EndTimer;
    bool GameComplete;
    bool IsGameRunning;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameComplete && IsGameRunning)
        {
            CheckGameWin();
            ControlsUpdate();
            TimerUpdate();
            CheckGameState();
        } 
        else
        {
            EndGameUpdate();
        }
    }

    public void InitGame()
    {
        //Destroy previous cards
        DeInitGame();

        CardsArray = new Card[12];
        int cardsPerRow = 4;
        int rowNumber = 0;
        int columnNumber = 0;

        for (int i = 0; i < CardsArray.Length; i++)
        {
            //create cards
            GameObject go = Instantiate(CardPrefab, CardParent);
            CardsArray[i] = go.GetComponent<Card>();
            go.name = "Card" + i;

            //set card position
            rowNumber = 0;
            columnNumber = i + 1;
            if(i + 1 > cardsPerRow + cardsPerRow)
            {
                rowNumber = 2;
            } else if(i + 1 > cardsPerRow)
            {
                rowNumber = 1;
            }
            if(columnNumber > cardsPerRow)
            {
                columnNumber = columnNumber - 4;
            } 
            if(columnNumber > cardsPerRow)
            {
                columnNumber = columnNumber - 4;
            }
            //Debug.Log("i: " + i + ", rowNumber: " + rowNumber + ", columnNumber: " + columnNumber);
            go.transform.position = new Vector3(0f + columnNumber, 0F - rowNumber, 0f);

            CardsArray[i].InitCard(0);
        }

        //Array for card types, same amount as cards
        int[] cardTypes = new int[CardsArray.Length];

        //card types based on number of cards
        for (int i = 0; i < cardTypes.Length; i++)
        {
            cardTypes[i] = i/2;
        }

        //randomize card types in array
        for (int i = 0; i < 10; i++)
        {
            int index0 = Random.Range(0, 12);
            int index1 = Random.Range(0, 12);
            int temp = cardTypes[index0];
            cardTypes[index0] = cardTypes[index1];
            cardTypes[index1] = temp;
        }

        //set pics based on the randomized card types
        for (int i = 0; i < CardsArray.Length; i++)
        {
            CardsArray[i].InitCard(cardTypes[i]);
        }


        //Move camera
        Camera.position = new Vector3(0f, 0f, 0f);
        Vector3 average = new Vector3(0f, 0f, 0f);

        for (int i = 0; i < CardsArray.Length; i++)
        {
            average += CardsArray[i].transform.position;
        }
        average = average / (float)(CardsArray.Length);
        average.z = -10f;
        Camera.position = average;

        //Set open cards as empty
        OpenCard1 = null;
        OpenCard2 = null;

        StartWaitTime = false;
        WaitTimer = 0f;
        Timer = 0f;
        EndTimer = 0f;
        GameComplete = false;
        IsGameRunning = true;
    }

    void DeInitGame()
    {
        if (CardsArray == null) return;
        for (int i = 0; i < CardsArray.Length; i++)
        {
            Destroy(CardsArray[i].gameObject);
        }
    }

    void ControlsUpdate()
    {
        //mouse click
        if (Input.GetMouseButtonDown(0) && !StartWaitTime)
        {
            //raycast
            Ray ray = Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                //get the card script
                Card card = hit.transform.parent.GetComponent<Card>();
                bool success = card.OpenCard();
                if (success)
                {
                    if(OpenCard1 == null)
                    {
                        OpenCard1 = card;
                    } 
                    else if(OpenCard2 == null)
                    {
                        OpenCard2 = card;
                    }
                }
                
            }
        }
    }

    void CheckGameState()
    {
        if(OpenCard1 != null && OpenCard2 != null)
        {
            if (OpenCard1.GetCardType() == OpenCard2.GetCardType())
            {
                OpenCard1.SetIisComplete(true);
                OpenCard2.SetIisComplete(true);
                OpenCard1 = null;
                OpenCard2 = null;
            }
            else
            {
                if (!StartWaitTime)
                {
                    StartWaitTime = true;
                    WaitTimer = 0f;
                }
            }
        }

        if (StartWaitTime)
        {
            if(WaitTimer >= WaitTime)
            {
                OpenCard1.CloseCard();
                OpenCard2.CloseCard();
                OpenCard1 = null;
                OpenCard2 = null;
                StartWaitTime = false;
            }
        }

        WaitTimer += Time.deltaTime;
        WaitTimer = Mathf.Min(WaitTime, WaitTimer);
    }

    void TimerUpdate()
    {
        Timer += Time.deltaTime;
        UIM.SetTimerText(Timer);
    }

    void CheckGameWin()
    {
        bool allCompleted = true;
        for (int i = 0; i < CardsArray.Length; i++)
        {
            if (!CardsArray[i].GetIsComplete())
            {
                allCompleted = false;
            }
        }
        if(allCompleted)
        {
            GameComplete = true;
        }
    }

    void EndGameUpdate()
    {
        if (GameComplete)
        {
            IsGameRunning = false;

            EndTimer += Time.deltaTime;
            EndTimer = Mathf.Min(EndWaitTime, EndTimer);
            if (EndTimer >= EndWaitTime)
            {
                UIM.ShowEndScreen();
                UIM.SetFinalTimerText(Timer);
            }
            
        }
    }


}
