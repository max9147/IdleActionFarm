using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] private GameObject shopMarker;
    [SerializeField] private GameObject[] shopWaypoints;
    [SerializeField] private GameObject coinSprite;
    [SerializeField] private GameObject coinStartSpot;
    [SerializeField] private GameObject coinEndSpot;
    [SerializeField] private TextMeshProUGUI moneyCounter;
    [SerializeField] private GameSettings gameSettings;

    private float curMoney;
    private GameObject curCoin;
    public List<GameObject> allCoins;
    private Vector3[] waypointsPositions;

    private void Start(){
        allCoins = new List<GameObject>();
        waypointsPositions = new Vector3[shopWaypoints.Length];
        for (int i = 0; i < shopWaypoints.Length; i++)
            waypointsPositions[i] = shopWaypoints[i].transform.position;
    }

    public void SellBlocks(List<GameObject> blocksToSell){
        shopMarker.SetActive(false);
        StartCoroutine(SellWait(blocksToSell));
    }

    private void SellBlock(GameObject grassBlock){
        grassBlock.transform.parent = null;
        grassBlock.transform.localScale *= 2.5f;
        grassBlock.transform.DOPath(waypointsPositions, gameSettings.soldBlockFlyTime, PathType.CatmullRom, PathMode.Full3D, 10, null);
        StartCoroutine(DestroyWait(grassBlock));
    }

    private void GetCoin(){
        curCoin = Instantiate(coinSprite, coinStartSpot.transform.position, Quaternion.identity, coinStartSpot.transform);
        allCoins.Add(curCoin);
        curCoin.transform.DOJump(coinEndSpot.transform.position, -300f, 1, gameSettings.coinFlyTime, false);
        StartCoroutine(MoneyGainWait());
    }

    private IEnumerator SellWait(List<GameObject> blocksToSell){
        SellBlock(blocksToSell[blocksToSell.Count - 1]);
        yield return new WaitForSeconds(gameSettings.delayBetweenSells);
        blocksToSell.RemoveAt(blocksToSell.Count - 1);
        if (blocksToSell.Count > 0)
            StartCoroutine(SellWait(blocksToSell));
    }

    private IEnumerator DestroyWait(GameObject grassBlock){
        yield return new WaitForSeconds(gameSettings.soldBlockFlyTime);
        Destroy(grassBlock);
        GetCoin();
    }

    private IEnumerator MoneyGainWait(){
        yield return new WaitForSeconds(gameSettings.soldBlockFlyTime);
        curMoney += gameSettings.grassBlockPrice;
        moneyCounter.text = curMoney.ToString();
        moneyCounter.gameObject.transform.DOShakePosition(gameSettings.delayBetweenSells, gameSettings.coinCounterShakeStrength, 10, 90, false, true);
        Destroy(allCoins[0]);
        allCoins.RemoveAt(0);
    }
}