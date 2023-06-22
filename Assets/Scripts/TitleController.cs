using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void GoNextScene()
    {
        LoadSceneManager.Instance.LoadSceneAsync(Scene.Game);   //Singleton
    }
}
