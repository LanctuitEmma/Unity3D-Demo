using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;

[System.Serializable]
public struct Profile
{
    public string name;
    public VideoClip[] videos;
}

public class ProjectionManagerScript : MonoBehaviour
{
    public VideoPlayer[] VideoPlayers;
    public GameObject[] Screens;
    public Profile[] Profiles;
    public EventManager EventManager;

    private int videoPointer = 0;
    private int[] currentProfiles;
    private bool projectionStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Screens.Length != Profiles.Length)
        {
            throw new Exception("The number of screens and profiles doesn't match!");
        }

        currentProfiles = new int[Profiles.Length];

        DataScript.Profiles = Profiles;
        DataScript.ProfileCounters = new int[Profiles.Length];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartProjection()
    {
        if (projectionStarted)
        {
            return;
        }

        // assign profiles to screens
        for (int i = 0; i < currentProfiles.Length; i++)
        {
            currentProfiles[i] = i;
        }

        // start playing the first video from each profile
        for (int i = 0; i < VideoPlayers.Length; i++)
        {
            VideoPlayers[i].clip = Profiles[currentProfiles[i]].videos[videoPointer];
            VideoPlayers[i].Play();
        }

        videoPointer++;
        projectionStarted = true;

        Debug.Log("Projection started!");
    }

    public void ScreenClicked(GameObject clickedScreen)
    {
        // find out which screen was clicked
        int clickedIndex = -1;
        for (int i = 0; i < Screens.Length; i++)
        {
            if (Screens[i].GetInstanceID() == clickedScreen.GetInstanceID())
            {
                clickedIndex = i;
                break;
            }
        }

        // record the vote
        DataScript.ProfileCounters[currentProfiles[clickedIndex]]++;

        // shuffle profiles and play next video
        if (videoPointer != 0)
        {
            permuteCurrentProfiles();

            for (int i = 0; i < VideoPlayers.Length; i++)
            {
                Debug.Log("Assigning video " + videoPointer + " from profile " + currentProfiles[i] + " to video player " + i);
                Debug.Log("Video name: " + Profiles[currentProfiles[i]].videos[videoPointer].name);

                VideoPlayers[i].clip = Profiles[currentProfiles[i]].videos[videoPointer];
                VideoPlayers[i].Play();
            }

            videoPointer = (videoPointer + 1) % Profiles[0].videos.Length;
        }
        else
        {
            // deactivate screens
            EventManager.VideoFinished();
            foreach (var videoPlayer in VideoPlayers)
            {
                videoPlayer.Stop();
            }
        }
    }

    private void permuteCurrentProfiles()
    {
        Debug.Log("Profiles before permutation: " + currentProfiles[0] + ", "  + currentProfiles[1] + ", "  + currentProfiles[2]);

        for (int i = currentProfiles.Length - 1; i > 0; i--)
        {
            int swapIndex = UnityEngine.Random.Range(0, i + 1);
            int tmp = currentProfiles[i];
            currentProfiles[i] = currentProfiles[swapIndex];
            currentProfiles[swapIndex] = tmp;
        }

        Debug.Log("Profiles after permutation: " + currentProfiles[0] + ", "  + currentProfiles[1] + ", "  + currentProfiles[2]);
    }
}
