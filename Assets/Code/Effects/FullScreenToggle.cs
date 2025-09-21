using UnityEngine;
using UnityEngine.EventSystems;

public class FullScreenToggle : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
