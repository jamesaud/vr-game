using UnityEngine;
using System.Collections;
public class CreateTower : MonoBehaviour
{

    public GameObject prefabCube;
    BridgeCube bcube;
    float counter = 1;
    int interval = 1;
    float nextTime = 0;

    // Use this for initialization
    void Start()
    {
        GameObject cube = (GameObject)Instantiate(prefabCube);
        bcube = new BridgeCube(cube);
        bcube.setPosition(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime)
        {
            nextTime += interval;
            GameObject cube = (GameObject)Instantiate(prefabCube);
            BridgeCube next_bcube = new BridgeCube(cube);
            Vector3 curr_pos = bcube.getPosition();
            Vector3 add_pos = new Vector3(0, counter, 0);
            Vector3 new_pos = curr_pos + add_pos;
            next_bcube.setPosition(new_pos);
            counter++;
        }
    }

    BridgeCube getBcube()
    {
        return bcube;
    }

}
