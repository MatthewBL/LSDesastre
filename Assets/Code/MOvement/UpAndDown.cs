using UnityEngine;
using System.Collections;

public class UpAndDown : IMovementTrajectory
{
    public float minY = -4.5f; // Límite inferior
    public float maxY = 4.5f;  // Límite superior
    public float speed = 2f;

    public IEnumerator MoveCoroutine(GameObject obj)
    {
        minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y;
        maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane)).y;

        int direction = 1; // 1: arriba, -1: abajo

        while (obj.activeInHierarchy)
        {
            // Mueve el objeto
            obj.transform.Translate(Vector3.up * direction * speed * Time.deltaTime);

            // Comprueba los límites
            float posY = obj.transform.position.y;
            if (posY >= maxY)
            {
                obj.transform.position = new Vector3(obj.transform.position.x, maxY, obj.transform.position.z);
                direction = -1; // abajo
            }
            else if (posY <= minY)
            {
                obj.transform.position = new Vector3(obj.transform.position.x, minY, obj.transform.position.z);
                direction = 1; // arriba
            }
            yield return null;
        }
    }
}