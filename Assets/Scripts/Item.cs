using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
    public ItemData data;
    [HideInInspector] public Rigidbody2D rigidbody2D;
    private void Awake(){
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
}
