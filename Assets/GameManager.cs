using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public variables
    public GameObject CardPrefab;
    public Transform CardParent;

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

        for (int i = 0; i < CardsArray.Length; i++)
        {
            //create cards
            GameObject go = Instantiate(CardPrefab, CardParent);
            CardsArray[i] = go.GetComponent<Card>();


        }
    }
}
