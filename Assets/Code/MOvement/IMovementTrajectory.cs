using UnityEngine;
using System.Collections;

public interface IMovementTrajectory
{
    IEnumerator MoveCoroutine(GameObject obj);
}