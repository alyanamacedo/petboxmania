using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class SystemUIMenu : MonoBehaviour
{
    //public static SystemUIMenu instance; //permite que outras classes acessem uma instância dessa classe diretamente

    public ScreenFader screenFader;

/*
    private void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //mantém a instância ativa para a próxima cena. A nova cena deve possuir o mesmo GameObject posto na hierarquia do projeto.
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
*/
    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void LoadScene(string scene){
        screenFader.FadeOut(() => SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single));
    }

    public void LoadScene()
    {
        screenFader.FadeOut(() => SceneManager.LoadSceneAsync("LevelList", LoadSceneMode.Single));
    }

    //Submit() é OnClick() e OnSubmit() juntos
    public void Submit(){
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
        {
            string selectedName = EventSystem.current.currentSelectedGameObject.name;

            switch(selectedName){
                case "Jogar":
                    Debug.Log("Jogar");
                    LoadScene("LevelList");
                    break;
                case "Opcoes":
                    Debug.Log("Opcoes");
                    
                    break;
                case "Sair":
                    Debug.Log("Sair");
                    QuitGame();
                    break;
                default:
                    Debug.Log("Nenhuma opção selecionada");
                    break;
            }

        }
        /*
        if(Input.GetKeyDown(KeyCode.Return)){
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            if(sceneName == "Home"){
                ToggleGroup toggleGroup = GameObject.Find("GroupLayout").GetComponent<ToggleGroup>();          
                Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
            }
        }*/
    }

    public void OnCancel(InputValue value)
    {
        Debug.Log("return");
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if(sceneName == "Home"){
            QuitGame();
        }else if(sceneName == "LevelList"){
            screenFader.FadeOut(() => SceneManager.LoadSceneAsync("Home", LoadSceneMode.Single));
        }else{
            screenFader.FadeOut(() => SceneManager.LoadSceneAsync("LevelList", LoadSceneMode.Single));
        }
    }
}
