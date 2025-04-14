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
        public BoxCollider limit;
        public Transform sphere;

        float boxDepth = 5;

        public Bounds bounds;

        public ScreenOrientation currentOrientation;

        void Awake()
        {
            if (cam == null)
                cam = Camera.main;
            if (!cam.orthographic)
                Debug.LogError("cam is not Orthographic, failed to create edge colliders");

            // lock screen rotation

            // Screen.orientation = ScreenOrientation.LandscapeLeft;

            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }

        void Start()
        {
            UpdateColliders();
        }

        void Update()
        {
            currentOrientation = Screen.orientation;
        }

        // adds EdgeCollider2D colliders to screen edges
        // only works with orthographic camera
        // https://github.com/UnityCommunity/UnityLibrary/blob/master/Assets/Scripts/2D/Colliders/ScreenEdgeColliders.cs

        void UpdateColliders()
        {
            // create bounds matching screen size
            bounds = new Bounds(
                new Vector3(0, 0, 0),
                cam.ScreenToWorldPoint(
                    new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane)
                )
            );

            float height = boxDepth / 2;

            /// _______
            /// |     |
            /// |     |
            /// |     |
            /// _______

            // 	The extents of a Bounds == half of the size of the Bounds.


            // top / bottom position
            wallTop.localPosition = new Vector3(bounds.center.x, height, bounds.extents.z * 2);
            wallBottom.localPosition = new Vector3(bounds.center.x, height, bounds.extents.z * -2);

            wallRight.localPosition = new Vector3(bounds.extents.x * 2, height, bounds.center.y);
            wallLeft.localPosition = new Vector3(bounds.extents.x * -2, height, bounds.center.y);

            floor.localPosition = new Vector3(bounds.center.x, 0, bounds.center.y);
            lid.localPosition = new Vector3(bounds.center.x, boxDepth, bounds.center.y);

            wallTop.localScale = new Vector3(bounds.extents.x * 4, boxDepth, 1);
            wallBottom.localScale = new Vector3(bounds.extents.x * 4, boxDepth, 1);

            wallRight.localScale = new Vector3(1, boxDepth, bounds.extents.z * 4);
            wallLeft.localScale = new Vector3(1, boxDepth, bounds.extents.z * 4);

            floor.localScale = new Vector3(bounds.extents.x * 4, 1, bounds.extents.y * 2);
            lid.localScale = new Vector3(bounds.extents.x * 4, 1, bounds.extents.y * 2);

            // limit bounds
            limit.transform.localPosition = new Vector3(
                bounds.center.x,
                boxDepth / 2,
                bounds.center.y
            );
            limit.transform.localScale = new Vector3(
                (bounds.extents.x * 4) + 1,
                (bounds.extents.z * 2) + 1,
                (bounds.extents.y * 2) + 1
            );
        }

        private void FixedUpdate()
        {
            if (GyroAffectsGravity.gameEnabled && !IsPointWithinCollider(limit, sphere.position))
            {
                Debug.Log("XXX FELL OUTSIDE XXX");
                sphere.position = bounds.center;
            }
        }

        public static bool IsPointWithinCollider(BoxCollider collider, Vector3 point)
        {
            return (collider.ClosestPoint(point) - point).sqrMagnitude
                < Mathf.Epsilon * Mathf.Epsilon;
        }
    }
}
