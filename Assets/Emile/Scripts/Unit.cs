using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Unit : MonoBehaviour
{
    public int Mov { get => GetComponent<Chara>().Mov; }
    [SerializeField]
    private float movDur = 0.3f;//, rotaDur = 0.1f;
    [SerializeField] private bool hasMoved = false;
    private GlowMov glowMov;
    private Queue<Vector3> pathPos = new Queue<Vector3>();
    private HexGrid grid;
    public event Action<Unit> MovementFinished;
    private Animator anim;
    [SerializeField] private AudioClip moveClip;
    soundManager sundManager;



    private void Start()
    {
        sundManager = FindObjectOfType<soundManager>();
        grid = FindObjectOfType<HexGrid>();
        glowMov = GetComponent<GlowMov>();
        anim = GetComponent<Chara>().GetAnim();
    }
    public void Select()
    {
        glowMov.ToggleGlow();
    }
    public void MoveThroughPath(List<Vector3> currentPath)
    {
        pathPos = new Queue<Vector3>(currentPath);
        Vector3 firstTarget = pathPos.Dequeue();
        if (hasMoved == false)
        {
            StartCoroutine(MoveCoroutine(firstTarget));
        }
    }
    public void MoveThroughPath(List<Vector3> currentPath,bool atk)
    {
        pathPos = new Queue<Vector3>(currentPath);
        Vector3 firstTarget = pathPos.Dequeue();
        if (atk == true)
        {
            StartCoroutine(MoveCoroutine(firstTarget));
        }
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
        sundManager.PlaySfx(moveClip);
        anim.SetBool("IsWalking", true);
        
        Vector3 startPos = transform.position;
        Vector3Int debut = grid.GetClosestHex(startPos);
        Vector3Int fin = grid.GetClosestHex(endpos);
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
            anim.SetBool("IsWalking", false);
        }
        gameObject.GetComponent<Chara>().HexEffect();
    }

    internal void MoveThroughPathE(List<Vector3> currentPath,int mov)
    {
        int movement = mov;
        pathPos = new Queue<Vector3>(currentPath);
        Vector3 firstTarget = pathPos.Dequeue();
        StartCoroutine(MoveCoroutineS(firstTarget,movement));

    }
    private IEnumerator MoveCoroutineS(Vector3 endpos,int mov)
    {
        sundManager.PlaySfx(moveClip);
        anim.SetBool("IsWalking", true);
        Vector3 startPos = transform.position;
        Vector3Int debut = grid.GetClosestHex(startPos);
        Vector3Int fin = grid.GetClosestHex(endpos);
        endpos.z = startPos.z;
        float timeSince = 0;
        while (timeSince < movDur)
        {
            timeSince += Time.deltaTime;
            float lerpStep = timeSince / movDur;
            transform.position = Vector3.Lerp(startPos, endpos, lerpStep);
            yield return null;
        }
        mov--;
        transform.position = endpos;
        if (pathPos.Count > 1 && mov>0)
        {
            
            StartCoroutine(MoveCoroutineS(pathPos.Dequeue(),mov));
        }
        else
        {
            MovementFinished?.Invoke(this);
            anim.SetBool("IsWalking", false);
            List<Vector3Int> a2 = grid.GetNeighbours(grid.GetClosestHex(fin));
            Chara[] a = FindObjectsOfType<Chara>();
            foreach (Chara non in a)
            {
                foreach (Vector3Int vec in a2)
                {

                    if (non.GetComponent<Chara>().Classe1 == Chara.Classe.Archer && vec == grid.GetClosestHex(non.transform.position))
                    {
                        non.GetComponent<Chara>().ArcherCac();
                    }
                }

            }
            grid.GetTileAt(fin).SetIsOccupied(true);
        }
        grid.GetTileAt(debut).SetIsOccupied(false);
        gameObject.GetComponent<Chara>().HexEffect();
        
    }

    public bool HasMoved()
    {
        return hasMoved;
    }

    public void SetHasMoved(bool hasMoved)
    {
        this.hasMoved = hasMoved;
        
    }
}
