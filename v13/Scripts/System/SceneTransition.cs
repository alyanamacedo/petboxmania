using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class SceneTransition : MonoBehaviour
{
    enum Scenes {Home,LevelList,Level01} //Precisa atualizar sempre que houver alteração na quantidade e nome das cenas

    public float duration = 0.5f;
    public Color color = Color.black;
    [SerializeField] private Scenes scene;

    public void PerformTransition()
    {
        Transition.LoadScene(scene.ToString(), duration, color);
    }
}
