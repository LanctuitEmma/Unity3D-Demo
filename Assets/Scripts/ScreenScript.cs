using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScript : MonoBehaviour
{
    public GameObject Camera;
    public AudioSource AudioSource;
    public Material VideoMaterial;
    public Material BlackMaterial;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnRoomTransition += SetVideo;
        EventManager.OnVideoFinished += SetBlack;

        AudioSource.mute = true;
        gameObject.GetComponent<MeshRenderer>().material = BlackMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isCameraFacingObject())
            {
                Debug.Log(gameObject.name + " clicked.");

                var ProjectionManager = FindObjectOfType<ProjectionManagerScript>();
                ProjectionManager.ScreenClicked(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        if (isCameraFacingObject())
        {
            AudioSource.mute = false;
            gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        }
        else
        {
            AudioSource.mute = true;
            gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }

    private void SetBlack()
    {
        gameObject.GetComponent<MeshRenderer>().material = BlackMaterial;
    }

    private void SetVideo()
    {
        gameObject.GetComponent<MeshRenderer>().material = VideoMaterial;
        gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
    }

    private bool isCameraFacingObject()
    {
        // Will contain the information of which object the raycast hit
            RaycastHit hit;

            Vector3 rayOrigin = Camera.transform.position;
            Vector3 rayDirection = Camera.transform.forward;

            if (Physics.Raycast(rayOrigin, rayDirection, out hit) &&
                hit.collider.gameObject == gameObject)
            {
                return true;
            }
            else
            {
                return false;
            }
    }
}
