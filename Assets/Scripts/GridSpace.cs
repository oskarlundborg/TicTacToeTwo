using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    private GameController gameController;
    public Button button;
    public string text;
    public Image image;


    public void SetSpace()
    {
        gameController.audioController.PlayCarvingSounds();
        text = gameController.GetPlayerSide();
        button.interactable = false;
        image.sprite = gameController.GetPlayerSideImage();
        image.color = Color.white;
        gameController.EndTurn();
    }

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    public void ResetGridSpace()
    {
        text = "";
        image.color = Color.clear;
    }
}
