using UnityEngine;
using System.Collections;

public class CreateBridge : MonoBehaviour {
    public GameObject prefabCube;
    public float interval = .05f;
    public int bridgeHeight = 20;

    float nextTime = 0;
    float growInterval = .05f;
    float growWidth;
    Vector3 bridgeInitPosition = new Vector3(-4, .1f,8);

    Transform playerCam;

    float gravity;
    float counter;
    Vector3 new_pos;
    Vector3 start_pos;

    enum Direction { NORTH, SOUTH, EAST, WEST };
    Direction direction;

    // Use this for initialization
    void Start () {
        playerCam = gameObject.GetComponentInChildren<Camera>().transform;
        growWidth = prefabCube.transform.localScale.x;
        direction = Direction.WEST;
    }

    // Update is called once per frame
    bool flag = false;
    void Update() {
        setDirection();

        if (Input.GetKeyDown(KeyCode.Space)) {
            playerCam = gameObject.GetComponentInChildren<Camera>().transform;

            Vector3 place_bridge_bottom = new Vector3(0, -.99f, 0);
            start_pos = playerCam.position + place_bridge_bottom;
            new_pos = start_pos;
            counter = 0;
            gravity = 0;
            flag = true;
            growWidth = prefabCube.transform.localScale.x;
        }

        if (Time.time >= nextTime && flag){

            GameObject cube = (GameObject)Instantiate(prefabCube);
            BridgeCube next_bcube = new BridgeCube(cube);

            next_bcube = setNewPos(next_bcube);

            counter++;
            growWidth = cube.transform.localScale.x * counter;
            nextTime += interval;

            if (new_pos.y <= 0) { flag = false; }
            if (counter <= bridgeHeight)
            {
                gravity += growInterval;
            }
            else {
                gravity -= growInterval;
            }   
        }
    }

 
    void setDirection() {
        if (Input.GetKeyDown(KeyCode.L)) { direction = Direction.EAST; }
        if (Input.GetKeyDown(KeyCode.J)) { direction = Direction.WEST; }
        if (Input.GetKeyDown(KeyCode.K)) { direction = Direction.SOUTH; }
        if (Input.GetKeyDown(KeyCode.I)) { direction = Direction.NORTH; }
    }

    BridgeCube setNewPos(BridgeCube bcube) {
        Vector3 add_pos;
        Vector3 add_rotation;
        if (direction == Direction.NORTH) {
            add_pos = new Vector3(0, gravity, growWidth);
            add_rotation = new Vector3(0, 90, 90);
        //    bcube.setRotation(add_rotation);

        }
        else if (direction == Direction.EAST)
        {
            add_pos = new Vector3(growWidth, gravity, 0);
        }

        else if (direction == Direction.SOUTH)
        {
            add_pos = new Vector3(0, gravity, growWidth * -1);
            add_rotation = new Vector3(0, 90, 90);
        //    bcube.setRotation(add_rotation);

        }
        else //if West
        {
            add_pos = new Vector3(growWidth * -1, gravity, 0);
        }
        new_pos = start_pos + add_pos;
        bcube.setPosition(new_pos);
        return bcube;
    }
}

