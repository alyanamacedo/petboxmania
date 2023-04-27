using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SystemManager : MonoBehaviour
{
    public static SystemManager instance; //permite que outras classes acessem uma instância dessa classe diretamente

    // Obter os dispositivos conectados no momento da inicialização
    private List<InputDevice> devices = new List<InputDevice>();
    private int gamepadCount = 0;

    private void Awake()
    {
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

    // Start is called before the first frame update
    void Start()
    {
        // Registrar o evento para identificar mudanças nos dispositivos
        InputSystem.onDeviceChange += OnDeviceChange; 
        
        foreach (var device in InputSystem.devices)
        {
            if (device is Gamepad){
                gamepadCount++;
            }
            //Debug.Log($"Device name: {device.name}, Device layout: {device.layout}");
            devices.Add(device);
        }
        //Debug.Log($"Total de gamepads conectados: {gamepadCount}");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("QUIT");
            QuitGame();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToScene(string scene){
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    private void OnDestroy()
    {
        // Remover o registro do evento ao destruir o objeto
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Added:
                gamepadCount++;
                //Debug.Log($"Dispositivo adicionado: {device.name}");
                break;

            case InputDeviceChange.Removed:
                gamepadCount--;
                //Debug.Log($"Dispositivo removido: {device.name}");
                break;
        }
    }
}
