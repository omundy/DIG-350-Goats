using System.Threading.Tasks;
using UnityEngine;

public class GyroAffectsGravity : MonoBehaviour
{
    public Vector3 gravity;
    public Quaternion initialRotation;
    public bool gyroEnabled = false;
    public bool compassEnabled = false;

    void Start() => Init();
    async void Init()
    {
        // Screen.orientation = ScreenOrientation.LandscapeLeft;

        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.orientation = ScreenOrientation.AutoRotation;


        if (SystemInfo.supportsGyroscope)
            await WaitForGyro();
        else
            Debug.LogError("Gyroscope not supported on this device.");
    }


    async Task<bool> WaitForGyro()
    {
        // 1. attempt enable
        Input.gyro.enabled = true;
        Input.compass.enabled = true;
        int maxWait = 100;
        while (maxWait > 0)
        {
            // 2. wait
            await Awaitable.WaitForSecondsAsync(0.1f);
            // 3. check (compass takes longer to warm up)
            gyroEnabled = Input.gyro.enabled;
            compassEnabled = Input.compass.enabled;

            // 4. check if sensors were enabled
            if (gyroEnabled && compassEnabled)
            {
                Debug.Log("EnableSensors() => gyro and compass enabled");
                // Capture the initial rotation as an inverse, so we can apply it as an offset
                initialRotation = ConvertGyroRotation(Input.gyro.attitude);
                return true;
            }
            // if not then loop
            Input.gyro.enabled = true;
            Input.compass.enabled = true;
            maxWait--;
        }
        return false;
    }


    void Update()
    {
        if (gyroEnabled)
        {
            // Apply the offset so the object starts with zeroed rotation
            transform.rotation = initialRotation * ConvertGyroRotation(Input.gyro.attitude);

            // Physics.gravity = new Vector3(-2f, -4f, 2f);
            // gravity = Vector3.Normalize((initialRotation * ConvertGyroRotation(Input.gyro.attitude)).eulerAngles)*4;
            // Physics.gravity = gravity;
        }
    }

    // Convert the gyroscope rotation to Unity's coordinate system
    private Quaternion ConvertGyroRotation(Quaternion q)
    {
        return new Quaternion(q.x, q.z, q.y, -q.w);
    }
}
