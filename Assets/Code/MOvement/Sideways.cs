using UnityEngine;
using System.Collections;

public class Sideways : IMovementTrajectory
{

    public float minX = -8f; // Límite izquierdo
    public float maxX = 8f;  // Límite derecho
    public float speed = 2f;

    public IEnumerator MoveCoroutine(GameObject obj)
    {
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane)).x;

        int direction = 1; // 1: derecha, -1: izquierda

        while (obj.activeInHierarchy)
        {
            // Mueve el objeto
            obj.transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

            // Comprueba los límites
            float posX = obj.transform.position.x;
            if (posX >= maxX)
            {
                obj.transform.position = new Vector3(maxX, obj.transform.position.y, obj.transform.position.z);
                direction = -1; //izquierda
            }
            else if (posX <= minX)
            {
                obj.transform.position = new Vector3(minX, obj.transform.position.y, obj.transform.position.z);
                direction = 1; //derecha
            }
            yield return null;
        }
    }
}