using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    //public
    public Material[] CardMaterials;
    public MeshRenderer Model;

    //private
    int Type;
    bool IsOpen;
    bool IsComplete;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitCard(int type)
    {
        //set initial values
        IsComplete = false;
        IsOpen = false;
        Type = type;

        //set the image material
        Model.material = CardMaterials[type];
    }

    public int GetCardType()
    {
        return Type;
    }

    public bool GetIsOpen()
    {
        return IsOpen;
    }


    public bool OpenCard()
    {
        if (IsOpen) return false;
        
        IsOpen = true;

        //rotate card
        transform.Rotate(new Vector3(0, 180, 0));

        return true;
    }

    public bool CloseCard()
    {
        if (!IsOpen) return false;

        IsOpen = false;

        //rotate card
        transform.Rotate(new Vector3(0, 180, 0));

        return true;
    }

    public bool GetIsComplete()
    {
        return IsComplete;
    }

    public void SetIisComplete(bool value)
    {
        IsComplete = value;
    }

}
