using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {
    [SerializeField] int startx, starty;
    [SerializeField] int width;
    [SerializeField] int  height, minheight, maxheight;
    [SerializeField] int variation;
    [SerializeField] int repeating;
    [SerializeField] GameObject dirt, grass,bedrock;
    void Start() {
        WallGeneration();
        Generation();
    }

    void WallGeneration() {
        int xx = startx-10;
        height = maxheight + 20;
        for (int x2= xx; x2<startx+1;x2++) {
            Generate(x2);
        }
        int xxx = width+10;
        height = maxheight + 20;
        for (int x3= xxx; x3>width-1;x3--) {
            Generate(x3);
        }
    }

    void Generation() {
        int repeat = 0;
        height = Random.Range(minheight, maxheight);
        for (int x = startx+1; x < width; x++) {
            if (repeat == 0) {
                height = Random.Range(Mathf.Clamp(height - variation, minheight, maxheight), Mathf.Clamp(height + variation, minheight, maxheight));
                Generate(x);
                repeat = repeating;
            } else {
                Generate(x);
                repeat--;
            }
        }
    }

    void Generate(int x) {
        if (x <= startx || x >= width) {
            for (int y = starty; y < height; y++) {
                Spawn(bedrock, x, y);
                for (int u = starty-1; u > starty- 7; u--) {
                    Spawn(bedrock, x, u);
                }
            }
        } else {
            for (int u = starty-1; u > starty- 7; u--) {
                    Spawn(bedrock, x, u);
                }
            for (int y = starty; y < height; y++) {
                Spawn(dirt, x, y);
            }
            Spawn(grass, x, height);
        }
    }


    void Spawn(GameObject obj, int width,int height) {
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        obj.transform.parent = this.transform;
    }

}
