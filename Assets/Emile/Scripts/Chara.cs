using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Chara : MonoBehaviour
{
    [SerializeField]private int _rangeMax;
    [SerializeField] private int _rangeMin;
    [SerializeField] private int _health;
    [SerializeField] private int _ultCharge;
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _currenUlt;
    [SerializeField] private int _dmg;
    [SerializeField] private int _mov;
    [SerializeField] private int _prio;
    private bool _allied;
    public bool _isUltOn { get; private set; }
    private Sprite sprite;
    [SerializeField] private Classe _classe;
    private Hex currentPos;
    [SerializeField] HexGrid grid;
    [SerializeField] private List<Types> types;
    public int Mov { get => _mov;}
    public int Prio { get => _prio;}
    public Classe Classe1 { get => _classe; }

    public enum Classe
    {
        Archer,
        Tank,
        Warrior,
        Oni,
        Kappa,
        Undead
    }
    private void Awake()
    {
        int i = ((int)_classe);
        _rangeMax = types[i]._rangeMax;
        _rangeMin = types[i]._rangeMin;
        _health = types[i]._health;
        _dmg = types[i]._dmg;
        _mov = types[i]._mov;
        _prio = types[i]._prio;
        _ultCharge = types[i]._ultCharge;
        _currentHealth = _health;
        _currenUlt = 0;
        _isUltOn = false;
        _allied = types[i]._allied;
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
        _ultCharge = types[i]._ultCharge;
        _currentHealth = _health;
        _currenUlt=0;
        _isUltOn = false;
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
    public void Ult()
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
    }
    public void Attack(Chara enemy)
    {
        if (_classe == Classe.Tank)
        {
            enemy.TakeDmg(_dmg);
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
            if (chara[i]!=null&&chara[i]._allied != this._allied)
            {
                Vector3Int posEnemy = grid.GetClosestHex(chara[i].gameObject.transform.position);
                BFSResult bfs= GraphSearch.BFSGetAttack(grid, grid.GetClosestHex(transform.position), _rangeMax);
                if (_rangeMin > 1)
                {
                    BFSResult bfsNot = GraphSearch.BFSGetAttack(grid, grid.GetClosestHex(transform.position), _rangeMin);
                    foreach (Vector3Int pos in bfs.GetRangePos())
                    {
                        if (posEnemy == pos&& !bfsNot.visitedNodeD.ContainsKey(posEnemy))
                        {
                            charaInRange.Add(chara[i]);
                            break;
                        }
                    }
                }
                else
                {
                    foreach (Vector3Int pos in bfs.GetRangePos())
                    {
                        if (posEnemy == pos)
                        {
                            charaInRange.Add(chara[i]);
                            break;
                        }
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
        public int _ultCharge;
        public int _dmg;
        public int _mov;
        public int _prio;
        public bool _allied;
    }
}


