using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public void DeactivateInTime(float time)
    {
        Invoke("Deactive", time);
    }

    private void Deactive()
    {
        gameObject.SetActive(false);
    }
}
