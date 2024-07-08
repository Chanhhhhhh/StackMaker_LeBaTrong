using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _particles;
    [SerializeField] private List<GameObject> Treasure;
    public Transform StartPoint;

    public void WinGame()
    {
        foreach (var particle in _particles)
        {
            particle.Play();
        }
        Treasure[0].SetActive(false);
        Treasure[1].SetActive(true);

    }
}
