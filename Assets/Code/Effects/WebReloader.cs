using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class WebReloader : MonoBehaviour, IPointerClickHandler
{
    [DllImport("__Internal")]
    private static extern void ReloadPage();
    
    public void ReloadWebPage()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        ReloadPage();
        #endif
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ReloadWebPage();
    }
}