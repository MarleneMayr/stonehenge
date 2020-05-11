using UnityEngine;

[RequireComponent(typeof(FixedJoint))]
public class SelectionManager : MonoBehaviour
{
    private bool isActive = false;

    enum Mode { Kinematic, Joint };
    [SerializeField] private Mode mode = Mode.Kinematic;

    private GameObject hoveredObject;
    private GameObject attachedObject;
    private FixedJoint joint;

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        DetachGameObject();
        ClearSelection();
        isActive = false;
    }

    public void HandleTap()
    {
        if (attachedObject != null)
        {
            DetachGameObject();
        }
        else if (hoveredObject)
        {
            AttachGameObject();
        }
        else
        {
            Debug.Log("Tapped without selection");
        }
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
        hoveredObject = obj;
        Outline hoveredOutline = hoveredObject.GetComponent<Outline>();
        hoveredOutline.OutlineWidth = 5;
    }

    private void ClearSelection()
    {
        if (hoveredObject == null) return;

        Outline hoveredOutline = hoveredObject.GetComponent<Outline>();
        hoveredOutline.OutlineWidth = 0;

        hoveredObject = null;
    }

    public void AttachGameObject()
    {
        attachedObject = hoveredObject;
        var rb = attachedObject.GetComponent<Rigidbody>();
        
        if (mode == Mode.Joint)
        {
            joint.connectedBody = rb;
            rb.drag = 80;
            rb.angularDrag = 80;
        }
        else if (mode == Mode.Kinematic)
        {
            attachedObject.transform.parent = Camera.main.transform;
            rb.isKinematic = true;
        }        
    }

    public void DetachGameObject()
    {
        if (attachedObject == null) return;
        var rb = attachedObject.GetComponent<Rigidbody>();

        if (mode == Mode.Joint)
        {
            joint.connectedBody = null;
            rb.drag = 0;
            rb.angularDrag = 0.05f;
        }
        else if (mode == Mode.Kinematic)
        {
            attachedObject.transform.parent = FindObjectOfType<MoldChecker>().transform;
            rb.isKinematic = false;
        }

        attachedObject = null;
    }
}
