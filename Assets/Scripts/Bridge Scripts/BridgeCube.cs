using UnityEngine;
using System.Collections;

public class BridgeCube
{
    //The prefabCube that the bridgeCube is created from
    GameObject cube;

    //Sets the position of the cube to the given position
    public BridgeCube(GameObject prefabCube, Vector3 position)
    {
        this.cube = prefabCube;

        //Set the initial position for the prefab cube
        setPosition(position);
    }

    //Sets the position and rotation to the given position
    public BridgeCube(GameObject prefabCube, Vector3 position, Quaternion rotation)
    {
        this.cube = prefabCube;
        setPosition(position);
        setRotation(rotation);
    }

    
    public BridgeCube(GameObject prefabCube)
    {
        cube = prefabCube;
    }

    //Updates position with floats
    public void setPosition(float x, float y, float z){
        cube.transform.position = new Vector3(x, y, z);
    }

    //Updates position with Vector
    public void setPosition(Vector3 position_vector){
           cube.transform.position = position_vector;
        }

    //Returns current position
    public Vector3 getPosition() {
        return cube.transform.position;
    }

    //Returns the scale of the cube
    public Vector3 getSize() {
        return cube.transform.localScale;
    }

    //Sets the rotation
    public void setRotation(Quaternion rotation_vector) {
        cube.transform.rotation = rotation_vector;
    }

    //Returns the current prefabCube
    public GameObject getPrefabCube() {
        return cube;
    }

    //Prints out the current position of the cube
    public override string ToString() {
        return "I'm at position: " + cube.transform.position; 
    }
}
