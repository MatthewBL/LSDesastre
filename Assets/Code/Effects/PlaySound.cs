using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySound : MonoBehaviour, IPointerClickHandler
{
    public AudioSource audioSource = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
