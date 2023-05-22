using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform content;
    public float padding = 10f;

    private Toggle selectedToggle;

    private void Start()
    {
        // Assumindo que os Toggles estão dentro de um Vertical Layout Group
        Toggle[] toggles = GetComponentsInChildren<Toggle>();
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }
    }

    private void OnToggleValueChanged(bool isOn)
    {
        selectedToggle = null;

        Toggle[] toggles = GetComponentsInChildren<Toggle>();
        foreach (Toggle toggle in toggles)
        {
            if (toggle.isOn)
            {
                selectedToggle = toggle;
                break;
            }
        }

        ScrollToSelectedToggle();
    }

    private void ScrollToSelectedToggle()
    {
        if (selectedToggle != null)
        {
            // Obter a posição do toggle selecionado em relação ao conteúdo
            RectTransform selectedToggleRect = selectedToggle.GetComponent<RectTransform>();
            Vector2 selectedTogglePos = selectedToggleRect.anchoredPosition;
            float heightLimit = selectedToggleRect.rect.height*3;
            float positionY = System.Math.Abs(selectedTogglePos.y+heightLimit);

            if(selectedTogglePos.y < -heightLimit){
                Vector2 destinationPos = new Vector2(0, positionY);
                content.anchoredPosition = destinationPos;
                // Definir a velocidade da rolagem para zero para parar a inércia
                scrollRect.velocity = Vector2.zero;
            } 
        }
    }

    public void Great(){
        Debug.Log("WELL DONE");
    }


}
