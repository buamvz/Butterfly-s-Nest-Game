using UnityEngine;
using UnityEngine.UI;

public class LightRaycast : MonoBehaviour
{
    [SerializeField] private int rayLength = 5;
    private LightController interactiveObj;


    void Update()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out RaycastHit hit, rayLength))
        {
            var raycastObj = hit.collider.gameObject.GetComponent<LightController>();
            if (raycastObj != null)
            {
                interactiveObj = raycastObj;
            }
            else
            {
                ClearInteraction();
            }
        }
        else
        {
            ClearInteraction();
        }

        if (interactiveObj != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                interactiveObj.InteractSwitch();
            }
        }
    }

    private void ClearInteraction()
    {
        if (interactiveObj != null)
        {
            interactiveObj = null;
        }
    }

}
