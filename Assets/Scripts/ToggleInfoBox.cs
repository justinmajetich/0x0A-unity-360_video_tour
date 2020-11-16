using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ToggleInfoBox : MonoBehaviour
{
    private GameObject infoBox;


    void Start()
    {
        // Get info box child object.
        infoBox = transform.GetChild(0).gameObject;
    }

    public void Toggle() {

        float delay = infoBox.activeSelf ? 0.1f : 0.2f;
        StartCoroutine(ToggleAfterDelay(delay));
    }

    IEnumerator ToggleAfterDelay(float delay) {

        yield return new WaitForSeconds(delay);
        infoBox.SetActive(!infoBox.activeSelf);
    }
}
