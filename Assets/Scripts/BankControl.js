#pragma strict

var keyboardControls : boolean = true;

var mouseControls : boolean = false;

//The max variables are to make the rotation framerate independent.
//You could alternatively do the work in FixedUpdate,
//but the controls might be less responsive there.

// Tilt
var maxTilt : float = 180.0f; //Degrees/second

var tiltScale : float = 60.0f; //Degrees/unitInput*second 
var tiltRange : float = 30.0f; //Degrees
private var rotX : float = 0.0f; //Degrees

//Turn
var maxTurn : float = 360.0f; //Degrees/second
var turnScale : float = 120.0f; //Degrees/unitInput*second
var turnRange : float = 360.0f; //Degrees private
var rotY : float = 0.0f; //Degrees

//Bank
var maxBank : float = 90.0f; //Degrees/second
var bankScale : float = 60.0f; //Degrees/unitInput*second 
var returnSpeed : float = 40.0f;//Degrees/second
var bankRange : float = 20.0f; //Degrees
private var rotZ : float = 0.0f; //Degrees

//Input
private var mouseScale : float = 1.0f; //Gs of acceleration/pixel 
private var deltaX : float = 0.0f; //Units of input
private var deltaY : float = 0.0f; //Units of input

//Start information 

private var originalRot : Quaternion = Quaternion.identity;

function Update () {
	if(keyboardControls) {
		deltaX = Input.GetAxis("Horizontal");
		deltaY = Input.GetAxis("Vertical");
	} else if(mouseControls) {
		deltaX = Input.GetAxis("Mouse X")*mouseScale;
		deltaY = Input.GetAxis("Mouse Y")*mouseScale;
	} else {
	deltaX = Input.acceleration.x; deltaY = Input.acceleration.y; }

	//Bank
	if(!Mathf.Approximately(deltaX, 0.0f))
		rotZ = ClampAngle(rotZ-ClampAngle(deltaX*bankScale,-maxBank,maxBank)*Time.deltaTime, -bankRange, bankRange);
	else if( rotZ == 0.0f)
		rotZ = ClampAngle(rotZ-Time.deltaTime*returnSpeed,0.0f,bankRange);
	else rotZ = ClampAngle(rotZ+Time.deltaTime*returnSpeed,-bankRange,0.0f);

	//Turn
	rotY = ClampAngle(rotY+
	       ClampAngle(deltaX*turnScale,-maxTurn,maxTurn)*Time.deltaTime,
	       -turnRange,turnRange);

	//Tilt
	rotX = ClampAngle(rotX-
	       ClampAngle(deltaY*tiltScale,-maxTilt,maxTilt)*Time.deltaTime,
	       -tiltRange,tiltRange);

	transform.localRotation = Quaternion.Euler(rotX,rotY,rotZ)*originalRot;

}

function Start () {
	if (GetComponent.<Rigidbody>()) GetComponent.<Rigidbody>().freezeRotation = true;
	originalRot = transform.localRotation;
}

//Modified to work when you get angles outside of -/+720z

static function ClampAngle (angle : float, min : float, max : float) : float {
	while (angle < -360.0)
		angle += 360.0;
	while (angle > 360.0)
		angle -= 360.0;
	return Mathf.Clamp(angle, min, max);
} 