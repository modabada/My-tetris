using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spanwer : MonoBehaviour
{
    Block target;
    private void Start() {
        Spawner();
    }
    private void Update() {
        if(target.isPlace) {
            Spawner();
        }
    }
    private void Spawner() {
        GameObject targetObj = Instantiate(transform.GetChild(Random.Range(0, transform.childCount)).gameObject, transform.position, Quaternion.identity);
        targetObj.SetActive(true);
        target = targetObj.GetComponent<Block>();
    }
}
