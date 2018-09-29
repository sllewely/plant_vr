using UnityEngine;

public class LaserGopherBehavior : MonoBehaviour {
    public enum GopherState { Underground, Rise, Rotate, Detect, Sink }
    private GopherState gopherState = GopherState.Underground;
    public float undergroundTime;
    public float verticalSpeed;
    public float verticalHeight;
    public float rotateSpeed;
    private Quaternion destRot;
    public float freezeTime;
    public float verticalDepth;

    float countDown;
    private Vector3 playerLocation;

	private void Start ()
	{
	    SetUnderground();
	}

	private void Update () {
        countDown -= Time.deltaTime;
        switch (gopherState)
        {
            case GopherState.Underground:
                Underground();
                break;
            case GopherState.Rise:
                Rise();
                break;
            case GopherState.Rotate:
                Rotate();
                break;
            case GopherState.Detect:
                Detect();
                break;
            case GopherState.Sink:
                Sink();
                break;
        }
    }

    private void SetRise()
    {
        gopherState = GopherState.Rise;
        Debug.Log("Rising");
    }

    private void Rise()
    {
        transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime, Space.World);
        
        // Determine if Rise state is done
        if (transform.position.y >= verticalHeight)
        {
            SetRotate();
        }
    }

    private void SetRotate()
    {
        gopherState = GopherState.Rotate;
        
        // Recalculate player location at the state of each rotation cycle
        playerLocation = PlayerHelper.GetPlayerLocation();
        var targetDir = playerLocation - transform.position;
        destRot = Quaternion.LookRotation(targetDir, Vector3.up);
        Debug.Log("rotating");
    }

    private void Rotate()
    {
        var rotation = Quaternion.RotateTowards(transform.rotation, destRot, rotateSpeed * Time.deltaTime);
        var diff = Quaternion.Angle(destRot, transform.rotation);
        Debug.Log("difference: " + diff);
        transform.rotation = rotation;
        
        // Determine if rotation state is done
        if (diff == 0)
        {
            SetDetect();
        }
    }

    private void SetDetect()
    {
        gopherState = GopherState.Detect;
        countDown = freezeTime;
        Debug.Log("detecting");
    }

    private void Detect()
    {
        if (countDown < 0)
        {
            gopherState = GopherState.Sink;
        }
    }

    private void Sink()
    {
        transform.Translate(Vector3.down * verticalSpeed * Time.deltaTime, Space.World);
        
        // Determine if Rise state is done
        if (transform.position.y <= verticalDepth)
        {
            SetUnderground();
        }
    }

    private void SetUnderground()
    {
        gopherState = GopherState.Underground;
        countDown = undergroundTime;
    }

    private void Underground()
    {
        if (countDown <= 0)
        {
            SetRise();
        }
    }
    

}
