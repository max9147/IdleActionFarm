using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassCutting : MonoBehaviour
{
    [SerializeField] private AnimationClip slashAnim;
    [SerializeField] private GameObject playerSickle;
    [SerializeField] private ParticleSystem cutPS;

    private Animator animator;
    private List<GameObject> grassInRange;

    private bool isSlashing = false;

    private void Start(){
        animator = GetComponent<Animator>();
        grassInRange = new List<GameObject>();
    }

    public void AddGrassInRange(GameObject grass){
        grassInRange.Add(grass);
    }

    public void RemoveGrassInRange(GameObject grass){
        grassInRange.Remove(grass);
    }

    private IEnumerator DestroyGrassWait(){
        yield return new WaitForSeconds(slashAnim.length / 2);
        foreach (var item in grassInRange){
            item.transform.parent.GetComponent<GrassGrow>().RespawnGrass();
            Instantiate(cutPS, item.transform.position, item.transform.rotation);
            Destroy(item);
        }
        grassInRange.Clear();
    }

    private IEnumerator SlashWait(){
        yield return new WaitForSeconds(slashAnim.length);
        isSlashing = false;
        playerSickle.SetActive(false);
        GetComponent<Rigidbody>().isKinematic = false;
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
        GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(DestroyGrassWait());
        StartCoroutine(SlashWait());
    }
}