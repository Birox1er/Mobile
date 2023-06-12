using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Projectile : MonoBehaviour
{
    bool aaa=false;
    Vector3 _cible;
    int speed=10;
    public void Prj(Vector3 cible)
    {
        aaa = true;
        _cible = cible;
    }
    private void Update()
    {
        if (aaa == true)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            transform.up = (_cible - transform.position).normalized;
            transform.position += (_cible - transform.position).normalized * speed * Time.deltaTime;
            Debug.Log(transform.position);
            Debug.Log(_cible);
            if((_cible - transform.position).magnitude < 1)
            {
                Destroy(gameObject);
            }
        }
    }
}