using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
    public Button button;
    public Sprite playerImage;
}

[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour
{
    private string playerSide;
    private int moveCount;
    public GridSpace[] gridSpaceList;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject restartButton;
    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    public GameObject startInfo;
    public GameObject activePlayerXSymbol; //Pentagram
    public GameObject activePlayerOSymbol; //Cross
    public GameObject blueFlameLeft;
    public GameObject blueFlameRight;
    public GameObject greenFlameLeft;
    public GameObject greenFlameRight;
    public AudioController audioController;



    private void Awake()
    {
        SetControllerButtons();
        gameOverPanel.SetActive(false);
        moveCount = 0;
        restartButton.SetActive(false);
        audioController.StartBackgroundMusic();
    }
    void SetControllerButtons()
    {
        for (int i = 0; i < gridSpaceList.Length; i++)
        {
            gridSpaceList[i].SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {

        moveCount++;
        if (gridSpaceList[0].text == playerSide && gridSpaceList[1].text == playerSide && gridSpaceList[2].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (gridSpaceList[3].text == playerSide && gridSpaceList[4].text == playerSide && gridSpaceList[5].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (gridSpaceList[6].text == playerSide && gridSpaceList[7].text == playerSide && gridSpaceList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (gridSpaceList[0].text == playerSide && gridSpaceList[3].text == playerSide && gridSpaceList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (gridSpaceList[1].text == playerSide && gridSpaceList[4].text == playerSide && gridSpaceList[7].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (gridSpaceList[2].text == playerSide && gridSpaceList[5].text == playerSide && gridSpaceList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (gridSpaceList[0].text == playerSide && gridSpaceList[4].text == playerSide && gridSpaceList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (gridSpaceList[2].text == playerSide && gridSpaceList[4].text == playerSide && gridSpaceList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (moveCount >= 9)
        {
            GameOver("draw");
        }
        else
        {
            ChangeSides();
        }
    }

    void GameOver(string winningPlayer)
    {
        if (winningPlayer == "draw")
        {
            SetGameOverText("It's a Draw!");
            SetPlayerColorsInactive();
        }
        else
        {
            if (winningPlayer == "X")
            {
                SetGameOverText("The unholy darkness wins!");
            } else
            {
                SetGameOverText("The holy light wins!");
            }
        }
        SetBoardInteractable(false);
        restartButton.SetActive(true);
        SetPlayerColorsInactive();
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
        if (playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
        ToggleActivePlayerSymbol();
    }
    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    public void RestartGame()
    {
        moveCount = 0;
        gameOverPanel.SetActive(false);
        for (int i = 0; i < gridSpaceList.Length; i++)
        {
            gridSpaceList[i].ResetGridSpace();
        }
        restartButton.SetActive(false);
        DisableActivePlayerSymbols();
        DisableFlames();
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
        startInfo.SetActive(true);
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i<gridSpaceList.Length; i++) 
        { 
             gridSpaceList[i].button.interactable = toggle; 
        }
    }


    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    public void SetStartingSide(string startingSide)
    {
        playerSide = startingSide;
        if (playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
        ToggleActivePlayerSymbol();
        StartGame();
    }

    void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayerButtons(false);
        startInfo.SetActive(false);
    }

    void SetPlayerButtons(bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

    void SetPlayerColorsInactive() 
    { 
        playerX.panel.color = inactivePlayerColor.panelColor; 
        playerX.text.color = inactivePlayerColor.textColor; 
        playerO.panel.color = inactivePlayerColor.panelColor; 
        playerO.text.color = inactivePlayerColor.textColor; 
    }

    public Sprite GetPlayerSideImage()
    {
        if (playerSide == "X")
        {
            return playerX.playerImage;
        } else
        {
            return playerO.playerImage;
        }
    }

    public void ToggleActivePlayerSymbol()
    {
        if (playerSide == "X")
        {
            activePlayerXSymbol.SetActive(true);
            activePlayerOSymbol.SetActive(false);
            ActivateGreenFlames();
        } else
        {
            activePlayerXSymbol.SetActive(false);
            activePlayerOSymbol.SetActive(true);
            ActivateBlueFlames();
        }
    }

    public void DisableActivePlayerSymbols()
    {
        activePlayerXSymbol.SetActive(false);
        activePlayerOSymbol.SetActive(false);
    }

    public void ActivateBlueFlames()
    {
        blueFlameLeft.SetActive(true);
        blueFlameRight.SetActive(true);
        greenFlameLeft.SetActive(false);
        greenFlameRight.SetActive(false);
    }

    public void ActivateGreenFlames()
    {
        greenFlameLeft.SetActive(true);
        greenFlameRight.SetActive(true);
        blueFlameLeft.SetActive(false);
        blueFlameRight.SetActive(false);
    }

    public void DisableFlames()
    {
        blueFlameLeft.SetActive(false);
        blueFlameRight.SetActive(false);
        greenFlameLeft.SetActive(false);
        greenFlameRight.SetActive(false);
    }
}
