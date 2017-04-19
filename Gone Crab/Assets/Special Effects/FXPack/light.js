#pragma strict
private var glow : Light;
var time : float = 3;
private var down : boolean = false;
function Start () {
	glow = GetComponent.<Light>();


}

function Update () {
	if(glow.intensity >= 0  && down == false){
		glow.intensity += time * Time.deltaTime;
	}
	if(glow.intensity >= 7 && down == false){
		down = true;
	}

	if(down){
		glow.intensity -= time * Time.deltaTime;
		if(glow.intensity == 0){
			down = false;
			Destroy(gameObject);
		}
	}
}