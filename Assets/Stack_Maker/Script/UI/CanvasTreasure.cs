using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTreasure : UICanvas
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject[] treasures;
    [SerializeField] private GameObject treasure_Open;

    public override void Setup()
    {
        base.Setup();
        this.transform.SetParent(UIManager.Instance.gameObject.transform);
        this.GetComponent<Canvas>().worldCamera = GameManager.Instance.CameraCanvas;
        for (int i = 0; i < buttons.Length; i++)
        {
            int tempindex = i;
            buttons[tempindex].onClick.AddListener(() => Click(tempindex));
        }
    }
    public void Click(int index)
    {
        Sequence sequence = DOTween.Sequence();
        for(int i= 0; i < treasures.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
            if (i != index)
            {
                sequence.Join(treasures[i].transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBack));
            }
        }
        sequence.OnComplete(() =>
        {
            
            Instantiate(treasure_Open,this.transform).transform.localPosition = treasures[index].transform.localPosition;
            treasures[index].gameObject.SetActive(false);
            UIManager.Instance.GetUI<CanvasItem>().EffectBonus(treasures[index].transform.localPosition, PoolType.CoinUI, 5);
        });
    }


}
