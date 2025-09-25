using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseParallax : MonoBehaviour
{
    public float _effectAmount = 1f; // how much does the mouse effect the images position.

    public bool _effectXaxis = true;
    public bool _effectYaxis = true;

    public bool _inverse = true;

    private Vector3 _targetPosition; // save a reference for our target position.
    private Vector3 _startPos; // save a reference for out start position.

    void Start()
    {
        _startPos = transform.localPosition; // save the start position. We use local position instead of just position so unity doesn't get confused with parents and children.
    }

   
    void Update()
    {
        _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // get's the mouse position on the screen and turns it into game world position.

        // I've long coded out if else statments for everything here to make it easier to read, there are ways of writing lines 29-59 in 2 lines.

        if (_effectXaxis) // if we're moving along the x axis or not.
        {
            if (_inverse) // if we want the movment to be inverse of the mouse position or not.
            {
                _targetPosition.x = _startPos.x - (_targetPosition.x * (_effectAmount * 0.01f)); // set the x and y positions. It needs to be a small number, so times by  0.01 to make it a lot smaller and easier to work with in the inspector
            }else
            {
                _targetPosition.x = _startPos.x + (_targetPosition.x * (_effectAmount * 0.01f)); // set the x and y positions. It needs to be a small number, so times by  0.01 to make it a lot smaller and easier to work with in the inspector
            }
        }
        else
        {
            _targetPosition.x = _startPos.x; // if we're not moving alng the x axis, set it to the start position of x.
        }

        if (_effectYaxis)
        {
            if (_inverse)
            {
                _targetPosition.y = _startPos.y - (_targetPosition.y * (_effectAmount * 0.01f)); // "
            }
            else
            {
                _targetPosition.y = _startPos.y + (_targetPosition.y * (_effectAmount * 0.01f)); // "
            }
        }
        else
        {
            _targetPosition.y = _startPos.y; // if we're not moving alng the y axis, set it to the start position of y.
        }

        _targetPosition.z = 0; // make sure the z is at 0, this is 2D we don't need it.

        transform.localPosition = _targetPosition; // finally set the position to our target position.
    }
}
