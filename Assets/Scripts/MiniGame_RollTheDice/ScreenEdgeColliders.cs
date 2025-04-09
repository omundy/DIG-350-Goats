// adds EdgeCollider2D colliders to screen edges
// only works with orthographic camera
// https://github.com/UnityCommunity/UnityLibrary/blob/master/Assets/Scripts/2D/Colliders/ScreenEdgeColliders.cs

//Includes two different ways of implementation into your project
//One is a method that uses cached fields on Awake() that requires this entire class but is more slightly more efficient (should use this if you plan to use the method in Update())
//The other is a standalone method that doesn't need the rest of the class and can be copy-pasted directly into any project but is slightly less efficient

using Unity.VisualScripting;
using UnityEngine;

namespace UnityLibrary
{
    public class ScreenEdgeColliders : MonoBehaviour
    {
        public Camera cam;
        public Transform wallTop;
        public Transform wallRight;
        public Transform wallBottom;
        public Transform wallLeft;
        public Transform floor;
        public Transform lid;

        float boxDepth = 5;

        public Bounds bounds;

        void Awake()
        {
            if (cam == null) Debug.LogError("cam not found, failed to create edge colliders");
            else cam = Camera.main;
            if (!cam.orthographic) Debug.LogError("cam is not Orthographic, failed to create edge colliders");
        }

        void FixedUpdate()
        {
            UpdateColliders();
        }

        void UpdateColliders()
        {
            bounds = new Bounds(new Vector3(0, 0, 0), cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane)));

            float height = boxDepth / 2;

            wallTop.localPosition = new Vector3(bounds.center.x, height, bounds.extents.y * 2);
            wallBottom.localPosition = new Vector3(bounds.center.x, height, bounds.extents.y * -2);
            wallRight.localPosition = new Vector3(bounds.extents.x * 2, height, bounds.center.y);
            wallLeft.localPosition = new Vector3(bounds.extents.x * -2, height, bounds.center.y);
            floor.localPosition = new Vector3(bounds.center.x, 0, bounds.center.y);
            lid.localPosition = new Vector3(bounds.center.x, boxDepth, bounds.center.y);

            wallTop.localScale = new Vector3(bounds.extents.x * 4, boxDepth, 1);
            wallBottom.localScale = new Vector3(bounds.extents.x * 4, boxDepth, 1);
            wallRight.localScale = new Vector3(1, boxDepth, bounds.extents.y * 3);
            wallLeft.localScale = new Vector3(1, boxDepth, bounds.extents.y * 3);
            floor.localScale = new Vector3(bounds.extents.x * 4, 1, bounds.extents.y * 3);
            lid.localScale = new Vector3(bounds.extents.x * 4, 1, bounds.extents.y * 3);
        }
    }
}
