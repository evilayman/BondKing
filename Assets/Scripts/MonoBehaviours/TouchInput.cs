using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Raskulls.Variables;
using Raskulls.Events;

public class TouchInput : MonoBehaviour
{
    [SerializeField]
    private float jumpButtonThreshold;
    [SerializeField]
    private GameEvent startTouchLeft, stoppedTouchLeft, startTouchRight, stoppedTouchRight, jumpedLeft, jumpedRight;

    private float screenHalf;

    private int[] touchesPositions;
    private float[] touchesTime;

    private void Start()
    {
        touchesPositions = new int[2];
        touchesTime = new float[2];

        screenHalf = Screen.width / 2;
    }

    private void Update()
    {
        CheckTouch();
    }

    private void CheckTouch()
    {
        Touch[] myTouches = Input.touches;
        for (int i = 0; i < myTouches.Length; i++)
        {
            if (i < 2)
            {
                if (myTouches[i].phase == TouchPhase.Began)
                {
                    touchesPositions[i] = GetTouchSide(myTouches[i].position);
                    touchesTime[i] = Time.time;
                    CheckTouchPositions(myTouches.Length);
                }
                else if (myTouches[i].phase == TouchPhase.Ended)
                {
                    float timeOfTouch = Time.time - touchesTime[i];

                    if (timeOfTouch <= jumpButtonThreshold)
                    {
                        if (touchesPositions[i] == 1)
                            jumpedRight.Raise();
                        else
                            jumpedLeft.Raise();
                    }

                    touchesTime[i] = 0;
                    touchesPositions[i] = 0;
                    CheckTouchPositions(myTouches.Length);

                }
            }
            else
                break;
        }
    }

    private int GetTouchSide(Vector2 pos)
    {
        return (pos.x < screenHalf) ? -1 : 1;
    }

    private void CheckTouchPositions(int touchLength)
    {
        if (touchLength == 1)
            touchesPositions[1] = 0;

        if (touchesPositions[0] == 1 || touchesPositions[1] == 1)
            startTouchRight.Raise();
        else
            stoppedTouchRight.Raise();

        if (touchesPositions[0] == -1 || touchesPositions[1] == -1)
            startTouchLeft.Raise();
        else
            stoppedTouchLeft.Raise();
    }
}