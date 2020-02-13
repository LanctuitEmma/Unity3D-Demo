using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct WatchImage
{
    public WatchModel Model;
    public Sprite Image;
}

[System.Serializable]
public struct ProfileImage
{
    public PackagingProfile Profile;
    public Sprite Image;
}

public class CatalogTriggerscript : MonoBehaviour
{
    public Canvas FinalScreen;
    public Text FavoriteWatchText;
    public Text FavoriteProfileText;
    public Image FavoriteWatchImage;
    public Image FavoriteProfileImage;
    public WatchImage[] WatchImagePairs;
    public ProfileImage[] ProfileImagePairs;

    bool readyToExit = false;
    
    // Start is called before the first frame update
    void Start()
    {
        FinalScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToExit && Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Quit!");
            Application.Quit();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PackagingProfile favoriteProfile = DataScript.GetFavoriteProfile();

        if (favoriteProfile == PackagingProfile.None || DataScript.FavoriteWatches == null)
        {
            Debug.Log("No data available!");
            return;
        }

        // get watch image to show
        Sprite watchSpriteToShow = null;
        foreach (var watchImagePair in WatchImagePairs)
        {
            if (DataScript.FavoriteWatches.Item1 == watchImagePair.Model)
            {
                watchSpriteToShow = watchImagePair.Image;
            }
        }

        // get profile image to show
        Sprite profileSpriteToShow = null;
        foreach (var profileImagePair in ProfileImagePairs)
        {
            if (favoriteProfile == profileImagePair.Profile)
            {
                profileSpriteToShow = profileImagePair.Image;
            }
        }

        FinalScreen.gameObject.SetActive(true);

        FavoriteWatchText.text = DataScript.FavoriteWatches.Item1.ToString();
        FavoriteWatchImage.sprite = watchSpriteToShow;
        var rt = FavoriteWatchImage.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(watchSpriteToShow.rect.width, watchSpriteToShow.rect.height) / 2.0f;

        FavoriteProfileText.text = favoriteProfile.ToString();
        FavoriteProfileImage.sprite = profileSpriteToShow;
        rt = FavoriteProfileImage.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(profileSpriteToShow.rect.width, profileSpriteToShow.rect.height) / 2.0f;

        Debug.Log(
            "Favorite watches: " + DataScript.FavoriteWatches.Item1 + ", " + DataScript.FavoriteWatches.Item2 +
            " and favorite profile: " + favoriteProfile
        );

        readyToExit = true;
    }

    void OnTriggerStay(Collider other)
    {
        //CanvasPackage.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        //CanvasPackage.SetActive(false);
    }
}
