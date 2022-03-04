using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject blockContainer;
    [SerializeField] private GameObject grassBlockInventory;
    [SerializeField] private GameObject shopMarker;
    [SerializeField] private GameObject shopSystem;
    [SerializeField] private Slider inventorySlider;
    [SerializeField] private TextMeshProUGUI grassCount;
    [SerializeField] private GameSettings gameSettings;

    private List<GameObject> blocksCarrying;

    private float offset;
    private int inventoryCount;

    private Vector3 blockSpawnPoint;

    private void Start(){
        blocksCarrying = new List<GameObject>();
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("GrassBlock") && inventoryCount < gameSettings.inventoryMax){
            blockSpawnPoint = collision.transform.position;
            Destroy(collision.gameObject);
            PickUpGrass();
        }        
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Shop")){
            shopSystem.GetComponent<ShopSystem>().SellBlocks(blocksCarrying);
            StartCoroutine(RemoveFromInventoryWait());
        }
    }

    private void PickUpGrass(){
        if (inventoryCount == 0)
            shopMarker.SetActive(true);
        inventoryCount++;
        RefreshInventory();
        offset = 0.4f / gameSettings.inventoryMax * inventoryCount;
        var curBlock = Instantiate(grassBlockInventory, blockSpawnPoint, Quaternion.identity, blockContainer.transform);
        blocksCarrying.Add(curBlock);
        curBlock.transform.DOLocalJump(new Vector3(0, offset, 0), 1f, 1, gameSettings.harvestedBlockFlyTime, false);
    }

    private void RefreshInventory(){
        grassCount.text = $"{inventoryCount}/{gameSettings.inventoryMax}";
        inventorySlider.value = (float)(inventoryCount) / (float)(gameSettings.inventoryMax);
    }

    private IEnumerator RemoveFromInventoryWait(){
        yield return new WaitForSeconds(gameSettings.delayBetweenSells);
        if (inventoryCount > 0){
            inventoryCount--;
            RefreshInventory();
            yield return RemoveFromInventoryWait();
        }
    }
}