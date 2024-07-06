using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Player player;
    public GameObject BrickFill;
    public int totalBrick = 0;

    // Start is called before the first frame update
}
