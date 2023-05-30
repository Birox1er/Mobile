using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chara : MonoBehaviour
{
    [SerializeField] private int _rangeMax;
    [SerializeField] private int _rangeMin;
    [SerializeField] private int _health;
    private int _ultCharge;
    private int _currentHealth;
    private int _currenUlt;
    [SerializeField] private int _dmg;
    [SerializeField] private int _mov;
    [SerializeField] private int _prio;
    [SerializeField] private bool _allied;
    [SerializeField] private bool _isUltOn;
    private Sprite sprite;
    [SerializeField] private Classe _classe;
    private Hex currentPos;
    [SerializeField] HexGrid grid;

    public int Mov { get => _mov;}
    public int Prio { get => _prio;}
    private Classe Classe1 { get => _classe; }

    public enum Classe
    {
        Archer,
        Tank,
        Warrior
    }
    private void Update()
    {
        currentPos = grid.GetTileAt(gameObject.GetComponent<HexCoord>().GetHexCoord());
        Debug.Log(currentPos);
    }
    private void Awake()
    {
        switch (Classe1)
        {
            case Classe.Archer:
                _rangeMax = 3;
                _rangeMin = 2;
                _health = 1;
                _dmg = 2;
                _mov = 1;
                _prio = 3;
                _ultCharge = 3;
                //sprite;
                break;
            case Classe.Tank:
                _rangeMax = 1;
                _rangeMin = 1;
                _health = 4;
                _dmg = 1;
                _mov = 2;
                _prio = 2;
                _ultCharge = 5;
                //sprite;
                break;
            case Classe.Warrior:
                _rangeMax = 1;
                _rangeMin = 1;
                _health = 2;
                _dmg = 3;
                _mov = 3;
                _prio = 1;
                _ultCharge = 2;
                //sprite;
                break;
            default:
                _classe = Classe.Archer;
                _rangeMax = 3;
                _rangeMin = 2;
                _health = 1;
                _dmg = 2;
                _mov = 1;
                _prio = 3;
                _ultCharge = 2;
                //sprite;
                break;
        }
        _currentHealth = _health;
        _currenUlt = 0;
        _isUltOn = false;
    }
    public Chara(Classe classe,bool allied)
    {
        switch (classe)
        {
            case Classe.Archer:
                _rangeMax = 3;
                _rangeMin = 2;
                _health = 1;
                _dmg = 2;
                _mov = 1;
                _prio = 3;
                _ultCharge = 3;
                //sprite;
                break;
            case Classe.Tank:
                _rangeMax = 1;
                _rangeMin = 1;
                _health = 4;
                _dmg = 1;
                _mov = 2;
                _prio = 2;
                _ultCharge = 5;
                //sprite;
                break;
            case Classe.Warrior:
                _rangeMax = 1;
                _rangeMin = 1;
                _health = 2;
                _dmg = 3;
                _mov = 3;
                _prio = 1;
                _ultCharge = 2;
                //sprite;
                break;
            default:
                _classe = Classe.Archer;
                _rangeMax = 3;
                _rangeMin = 2;
                _health = 1;
                _dmg = 2;
                _mov = 1;
                _prio = 3;
                _ultCharge = 2;
                //sprite;
                break;
        }
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

        }
        else
        {
            enemy.TakeDmg(_dmg);
        }
    }
}


