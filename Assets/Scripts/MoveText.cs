using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveText : MonoBehaviour
{
    
    void Start()
    {
        transform.position = SystemConfig.Instance.GetData<Vector3>("TextPos", transform.position);
        transform.localScale = SystemConfig.Instance.GetData<Vector3>("TextScale", transform.localScale);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0)){
            transform.position = transform.position + new Vector3(0, 0, 0.1f);
            SystemConfig.Instance.SaveData("TextPos", transform.position);
        }
        if(Input.GetKeyDown(KeyCode.Alpha9)){
            transform.position = transform.position - new Vector3(0, 0, 0.1f);
            SystemConfig.Instance.SaveData("TextPos", transform.position);
        }
        if(Input.GetKeyDown(KeyCode.Alpha7)){
            transform.localScale = transform.localScale + new Vector3(0.01f, 0.01f, 0.1f);
            SystemConfig.Instance.SaveData("TextScale", transform.localScale);
        }
        if(Input.GetKeyDown(KeyCode.Alpha8)){
            transform.localScale = transform.localScale - new Vector3(0.01f, 0.01f, 0.1f);
            SystemConfig.Instance.SaveData("TextScale", transform.localScale);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)){
            transform.position = transform.position + new Vector3(0, 0.1f, 0);
            SystemConfig.Instance.SaveData("TextPos", transform.position);
        }
        if(Input.GetKeyDown(KeyCode.Alpha6)){
            transform.position = transform.position - new Vector3(0, 0.1f, 0);
            SystemConfig.Instance.SaveData("TextPos", transform.position);
        }
    }

    
}
