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
        Type = type;

        //set the image material
        Model.material = CardMaterials[type];
    }
}
