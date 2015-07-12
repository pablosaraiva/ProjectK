using UnityEngine;
using System.Collections;

[System.Serializable]
public class RoomIndex
{
	public int x;
	public int y;
	
	public RoomIndex (int x, int y)
	{
		this.x = x;
		this.y = y;
	}
	
	public override bool Equals (object obj)
	{
		RoomIndex ri = (RoomIndex)obj;
		return (ri.x == this.x && ri.y == this.y);
	}
	
	public override int GetHashCode ()
	{
		return (x << 10) + y;
	}
	
	
}
