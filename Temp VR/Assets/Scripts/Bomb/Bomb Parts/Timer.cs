using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : BombParts
{
    #region Timer Object
    [Space(20)]
    [Header("Timer Object")]
    [SerializeField] private Transform timerLoc;                            //Location of the Obejct, attached to centerEyeAnchor
    [SerializeField] private GameObject centerEyeAnchor;                    //Refernce to centerEyeAnchor - The Camera
    private MeshRenderer meshRenderer;
    private bool connectedToCamera;
    #endregion

    #region Timer Countdown
    [Space(20)]
    [Header("Timer Countdown")]
    [SerializeField] private TextMeshPro timerText;
    private float timeLeft;
    private bool runTimer;
    #endregion

    protected override void Start()
    {
        base.Start();

        //Set variables
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        connectedToCamera = true;
        runTimer = true;
    }

    private void Update()
    {
        //Runs tiemr
        if (runTimer)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }

            //Ends game
            else
            {
                timeLeft = 0;
                runTimer = false;

                GameManager.Instance.RanOUtOfTime();
            }
        }

        //Keeps the timer connected to the camera
        if (connectedToCamera)
        {
            gameObject.transform.position = timerLoc.position;
            gameObject.transform.rotation = centerEyeAnchor.transform.rotation;
        }
    }

    /// <summary>
    /// Update and Print the timer
    /// </summary>
    /// <param name="currentTime"></param>
    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    /// <summary>
    /// Drop Timer when grabbed - called in Hand Grab Interactable Event
    /// </summary>
    public void DropCamera()
    {
        meshRenderer.enabled = true;
        connectedToCamera = false;
        bombPartRigid.isKinematic = false;
        bombPartRigid.useGravity = true;
    }
}
