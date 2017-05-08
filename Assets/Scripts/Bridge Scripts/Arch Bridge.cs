using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArchBridge : Bridge {
    //The prefab cube that will be used for the bridge steps
    GameObject prefabCube;

    //How high the bridge will go
    int bridgeHeight;

    //How much higher the next brick is generated at
    float growHeight;

    //width of the prefab cube to know the distance between steps
    float growWidth;

    //starting position for the bridge
    Vector3 initPosition;

    //most recent bridgeCube
    BridgeCube recentBrick;

    //Queue of all bridgeCubes in our bridge
    List<BridgeCube> brickList = new List<BridgeCube>();

    //Direction is the direction in which the bridge should be created, by playercam
    Quaternion rotate;

    //Gravity! To add the arc to the bridge. It should be a non-linear curve
    float gravity = .98f;

    //Sub Height: lessens/strengthens the gravity impact and creates the arch effect
    float subHeight = -.003f;

    float zaffect = 40f;


    public ArchBridge(GameObject prefabCube, Vector3 initPosition, Quaternion rotation)
    {
        prefabCube.GetComponent<Renderer>().material.color = Color.green;
        //Set the initial fields
        this.prefabCube = prefabCube;
        this.initPosition = initPosition;
        this.rotate = rotation;

        //Get the scaling of the prefabCube and assign it to the width
        this.growWidth = prefabCube.transform.localScale.x;

        //Assign grow height
        this.growHeight = prefabCube.transform.localScale.y;

 
        //Instantiate a brick (BridgeCube)
        BridgeCube brick = new BridgeCube(this.prefabCube, this.initPosition, this.rotate * Quaternion.Euler(0, 90f, 0));

        //Update our recent and list
        updateBridge(brick);
    }

    //Update the bridge with a brick
    void updateBridge(BridgeCube brick)
    {
        //Update the recent brick
        recentBrick = brick;

        //Update the Brick List
        brickList.Add(brick);
    }

    //Add a new brick to the bridge
    public override void addNextBrick(GameObject prefabCube)
    { 
        prefabCube.GetComponent<Renderer>().material.color = Color.green;
        //Get the current brick
        BridgeCube recent = recentBrick;

        //Calculate the new position for the next brick
        Vector3 curr_pos = recent.getPosition();
        Vector3 next_pos = nextPosition(curr_pos);

        //Create the next brick
        this.zaffect += zaffect - zaffect*this.gravity;
        BridgeCube nextBrick = new BridgeCube(prefabCube, next_pos, this.rotate * Quaternion.Euler(0, 90f, this.zaffect));

        //Add the next brick to our bridge
        updateBridge(nextBrick);
    }

    //Takes in current position and returns new one
    Vector3 nextPosition(Vector3 curr_pos)
    {
        //Adjust next growheight to the gravity
        this.growHeight = this.growHeight * this.gravity + this.subHeight;

        //Move upwards by the growheight
        Vector3 add_pos = new Vector3(0, this.growHeight, 0);

        //Move forward by the growwidth
        Vector3 add_forward = new Vector3(0, 0, this.growWidth);
        add_pos += this.rotate * add_forward;

        //add that to the current position to get the new one
        Vector3 next_pos = curr_pos + add_pos;
        return next_pos;
    }

    public BridgeCube getRecentBrick()
    {
        return recentBrick;
    }

    public override string ToString()
    {
        return "Number of Bricks: " + brickList.Count;
    }

    public Quaternion getRotate()
    {
        return rotate;
    }

    //Returns the list of all bricks in this bridge
    public override List<BridgeCube> getBrickList()
    {
        return brickList;
    }

    public void setGravity(float grav) {
        this.gravity = grav;
    }


    //Sets the growth height, how much heigher each brick will be from the previous
    //Always is 0 because this is a flat bridge
    public override void setGrowHeight(float growth) { this.growHeight = 0; }

    //Sets the grow width, how much farther each brick will be from the previous
    public override void setGrowWidth(float growth) { this.growWidth = growth; }

}
