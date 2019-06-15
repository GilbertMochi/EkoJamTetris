using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DifficultyVaraibles : MonoBehaviour, INotifyPropertyChanged
{
    private static float _simpleCarCooldown;
    private static float _houseCooldown;

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
