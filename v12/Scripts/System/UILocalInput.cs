using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILocalInput : MonoBehaviour
{
    public float duration = 0.5f;
    public Color color = Color.black;

    public void PerformTransition(string scene)
    {
        Transition.LoadScene(scene, duration, color);
    }
  
    public void Cancel(){
        SystemManager.instance.hud.Cancel();
    }
}
