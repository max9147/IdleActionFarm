using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject blockContainer;
    [SerializeField] private GameObject grassBlockInventory;
    [SerializeField] private GameObject shopMarker;
    [SerializeField] private GameObject shopSystem;
    [SerializeField] private Slider inventorySlider;
    [SerializeField] private TextMeshProUGUI grassCount;

    private List<GameObject> blocksCarrying;

    private float offset;
    private int inventoryCount;
    private int inventotyMax = 40;

    private void Start(){
        blocksCarrying = new List<GameObject>();
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("GrassBlock") && inventoryCount < inventotyMax){
            Destroy(collision.gameObject);
            PickUpGrass();
        }        
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Shop")){
            shopSystem.GetComponent<ShopSystem>().SellBlocks(blocksCarrying);
            ClearInventory();
        }
    }

    private void PickUpGrass(){
        if (inventoryCount == 0)
            shopMarker.SetActive(true);
        inventoryCount++;
        RefreshInventory();
        offset = 0.4f / inventotyMax * inventoryCount;
        blocksCarrying.Add(Instantiate(grassBlockInventory, blockContainer.transform.position + new Vector3(0, offset, 0), blockContainer.transform.rotation, blockContainer.transform));
    }

    private void RefreshInventory(){
        grassCount.text = $"{inventoryCount}/{inventotyMax}";
        inventorySlider.value = (float)(inventoryCount) / (float)(inventotyMax);
    }

    private void ClearInventory(){
        inventoryCount = 0;
        RefreshInventory();
    }
}