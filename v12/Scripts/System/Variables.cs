using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 //Classe com variáreis e constantes globais
public class Variables
{
    public List<string> sceneNames;

    public List<string> SceneNames(){
        List<string> scenes = new List<string>();
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            scenes.Add(sceneName);
        }

        return scenes;
    }    
}   