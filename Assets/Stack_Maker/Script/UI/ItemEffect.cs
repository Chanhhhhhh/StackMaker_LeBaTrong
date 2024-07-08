using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : GameUnit
{
    public override void OnInit()
    {

    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
