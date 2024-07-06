using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Line : MonoBehaviour
{    
    public bool isCollect = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_PLAYER) && !isCollect)
        {
            Player player = other.GetComponent<Player>();
            if (LevelManager.Instance.totalBrick > 0)
            {
                isCollect = true;
                Instantiate(LevelManager.Instance.BrickFill, this.transform.position, Quaternion.Euler(-90f, 0, 0));
                player.RemoveBrick();
                if (LevelManager.Instance.totalBrick <= 0)
                {
                    player.MovePoint = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
                }
            }
        }
    }

}
