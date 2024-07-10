using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Line : GameUnit
{
    public bool isCollect = false;
    public override void OnInit()
    {
        isCollect = false;
    }

    public override void OnDespawn()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_PLAYER) && !isCollect)
        {
            Player player = other.GetComponent<Player>();
            if (LevelManager.Instance.totalBrick > 0)
            {
                isCollect = true;
                SimplePool.Spawn<BrickFill>(PoolType.BrickFill, this.transform.position, Quaternion.Euler(-90f, 0, 0));
                player.RemoveBrick();
                if (LevelManager.Instance.totalBrick <= 0)
                {
                    player.MovePoint = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
                }
            }
        }
    }

}
