using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatchDisplay : MonoBehaviour
{
    public GameObject WatchDescriptionCanvas;
    public Canvas Prompt;

    private DateTime timeEnter;
    private bool triggerActive = false;

    // Start is called before the first frame update
    void Start()
    {
        WatchDescriptionCanvas.SetActive(false);
        Prompt.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerActive = true;

        // show prompt for watch description
        Prompt.GetComponentInChildren<Text>().text = "Press 'E' to view description";
        Prompt.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        triggerActive = false;

        // hide prompt for watch description
        Prompt.gameObject.SetActive(false);

        // hide the description itself if it was being shown
        if (WatchDescriptionCanvas.activeInHierarchy)
        {
            WatchDescriptionCanvas.SetActive(false);
            stopTrackingWatchTime();
        }
    }

    void OnGUI()
    {
        // only if the player is inside my own trigger!
        if (triggerActive)
        {
            Event e = Event.current;
            if (e.isKey && e.type == EventType.KeyDown && e.keyCode == KeyCode.E)
            {
                if (WatchDescriptionCanvas.activeInHierarchy)
                {
                    Prompt.GetComponentInChildren<Text>().text = "Press 'E' to view description";
                    WatchDescriptionCanvas.SetActive(false);

                    stopTrackingWatchTime();
                }
                else
                {
                    Prompt.GetComponentInChildren<Text>().text = "Press 'E' to close description";
                    WatchDescriptionCanvas.SetActive(true);

                    startTrackingWatchTime();
                }
            }
        }
    }

    private void startTrackingWatchTime() {
        string watchName = gameObject.transform.GetChild(0).gameObject.name;
        WatchModel watchModel = DataScript.GetWatchModel(watchName);

        if (DataScript.WatchTimes.ContainsKey(watchModel))
        {
            Debug.Log("Watch time already present!");
        }
        else
        {
            Debug.Log("Creating new watch time!");
            DataScript.WatchTimes.Add(watchModel, 0);
        }

        timeEnter = DateTime.Now;
    }

    private void stopTrackingWatchTime() {
        // record watch time
        string watchName = gameObject.transform.GetChild(0).gameObject.name;
        WatchModel watchModel = DataScript.GetWatchModel(watchName);

        DataScript.WatchTimes[watchModel] += (DateTime.Now - timeEnter).TotalSeconds;
        Debug.Log("Watch time: " + DataScript.WatchTimes[watchModel] + "seconds.");
    }
}
