using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UIInput : MonoBehaviour
{
    //parâmetros do GameObject para realizar a transição de cena
    public float duration = 0.5f;
    public Color color = Color.black;

    private List<string> sceneName = new List<string>();

    private GameObject keyboard, gamepad;

    private void Start() {
        sceneName = SystemManager.instance.variable.sceneName;
    }

    public void PerformTransition(string scene)
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
        string name = currentScene.name;
        if(name == sceneName[0]){
            QuitGame();
        }else if(name == sceneName[1]){
            Transition.LoadScene(sceneName[0], duration, color);
        }else{
            Transition.LoadScene(sceneName[1], duration, color);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void OnScrollWheel(InputValue value){
        Debug.Log("Scroll");
    }

    public void GamepadUI(bool isOn){
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Device");

        foreach (GameObject obj in objectsWithTag)
        {
            if(obj.name == "Keyboard"){
                keyboard = obj;
            }else if(obj.name == "Gamepad"){
                gamepad = obj;
            }
        }

        if (keyboard != null && gamepad != null)
        {
            if(isOn){
                keyboard.SetActive(false);
                gamepad.SetActive(true);
            }else{
                keyboard.SetActive(true);
                gamepad.SetActive(false);
            }
        }else{
            Debug.LogWarning("O objeto não foi encontrado!");
        }
    }
}
