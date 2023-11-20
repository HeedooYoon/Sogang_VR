using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] LiftController LinkedController;
    [SerializeField] LiftFloor _StartingFloor;
    [SerializeField] float LiftSpeed = 2f;

    public LiftFloor CurrentFloor { get; private set; } = null;
    public LiftFloor TargetFloor { get; private set; } = null;

    public bool IsMoving { get; private set; } = false;

    public LiftFloor StartingFloor => _StartingFloor;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, StartingFloor.TargetY, transform.position.z);
        CurrentFloor = StartingFloor;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving)
        {
            //lift moving
            Vector3 targetLocation = transform.position;
            targetLocation.y = TargetFloor.TargetY;

            transform.position = Vector3.MoveTowards(transform.position, targetLocation, LiftSpeed * Time.deltaTime);

            //have we arrived?
            if (Vector3.Distance(transform.position, targetLocation) < float.Epsilon)
            {
                IsMoving = false;
                CurrentFloor = TargetFloor;
                TargetFloor = null;

                CurrentFloor.OnLiftArrived();
            }
        }
    }

    public void MoveTo(LiftFloor targetFloor)
    {
        IsMoving = true;
        TargetFloor = targetFloor;
    }
}
