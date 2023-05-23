using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Essa classe não é atribuída a nenhum GameObject, logo não chama as funções: Awake, Start, Update,...
//Não herda MonoBehaviour 
public class Variables
{
    public List<string> sceneNames;

    public void SceneNames(){
        sceneNames = new List<string>();
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            sceneNames.Add(sceneName);
        }
    }
}
