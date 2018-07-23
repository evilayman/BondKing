using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Raskulls.Variables;
using Raskulls.Events;

public class TouchInput : MonoBehaviour
{
    [SerializeField]
    private GameEvent startTouchLeft, stoppedTouchLeft, startTouchRight, stoppedTouchRight;

    private float screenHalf;

    private int[] touchesPositions;

    private void Start()
    {
        touchesPositions = new int[2];
        screenHalf = Screen.width / 2;
    }

    private void Update()
    {
        Touch[] myTouches = Input.touches;
        for (int i = 0; i < myTouches.Length; i++)
        {
            //Take input from 2 touch fingers only
            if (i < 2)
            {
                if (myTouches[i].phase == TouchPhase.Began)
                {
                    touchesPositions[i] = GetTouchSide(myTouches[i].position);
                    CheckTouchPositions(myTouches.Length);
                }
                else if (myTouches[i].phase == TouchPhase.Ended)
                {
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