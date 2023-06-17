using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class Chara : MonoBehaviour
{
    [SerializeField]private int _rangeMax;
    [SerializeField] private int _rangeMin;
    [SerializeField] private int _health;
    //[SerializeField] private int _ultCharge;
    [SerializeField] private int _currentHealth;
    //[SerializeField] private int _currenUlt;
    [SerializeField] private int _dmg;
    [SerializeField] private int _mov;
    [SerializeField] private int _prio;
    [SerializeField]private bool _canAtk;
    [SerializeField] private bool _canBeAtkAtRange;
    [SerializeField] bool inForest;
    [SerializeField] bool inWater;
     public List<GameObject> sprite;
    [SerializeField] Sprite prj;
    [SerializeField]private bool _allied;
    public bool _isUltOn { get; private set; }
    [SerializeField] private Classe _classe;
    [SerializeField] private Hex currentPos;
     HexGrid grid;
    [SerializeField] private List<Types> types;
    public Animator anim;
    private bool dead=false;
    private int killed = 0;
    public int Prio { get => _prio;}

    public Classe Classe1
    {
        get => _classe; set
        {
            _classe = value;
            GetInfo();
            Recreate();
        }
    }

    public bool canAtk { get => _canAtk;  }
    public int Mov
    {
        get => _mov; set
        {
            _mov = value;
        }
    }

    public int RangeMax { get => _rangeMax; set => _rangeMax = value; }
    public int RangeMin { get => _rangeMin; set => _rangeMin = value; }
    public bool Dead { get => dead; }
    public bool Allied { get => _allied; }
    public Sprite Prj { get => prj; set => prj = value; }
    public int Killed { get => killed; set => killed = value; }
    public bool InForest { get => inForest; set => inForest = value; }
    public bool InWater { get => inWater; set => inWater = value; }
    public int Health { get => _health; }

    internal int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public enum Classe
    {
        Archer,
        Tank,
        Warrior,
        Oni,
        Kappa,
        Undead
    }
    private void Start()
    {
        grid = FindObjectOfType<HexGrid>();
        GetInfo();
        Recreate();
    }
    public Chara(Classe classe, bool allied, int killed = 0)
    {
        int i = ((int)classe);
        _rangeMax = types[i]._rangeMax;
        _rangeMin = types[i]._rangeMin;
        _health = types[i]._health;
        _dmg = types[i]._dmg;
        _mov = types[i]._mov;
        _prio = types[i]._prio;
        //_ultCharge = types[i]._ultCharge;
        _currentHealth = _health;
        //_currenUlt=0;
        _isUltOn = false;
        this.killed = killed;
    }
    public void CannotAtk()
    {
        _canAtk = false;
    }
    public void CanAtk()
    {
        _canAtk = true;
    }
    public void AddRange(int added)
    {
        _rangeMax += added;
    }
    public void RemoveRange(int reduced)
    {
        _rangeMax -= reduced;
    }
    public void AddHealth(int added)
    {
        _health += added;
    }
    public void RemoveHealth(int reduced)
    {
        _health -= reduced;
        if (_currentHealth > _health)
        {
            _currentHealth = _health;
        }
    }
    public void AddDmg(int added)
    {
        _dmg += added;
    }
    public void Removedmg(int reduced)
    {
        _dmg -= reduced;
    }
    public void AddMov(int added)
    {
        _mov += added;
    }
    public void RemoveMov(int reduced)
    {
        _mov -= reduced;
        if (_mov < 1)
        {
            _mov = 1;
        }
    }
    public void TakeDmg(int dmg)
    {
        if (dmg > _currentHealth)
        {
            GetComponentInChildren<HealthBar>().OnDamage(_currentHealth);
            FindObjectOfType<ActionBar>().SetDead(this);
        }
        else
        {
            GetComponentInChildren<HealthBar>().OnDamage(dmg);
        }
        _currentHealth -= dmg;
        
        if (_currentHealth <= 0)
        {
            StartCoroutine(Death());
        }
        anim.SetTrigger("IsTakingDamage");
    }
    IEnumerator Death()
    {
        anim.SetBool("IsAlive", false);
        Vector3Int a = grid.GetClosestHex(transform.position);
        grid.GetTileAt(a).SetIsOccupied(false);
        _canAtk = false;
        yield return new WaitForSeconds(1.73f);
        dead = true;
        
        if (!_allied)
        {
            if (inWater)
            {
               // Achievement.HandleAchievemen("CgkIsfzlyYQEEAIQDA");
            }
            //Achievement.HandleAchievemen("CgkIsfzlyYQEEAIQAQ");
        }
        Destroy(gameObject);
    }
    
  
    public void Heal(int heal)
    {
        _currentHealth += heal;
        if (_currentHealth > _health)
        {
            _currentHealth = _health;
        }
    }
    /*public void Ult()
    {
        switch (_classe)
        {
            case Classe.Archer:
                break;
            case Classe.Tank:
                break;
            case Classe.Warrior:
                break;
        }
    }*/
    public void Attack(Chara enemy)
    {
        if (enemy.transform.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        List<Vector3> ah = new List<Vector3>();
        List<Vector3> bh = new List<Vector3>();
        if (_classe == Classe.Tank || _classe==Classe.Oni)
        {
            
            bool pushed = true;
            Vector3 push = enemy.transform.position - transform.position;
            if (grid.GetTileAtClosestHex(enemy.transform.position + push) != null)
            {
                if (grid.GetTileAtClosestHex(enemy.transform.position + push).hexType == Hex.HexType.Obstacle)
                {

                    pushed = false;
                    enemy.TakeDmg(1);
                }
                else
                {
                    Chara[] enemies = FindObjectsOfType<Chara>();
                    foreach (Chara enemie in enemies)
                    {
                        if (grid.GetClosestHex(enemie.transform.position) == grid.GetClosestHex(enemy.transform.position + push))
                        {
                            enemy.TakeDmg(1);
                            enemie.TakeDmg(1);
                            if (!enemie.Allied && enemy._currentHealth <= 0 && enemie._currentHealth <= 0)
                            {
                                Achievement.HandleAchievemen("CgkIsfzlyYQEEAIQDQ");
                            }
                            pushed = false;
                        }

                    }
                }
                if (pushed == true)
                {
                    Vector3Int currentHexCoord = grid.GetClosestHex(transform.position);
                    Hex currentHex = grid.GetTileAt(currentHexCoord);
                    currentHex.SetIsOccupied(false);
                    Vector3Int currentHexCoordE = grid.GetClosestHex(enemy.transform.position);
                    Hex currentHexE = grid.GetTileAt(currentHexCoordE);
                    currentHexE.SetIsOccupied(false);
                    bh.Add(grid.GetTileAtClosestHex(enemy.transform.position + push).transform.position);
                    ah.Add(grid.GetTileAtClosestHex(transform.position + push).transform.position);
                    if (_allied == true)
                    {
                        currentHexE.SetIsOccupied(true);
                    }
                    else
                    {
                        currentHex.SetIsOccupied(true);
                    }
                }
                else
                {
                    ah.Add(transform.position + push/ 2);
                    ah.Add(transform.position);
                    bh.Add(enemy.transform.position + push / 2);
                    bh.Add(enemy.transform.position);
                }
                GetComponent<Unit>().MoveThroughPath(ah,true);
                enemy.GetComponent<Unit>().MoveThroughPath(bh,true);
            }
            enemy.TakeDmg(1);
        }
        else
        {
            if (_classe == Classe.Warrior || _classe == Classe.Undead)
            {
                ah.Add(transform.position+( enemy.transform.position-transform.position)/2);
                ah.Add(transform.position);
            }
            if (ah.Count > 0)
            {
                Debug.Log("12354");
                GetComponent<Unit>().MoveThroughPath(ah,true);
            }
            enemy.TakeDmg(_dmg);
            
        }
        anim.SetTrigger("IsAttacking");
        enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, -0.5f);
    }
    internal List<Chara> CheckInRange()
    {
        List<Chara> charaInRange = new List<Chara>();
        Chara[] chara= FindObjectsOfType<Chara>();
        for(int i =0;i<chara.Length; i++)
        {
           
            Vector3Int posEnemy = grid.GetClosestHex(chara[i].gameObject.transform.position);
            BFSResult bfs = GraphSearch.BFSGetAttack(grid, grid.GetClosestHex(transform.position), _rangeMax);
            BFSResult bfsNot = GraphSearch.BFSGetAttack(grid, grid.GetClosestHex(transform.position), _rangeMin - 1);
            if (_classe == Classe.Archer || _classe == Classe.Kappa)
            {
                bfs = GraphSearch.BFSGetAttackRanged(grid, grid.GetClosestHex(transform.position), _rangeMax);
                bfsNot = GraphSearch.BFSGetAttackRanged(grid, grid.GetClosestHex(transform.position), _rangeMin - 1);
               

            }
            if (chara[i]!=null&&chara[i]._allied != this._allied)
            {
                foreach (Vector3Int pos in bfs.GetRangePos())
                {
                    if (posEnemy == pos&& !bfsNot.visitedNodeD.ContainsKey(posEnemy))
                    {                         
                        charaInRange.Add(chara[i]);
                        break;
                    }
                }
            }
        }
        return charaInRange;
    }

    [System.Serializable]
    public class Types
    {
        public string _name;
        public int _rangeMax;
        public int _rangeMin;
        public int _health;
        //public int _ultCharge;
        public int _dmg;
        public int _mov;
        public int _prio;
        public bool _allied;
    }
    public void BonusRiverON()
    {
        if (Classe1 == Classe.Kappa)
        {
            RangeMax += 1;
        }
        else
        {
            _canAtk = false;
            FindObjectOfType<ActionBar>().SetNAtkActive(this, !canAtk);
        }
        inWater = true;
    }
    public void BonusRiverOff()
    {
        if (Classe1 == Classe.Kappa)
        {
            RangeMax -= 1;
        }
        else
        {
            _canAtk = true;
            FindObjectOfType<ActionBar>().SetNAtkActive(this, !canAtk);
        }
        inWater = false;
    }
    public void BonusForestON()
    {

        if (Classe1 == Classe.Archer || Classe1 == Classe.Kappa)
        {
            _canAtk = false;
            FindObjectOfType<ActionBar>().SetNAtkActive(this, !canAtk);
        }
        _canBeAtkAtRange = false;
        RemoveMov(1);
        inForest = true;
    }
    public void BonusForestOff()
    {
        if (Classe1 == Classe.Archer || Classe1 == Classe.Kappa)
        {
            _canAtk = true;
            FindObjectOfType<ActionBar>().SetNAtkActive(this, !canAtk);
        }
        _canBeAtkAtRange = true;
        AddMov(1);
        inForest = false;
    }
    public void HexEffect()
    {
        Vector3Int currentHexCoord = grid.GetClosestHex(transform.position);
        Hex currentHex = grid.GetTileAt(currentHexCoord);
        switch (currentHex.hexType)
        {
            case Hex.HexType.Default:
                if (inForest)
                {
                    BonusForestOff();
                }
                if (inWater)
                {
                    BonusRiverOff();
                }
                break;
            case Hex.HexType.River:
                if (!inWater)
                {
                    BonusRiverON();
                }
                if (inForest)
                {
                    BonusForestOff();
                }
                break;
            case Hex.HexType.Forest:
                if (inWater)
                {
                    BonusRiverOff();
                }
                if (!inForest)
                { 
                    BonusForestON();
                }
                break;
        }
        /*if (_classe == Classe.Oni)
        {
            List<Vector3Int> neighs= grid.GetNeighbours(grid.GetClosestHex(transform.position));
            foreach(Vector3Int neigh in neighs)
            {
                Vector3Int actualNeigh = grid.GetClosestHex(currentHex.transform.position) + neigh;
                if (grid.hexTileD.ContainsKey(actualNeigh))
                {
                    Hex neighHex = grid.GetTileAt(actualNeigh);
                    if (neighHex.hexType == Hex.HexType.Obstacle)
                    {
                        break;
                    }
                }                
            }
        }*/
    }
    public void Recreate()
    {
        switch (_classe)
        {
            case Classe.Archer:
                foreach (GameObject spr in sprite)
                {
                    spr.SetActive(false);
                }
                anim = sprite[0].GetComponent<Animator>();
                sprite[0].SetActive(true);
                break;
            case Classe.Warrior:
                foreach (GameObject spr in sprite)
                {
                    spr.SetActive(false);
                }
                anim = sprite[1].GetComponent<Animator>();
                sprite[1].SetActive(true);
                break;
            case Classe.Tank:
                foreach (GameObject spr in sprite)
                {
                    spr.SetActive(false);
                }
                anim = sprite[2].GetComponent<Animator>();
                sprite[2].SetActive(true);
                break;
            case Classe.Kappa:
                foreach (GameObject spr in sprite)
                {
                    spr.SetActive(false);
                }
                anim = sprite[0].GetComponent<Animator>();
                sprite[0].SetActive(true);
                break;
            case Classe.Undead:
                foreach (GameObject spr in sprite)
                {
                    spr.SetActive(false);
                }
                anim = sprite[1].GetComponent<Animator>();
                sprite[1].SetActive(true);
                break;
            case Classe.Oni:
                foreach (GameObject spr in sprite)
                {
                    spr.SetActive(false);
                }
                anim = sprite[2].GetComponent<Animator>();
                sprite[2].SetActive(true);
                break;
        }
        int i = ((int)_classe);
        _rangeMax = types[i]._rangeMax;
        _rangeMin = types[i]._rangeMin;
        _health = types[i]._health;
        _dmg = types[i]._dmg;
        _mov = types[i]._mov;
        _prio = types[i]._prio;
        //_ultCharge = types[i]._ultCharge;
        _currentHealth = _health;
        //_currenUlt = 0;
        //_isUltOn = false;
        _allied = types[i]._allied;
        _canAtk = true;
        _canBeAtkAtRange = true;
    }
    public void GetInfo()
    {
        if (sprite.Count != 0)
        {
            sprite.Clear();
        }
        int i = 0;
        foreach (Transform child in transform)
        {
            if (i == 0)
            {
                i++;
                continue;
            }
            sprite.Add(child.gameObject);
            i++;
        }
    }
    public void ArcherCac()
    {
        if (_canAtk)
        {
            _canAtk = false;
            _mov += 1;
        }
        /*Achievement.HandleAchievemen("CgkIsfzlyYQEEAIQCw");*/
    }
    public void ArcherCacResolve()
    {
        if (!_canAtk)
        {
            if(!inWater && !inForest)
                _canAtk = true;
            if (!inForest)
                _mov -= 1;

        }
    }
    public Animator GetAnim()
    {
        return anim;
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(Chara))]
public class CharaEdit : Editor
{
    public override void OnInspectorGUI()
    {
        var chara = (Chara)target;
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();
        if (EditorGUI.EndChangeCheck())
        {
            chara.GetInfo();
            chara.Recreate();
        }
    }
}
#endif