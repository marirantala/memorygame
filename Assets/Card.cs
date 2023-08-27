using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    //public
    public Material[] CardMaterials;
    public MeshRenderer Model;
    public float AnimationTime = 0.25f;

    //private
    int Type;
    bool IsOpen;
    bool IsComplete;
    float AnimationTimer = 0f;
    bool StartOpenAnimation;
    bool StartCloseAnimation;
    Quaternion ClosedRotation;
    Quaternion OpenRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimationUpdate();
    }

    public void InitCard(int type)
    {
        //set initial values
        IsComplete = false;
        IsOpen = false;
        Type = type;
        StartOpenAnimation = false;
        StartCloseAnimation = false;
        ClosedRotation = transform.rotation;
        transform.Rotate(new Vector3(0, 180, 0));
        OpenRotation = transform.rotation;
        transform.rotation = ClosedRotation;

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
        //transform.Rotate(new Vector3(0, 180, 0));
        StartOpenAnimation = true;
        AnimationTimer = 0f;

        return true;
    }

    public bool CloseCard()
    {
        if (!IsOpen) return false;

        IsOpen = false;

        //rotate card
        //transform.Rotate(new Vector3(0, 180, 0));

        StartCloseAnimation = true;
        AnimationTimer = 0f;

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

    void AnimationUpdate()
    {
        if (StartOpenAnimation)
        {
            transform.rotation = Quaternion.Slerp(ClosedRotation, OpenRotation, AnimationTimer / AnimationTime);
            if (AnimationTimer >= AnimationTime) StartOpenAnimation = false;
        }
        else if (StartCloseAnimation)
        {
            transform.rotation = Quaternion.Slerp(OpenRotation, ClosedRotation, AnimationTimer / AnimationTime);
            if (AnimationTimer >= AnimationTime) StartCloseAnimation = false;
        }

        AnimationTimer += Time.deltaTime;
        AnimationTimer = Mathf.Min(AnimationTime, AnimationTimer);
    }

}
