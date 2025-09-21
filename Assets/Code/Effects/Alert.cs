using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Alert : MonoBehaviour, IPointerClickHandler
{
    // Import the functions from our .jslib plugin
    // The function name must match the one in the .jslib file

    [DllImport("__Internal")]
    private static extern void ShowAlert(string message);

    [DllImport("__Internal")]
    private static extern bool ShowConfirm(string message);

    // Public methods to be called by other C# scripts (e.g., on button clicks)
    public void TriggerAlertPopup(string message)
    {
        // This check prevents trying to call the function in the Unity Editor or a standalone build
        #if UNITY_WEBGL && !UNITY_EDITOR
            ShowAlert(message);
        #else
            // Fallback for testing in the Editor
        #endif
    }

    public void TriggerConfirmPopup(string message)
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            bool userClickedOK = ShowConfirm(message);
            Debug.Log("User clicked: " + (userClickedOK ? "OK" : "Cancel"));

            // You can now call different methods based on the user's choice
            if (userClickedOK)
            {
                OnConfirmOK();
            }
            else
            {
                OnConfirmCancel();
            }
        #else
            OnConfirmOK();
        #endif
    }

    // Methods to handle the result of the confirmation
    private void OnConfirmOK()
    {
    }

    private void OnConfirmCancel()
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TriggerAlertPopup("Hello from Unity WebGL!");
        // TriggerConfirmPopup("Do you want to delete this save file?");
    }
}