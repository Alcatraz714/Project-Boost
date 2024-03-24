using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 StartingPosition;
    [SerializeField] [Range(0,1)] float MovementFactor;
    [SerializeField] Vector3 MovementVector;
    [SerializeField] float Period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = transform.position; // current position of the object
    }

    // Update is called once per frame
    void Update()
    {
        if (Period <= Mathf.Epsilon) {return;} // can use math.epsilon
        float Cycles = Time.time / Period; // growth from sin wave
        const float Tau = Mathf.PI*2; // 2Pie full circle

        float RawSinWave = Mathf.Sin(Cycles * Tau); // ranges from -1 to 1

        MovementFactor = (RawSinWave + 1f)/2f; //how fast or slow

        Vector3 Offset = MovementVector * MovementFactor;
        transform.position = StartingPosition + Offset;
    }
}
