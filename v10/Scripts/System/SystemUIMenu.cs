using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class SystemUIMenu : MonoBehaviour
{
    //parâmetros do GameObject para realizar a transição de cena
    public string scene = "<Insert scene name>";
    public float duration = 0.5f;
    public Color color = Color.black;

    //variável global com os nomes das cenas
    public List<string> scenesName;

    private void Start() {
        scenesName = new List<string>();
        scenesName.Add("Home");
        scenesName.Add("LevelList");
        scenesName.Add("Level01"); //futuramente pode criar um FOR aqui
    }

    public void PerformTransition()
    {
        Transition.LoadScene(scene, duration, color);
    }

    //Player Input Send Messages. Função de Sair da Cena ou do Jogo
    public void OnCancel(InputValue value)
    {
        Cancel();
    }
    public void Cancel(){
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if(sceneName == scenesName[0]){
            QuitGame();
        }else if(sceneName == scenesName[1]){
            Transition.LoadScene(scenesName[0], duration, color);
        }else{
            Transition.LoadScene(scenesName[1], duration, color);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
