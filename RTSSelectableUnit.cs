using UnityEngine;
using System.Collections;

public class RTSSelectableUnit : MonoBehaviour {
	RTSUnitSelectionManager UnitManager;
	GameObject gameObject;
	
	void Start () {
		UnitManager = GameObject.FindWithTag("UnitManager").GetComponent<RTSUnitSelectionManager>();
		gameObject = GameObject.FindWithTag("Player");
	}
	
	void OnTriggerEnter(Collider col) {
		UnitManager.SelectUnit(gameObject);
		gameObject.renderer.material.color = Color.white;
	}
	
	void OnTriggerExit(Collider col) {
		UnitManager.DeselectUnit(gameObject);
		gameObject.renderer.material.color = Color.green;
	}
}
