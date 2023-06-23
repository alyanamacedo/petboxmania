using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class Devices
{
    private List<InputDevice> devices = new List<InputDevice>(); // lista com os dispositivos conectados
    private int gamepadCount = 0;
    private bool isOn = false; //gamepad conectado

    public (bool, int) OnDeviceCheck(){
        gamepadCount = 0;
        devices = new List<InputDevice>();
        foreach (var device in InputSystem.devices)
        {
            if (device is Gamepad){
                gamepadCount++;
                isOn = true;
            }
            //Debug.Log($"Device name: {device.name}, Device layout: {device.layout}");
            devices.Add(device);
        }
        return (isOn, gamepadCount);
        //Debug.Log($"Total de gamepads conectados: {gamepadCount}");
    }

    public (bool, int) OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Added:
                if (device is Gamepad){
                    gamepadCount++;
                    isOn = true;
                }
                //Debug.Log($"Dispositivo adicionado: {device.name}, Device layout: {device.layout}");
                break;

            case InputDeviceChange.Removed:
                if (device is Gamepad){
                    gamepadCount--;
                    isOn = false;
                }
                //Debug.Log($"Dispositivo removido: {device.name}, Device layout: {device.layout}");
                break;
            case InputDeviceChange.ConfigurationChanged:
                //Debug.Log("Device configuration changed: " + device);
                break;
            default:
                break;
        }
        return (isOn, gamepadCount);
    }
}
