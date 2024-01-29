using UnityEngine;
using System.Collections;

public class Gizmo : MonoBehaviour {
	public float gizmoSize = .75f;
	public Color gizmoColor = Color.yellow;
	// Use this for initialization
	void OnDrawGizmos() {
		Gizmos.color = gizmoColor;
		Gizmos.DrawWireSphere (transform.position, gizmoSize);
	}

}

public class LinkPoseX : MonoBehaviour
{

    public GameObject GamePose1, GamePose2;
    public Vector3 position2, position1;
    public Vector3 pos_tempX;
    Vector3 position_difference, size_temp;
    float C, B;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        position_difference = transform.position;
        size_temp = transform.localScale;
        pos_tempX = transform.position;
        position1 = GamePose1.GetComponent<Pose>().pos;
        position2 = GamePose2.GetComponent<Pose2>().pos2;

        float distance = position2.x - position1.x;
        size_temp.z = distance / 2;
        pos_tempX.z = position2.z;
        pos_tempX.x = position1.x;
        pos_tempX.y = position1.y;
        transform.position = pos_tempX;
        transform.localScale = size_temp;
    }
}

public class LinkPoseY : MonoBehaviour
{

    public GameObject GamePose1, GamePose2;
    public Vector3 position2, position1;
    public Vector3 pos_tempY;
    Vector3 position_difference, size_temp;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        position_difference = transform.position;
        size_temp = transform.localScale;
        position1 = GamePose1.GetComponent<Pose>().pos;
        position2 = GamePose2.GetComponent<Pose2>().pos2;

        float distance = position1.y - position2.y;
        size_temp.z = distance / 2;
        pos_tempY = position2;

        transform.position = pos_tempY;
        transform.localScale = size_temp;
    }
}

public class LinkPoseY : MonoBehaviour
{

    public GameObject GamePose1, GamePose2;
    public Vector3 position2, position1;
    public Vector3 pos_tempY;
    Vector3 position_difference, size_temp;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        position_difference = transform.position;
        size_temp = transform.localScale;
        position1 = GamePose1.GetComponent<Pose>().pos;
        position2 = GamePose2.GetComponent<Pose2>().pos2;

        float distance = position1.y - position2.y;
        size_temp.z = distance / 2;
        pos_tempY = position2;

        transform.position = pos_tempY;
        transform.localScale = size_temp;
    }
}

public class Pose : MonoBehaviour {

	public Vector3 pos;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		pos = transform.position;
		//Debug.Log("pos is " + pos);
	}
}

public class Pose2 : MonoBehaviour {

	public Vector3 pos2;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		pos2 = transform.position;
		//Debug.Log("pos2 is " + pos2);
	}
}

public class X_Text : MonoBehaviour
{
    public GameObject GamePose1, GamePose2;
    public GameObject LenthX;
    public Vector3 position2, position1;
    Vector3 position_difference, pos_temp, size_temp;
    float C, B;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        position_difference = transform.position;
        pos_temp = transform.position;
        position1 = GamePose1.GetComponent<Pose>().pos;
        position2 = GamePose2.GetComponent<Pose2>().pos2;

        double distance = position2.x - position1.x;
        //distance = Mathf.RoundToInt(distance);
        distance = 2.54*distance ;
        GetComponent<TextMesh>().text = distance.ToString("f1");

        pos_temp = LenthX.GetComponent<LinkPoseX>().pos_tempX;
        pos_temp.x = pos_temp.x + distance/2;
        transform.position = pos_temp;
    }
}


public class Y_Text : MonoBehaviour
{
    public GameObject GamePose1, GamePose2;
    public GameObject LenthY;
    public Vector3 position2, position1;
    Vector3 position_difference, pos_temp, size_temp;
    float C, B;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        position_difference = transform.position;
        pos_temp = transform.position;
        position1 = GamePose1.GetComponent<Pose>().pos;
        position2 = GamePose2.GetComponent<Pose2>().pos2;

        float distance = position1.y - position2.y;
        GetComponent<TextMesh>().text = distance.ToString("f1");
        pos_temp = LenthY.GetComponent<LinkPoseY>().pos_tempY;
        pos_temp = position2;
       // pos_temp.y = Mathf.Abs((position2.y - position1.y) / 2);
        transform.position = pos_temp;
    }
}

public class Z_Text : MonoBehaviour
{
    public GameObject GamePose1, GamePose2;
    public GameObject LenthZ;
    public Vector3 position2, position1;
    Vector3 position_difference, pos_temp, size_temp;
    float C, B;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        position_difference = transform.position;
        pos_temp = transform.position;
        position1 = GamePose1.GetComponent<Pose>().pos;
        position2 = GamePose2.GetComponent<Pose2>().pos2;

        float distance = position2.z - position1.z;
        GetComponent<TextMesh>().text = distance.ToString("f1");
        pos_temp = LenthZ.GetComponent<LinkPoseZ>().position1;
        pos_temp.z = pos_temp.z + distance / 2;
        transform.position = pos_temp;
    }
}