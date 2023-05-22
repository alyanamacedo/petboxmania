using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Device : MonoBehaviour
{
    // Obter os dispositivos conectados no momento da inicialização
    private List<InputDevice> devices = new List<InputDevice>();
    private int gamepadCount = 0;

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
                SystemManager.instance.UI_GamepadUI(true);
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
                    SystemManager.instance.UI_GamepadUI(true);
                }
                //Debug.Log($"Dispositivo adicionado: {device.name}, Device layout: {device.layout}");
                break;

            case InputDeviceChange.Removed:
                if (device is Gamepad){
                    gamepadCount--;
                    SystemManager.instance.UI_GamepadUI(false);
                }
                //Debug.Log($"Dispositivo removido: {device.name}, Device layout: {device.layout}");
                break;
            case InputDeviceChange.ConfigurationChanged:
                //Debug.Log("Device configuration changed: " + device);
                break;
        }
    }
}
