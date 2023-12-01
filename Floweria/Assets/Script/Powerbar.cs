using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerbar : MonoBehaviour{
    public Slider slider;

    private void Update() {
        slider.value = (GameObject.Find("Red Bush").GetComponent<Shoot>().force);
    }

}
