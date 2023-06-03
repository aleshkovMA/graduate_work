using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedController : MonoBehaviour
{
    [SerializeField] private GameObject ControllButton;
    [SerializeField] private Sprite GamePauset;
    [SerializeField] private Sprite UnGamePauset;
    [SerializeField] private Text multiplyField;
    private bool GameState = true;
    private Image bImage;
    private float timeScale;
    private void Start()
    {
        bImage = ControllButton.GetComponent<Image>();
    }
    public void PauseGame()
    {
        bImage.sprite = GamePauset;
        bImage.color = new Color32(238, 28, 28, 255);
        timeScale = Time.timeScale;
        Time.timeScale = 0;
        GameState = false;
    }
    public void ResumeGame()
    {
        bImage.sprite = UnGamePauset;
        bImage.color = new Color32(70, 255, 38, 255);
        Time.timeScale = timeScale;
        GameState = true;
    }
    public void Controller()
    {
        if (GameState == true)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }
    public void MultiplyTime()
    {
        if(Time.timeScale>0)
        {
            switch(multiplyField.text)
            {
                case "X1":
                    Time.timeScale = 2;
                    multiplyField.text = "X2";
                    break;
                case "X2":
                    Time.timeScale = 3;
                    multiplyField.text = "X3";
                    break;
                case "X3":
                    Time.timeScale = 4;
                    multiplyField.text = "X4";
                    break;
                case "X4":
                    Time.timeScale = 1;
                    multiplyField.text = "X1";
                    break;
            }
        }
    }
}
