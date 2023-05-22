using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public static SystemManager instance; //permite que outras classes acessem uma instância dessa classe diretamente

    public Device device;
    public UIInput hud;
    public Variable variable;    

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

    public void UI_GamepadUI(bool isOn){
        hud.GamepadUI(isOn);
    }
}
