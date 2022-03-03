using System.Collections;
using UnityEngine;

public class GrassGrow : MonoBehaviour
{
    [SerializeField] private GameObject grassGrown;
    [SerializeField] private GameObject grassBlock;

    private float growTime = 10f;

    private void Start(){
        StartCoroutine(GrowProcess());
    }

    private IEnumerator GrowProcess(){
        yield return new WaitForSeconds(growTime);
        Instantiate(grassGrown, transform.position, transform.rotation, transform);
    }

    public void RespawnGrass(){
        Instantiate(grassBlock, transform.position, transform.rotation);
        StartCoroutine(GrowProcess());
    }
}