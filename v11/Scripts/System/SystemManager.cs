using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public static SystemManager instance; //permite que outras classes acessem uma instância dessa classe diretamente

    [Header("Classes de controle")]
    [Tooltip("Nome das classes")]
    public Devices devices;
    public UIGlobalInput hud;
    [HideInInspector]
    public Variables variables = new Variables();

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

    private void Start() {
        variables.SceneNames(); //carrega o nome de todas as cenas na variável
    }
}
