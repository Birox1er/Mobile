using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Unit : MonoBehaviour
{
    private int mov;
    public int Mov { get => mov; }
    [SerializeField]
    private float movDur = 0.3f;//, rotaDur = 0.1f;

    private GlowMov glowMov;
    private Queue<Vector3> pathPos = new Queue<Vector3>();
    public event Action<Unit> MovementFinished;
    private void Awake()
    {
        mov = GetComponent<Chara>().Mov;
        glowMov = GetComponent<GlowMov>();
    }
    public void Deselect()
    {
        glowMov.ToggleGlow(false);
    }
    public void Select()
    {
        glowMov.ToggleGlow();
    }
    public void MoveThroughPath(List<Vector3> currentPath)
    {
        pathPos = new Queue<Vector3>(currentPath);
        Vector3 firstTarget = pathPos.Dequeue();
        StartCoroutine(MoveCoroutine(firstTarget));
    }
    /// <summary>
    /// Might not be necessary for the end result at least not in this form.
    /// </summary>
    /// <param name="endPos"></param>
    /// <param name="rotaDur"></param>
    /// <returns></returns>
   /* private IEnumerator RotaCoroutine(Vector3 endPos,float rotaDur)
    {
        Quaternion startRota = transform.rotation;
        endPos.z = transform.position.z;
        Vector3 direction = endPos - transform.position;
        Quaternion endRota = Quaternion.LookRotation(direction, Vector3.forward);
        if (Mathf.Approximately(Mathf.Abs(Quaternion.Dot(startRota, endRota)), 1.0f) == false)
        {
            float timeSince = 0;
            while (timeSince < rotaDur)
            {
                timeSince += Time.deltaTime;
                float lerpStep = timeSince / rotaDur;
                transform.rotation = Quaternion.Lerp(startRota, endRota, lerpStep);
                yield return null;
            }
            transform.rotation = endRota;
        }
        StartCoroutine(MoveCoroutine(endPos));   
    }*/
    private IEnumerator MoveCoroutine(Vector3 endpos)
    {
        Vector3 startPos = transform.position;
        endpos.z = startPos.z;
        float timeSince = 0;
        while (timeSince < movDur)
        {
            timeSince += Time.deltaTime;
            float lerpStep = timeSince / movDur;
            transform.position = Vector3.Lerp(startPos, endpos, lerpStep);
            yield return null;
        }
        transform.position = endpos;
        if (pathPos.Count > 0)
        {
            StartCoroutine(MoveCoroutine(pathPos.Dequeue()));
        }
        else
        {
            MovementFinished?.Invoke(this);
        }
    }
}
