using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlV360Cam : HimeLib.SingletonMono<ControlV360Cam>
{
    public void UpdateView(Vector3 pos, Vector3 rot){
        transform.position = pos;
        transform.eulerAngles = rot;
    }
}
