using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    public static NavigationController Instance { get; private set; }
    public static bool IsMoving { get; private set; }

    public GameObject playerPoint;

    List<WorldMapPoint> _wmPoints = new();


    private int _idCurrent = 0;
    private int _idTemp = 0;
    private int _idTarget = 0;

    private WorldMapPoint _wmpCurrent;
    private WorldMapPoint _wmpTemp;
    private WorldMapPoint _wmpTarget;

    private float _timerMove;
    private int _nbStep;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _wmPoints.AddRange(FindObjectsOfType<WorldMapPoint>());
    }

    private void Update()
    {
        IsMoving = _idCurrent != _idTarget;

        if (IsMoving)
        {
            if(_idCurrent == _idTemp)
            {
                _idTemp = _idCurrent < _idTarget ? _idTemp + 1 : _idTemp - 1;

                RefreshPoint();
                playerPoint.transform.position = _wmpCurrent.transform.position;
            }

            playerPoint.transform.position = Vector2.Lerp(_wmpCurrent.transform.position, _wmpTemp.transform.position, _timerMove);
            _timerMove += Time.deltaTime * _nbStep;

            if (Vector2.Distance(playerPoint.transform.position, _wmpTemp.transform.position) < 0.1f)
            {
                playerPoint.transform.position = _wmpTemp.transform.position;
                Debug.Log("azezr");
                _idCurrent = _idTemp;
                _timerMove = 0;

                if (_idCurrent == _idTarget)
                    _wmPoints.Find(x => x.ID == _idTarget).panelInfo.SetActive(true);
            }

        }

    }

    public void SetTargetPoint(WorldMapPoint wmpTarget)
    {
        _idTarget = wmpTarget.ID;
        _nbStep = Mathf.Abs(_idCurrent - _idTarget);
    }
    private void RefreshPoint()
    {
        _wmpCurrent = _wmPoints.Find(x => x.ID == _idCurrent);
        _wmpTemp = _wmPoints.Find(x => x.ID == _idTemp);
        //_wmpTarget = _wmPoints.Find(x => x.ID == _idTarget);
    }
}
