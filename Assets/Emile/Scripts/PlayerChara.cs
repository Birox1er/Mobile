using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChara : MonoBehaviour
{
    [SerializeField] private int _rangeMax;
    [SerializeField] private int _rangeMin;
    [SerializeField] private int _health;
    [SerializeField] private int _dmg;
    [SerializeField] private int _mov;
    [SerializeField] private int _prio;
    private Classe _classe;
    enum Classe
    {
        Archer,
        Tank,
        Warrior
    }
    public PlayerChara(int classe)
    {
        switch (classe)
        {
            case 0:
                _classe = Classe.Archer;
                _rangeMax = 3;
                _rangeMin = 2;
                _health = 1;
                _dmg = 2;
                _mov = 1;
                _prio = 3;
                break;
            case 1:
                _classe = Classe.Tank;
                _rangeMax = 1;
                _rangeMin = 1;
                _health = 4;
                _dmg = 1;
                _mov = 2;
                _prio = 2;
                break;
            case 2:
                _classe = Classe.Warrior;
                _rangeMax = 1;
                _rangeMin = 1;
                _health = 2;
                _dmg = 3;
                _mov = 3;
                _prio = 1;
                break;
        }
    }
    public class Archer
    {

    }
}


