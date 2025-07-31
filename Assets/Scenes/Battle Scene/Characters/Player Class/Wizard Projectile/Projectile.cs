using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool collided;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Projectile" && collision.gameObject.tag != "Player" && !collided)
        {
            Debug.Log(collision.gameObject);
            collided = true;
            Destroy(gameObject);
        }
    }
}
