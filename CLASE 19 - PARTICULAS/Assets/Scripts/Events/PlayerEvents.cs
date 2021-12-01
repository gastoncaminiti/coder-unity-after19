using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEvents 
{
    public static event Action onDeath;

    public static void OnDeath()
    {
        onDeath?.Invoke();
    }
}
