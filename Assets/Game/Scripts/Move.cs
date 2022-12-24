using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

	public float moveDist;
	public string easetype;

	void Start(){
		iTween.MoveBy(gameObject, iTween.Hash("y", moveDist, "easeType", easetype, "loopType", "pingPong", "delay", 0, "time", .4f));
	}
}
