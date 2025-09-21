using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySound : MonoBehaviour, IPointerClickHandler
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
