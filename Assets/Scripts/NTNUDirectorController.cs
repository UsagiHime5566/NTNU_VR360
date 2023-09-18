using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NTNUDirectorController : MonoBehaviour
{
    public PlayableDirector mainDirector;
    public AudioSource bgm;
    void Start()
    {
        SystemManager.instance.OnSynchroPlay += () => {
            mainDirector.Play();
            bgm.Play();
        };

        SystemManager.instance.OnSynchroStop += () => {
            mainDirector.Stop();
            mainDirector.time = 0;
            bgm.Stop();
            bgm.time = 0;
        };
    }
}
