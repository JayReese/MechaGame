using UnityEngine;
using System.Collections;

public class SpiderController : MonoBehaviour
{
    public GameObject[] buildings;
    public GameObject ClosestBuilding;
    Spider spider;
    public Vector3 TargetDestination;
    public float Speed;
    // Use this for initialization
    void Awake()
    {
        spider = new Spider();
        buildings = GameObject.FindGameObjectsWithTag("Building");
    }
    void Start()
    {
        ClosestBuilding = buildings[0];
        for (int i = 1; i < buildings.Length; i++)
        {
            if ((this.transform.position - ClosestBuilding.transform.position).magnitude >
                (this.transform.position - buildings[i].transform.position).magnitude)
            {
                ClosestBuilding = buildings[i];
            }
        }
        TargetDestination = ClosestBuilding.transform.GetChild(0).transform.position;
        spider.ChangeStateExt(SpiderMechState.Traveling);
    }
    // Update is called once per frame
    void Update()
    {
        if (spider.spiderState == SpiderMechState.Traveling)
        {
            if (Mathf.Abs(this.transform.position.x - TargetDestination.x) > .2)
            {
                //Debug.Log("Moving x");
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(TargetDestination.x, transform.position.y, transform.position.z), Speed * Time.deltaTime);
            }
            else if ((Mathf.Abs(this.transform.position.z - TargetDestination.z) > .2))
            {
                //Debug.Log("Moving z");
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(TargetDestination.x, transform.position.y, TargetDestination.z), Speed * Time.deltaTime);
            }
            else
            {
                //Debug.Log("Moving y");
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(TargetDestination.x, TargetDestination.y, TargetDestination.z), Speed * Time.deltaTime);
            }
            CheckForArrival();
            Debug.Log(spider.spiderState);
        }
    }

    private void CheckForArrival()
    {
        if ((this.transform.position - TargetDestination).magnitude <= .4)
        {
            spider.ChangeStateExt(SpiderMechState.Armed);
            this.transform.position = TargetDestination;
        }
    }

}
