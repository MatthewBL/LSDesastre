using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundClicker : MonoBehaviour, IPointerClickHandler
{
    public int counter = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Increases counter by 1 when the object is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        counter++;
    }
}