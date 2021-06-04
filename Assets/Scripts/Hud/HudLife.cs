using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudLife : MonoBehaviour
{
    public static HudLife instance;
    [SerializeField]
    private List<GameObject> _listlife;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);

    }

    public void RefreshLife(int value)
    {
        for(int i = 1; i <= _listlife.Count ; i++)
        {
            if (i <= value)
            {
                _listlife[i - 1].SetActive(true);
            } else
            {
                _listlife[i - 1].SetActive(false);
            }
        }
        
    }
}
