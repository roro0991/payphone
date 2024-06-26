using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] GameObject coin;

    public void GenerateCoin()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            Instantiate
            (coin, worldPosition,
            Quaternion.identity,
            GameObject.FindGameObjectWithTag("Canvas").transform);
    }
}
