using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlV360Cam : HimeLib.SingletonMono<ControlV360Cam>
{
    public float posAmp = 0.1f;
    public float rotAmp = 1;
    public void UpdateView(Vector3 pos, Vector3 rot){
        transform.position = pos;
        transform.eulerAngles = rot;
    }
    public void SaveView(){
        SystemConfig.Instance.SaveData("v360pos", transform.position);
        SystemConfig.Instance.SaveData("v360rot", transform.eulerAngles);
    }
    public void UpdatePos(PosDir pos){
        switch (pos)
        {
            case PosDir.Up:
                transform.position += transform.up * posAmp;
            break;
            case PosDir.Left:
                transform.position -= transform.right * posAmp;
            break;
            case PosDir.Right:
                transform.position += transform.right * posAmp;
            break;
            case PosDir.Down:
                transform.position -= transform.up * posAmp;
            break;
        }
        SystemConfig.Instance.SaveData("v360pos", transform.position);
        SystemManager.instance.SynchoCmd($"v360:{transform.position.x},{transform.position.y},{transform.position.z},{transform.eulerAngles.x},{transform.eulerAngles.y},{transform.eulerAngles.z}");
    }
    public void UpdateRot(RotDir rot){
        switch (rot)
        {
            case RotDir.Up:
                transform.eulerAngles -= new Vector3(1, 0, 0);
            break;
            case RotDir.Left:
                transform.eulerAngles -= new Vector3(0, 1, 0);
            break;
            case RotDir.Right:
                transform.eulerAngles += new Vector3(0, 1, 0);
            break;
            case RotDir.Down:
                transform.eulerAngles += new Vector3(1, 0, 0);
            break;
        }
        SystemConfig.Instance.SaveData("v360rot", transform.eulerAngles);
        SystemManager.instance.SynchoCmd($"v360:{transform.position.x},{transform.position.y},{transform.position.z},{transform.eulerAngles.x},{transform.eulerAngles.y},{transform.eulerAngles.z}");
    }
    public void UpdateDepth(Depth depth){
        switch (depth)
        {
            case Depth.Forward:
                transform.position += transform.forward * posAmp;
            break;
            case Depth.Back:
                transform.position -= transform.forward * posAmp;
            break;
        }
        SystemConfig.Instance.SaveData("v360pos", transform.position);
        SystemManager.instance.SynchoCmd($"v360:{transform.position.x},{transform.position.y},{transform.position.z},{transform.eulerAngles.x},{transform.eulerAngles.y},{transform.eulerAngles.z}");
    }
}

public enum PosDir
{
    Up = 0,
    Left = 1,
    Right = 2,
    Down = 3,
}

public enum RotDir
{
    Up = 0,
    Left = 1,
    Right = 2,
    Down = 3,
}

public enum Depth
{
    Forward = 0,
    Back = 3,
}