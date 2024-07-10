using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPos : GameUnit
{

    [SerializeField] private ParticleSystem[] _win;
    [SerializeField] private GameObject box_close;
    [SerializeField] private GameObject box_open;

    public Vector3 EndPos => this.transform.position + new Vector3(0, 0, 6);
    public void PlayParticleSystem()
    {

        for (int i = 0; i < _win.Length; i++)
        {
            if (_win[i] != null)
            {
                _win[i].Play();
            }
        }
        box_close.SetActive(false);
        box_open.SetActive(true);
    }

    public override void OnInit()
    {
        
    }

    public override void OnDespawn()
    {
        
    }
}
