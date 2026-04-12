using System.Collections;
using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    private float _time = 0f;



    public void UpdateRotation(AnimationCurve curve)
    {
        _time += Time.deltaTime;
        float deltaRotation = curve.Evaluate(_time);
        Quaternion quaternion = Quaternion.Euler(0, 0, transform.rotation.z + deltaRotation);
        transform.Rotate(new Vector3(0, 0, deltaRotation));
    }
}
