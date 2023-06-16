using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject posD;
    [SerializeField] private GameObject posF;
    [SerializeField] private GameObject healh;

    private Chara chara;
    List<GameObject> healthPints= new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        chara = GetComponentInParent<Transform>().GetComponentInParent<Chara>();
        for (int i = 0; i < chara.Health; i++)
        {
            Debug.Log("ahh");
            Vector3 ps = Vector3.Lerp(posD.transform.position, posF.transform.position, (float)i / (float)chara.Health);
            GameObject bar = Instantiate(healh, ps, transform.rotation,transform);
            bar.transform.localScale = new Vector3(bar.transform.localScale.x/(chara.Health+0.4f), bar.transform.localScale.y, bar.transform.localScale.z);
            healthPints.Add(bar);
        }
    }
    // Update is called once per frame
    public void OnDamage(int damage)
    {
        for(int i = 0; i < damage; i++)
        {
            GameObject a = healthPints[healthPints.Count - 1];
            healthPints.RemoveAt(healthPints.Count - 1);
            Destroy(a);
        }
    }
}
