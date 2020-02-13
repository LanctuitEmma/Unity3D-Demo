using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackagingTriggerScript : MonoBehaviour
{
    public GameObject CanvasPackage;
    // Start is called before the first frame update
    void Start()
    {
        CanvasPackage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerStay(Collider other){
        CanvasPackage.SetActive(true);
    }

    void OnTriggerExit(Collider other){
        CanvasPackage.SetActive(false);
    }
    void OnGUI(){
        Event e = Event.current;
        if (e.isKey && e.keyCode == KeyCode.Space)
        {
            print("exit");
            EditorApplication.Quit()
        }
    }
}
