using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private UnityEvent disableInteraction;

    [SerializeField] private GameObject quitObject;
    [SerializeField] private GameObject restartObject;

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject caughtText;
    [SerializeField] private GameObject placedAllBombPartsText;
    [SerializeField] private GameObject ranOutOfTimeText;

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
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CaughtPlayer()
    {
        canvas.SetActive(true);
        caughtText.SetActive(true);
        quitObject.SetActive(true);
        restartObject.SetActive(true);

        disableInteraction?.Invoke();
    }

    public void PlacedAllBombParts()
    {
        canvas.SetActive(true);
        placedAllBombPartsText.SetActive(true);
        quitObject.SetActive(true);
        restartObject.SetActive(true);

        disableInteraction?.Invoke();
    }

    public void RanOUtOfTime()
    {
        canvas.SetActive(true);
        ranOutOfTimeText.SetActive(true);
        quitObject.SetActive(true);
        restartObject.SetActive(true);

        disableInteraction?.Invoke();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
