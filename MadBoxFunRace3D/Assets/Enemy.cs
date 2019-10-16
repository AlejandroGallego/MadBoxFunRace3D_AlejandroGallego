using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { rotate, lineal}
    public EnemyType enemyType;

    public Transform piece;
    public float currentTime;

    // Update is called once per frame
    void Update()
    {
        if(enemyType == EnemyType.lineal)
        {
            currentTime += Time.deltaTime;
            if(currentTime <= 1) piece.localPosition += new Vector3(1,0,0) * Time.deltaTime;
            else if(currentTime <= 2) piece.localPosition -= new Vector3(1, 0, 0) * Time.deltaTime;
            else
            {
                currentTime = 0;
                piece.localPosition = Vector3.zero;
            }

        }
        else
        {
            piece.Rotate(0, 90 * Time.deltaTime, 0);
        }
    }
}
