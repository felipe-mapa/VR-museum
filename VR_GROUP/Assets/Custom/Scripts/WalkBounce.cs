using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMotion : MonoBehaviour {
    public AnimationCurve curve = new AnimationCurve(
        new Keyframe(0, 0)
    ,   new Keyframe(0.5f, 1)
    ,   new Keyframe(1, 0)
    );
    
    private const float MOVE_AMOUNT = 0.1f;
    private const float HEAD_MOVE_SPEED = 2.0f;

    private float progress;
    private float initialY;

    private void Awake() {
        initialY = Camera.main.transform.position.y;
        curve.preWrapMode = WrapMode.PingPong;
        curve.postWrapMode = WrapMode.PingPong;
    }

    void Update() {
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0) {
            MoveHead();
        } else {
            progress = Mathf.MoveTowards(progress, 0, 1 * Time.deltaTime);
        }
    }

    private void MoveHead() {
        progress += Time.deltaTime * HEAD_MOVE_SPEED;

        Vector3 camPos = Camera.main.transform.position;
        Camera.main.transform.position = new Vector3(
            camPos.x
        ,   initialY + curve.Evaluate(progress)
        ,   camPos.z
        );
    }
}
