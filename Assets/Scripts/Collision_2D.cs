using UnityEngine;
using System.Collections;

public class Collision_2D : MonoBehaviour {

    public void OnCollisionEnter2D(Collision2D col)
    {

        col.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;

    }
}
