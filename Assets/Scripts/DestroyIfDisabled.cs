using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfDisabled : MonoBehaviour //уничтожение объекта
{
    public bool SelfDestructionEnabled { get; set; } = false;

    private void OnDisable()
    {
        if (SelfDestructionEnabled )
        {
            Destroy(gameObject);
        }
    }
}
