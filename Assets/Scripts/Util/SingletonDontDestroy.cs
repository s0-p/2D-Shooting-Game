using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonDontDestroy<T> : MonoBehaviour where T : SingletonDontDestroy<T>
{
    public static T Instance { get; private set; }
    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
            DontDestroyOnLoad(gameObject);
            OnAwake();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == (T)this)
        {
            OnStart();
        }
    }
}

