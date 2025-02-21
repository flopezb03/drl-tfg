using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour{

    private Rigidbody rb;

    public float gasIn;
    public float steeringIn;
    public float brakeIn;
    public float slipAngle;

    public Wheel FLWheel;
    public Wheel FRWheel;
    public Wheel RLWheel;
    public Wheel RRWheel;
    public float motorPower;
    public float brakesPower;
    public float maxSpeed = 30;

    public float speed;


    public float decelerationMultiplier = 0.2f;
    public float stopVelocity = 0.2f;
    
    


    void Start(){
        rb = gameObject.GetComponent<Rigidbody>();

    }
    void FixedUpdate(){
        
        speed = rb.linearVelocity.magnitude;
        slipAngle = Vector3.Angle(transform.forward,rb.linearVelocity);


        FLWheel.update();
        FRWheel.update();
        RLWheel.update();
        RRWheel.update();

        
        accelerate();
        if(gasIn == 0 && brakeIn == 0){
            deccelerate();
        }
        steer();
        brake();
    }

    public void getInput(float x, float y){

        gasIn = y;
        steeringIn = x;
        

        if(gasIn < 0){
            brakeIn = Mathf.Abs(gasIn);
            gasIn = 0;
        }else{
            brakeIn = 0;
        }
    }
    void accelerate(){
        RLWheel.collider.motorTorque = motorPower*gasIn;
        RRWheel.collider.motorTorque = motorPower*gasIn;
    }
    void deccelerate(){
        rb.linearVelocity = rb.linearVelocity * (1f / (1f + (0.025f * decelerationMultiplier)));
        if(rb.linearVelocity.magnitude < stopVelocity){
            rb.linearVelocity = Vector3.zero;
        }
    }
    
    void steer(){
        float maxSteerAngle = Mathf.Lerp(60, 20, Mathf.InverseLerp(0, maxSpeed, speed));

        float angle = steeringIn*maxSteerAngle;

        //  Countersteer
        if (slipAngle < 60f){
            float counterSteer = Vector3.SignedAngle(transform.forward, rb.linearVelocity + transform.forward, Vector3.up);
            angle += Mathf.Lerp(0, counterSteer, 0.5f);

        }
        angle = Mathf.Clamp(angle, -maxSteerAngle, maxSteerAngle);

        FLWheel.collider.steerAngle = angle;
        FRWheel.collider.steerAngle = angle;
    }
    
    void brake(){
        float frontBrakeMultiplier;
        float rearBrakeMultiplier;

        frontBrakeMultiplier = 0.6f;
        rearBrakeMultiplier = 0.4f;

        FLWheel.collider.brakeTorque = brakeIn*brakesPower*frontBrakeMultiplier;
        FRWheel.collider.brakeTorque = brakeIn*brakesPower*frontBrakeMultiplier;
        RLWheel.collider.brakeTorque = brakeIn*brakesPower*rearBrakeMultiplier;
        RRWheel.collider.brakeTorque = brakeIn*brakesPower*rearBrakeMultiplier;
    }
}
[System.Serializable]
public class Wheel{
    public MeshRenderer mesh;
    public WheelCollider collider;

    public void update(){
        Vector3 pos;
        Quaternion q;
        
        collider.GetWorldPose(out pos, out q);

        mesh.transform.position = pos;
        mesh.transform.rotation = q;

    }
}