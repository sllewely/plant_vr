using UnityEngine;

public class LaserGopherBehavior : MonoBehaviour {
    private enum GopherState { Underground, Rise, Rotate, Detect, Sink }
    private GopherState gopherState = GopherState.Underground;
    float countDown;
    private GameObject player;

    public Vector2[] spawnLocations;
    
    // Underground State variables
    public float undergroundTime;
    public Quaternion faceAwayRot;
    
    // Rise and Sink state variables
    public float verticalSpeed;
    public float verticalHeight;
    public float verticalDepth;

    // Rotate State Variables
    public float rotateSpeed;
    private Quaternion destRot;
    
    // Detect state variables
    public float freezeTime;
    private FreezeTime freezeScript;
    public ParticleSystem sprinkler;
    private bool isPlayerMoving = false;

	private void Start ()
	{
	    faceAwayRot = transform.rotation;
	    player = PlayerHelper.GetPlayer();
	    freezeScript = FreezeHelper.GetFreezeScript();
	    SetUnderground();
	    sprinkler.Pause();
	}

    // Gopher behavior when it detects movement
    public void AlertOn()
    {
        sprinkler.transform.rotation = transform.rotation;
        sprinkler.Play();
    }

    public void AlertOff()
    {
        sprinkler.Pause();
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
            default:
                Debug.LogWarning("Gopher in unhandled state: " + gopherState);
                break;
        }
    }

    private void SetUnderground()
    {
        gopherState = GopherState.Underground;
        transform.rotation = faceAwayRot;
        countDown = undergroundTime;
        transform.position = NewSpawnLocation();
    }

    private void Underground()
    {
        if (countDown <= 0)
        {
            SetRise();
        }
    }
    
    private void SetRise()
    {
//        Debug.Log("rising at " + transform.position + ": " + name);
        gopherState = GopherState.Rise;
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
        var playerLocation = player.transform.position;
        var targetDir = playerLocation - transform.position;
        destRot = Quaternion.LookRotation(targetDir, Vector3.up);
    }

    private void Rotate()
    {
        var rotation = Quaternion.RotateTowards(transform.rotation, destRot, rotateSpeed * Time.deltaTime);
        var diff = Quaternion.Angle(destRot, transform.rotation);
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
        freezeScript.BeginFreezeTime(gameObject.GetComponent<LaserGopherBehavior>());
        countDown = freezeTime;
    }

    private void Detect()
    {
        // Determine if Detect state is done
        if (!(countDown <= 0)) return;
        freezeScript.EndFreezeTime();
        gopherState = GopherState.Sink;
    }

    private void Sink()
    {
        transform.Translate(Vector3.down * verticalSpeed * Time.deltaTime, Space.World);
        
        // Determine if Sink state is done
        if (transform.position.y <= verticalDepth)
        {
            SetUnderground();
        }
    }

    private Vector3 NewSpawnLocation()
    {
        if (spawnLocations.Length == 0)
        {
            return transform.position;
        }

        var spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Length)];
        return new Vector3(spawnLocation.x, transform.position.y, spawnLocation.y);
    }
}
