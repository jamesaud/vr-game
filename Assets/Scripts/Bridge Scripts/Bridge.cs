using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract public class Bridge
{
    public abstract void addNextBrick(GameObject prefabCube);
    public abstract List<BridgeCube> getBrickList();
    public abstract void setGrowHeight(float growth);
    public abstract void setGrowWidth(float growth);
}
	
