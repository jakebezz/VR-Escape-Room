using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : BombParts
{
    //Timer Object
    [SerializeField] private Transform timerLoc;
    [SerializeField] private GameObject centerEyeAnchor;
    private MeshRenderer meshRenderer;
    private bool connectedToCamera;
    

    //Timer Countdown
    [SerializeField] private float timeLeft;
    [SerializeField] private TextMeshPro timerText;
    private bool runTimer;

    protected override void Start()
    {
        base.Start();

        //Sets variabls
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        connectedToCamera = true;
        runTimer = true;
    }

    private void Update()
    {
        //Runs tiemr
        if(runTimer)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                //Ends game
                Debug.Log("END GAME");
                timeLeft = 0;
                runTimer = false;
            }
        }

        //Keeps the timer connected to the camera
        if (connectedToCamera)
        {
            gameObject.transform.position = timerLoc.position;
            gameObject.transform.rotation = centerEyeAnchor.transform.rotation;
        }
    }

    //Updates and prints the timer every frame/second
    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    //Drops camera
    public void DropCamera()
    {
        meshRenderer.enabled = true;
        connectedToCamera = false;
        bombPartRigid.isKinematic = false;
        bombPartRigid.useGravity = true;
    }
}
