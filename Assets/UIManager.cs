using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameManager GM;
    public TextMeshProUGUI TimerText;
    public GameObject StartScreen;
    public GameObject EndScreen;
    public GameObject GameUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTimerText(float seconds)
    {
        TimerText.text = Mathf.FloorToInt(seconds).ToString() + " s";
    }

    public void ExitGameButtonFunction()
    {
        Application.Quit();
    }

    public void StartGameButtonFunction()
    {
        StartScreen.SetActive(false);
        GameUI.SetActive(true);
        EndScreen.SetActive(false);
        GM.InitGame();
    }

    public void ShowEndScreen()
    {
        StartScreen.SetActive(false);
        GameUI.SetActive(false);
        EndScreen.SetActive(true);
    }

}
