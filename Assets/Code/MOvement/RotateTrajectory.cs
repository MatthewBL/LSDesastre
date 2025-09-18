using UnityEngine;
using System.Collections;

public class RotateTrajectory : IMovementTrajectory
{
    public IEnumerator MoveCoroutine(GameObject obj)
    {
        while (obj.activeInHierarchy)
        {
            obj.transform.Rotate(Vector3.forward, 90f * Time.deltaTime);
            yield return null;
        }
    }
}