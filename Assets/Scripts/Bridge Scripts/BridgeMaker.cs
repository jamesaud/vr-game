using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class BridgeMaker : MonoBehaviour
{
    //1,2,3,4,etc.
    static public int BridgeType=0;

     
    //prefab for bridge bricks
    public GameObject prefabCube;

    //List Of Actively Created Bridges (List of bridges that need to be updated)
    List<Bridge> active_bridge_list = new List<Bridge>();

    //All Bridges is a list of all bridges that are currently existing (bridges that are finished updating)
    List<Bridge> all_bridges = new List<Bridge>();

    //Player Cam
    Transform camTrans;

    //Grow Width. 0 means set to default
    public float stair_bridge_growWidth;

    //Grow Height. 0 means set to default
    public float stair_bridge_growHeight;

    //How many bricks a bridge can contain
    public int maxBricks;

    //How many bridges can exist at once. The oldest bridge is deleted
    public int maxBridges;

    //createbridge is triggered by another script
    static public bool CreateBridge = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Update the current camera location
        camTrans = gameObject.GetComponentInChildren<Camera>().transform;

        //Press space to generate a bridge
        if (Input.GetKeyDown(KeyCode.Space) | CreateBridge)
        {
            CreateBridge = false;
            switch (BridgeType)
            {
                case 0:
                     //Create a new stair bridge
                    addStairBridge();
                    break;
                case 1:
                    //Create Flat Bridge
                    addFlatBridge();
                    break;
                case 2:
                    //Create Flat Bridge
                    addArchBridge();
                    break;
            }

        //Remove a bridge if there are too many bridges
        if (all_bridges.Count > maxBridges)
            {
                removeOldestBridge();
            }
        }

        //Update all the bridges accordingly if there are bridges in the active_bridge_list
        if (active_bridge_list.Count > 0)
        {
            //Generate another brick for every bridge that needs to be updated
            updateBridges();
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket)) { BridgeType -= 1; }
        if (Input.GetKeyDown(KeyCode.RightBracket)) { BridgeType += 1; }
    }

    //Calculates the starting position and returns it
    public Vector3 bridgeStartPos() {
        //Sub position is necessary to get the bridge to start from the ground, instead of 1 up from the ground
        Vector3 subPos = new Vector3(0, -1f, 0);
        Vector3 starting_pos = transform.position + camTrans.rotation * Vector3.forward + subPos;
        return starting_pos;
    }

    //Creates a new bridge at the player position, and adds it to the active_bridge_list
    public void addStairBridge()
    {
        //Create a new starting cube, necessary to init a bridge
        GameObject cube = (GameObject)Instantiate(prefabCube);

        //Calculate the starting position
        Vector3 start_pos = bridgeStartPos();

        //Create a new bridge, with the starting postion and the camera's current rotation
        Bridge stair_bridge = new StairBridge(cube, start_pos, camTrans.rotation);

        //Set grow dimentions for the bridge to default if 0, or to the user specified
        if (stair_bridge_growHeight != 0) { stair_bridge.setGrowHeight(stair_bridge_growHeight); }
        if (stair_bridge_growWidth != 0) { stair_bridge.setGrowWidth(stair_bridge_growWidth); }

        //Add the stairbridge
        addBridge(stair_bridge);
    }

    public void addFlatBridge()
    {
        //Create a new starting cube, necessary to init a bridge
        GameObject cube = (GameObject)Instantiate(prefabCube);

        //Calculate the starting position
        Vector3 start_pos = bridgeStartPos();

        //Create a new bridge, with the starting postion and the camera's current rotation
        Bridge flat_bridge = new FlatBridge(cube, start_pos, camTrans.rotation);

        //Add the FlatBridge
        addBridge(flat_bridge);
    }

    public void addArchBridge()
    {
        //Create a new starting cube, necessary to init a bridge
        GameObject cube = (GameObject)Instantiate(prefabCube);

        //Calculate the starting position
        Vector3 start_pos = bridgeStartPos();

        //Create a new bridge, with the starting postion and the camera's current rotation
        Bridge arch_bridge = new ArchBridge(cube, start_pos, camTrans.rotation);

        //Add the FlatBridge
        addBridge(arch_bridge);
    }

    //Geenerically adds a bridge
    public void addBridge(Bridge bridge) {

        //Add our newly created bridge to the list of bridges, and the active bridges
        active_bridge_list.Add(bridge);
        all_bridges.Add(bridge);
    }

    public void updateBridges() {
        //Add one more brick per frame
        //For each bridge in active_bridge_list, we check if the bricklist length is > maxBricks
        foreach (var br in active_bridge_list)
        {
            //Remove the bridge from the active_bridge_list if it is full of bricks
            if (br.getBrickList().Count > maxBricks)
            {
                active_bridge_list.Remove(br);
            }

            //Otherwise, add the next brick to the bridge
            else {
                GameObject next_cube = (GameObject)Instantiate(prefabCube);
                br.addNextBrick(next_cube);
            }
        }
    }

    //Removes the oldest bridge - There is a better way to do this but I don't know how
    public void removeOldestBridge() {
        //Get the oldest bridge at position 0
        Bridge oldest_bridge = all_bridges[0];

        //Remove the bridge from the all_bridges list
        all_bridges.RemoveAt(0);

        //Delete the bridge from the world by killing it's cubes
        List<BridgeCube> bricksList = oldest_bridge.getBrickList();

        //Go through each brick and destroy it's prefab cube
        foreach (var brick in bricksList)
        {
            //Destroy the prefab
            Destroy(brick.getPrefabCube());
        }

    }

    //Turns the bridge on
    static public void bridgeOn() {
        CreateBridge = true;
    }

    //NextBridgeType cycles through the available bridges
    static public void NextBridgeType() {
        if (BridgeType < 2)
        {
            BridgeType++;
        }
        else if (BridgeType == 2) {
            BridgeType = 0;
        }

    }
}
