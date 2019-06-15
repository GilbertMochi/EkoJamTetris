using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DifficultyVaraibles : MonoBehaviour, INotifyPropertyChanged
{
    private static float _simpleCarCooldown;
    private static float _houseCooldown;
    private static float _truckCooldown;
    private static float _cowCooldown;
    private static float _building1down;
    private static float _factoryCooldown;


    public float SimpleCarSpawnCooldown
    {
        get
        {
            return _simpleCarCooldown;
        }
        set
        {
            _simpleCarCooldown = value;
            RaisePropertyChanged("SimpleCarSpawnCoolDown");
        }
    }

    public float HouseSpawnCooldown
    {
        get
        {
            return _houseCooldown;
        }
        set
        {
            _houseCooldown = value;
            RaisePropertyChanged("HouseSpawnCoolDown");
        }
    }

public float TruckSpawnCooldown
    {
        get
        {
            return _truckCooldown;
        }
        set
        {
            _truckCooldown = value;
            RaisePropertyChanged("TruckSpawnCooldown");
        }
    }

public float CowSpawnCooldown
    {
        get
        {
            return _cowCooldown;
        }
        set
        {
            _cowCooldown = value;
            RaisePropertyChanged("CowSpawnCooldown");
        }
    }

    public float Building1SpawnCooldown
    {
        get
        {
            return _building1down;
        }
        set
        {
            _building1down = value;
            RaisePropertyChanged("Building1SpawnCooldown");
        }
    }

    public float FactorySpawnCooldown
    {
        get
        {
            return _factoryCooldown;
        }
        set
        {
            _factoryCooldown = value;
            RaisePropertyChanged("FactorySpawnCooldown");
        }
    }

public static DifficultyVaraibles DifficultyVariables { get; private set; }

    void Awake()
    {
        if (DifficultyVariables == null)
        {
            DifficultyVariables = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void RaisePropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
