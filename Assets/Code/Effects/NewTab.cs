using UnityEngine;
using UnityEngine.EventSystems;

public class NewTab : MonoBehaviour, IPointerClickHandler
{
    public string url = "http://www.unity3d.com";

    public void OnPointerClick(PointerEventData eventData)
    {
        Application.OpenURL(url);
    }
}
