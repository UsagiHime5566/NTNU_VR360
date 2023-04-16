using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TxtResolution : MonoBehaviour
{
    Text target;

    void Awake(){
        target = GetComponent<Text>();
    }

    IEnumerator Start()
    {
        while(true){
            yield return new WaitForSeconds(1);

            int sc_width = Screen.width;
            int sc_height = Screen.height;
            target.text = $"{sc_width}x{sc_height}";
        }
    }
}
