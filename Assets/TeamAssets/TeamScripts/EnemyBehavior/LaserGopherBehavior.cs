using UnityEngine;

public class LaserGopherBehavior : MonoBehaviour {
    public enum GopherState { Underground, Rise, Rotate, Detect, Sink }
    public GopherState gopherState = GopherState.Underground;
    public float undergroundTime;
    public float verticalSpeed;
    public float verticalHeight;
//    public float randomCircleRadius = 2f;
    public float rotateSpeed;
    public float freezeTime;

    float countDown;
    private Vector3 playerLocation;

	// Use this for initialization
	private void Start () {
        // Start underground
        gopherState = GopherState.Underground;
        countDown = undergroundTime;
//        startPosition = transform.position;
	    
	    // I guess we only need to calculate this once since players don't move
	    playerLocation = GameObject.Find("Player").transform.position;
	}


    // Pick location
    // Detect player location
    // Begin rotating towards player
    // Stay facing, detect player motion
    // Sink
    // hide and pick new location
	
	// Update is called once per frame
	private void Update () {
        countDown -= Time.deltaTime;
        switch (gopherState)
        {
            case GopherState.Underground:
                if (countDown <= 0)
                {
                    SetRise();
                }
                break;
            case GopherState.Rise:
                if (transform.position.y >= verticalHeight)
                {
                    SetRotate();
                }
                else
                {
                    // maybe rise to position and lower to position
                    transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime, Space.World);
                }
                break;
            case GopherState.Rotate:
                
                var targetDir = playerLocation - transform.position;
                var destRot = Quaternion.LookRotation(targetDir, Vector3.up);
                Debug.Log(destRot);
                var rotation = Quaternion.RotateTowards(transform.rotation, destRot, rotateSpeed * Time.deltaTime);
                var diff = Quaternion.Angle(destRot, transform.rotation);
                Debug.Log("difference: " + diff);
                transform.rotation = rotation;

//                // Will contain the information of which object the raycast hit
//                RaycastHit hit;
// 
//                // if raycast hits, it checks if it hit an object with the tag Player
//                if (Physics.Raycast(transform.position, transform.forward, out hit, 100f) &&
//                    hit.collider.gameObject.CompareTag("Player"))
//                {
//                    SetDetect();
//                }

                if (diff == 0)
                {
                    SetDetect();
                }
                break;
                
                
//            case MoleState.Lower:
//                transform.position = transform.position + (new Vector3(0, -verticalSpeed, 0) * Time.deltaTime);
//                break;
        }
    }

    private void SetRise()
    {
        gopherState = GopherState.Rise;
        Debug.Log("Rising");
    }

    private void SetRotate()
    {
        gopherState = GopherState.Rotate;
        Debug.Log("rotating");
    }

    private void SetDetect()
    {
        gopherState = GopherState.Detect;
        countDown = freezeTime;
        Debug.Log("detecting");
    }

//    void FixedUpdate()
//    {
//
//    }
//
//    void NewLocation()
//    {
//        Vector2 randomCirclePos = Random.insideUnitCircle * randomCircleRadius;
//        transform.position = startPosition + new Vector3(randomCirclePos.x, 0, randomCirclePos.y);
//        // Debug.Log("new position is " + transform.position);
//    }

//    IEnumerator MoleMove()
//    {
//        for (int i = 0; true; i = (i + 1) % 3)
//        {
//            moleState = (GopherState)i;
//            // Debug.Log("mole state is " + moleState);
//            if (moleState == GopherState.Rest) {
//                NewLocation();
//            }
//            yield return new WaitForSeconds(timeInterval);
//        }
//
//    }
}
