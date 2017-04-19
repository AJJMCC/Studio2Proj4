#pragma strict

function Start () {

}

function create (obj : GameObject) {
	Instantiate(obj, transform.position, obj.transform.rotation);
}