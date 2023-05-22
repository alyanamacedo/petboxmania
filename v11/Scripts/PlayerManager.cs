using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public List<Player> players;
    public List<InputActionAsset> assetPlayer; //Hierarquia: InputActionAsset -> InputActionMap -> InputAction
    private List<(int,List<InputAction>)> actionPlayer; //actionPlayer contém a lista com todos os players e suas respectivas actions
    [HideInInspector]
    public bool winLevel, loseLevel;
    public GameObject winPanel, losePanel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        winLevel = loseLevel = false;

        actionPlayer = new List<(int,List<InputAction>)>();
        for(int i=0; i<players.Count; i++){
            players[i].idPlayer = i;
            InputActionMap actionMap = assetPlayer[i].FindActionMap("Player"); // Busca o ActionMap pelo nome
            List<InputAction> actions = new List<InputAction>();
            int j = 0;
            foreach (InputAction action in actionMap.actions){
                MethodInfo methodInfo = this.GetType().GetMethod(action.name, BindingFlags.NonPublic | BindingFlags.Instance);
                actions.Add(actionMap.FindAction(action.name)); // Procura o InputAction de movimentação pelo nome
                actions[j].performed += (context) => methodInfo.Invoke(this, new object[] { context });
                actions[j].canceled += (context) => methodInfo.Invoke(this, new object[] { context });
                actions[j].Enable();
                j++;
            }
            actionPlayer.Add((i, actions)); //i = id do player, actions é a lista de actions do player
        }
   
        /*foreach (var tuple in actionPlayer){
            Debug.Log($"ID: {tuple.Item1}");
            foreach (var action in tuple.Item2){
                Debug.Log($"Action id: {action.id}, Action Name: {action.name}" );
            }
        }*/
    }

    private void Move(InputAction.CallbackContext context) 
    {
        var tuple = actionPlayer.Find(t => t.Item2.Exists(a => a.id == context.action.id));
        int id = tuple.Item1;  // procura o id com o action.id. O action.id é único para cada Player/Action
        //Debug.Log($"Action {context.action.id} found for player {id}");
        
        Vector2 value = context.ReadValue<Vector2>(); // Obtém o valor de entrada do dispositivo
        string commandName = context.action.name; // Obtém o nome do comando pressionado
        string deviceName = context.control.device.name; // Obtém o nome do dispositivo utilizado
        //Debug.Log("Command " + commandName + " used on device " + deviceName + " with value " + value + " \nAction: "+ context.action);
        
        players[id].OnMove(value);
    }

    private void Tilt(InputAction.CallbackContext context){
        var tuple = actionPlayer.Find(t => t.Item2.Exists(a => a.id == context.action.id));
        int id = tuple.Item1;
        
        Vector2 value = context.ReadValue<Vector2>();
        players[id].OnTilt(value);
    }

    private void Others(InputAction.CallbackContext context){
        Debug.Log(context.control); //identifica as outras entradas do Gamepad
    }

    public void WinLevel(){
        if(players[0].collisionTarget && players[1].collisionTarget && !winLevel){
            winLevel = true;
            Debug.Log("WIN LEVEL");
            winPanel.SetActive(true);
            //GameObject.Find("UICanvas/WinPanel").gameObject.SetActive(true);
        }
    }

    public void LoseLevel(){
        loseLevel = true;
        Debug.Log("LOSE LEVEL");
        losePanel.SetActive(true);
    }
}
