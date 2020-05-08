using UnityEngine;

[RequireComponent(typeof(FixedJoint))]
public class SelectionManager : MonoBehaviour
{
    private bool isActive = false;

    private GameObject hoveredObject;
    private GameObject attachedObject;
    private FixedJoint joint;

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }

    private void Start()
    {
        joint = GetComponent<FixedJoint>();
    }

    private void Update()
    {
        if (isActive)
        {
            Transform cam = Camera.main.transform;
            Ray ray = new Ray(cam.position, cam.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 200))
            {

                if (hit.transform.gameObject == hoveredObject)
                {
                    return;
                }
                else if (hit.transform.CompareTag("Selectable"))
                {
                    SelectObject(hit.transform.gameObject);
                }
                else
                {
                    ClearSelection();
                }
            }
            else
            {
                ClearSelection();
            }
        }
        
    }

    private void SelectObject(GameObject obj)
    {
        Debug.Log("Brick selected");
        hoveredObject = obj;
        Outline hoveredOutline = hoveredObject.GetComponent<Outline>();
        hoveredOutline.OutlineWidth = 5;
    }

    private void ClearSelection()
    {
        if (hoveredObject == null) return;

        Debug.Log("Brick deselected");
        Outline hoveredOutline = hoveredObject.GetComponent<Outline>();
        hoveredOutline.OutlineWidth = 0;

        hoveredObject = null;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    touched = collision.gameObject;
    //    mat.color = Color.green;
    //    collision.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (touched == collision.gameObject)
    //    {
    //        touched = null;
    //    }
    //    mat.color = Color.white;
    //    collision.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
    //}

    //public void AttachGameObject()
    //{
    //    var rb = touched?.GetComponent<Rigidbody>();
    //    if (rb == null) return;
    //    joint.connectedBody = rb;
    //    mat.color = Color.yellow;
    //}

    //public void DetachGameObject()
    //{
    //    joint.connectedBody = null;
    //    mat.color = Color.white;
    //}
}
