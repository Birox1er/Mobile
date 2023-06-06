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
     public List<GameObject> sprite;
    private bool _allied;
    public bool _isUltOn { get; private set; }
    [SerializeField] private Classe _classe;
    private Hex currentPos;
     HexGrid grid;
    [SerializeField] private List<Types> types;
    public int Mov { get => _mov;}
    public int Prio { get => _prio;}
    public Classe Classe1 { get => _classe; }

    public bool canAtk { get => _canAtk;  }

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
    }
    public Chara(Classe classe,bool allied)
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
        _currentHealth -= dmg;
        if (_currentHealth <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        grid.GetTileAtClosestHex(transform.position).SetIsOccupied(false);
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
                            pushed = false;
                        }

                    }
                }
                if (pushed == true)
                {
                    grid.GetTileAtClosestHex(transform.position).SetIsOccupied(false);
                    grid.GetTileAtClosestHex(enemy.transform.position).SetIsOccupied(false);
                    enemy.transform.position = grid.GetTileAtClosestHex(enemy.transform.position + push).transform.position;
                    transform.position = grid.GetTileAtClosestHex(transform.position + push).transform.position;
                    if (_allied == true)
                    {
                        grid.GetTileAtClosestHex(enemy.transform.position).SetIsOccupied(true);
                    }
                    else
                    {   
                        grid.GetTileAtClosestHex(transform.position).SetIsOccupied(true);
                    }
                }
            }
            enemy.TakeDmg(1);
        }
        else
        {
            enemy.TakeDmg(_dmg);
        }
    }
    internal List<Chara> CheckInRange()
    {
        List<Chara> charaInRange = new List<Chara>();
        Chara[] chara= FindObjectsOfType<Chara>();
        for(int i =0;i<chara.Length; i++)
        {
            Vector3Int posEnemy = grid.GetClosestHex(chara[i].gameObject.transform.position);
            BFSResult bfs = GraphSearch.BFSGetAttack(grid, grid.GetClosestHex(transform.position), _rangeMax);
            BFSResult bfsNot = GraphSearch.BFSGetAttack(grid, grid.GetClosestHex(transform.position), _rangeMin-1);
            if (chara[i]!=null&&chara[i]._allied != this._allied)
            { 
                foreach (Vector3Int pos in bfs.GetRangePos())
                {
                    if (posEnemy == pos&& !bfsNot.visitedNodeD.ContainsKey(posEnemy))
                    {
                        if ((Classe1 == Classe.Archer || Classe1 == Classe.Kappa) && !chara[i]._canBeAtkAtRange)
                        {
                            continue;
                        }
                        charaInRange.Add(chara[i]);
                        break;
                    }
                }
            }
        }
        Debug.Log(charaInRange.Count);
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

        }
        else
        {
            _canAtk = false;
        }
    }
    public void BonusRiverOff()
    {
        if (Classe1 == Classe.Kappa)
        {

        }
        else
        {
            _canAtk = true;
        }
    }
    public void BonusForestON()
    {
        if (Classe1 == Classe.Archer || Classe1 == Classe.Kappa)
        {
            _canAtk = false;
        }
        _canBeAtkAtRange = false;
        RemoveMov(1);
    }
    public void BonusForestOff()
    {
        if (Classe1 == Classe.Archer || Classe1 == Classe.Kappa)
        {
            _canAtk = true;
        }
        _canBeAtkAtRange = true;
        AddMov(1);
    }
    public void HexEffect()
    {
        Hex currentHex = grid.GetTileAtClosestHex(transform.position);
        switch (currentHex.hexType)
        {
            case Hex.HexType.Default:
                BonusForestOff();
                BonusRiverOff();
                break;
            case Hex.HexType.River:
                BonusRiverON();
                BonusForestOff();
                break;
            case Hex.HexType.Forest:
                BonusRiverOff();
                BonusForestON();
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
                sprite[0].SetActive(true);
                break;
            case Classe.Warrior:
                foreach (GameObject spr in sprite)
                {
                    spr.SetActive(false);
                }
                sprite[1].SetActive(true);
                break;
            case Classe.Tank:
                break;
            case Classe.Kappa:
                foreach (GameObject spr in sprite)
                {
                    spr.SetActive(false);
                }
                sprite[0].SetActive(true);
                break;
            case Classe.Undead:
                foreach (GameObject spr in sprite)
                {
                    spr.SetActive(false);
                }
                sprite[1].SetActive(true);
                break;
            case Classe.Oni:
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
        foreach (Transform child in transform)
        {
            sprite.Add(child.gameObject);
        }
    }
}
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
            if (chara.sprite.Count == 0)
            {
                chara.GetInfo();
            }
            chara.Recreate();
        }
    }
}


