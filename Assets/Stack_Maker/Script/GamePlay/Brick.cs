using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : GameUnit
{
    public override void OnInit()
    {

    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag(Constant.TAG_PLAYER))
        {                       
            other.GetComponent<Player>().AddBrick();
            gameObject.SetActive(false);
        }
    }
}
