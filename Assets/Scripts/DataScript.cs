using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public enum WatchModel 
{
    Retrograde,
    Quantieme,
    Bathyscape,
    Shakudo,
    FiftyFathoms,
    Ladybird,
    HuitJoursManuelle,
    MilleEtUneNuits,
    None
};

[System.Serializable]
public enum PackagingProfile
{
    Sea,
    Countryside,
    India,
    None
}

public static class DataScript
{
    public static Vector3 PlayerPosition = Vector3.zero;
    public static Quaternion PlayerRotation = Quaternion.identity;
    public static Dictionary<WatchModel, double> WatchTimes = new Dictionary<WatchModel, double>();
    public static Tuple<WatchModel, WatchModel> FavoriteWatches = null;
    
    // this is set inside ProjectionManagerScript because it is more convenient to do it through the inspector
    public static Profile[] Profiles = null;
    public static int[] ProfileCounters = null;

    public static WatchModel GetWatchModel(string watchName)
    {
        switch (watchName)
        {
            case "RetrogradeWatch":
                return WatchModel.Retrograde;
            case "QuantiemeWatch":
                return WatchModel.Quantieme;
            case "BathyscapeWatch":
                return WatchModel.Bathyscape;
            case "ShakudoWatch":
                return WatchModel.Shakudo;
            case "FiftyFathomsWatch":
                return WatchModel.FiftyFathoms;
            case "1001NuitsWatch":
                return WatchModel.MilleEtUneNuits;
            case "LadybirdWatch":
                return WatchModel.Ladybird;
            case "8JoursManuelleWatch":
                return WatchModel.HuitJoursManuelle;
            default:
                throw new ArgumentException("Invalid watch name!");
        }
    }

    public static PackagingProfile GetPackagingProfile(string profileName)
    {
        switch (profileName)
        {
            case "Sea":
                return PackagingProfile.Sea;
            case "Countryside":
                return PackagingProfile.Countryside;
            case "India":
                return PackagingProfile.India;
            default:
                return PackagingProfile.None;
                //throw new ArgumentException("Invalid profile name!");
        }
    }

    public static Tuple<WatchModel, WatchModel> GetFavoriteWatches()
    {
        if (DataScript.WatchTimes.Count < 3)
            return null;
        
        var watchTimesCopy = new Dictionary<WatchModel, double>(WatchTimes);
        WatchModel favoriteWatch = getFavoriteWatch(watchTimesCopy);
        watchTimesCopy.Remove(favoriteWatch);
        WatchModel secondFavoriteWatch = getFavoriteWatch(watchTimesCopy);

        return new Tuple<WatchModel, WatchModel>(favoriteWatch, secondFavoriteWatch);
    }

    public static PackagingProfile GetFavoriteProfile()
    {
        int maxCounter = 0;
        string favoriteProfile = null;

        for (int i = 0; i < ProfileCounters.Length; i++)
        {
            if (ProfileCounters[i] > maxCounter)
            {
                maxCounter = ProfileCounters[i];
                favoriteProfile = Profiles[i].name;
            }
        }

        Debug.Log("Fav profile: " + favoriteProfile);

        return GetPackagingProfile(favoriteProfile);
    }

    private static WatchModel getFavoriteWatch(Dictionary<WatchModel, double> watchTimes)
    {
        WatchModel favoriteWatch = WatchModel.None;
        double longestTime = -1.0;

        foreach (var item in watchTimes)
        {
            if (watchTimes[item.Key] > longestTime)
            {
                longestTime = item.Value;
                favoriteWatch = item.Key;
            }
        }

        return favoriteWatch;
    }
}
