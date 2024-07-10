using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Singleton<Generator>
{  
    private List<PoolType> MapObjectss = new List<PoolType>() {PoolType.Wall,  PoolType.BrickPlayer, PoolType.Line, PoolType.UnBrick, PoolType.WinPos };
    private bool IsFisrtRow = true;
    private Vector3 startPos;
    public Vector3 StartPos => startPos;
    
    public void InitMap(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                switch (matrix[i, j])
                {
                    case 0:
                        SpawnObjects(MapObjectss[0], new Vector3(j,0,i));
                        break;
                    case 1:
                        if (IsFisrtRow)
                        {
                            startPos = new Vector3(j,0,i);
                            IsFisrtRow = false;
                        }
                        SpawnObjects(MapObjectss[1], new Vector3(j, 0, i));
                        SpawnObjects(MapObjectss[3], new Vector3(j, 0, i));
                        break;
                    case 2:
                        SpawnObjects(MapObjectss[2], new Vector3(j, 0,i));
                        break;
                    case 3:
                        SpawnObjects(MapObjectss[3], new Vector3(j,0,i));
                        break;
                    case 4:
                        SpawnObjects(MapObjectss[4], new Vector3(j, 0, i));
                        break;
                    default:
                        break;

                }

            }
        }
    }
    public void SpawnObjects (PoolType typeObjects, Vector3 pos)
    {
        GameUnit newObject = SimplePool.Spawn<GameUnit>(typeObjects);
        newObject.transform.localPosition = pos;
        newObject.OnInit();
    }
}
