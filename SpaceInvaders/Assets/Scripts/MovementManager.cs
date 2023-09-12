using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public Transform UnitHolder;
    public bool Started = false;
    public float Side = 1;
    public float InitialSpeed = 1.0f;
    public float SpeedIncrement = 1.0f;
    public LevelData Data;

    private float _speed = 1.0f;
    private bool _movingForward = false;
    private float _targetZ;

    public static MovementManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        _speed = InitialSpeed;
    }

    void Update()
    {
        if (Started)
        {
            if (_movingForward)
            {
                UpdateForwardMovement();
            }
            else
            {
                UpdateSideMovement();
            }
        }
    }

    public void Restart()
    {
        _speed = InitialSpeed;
        Started = false;
        _movingForward = false;
        UnitHolder.position = Vector3.zero;
    }

    public void IncreaseSpeed()
    {
        _speed += SpeedIncrement;
    }

    private void UpdateSideMovement()
    {
        Vector3 pos = UnitHolder.position;
        pos.x += _speed * Time.deltaTime * Side;
        UnitHolder.position = pos;
    }

    private void UpdateForwardMovement()
    {
        if (UnitHolder.position.z > _targetZ)
        {
            Vector3 pos = UnitHolder.position;
            pos.z -= _speed * Time.deltaTime;
            UnitHolder.position = pos;
        }
        else
        {
            _movingForward = false;
        }
    }

    public void StartMoveForward()
    {
        if (!_movingForward)
        {
            _movingForward = true;
            _targetZ = UnitHolder.position.z - Data.SpaceBetweenUnits;
        }
    }
}
