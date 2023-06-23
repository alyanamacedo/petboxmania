using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class GlobalUI : MonoBehaviour
{
    private GameObject keyboard, gamepad, gamepadNumber;

    private void OnEnable() {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Device");
        keyboard = objectsWithTag.FirstOrDefault(t => t.name == "Keyboard");
        gamepad = objectsWithTag.FirstOrDefault(t => t.name == "Gamepad");
        gamepadNumber = objectsWithTag.FirstOrDefault(t => t.name == "GamepadNumber");
    }

    //Player Input Send Messages. Função de Sair da Cena ou do Jogo
    public void OnCancel(InputValue value)
    {
        Cancel();
    }

    public void Cancel(){
        Scene currentScene = SceneManager.GetActiveScene();
        string name = currentScene.name;
        if(name == Variables.sceneNames[0]){
            QuitGame();
        }else if(name == Variables.sceneNames[1]){
            Transition.LoadScene(Variables.sceneNames[0]);
        }else{
            Transition.LoadScene(Variables.sceneNames[1]);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    /*public void OnScrollWheel(InputValue value){
        Debug.Log("Scroll");
    }*/

    //atualiza os componentes visuais da UI/HUD
    public void UpdateUI(bool isOn, int gamepadCount){
        //Changes between Gamepad and Keyboard
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
        if (gamepadNumber != null)
        {
            TextMeshProUGUI textComponent = gamepadNumber.GetComponentInChildren<TextMeshProUGUI>();
            textComponent.text = gamepadCount.ToString();
        }
    }
}
