using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friutScript : MonoBehaviour
{
    [Header("Scripts")]
    private friutspwner _friutspwner;

    private void Start()
    {
        GameObject friutspwner = GameObject.FindGameObjectWithTag("FriutSpawner");
        _friutspwner=friutspwner.GetComponent<friutspwner>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collide");
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
            _friutspwner.friutSpawner();
        }
    }

}
