using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public variables
    public GameObject CardPrefab;
    public Transform CardParent;
    public Transform Camera;

    //private variables
    Card[] CardsArray;
    


    // Start is called before the first frame update
    void Start()
    {
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitGame()
    {
        CardsArray = new Card[12];
        int cardsPerRow = 4;
        int rowNumber = 0;
        int columnNumber = 0;

        for (int i = 0; i < CardsArray.Length; i++)
        {
            //create cards
            GameObject go = Instantiate(CardPrefab, CardParent);
            CardsArray[i] = go.GetComponent<Card>();

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
    }

}
