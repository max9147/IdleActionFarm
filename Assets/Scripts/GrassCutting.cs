using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassCutting : MonoBehaviour
{
    [SerializeField] private AnimationClip slashAnim;
    [SerializeField] private GameObject playerSickle;

    private Animator animator;
    private List<GameObject> grassInRange;

    private bool isSlashing = false;

    private void Start(){
        animator = GetComponent<Animator>();
        grassInRange = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Grass"))
            grassInRange.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Grass"))
            grassInRange.Remove(other.gameObject);
    }

    private IEnumerator DestroyGrassWait(){
        yield return new WaitForSeconds(slashAnim.length / 2);
        foreach (var item in grassInRange){
            item.transform.parent.GetComponent<GrassGrow>().RespawnGrass();
            Destroy(item);
        }
        grassInRange.Clear();
    }

    private IEnumerator SlashWait(){
        yield return new WaitForSeconds(slashAnim.length);
        isSlashing = false;
        playerSickle.SetActive(false);
    }

    public bool GetSlashingStatus(){
        return isSlashing;
    }

    public bool CheckGrass(){
        if (grassInRange.Count > 0)
            return true;
        else
            return false;
    }

    public void SlashGrass(){
        isSlashing = true;
        animator.SetTrigger("SlashGrass");
        playerSickle.SetActive(true);
        StartCoroutine(DestroyGrassWait());
        StartCoroutine(SlashWait());
    }
}