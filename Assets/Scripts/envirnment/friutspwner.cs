using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friutspwner : MonoBehaviour
{
    [SerializeField] private GameObject friut;

    void Start()
    {
        friutSpawner();
    }


    void Update()
    {

    }
  

    public void friutSpawner()
    {
        float x_valuve = Random.Range(24f,-24f);
        float y_valuve = Random.Range(-15.5f, 15.5f);
        Vector3 randomPos=new Vector3(x_valuve,y_valuve,friut.transform.position.z);
        GameObject friut01 = Instantiate(friut, randomPos, Quaternion.identity);
    }
}
