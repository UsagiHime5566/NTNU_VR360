using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MovieSyncho : MonoBehaviour
{
    public List<VideoPlayer> vp;
    public float prepareTime = 3;

    private void OnEnable() {
        SystemManager.instance.OnSynchroPlay += AddPlayCallback;

        SystemManager.instance.OnSynchroStop += AddStopCallback;
        
        SystemManager.instance.OnSwitchScreen += AddSwitchScreen;
    }
    private void OnDisable() {
        SystemManager.instance.OnSynchroPlay -= AddPlayCallback;

        SystemManager.instance.OnSynchroStop -= AddStopCallback;

        SystemManager.instance.OnSwitchScreen -= AddSwitchScreen;
    }
    void AddPlayCallback(){
        for (int i = 0; i < vp.Count; i++)
        {
            if(i == SystemManager.instance.currentDisplay - 1){
                vp[i].Play();
            }
        }
    }
    void AddStopCallback(){
        foreach (var item in vp)
        {
            item.Stop();
            item.time = 0;
        }
    }
    void AddSwitchScreen(int display)
    {
        switch (display)
        {
            case 0:
                vp[0].Stop();
                vp[1].Stop();
                vp[2].Stop();
                break;
            case 1:
                //vp[0].Stop();
                vp[1].Stop();
                vp[2].Stop();
                break;
            case 2:
                vp[0].Stop();
                //vp[1].Stop();
                vp[2].Stop();
                break;
            case 3:
                vp[0].Stop();
                vp[1].Stop();
                //vp[2].Stop();
                break;
        }
    }
    IEnumerator Start()
    {
        yield return new WaitForSeconds(prepareTime);

        SystemManager.instance.InvokeSynchoPlay();
        SystemManager.instance.SynchoCmd("play");
    }
}
