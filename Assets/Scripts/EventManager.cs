using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void RoomTransitionAction();
    public static event RoomTransitionAction OnRoomTransition;

    public delegate void VideoFinishedAction();
    public static event VideoFinishedAction OnVideoFinished;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RoomTransition()
    {
        if (OnRoomTransition != null)
        {
            OnRoomTransition();
        }
    }

    public void VideoFinished()
    {
        if (OnVideoFinished != null)
        {
            OnVideoFinished();
        }
    }
}
