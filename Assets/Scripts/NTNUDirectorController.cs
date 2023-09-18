using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NTNUDirectorController : MonoBehaviour
{
    public PlayableDirector mainDirector;
    public AudioSource bgm;
    public string objToFind = "360ViewPosition";
    public Vector3 viewPos;
    public Vector3 viewRotate;
    void Start()
    {
        SystemManager.instance.OnSynchroPlay += () => {
            if(mainDirector != null)
                mainDirector.Play();
            if(bgm != null)
                bgm.Play();
        };

        SystemManager.instance.OnSynchroStop += () => {
            if(mainDirector != null)
            {
                mainDirector.Stop();
                mainDirector.time = 0;
            }
            if(bgm != null)
            {
                bgm.Stop();
                bgm.time = 0;
            }
        };

        var cams = GameObject.Find(objToFind);

        if(cams){
            cams.transform.position = viewPos;
            cams.transform.eulerAngles = viewRotate;
        }
    }
}
