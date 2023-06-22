using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool<T> where T : class
{
    Queue<T> m_dataQueue = new Queue<T>();
    Func<T> m_createFunc;
    int m_count = 0;
    
    public GameObjectPool(int count, Func<T> func)
    {
        m_count = count;
        m_createFunc = func;
        Allocation();
    }
    public T Get()
    {
        if (m_dataQueue.Count > 0)
            return m_dataQueue.Dequeue();
        return m_createFunc();
    }
    public void Set(T data)
    {
        m_dataQueue.Enqueue(data);
    }
    void Allocation()
    {
        for (int i = 0; i < m_count; i++)
        {
            m_dataQueue.Enqueue(m_createFunc());
        }
    }
    
}