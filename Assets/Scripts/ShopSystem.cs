using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] private GameObject shopMarker;
    [SerializeField] private GameObject[] shopWaypoints;

    private Vector3[] waypointsPositions;

    private void Start(){
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
        grassBlock.transform.localScale *= 5;
        grassBlock.transform.DOPath(waypointsPositions, 1f, PathType.CatmullRom, PathMode.Full3D, 10, null);
        StartCoroutine(DestroyWait(grassBlock));
    }

    private IEnumerator SellWait(List<GameObject> blocksToSell){
        SellBlock(blocksToSell[blocksToSell.Count - 1]);
        yield return new WaitForSeconds(0.05f);
        blocksToSell.RemoveAt(blocksToSell.Count - 1);
        if (blocksToSell.Count > 0)
            StartCoroutine(SellWait(blocksToSell));
    }

    private IEnumerator DestroyWait(GameObject grassBlock){
        yield return new WaitForSeconds(1f);
        Destroy(grassBlock);
    }
}