using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SystemManager : MonoBehaviour
{
    public static SystemManager instance; //static permite que outras classes acessem essa instância diretamente

    //[Header("Classes de controle")]
    //[Tooltip("Nome das classes")]
    //[HideInInspector]
    public Devices devices;
    public GlobalUI hud;
    public Variables variables;
    
    // called zero
    private void Awake()
    {
        devices = new Devices();
        hud = gameObject.AddComponent<GlobalUI>();
        variables = new Variables();
        Variables.SceneNames(); //carrega o nome de todas as cenas na variável
    }
    
    // called third
    private void Start()
    {
        //InputSystem.onDeviceChange += OnDeviceChangeHandler; // Registrar o evento para identificar mudanças nos dispositivos
        InputSystem.onDeviceChange += (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    OnDeviceChangeHandler(device, change);
                    // New Device.
                    break;
                case InputDeviceChange.Removed:
                    OnDeviceChangeHandler(device, change);
                    // Remove from Input System entirely; by default, Devices stay in the system once discovered.
                    break;
                default:
                    // See InputDeviceChange reference for other event types.
                    break;
            }
        };
    }
    
    private void OnDestroy()
    {
        InputSystem.onDeviceChange -= OnDeviceChangeHandler;
    }

    // called first
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // called second
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        (bool isOn, int gamepadCount) = devices.OnDeviceCheck();
        hud.UpdateUI(isOn, gamepadCount);
    }

    private void OnDeviceChangeHandler(InputDevice device, InputDeviceChange change)
    {
        // Chama a função OnDeviceChange e armazena os valores retornados
        (bool isOn, int gamepadCount) = devices.OnDeviceChange(device, change);
        hud.UpdateUI(isOn, gamepadCount);
    }

    //Events, inputs
    public void Cancel(){
        hud.Cancel();
    }
}