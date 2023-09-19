using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieScene : MonoBehaviour
{
    public List<string> toEnable;
    public List<string> toDisable;
    public ActType actType;
    void Start2()
    {
        foreach (var item in toEnable)
        {
            var obje = GameObject.Find(item);
            if(obje != null){
                obje.SetActive(true);
            }
            Debug.Log(obje);
        }

        foreach (var item in toDisable)
        {
            var objd = GameObject.Find(item);
            if(objd != null){
                objd.SetActive(false);
            }
        }
    }

    void Start(){
        if(actType == ActType.Movie){
            ObjectEnableControl.instance.ToMovieMode();
        }
        if(actType == ActType.V3D){
            ObjectEnableControl.instance.To3DMode();
        }
    }

    public enum ActType
    {
        Movie = 0,
        V3D = 1
    }
}
