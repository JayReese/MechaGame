using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AmmoGUI : MonoBehaviour {

    Player player;

    //MachiGUI images
    [SerializeField]
    GameObject MachiGui;
    [SerializeField]
    Image Ammo;
    [SerializeField]
    Image ReloadGui;

    //BaemGui stuff
    [SerializeField]
    GameObject BaemGui;
    [SerializeField]
    Image chargeGui;

    public float ammoFill;
    public float ReloadGuiFill;
    public float reloadSpeed;

    Weapon_MachineGunner personalWep;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        MachiGui = transform.FindChild("MachiGui").gameObject;
        MachiGui.SetActive(false);
        BaemGui = transform.FindChild("BaemGui").gameObject;
        BaemGui.SetActive(false);
        switch (player.GUIDisplayID)
        {
            case 0:
                MachiGui.gameObject.SetActive(true);
                break;
            case 1:
                BaemGui.gameObject.SetActive(true);
                break;
            case 2:
                break;
            default:
                break;
        }
    }


	// Use this for initialization
	void Start () {
        switch (player.GUIDisplayID)
        {
            case 0:
                Ammo = MachiGui.transform.GetChild(0).GetComponent<Image>();
                ReloadGui = Ammo.transform.GetChild(0).GetComponent<Image>();
                ReloadGuiFill = 0;
                ammoFill = 1;
                Ammo.enabled = true;
                Ammo.gameObject.SetActive(true);
                ReloadGui.fillAmount = ReloadGuiFill;
                ReloadGui.gameObject.SetActive(false);
                ReloadGui.enabled = false;
                reloadSpeed = 1f;
                break;
            case 1:
                chargeGui = BaemGui.transform.GetChild(0).GetComponent<Image>();
                ammoFill = 1;
                chargeGui.fillAmount = ammoFill;
                reloadSpeed = 2f;
                break;
            case 2:
                break;
            default:
                break;
        }
        
	}
	
	// Update is called once per frame
	void Update () {

        switch (player.GUIDisplayID)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ammoFill -= (1f / 14f);
                }
                Ammo.fillAmount = ammoFill;
                if (ammoFill <= 0)
                {
                    if (ReloadGui.enabled == false)
                        ChangeActiveGUI(ReloadGui);
                }
                if (ReloadGui.enabled)
                {
                    ReloadGuiFill += Time.deltaTime / reloadSpeed;
                    ReloadGui.fillAmount = ReloadGuiFill;
                    if (ReloadGuiFill >= 1)
                    {
                        ammoFill = 1;
                        ReloadGuiFill = 0;
                        ReloadGui.fillAmount = ReloadGuiFill;
                        ChangeActiveGUI(ReloadGui);
                    }
                }
                break;
            case 1:
                if(ammoFill < 1)
                ammoFill += Time.deltaTime / reloadSpeed;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if(ammoFill >= .25f)
                        ammoFill -= (1f / 4f);
                }
                chargeGui.fillAmount = ammoFill;
                break;
            case 2:
                break;
            default:
                break;
        }
        

	}

    void ChangeActiveGUI(Image g)
    {
        if (g.isActiveAndEnabled)
        {
            g.enabled = false;
            g.gameObject.SetActive(false);
        }
        else
        {
            g.enabled = true;
            g.gameObject.SetActive(true);
        }
    }

}
