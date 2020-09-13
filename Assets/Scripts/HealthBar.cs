using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class HealthBar
{
    public static Slider slider;
    
    
    public static void updateHealth(int amount){
        slider.value = amount;
    }
}

