using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingRange : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameSettings gameSettings;

    private void Start(){
        GetComponent<CapsuleCollider>().radius = gameSettings.grassCutRange;
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Grass")){
            player.GetComponent<GrassCutting>().AddGrassInRange(other.gameObject);
            other.GetComponent<Renderer>().material.color = gameSettings.grassToCutColor;
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Grass")){
            player.GetComponent<GrassCutting>().RemoveGrassInRange(other.gameObject);
            other.GetComponent<Renderer>().material.color = Color.green;
        }
    }
}