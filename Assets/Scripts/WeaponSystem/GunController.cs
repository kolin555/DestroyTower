using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum GunType
{
    shotgun1,
    
}


public class GunController : MonoBehaviour
{
    public static GunController gunInstance;
    //存放右手枪支
    public List<GameObject> rightGunList = new List<GameObject>();
    public GameObject currentRightGun;
    public int currentRightId;

    //存放左手枪支
    public List<GameObject> leftGunList = new List<GameObject>();
    public GameObject currentLeftGun;
    public int currentLeftId;



    private void Awake()
    {
        gunInstance = this;
        var gunRightObject = GameObject.FindGameObjectWithTag("RightHand");
        foreach(Transform gun in gunRightObject.transform)
        {
            if (gun.gameObject.activeInHierarchy == true)
            {
                currentRightGun = gun.gameObject;
                currentRightId = gun.GetSiblingIndex();
            }
            rightGunList.Add(gun.gameObject);
        }

        var gunLeftObject = GameObject.FindGameObjectWithTag("LeftHand");
        foreach (Transform gun in gunLeftObject.transform)
        {
            if (gun.gameObject.activeInHierarchy == true)
            {
                currentLeftGun = gun.gameObject;
                currentLeftId = gun.GetSiblingIndex();
            }
            leftGunList.Add(gun.gameObject);
        }

    }
    private void OnDestroy()
    {
        gunInstance = null;
    }
    private void Update()
    {
        
    }
    public void AddGunToRightList(string name)
    {
        Debug.Log(name);
        for (int i = 0; i < rightGunList.Count; i++)
        {
            if (name == rightGunList[i].name)
            {
                currentRightGun = rightGunList[i];
                currentRightId = i;
                return;
            }
        }

        var rightgun = Instantiate(Resources.Load<GameObject>("Gun/" + name));
        var rightHand = GameObject.FindGameObjectWithTag("RightHand");

        rightgun.name=rightgun.name.Split('(')[0];

        rightgun.transform.SetParent(rightHand.transform, false);
        rightGunList.Add(rightgun);
        var lastGun = currentRightGun;
        lastGun.SetActive(false);
        currentRightGun = rightgun;
        currentRightId = rightGunList.Count - 1;
    }
    public void ChangeRightGun()
    {
        var currentgun = currentRightGun;
        currentgun.SetActive(false);

        var id = currentRightId;
        var count = rightGunList.Count;
        var index = (id == count - 1) ? 0 : id + 1;
        var gun =rightGunList[index];
        gun.SetActive(true);

        currentRightGun = gun;
        currentRightId = index;
    }
    public void AddGunToLeftList(string name)
    {
        
        Debug.Log(name);

        for (int i = 0; i < leftGunList.Count; i++)
        {
            if (name == leftGunList[i].name)
            {
                currentLeftGun = leftGunList[i];
                currentLeftId = i;
                return;
            }
        }

        var leftgun = Instantiate(Resources.Load<GameObject>("Gun/" + name));
        var leftHand = GameObject.FindGameObjectWithTag("LeftHand");

        leftgun.name = leftgun.name.Split('(')[0];

        leftgun.transform.SetParent(leftHand.transform, false);
        leftGunList.Add(leftgun);
        var lastGun = currentLeftGun;
        lastGun.SetActive(false);
        currentLeftGun = leftgun;
        currentLeftId = leftGunList.Count - 1;
    }
    public void ChangeLeftGun()
    {
        var currentgun = currentLeftGun;
        currentgun.SetActive(false);

        var id = currentLeftId;
        var count = leftGunList.Count;
        var index = (id == count - 1) ? 0 : id + 1;
        var gun = leftGunList[index];
        gun.SetActive(true);

        currentLeftGun = gun;
        currentLeftId = index;
    }

}
