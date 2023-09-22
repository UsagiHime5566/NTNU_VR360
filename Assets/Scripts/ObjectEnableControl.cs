using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEnableControl : HimeLib.SingletonMono<ObjectEnableControl>
{
    public List<GameObject> ControlsObject;
    public GameObject MovieCanvas;

    public void ToMovieMode(){
        foreach (var item in ControlsObject)
        {
            item.SetActive(false);
        }
        MovieCanvas.SetActive(true);
    }
    
    public void To3DMode(){
        foreach (var item in ControlsObject)
        {
            item.SetActive(true);
        }
        MovieCanvas.SetActive(false);
    }
}
