using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class CanvasItem : UICanvas
{
    [SerializeField] private TextMeshProUGUI Coin;
    [SerializeField] private TextMeshProUGUI Diamond;
    [SerializeField] private Transform targetDiamond;
    [SerializeField] private Transform targetCoin;
    public override void Setup()
    {
        base.Setup();
        Coin.text = SaveManager.Instance.Coin.ToString();
        Diamond.text = SaveManager.Instance.Diamond.ToString();

    }

    public void EffectItem(Vector3 pos, PoolType poolType, int amount)
    {
        for(int i= 0; i<amount; i++)
        {
            ItemEffect itemUI = SimplePool.Spawn<ItemEffect>(poolType);
            itemUI.TF.SetParent(this.transform);
            itemUI.TF.position = Cache.MainCamera.WorldToScreenPoint(pos) + new Vector3(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5), 0);
            Move(itemUI, poolType, false);
        }
    }

    public void EffectBonus(Vector3 pos, PoolType poolType, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            ItemEffect itemUI = SimplePool.Spawn<ItemEffect>(poolType);
            itemUI.TF.SetParent(this.transform);
            Vector2 posrect = RectTransformUtility.WorldToScreenPoint(Cache.MainCamera, pos);
            itemUI.TF.GetComponent<RectTransform>().position = (Vector3)posrect + new Vector3(UnityEngine.Random.Range(-80f, 80f), UnityEngine.Random.Range(-80f, 80f), 0);
            bool isLast = i == amount - 1;
            Move(itemUI, poolType,isLast);
        }
    }

    public void Move(ItemEffect itemUI, PoolType poolType, bool isLast)
    {
        Tween tween = null;
        switch (poolType)
        {
            case PoolType.DiamondUI:

                tween = itemUI.transform.DOMove(targetDiamond.transform.position, UnityEngine.Random.Range(0.8f, 1.2f)).SetEase(Ease.InBack).OnComplete(() =>
                {
                    Diamond.text = (++SaveManager.Instance.Diamond).ToString();
                    itemUI.OnDespawn();
                });
                break;
            case PoolType.CoinUI:
                
                tween = itemUI.transform.DOMove(targetCoin.transform.position, UnityEngine.Random.Range(0.8f, 1.2f)).SetEase(Ease.InBack).OnComplete(() =>
                {
                    Coin.text = (++SaveManager.Instance.Coin).ToString();
                    itemUI.OnDespawn();
                });
                break;
            default: break;

        }
        if(tween != null && isLast)
        {
            tween.OnComplete(() =>
            {
                itemUI.OnDespawn();
                UIManager.Instance.CloseUI<CanvasTreasure>(1.5f);
                UIManager.Instance.OpenUI<CanvasFinish>(1.7f);
            });
        }
    }


}
