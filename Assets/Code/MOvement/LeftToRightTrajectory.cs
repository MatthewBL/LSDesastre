using UnityEngine;
using System.Collections;

public class LeftToRightTrajectory : IMovementTrajectory
{
    public IEnumerator MoveCoroutine(GameObject obj)
    {
        while (obj.activeInHierarchy)
        {
            obj.transform.Translate(Vector3.right * Time.deltaTime * 2f);
            yield return null;
        }
    }
}