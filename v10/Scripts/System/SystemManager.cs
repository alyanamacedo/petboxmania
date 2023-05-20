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

        if (!PlayerPrefs.HasKey("music_on"))
            PlayerPrefs.SetInt("music_on", 1);

        if (!PlayerPrefs.HasKey("sound_on"))
            PlayerPrefs.SetInt("sound_on", 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        InputSystem.onDeviceChange += OnDeviceChange; // Registrar o evento para identificar mudanças nos dispositivos
    }
    
    private void OnDestroy()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnDeviceCheck();
    }

    private void OnDeviceCheck(){
        gamepadCount = 0;
        devices = new List<InputDevice>();
        foreach (var device in InputSystem.devices)
        {
            if (device is Gamepad){
                gamepadCount++;
                UIGamepad(true);
            }
            //Debug.Log($"Device name: {device.name}, Device layout: {device.layout}");
            devices.Add(device);
        }
        //Debug.Log($"Total de gamepads conectados: {gamepadCount}");
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Added:
                if (device is Gamepad){
                    gamepadCount++;
                    UIGamepad(true);
                }
                //Debug.Log($"Dispositivo adicionado: {device.name}, Device layout: {device.layout}");
                break;

            case InputDeviceChange.Removed:
                if (device is Gamepad){
                    gamepadCount--;
                    UIGamepad(false);
                }
                //Debug.Log($"Dispositivo removido: {device.name}, Device layout: {device.layout}");
                break;
            case InputDeviceChange.ConfigurationChanged:
                //Debug.Log("Device configuration changed: " + device);
                break;
        }
    }
    private void UIGamepad(bool isOn){
        if(isOn){
            GameObject.Find("Canvas/GroupLayout/Footer/GroupLayoutKeyboard").gameObject.SetActive(false);
            GameObject.Find("Canvas/GroupLayout/Footer/GroupLayoutGamepad").gameObject.SetActive(true);
        }else{
            GameObject.Find("Canvas/GroupLayout/Footer/GroupLayoutKeyboard").gameObject.SetActive(true);
            GameObject.Find("Canvas/GroupLayout/Footer/GroupLayoutGamepad").gameObject.SetActive(false);
        }
    }
}
