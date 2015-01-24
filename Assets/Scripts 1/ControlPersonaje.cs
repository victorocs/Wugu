using UnityEngine;
using System.Collections;

public class ControlPersonaje : MonoBehaviour {

	private Animator _animator;
	public bool caminarEsPresionado;
	public bool saltarEsPresionado;

	void Start () {
		_animator = GetComponent<Animator> ();
	}

	void Update () {
	
	}

	void obtenerControles(){
		if(Input.GetKeyDown(KeyCode.Space){
			saltarEsPresionado = true;
		}
		if(Input.GetKeyDown(KeyCode.W)){
			caminarEsPresionado = true;
		}
	}

	void IAPersonaje(){

	}
}
