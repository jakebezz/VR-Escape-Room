using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool lostGame;

    [SerializeField] private GameObject quitObject;
    [SerializeField] private GameObject restartObject;

    #region Text
    [Header("TextMeshPro")]
    [SerializeField] private TextMeshProUGUI caughtText;
    [SerializeField] private TextMeshProUGUI placedAllBombPartsText;
    [SerializeField] private TextMeshProUGUI ranOutOfTimeText;
    [SerializeField] private TextMeshProUGUI killedByElectricText;
    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        caughtText.enabled = false;
        placedAllBombPartsText.enabled = false;
        ranOutOfTimeText.enabled = false;
        killedByElectricText.enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #region End Game
    public void CaughtPlayer()
    {
        caughtText.enabled = true;
        quitObject.SetActive(true);
        restartObject.SetActive(true);
        lostGame = true;
    }

    public void PlacedAllBombParts()
    {
        if (lostGame == false)
        {
            placedAllBombPartsText.enabled = true;
            quitObject.SetActive(true);
            restartObject.SetActive(true);
        }
    }

    public void RanOUtOfTime()
    {
        ranOutOfTimeText.enabled = true;
        quitObject.SetActive(true);
        restartObject.SetActive(true);
        lostGame = true;
    }

    public void KilledByElectricBox()
    {
        killedByElectricText.enabled = true;
        quitObject.SetActive(true);
        restartObject.SetActive(true);
        lostGame = true;
    }
    #endregion
}
