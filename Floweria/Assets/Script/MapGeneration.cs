using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {
    [SerializeField] int startx, starty;
    [SerializeField] int width;
    [SerializeField] int  height, minheight, maxheight;
    [SerializeField] int variation;
    [SerializeField] int repeatNum;
    [SerializeField] GameObject dirt, grass;
    void Start() {
        Generation();
    }

    void Generation() {
        int repeatValue = 0;
        height = Random.Range(minheight, maxheight);
        for (int x = startx; x < width; x++) {
            if (repeatValue == 0) {
                height = Random.Range(Mathf.Clamp(height - variation, minheight, maxheight), Mathf.Clamp(height + variation, minheight, maxheight));
                Generate(x);
                repeatValue = repeatNum;
            } else {
                Generate(x);
                repeatValue--;
            }
        }
    }

    void Generate(int x) {
        for (int y = starty; y < height; y++) {
            Spawn(dirt, x, y);
        }
        Spawn(grass, x, height);
    }


    void Spawn(GameObject obj, int width,int height) {
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        obj.transform.parent = this.transform;
    }

}
