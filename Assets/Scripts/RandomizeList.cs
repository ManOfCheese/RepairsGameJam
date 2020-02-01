using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeList : MonoBehaviour {

    //List of the prefabs from the different objects
    public GameObject[] randomizedObjects;

    void Start() {
        int randomIndex = Random.Range(0, randomizedObjects.Length);

        //GameObject instantiatedObject = Instantiate(randomizedObjects[randomIndex], 0, Quaternion.identity) as GameObject;
    }


    void Update() {
        
    }
}
