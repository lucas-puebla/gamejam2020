using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {
    private bool state;
    private float maxTime;
    private float remainingTime;

    public Timer(float maxTime) {
        state = false;
        this.maxTime = maxTime;
    }

    public void reset() {
        state = true;
        remainingTime = maxTime;
    }
    
    public void disable() {
        state = false;
    }

    // Toggle use of countDown for update or fixedUpdate
    // Do not use fixedUpdate on Update and vice versa
    public void countDown(bool fixedUpdate = false) {
        if (state) {
            float time = fixedUpdate ? Time.fixedDeltaTime : Time.deltaTime;
            remainingTime -= time;
        }

        if (state && remainingTime <= 0) {
            disable();
        }
    }

    public bool isEnabled() {
        return state;
    }

    public void setMaxTime(float maxTime) {
        this.maxTime = maxTime;
    }

    public float getMaxTime() {
        return maxTime;
    }

    public float getRemTime() {
        return remainingTime;
    } 
}