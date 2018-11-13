using System.Collections;
using UnityEngine;

public class LaserGopherBehavior : ExtendMonoBehaviour {
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
    public GameObject sprinkler;
    private bool isPlayerMoving = false;

    public GameObject laserEyes;

    private void init()
    {
        faceAwayRot = transform.rotation;
        player = PlayerHelper.GetPlayer();
        freezeScript = FreezeHelper.GetFreezeScript();
        SetUnderground();
    }

	private void Start ()
	{
	    init();
	}

    private void OnEnable()
    {
        init();
    }
    
    // Gopher behavior when it detects movement
    public void AlertOn()
    {
        sprinkler.transform.rotation = transform.rotation;
        sprinkler.SetActive(true);
    }

    public void AlertOff()
    {
        sprinkler.SetActive(false);
    }

    private void SetUnderground()
    {
        StartCoroutine("UndergroundThenRise", undergroundTime);
    }

    
    private IEnumerator UndergroundThenRise()
    {
        // Underground
        transform.rotation = faceAwayRot;
        transform.position = NewSpawnLocation();
        yield return new WaitForSeconds(undergroundTime);
        Debug.Log("rise");
        while (transform.position.y < verticalHeight)
        {
            transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime, Space.World);
            yield return null;
        }

        StartCoroutine(RotateAndFreeze());
    }

    private IEnumerator RotateAndFreeze()
    {
        var playerLocation = player.transform.position;
        var targetDir = playerLocation - transform.position;
        destRot = Quaternion.LookRotation(targetDir, Vector3.up);
        while (true)
        {
            var rotation = Quaternion.RotateTowards(transform.rotation, destRot, rotateSpeed * Time.deltaTime);
            var diff = Quaternion.Angle(destRot, transform.rotation);
            if (diff == 0)
            {
                break;
            }
            transform.rotation = rotation;
            yield return null;
        }
        
        // Freeze
        freezeScript.BeginFreezeTime(gameObject.GetComponent<LaserGopherBehavior>());
        laserEyes.SetActive(true);
        InvokeAction(EndFreeze, freezeTime);
    }

    private void EndFreeze()
    {
        laserEyes.SetActive(false);
        freezeScript.EndFreezeTime();
        // and start to sink
        StartCoroutine(Sink());
    }

    private IEnumerator Sink()
    {
        while (transform.position.y > verticalDepth)
        {
            transform.Translate(Vector3.down * verticalSpeed * Time.deltaTime, Space.World);
            yield return null;
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
