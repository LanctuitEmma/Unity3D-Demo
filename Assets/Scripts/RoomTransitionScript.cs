using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomTransitionScript : MonoBehaviour
{
    public EventManager EventManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Tuple<WatchModel, WatchModel> favoriteWatches = DataScript.GetFavoriteWatches();

        // if not enough data available, do nothing
        if (favoriteWatches == null)
        {
            Debug.Log("Not enough data available!");
            return;
        }

        EventManager.RoomTransition();

        DataScript.FavoriteWatches = favoriteWatches;
        Debug.Log("Favorite watches are " + favoriteWatches.Item1 + " and " + favoriteWatches.Item2);

        var projectionManager = FindObjectOfType<ProjectionManagerScript>();
        projectionManager.StartProjection();

        // freeze watch choice
        gameObject.SetActive(false);
    }
}
