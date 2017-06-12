using UnityEngine;
using System.Collections;

public class Player_MachineGunner : Player
{
    new void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    new void Start()
    {
        FirstSubWeaponCooldown = 3f;
        SecondSubWeaponCooldown = 5f;

        Health = 10;

        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    new void FixedUpdate()
    {
        PerformCommandExecution();
        CheckIfUsingMelee();
        ManageCooldownTimers();

        base.FixedUpdate(); 
    }

    protected override void UseFirstSubweapon()
    {
        if (FirstSubWeaponCooldownTimer <= 0) FirstSubweapon_ShedArmor();

        base.UseFirstSubweapon();
    }

    protected override void UseSecondSubweapon()
    {
        if (SecondSubWeaponCooldownTimer <= 0) SecondSubweapon_ArmorShield();
        base.UseSecondSubweapon();
    }

    #region Machi's subweapons.
    private void FirstSubweapon_ShedArmor()
    {
        StartCoroutine(ShedArmorOverTime());
    }

    private IEnumerator ShedArmorOverTime()
    {
        #region commented out while loop that did this but just every 0.3s.
        //while(ArmorPiecesReference.ActiveChildrenCount() > 0)
        //{
        //    yield return new WaitForSeconds(0.3f);

        //    for(byte i = 0; i < ArmorPiecesReference.childCount; i++)
        //    {
        //        if(ArmorPiecesReference.GetChild(i).gameObject.activeSelf)
        //        {
        //            ArmorPiecesReference.GetChild(i).gameObject.SetActive(false);
        //            break;
        //        }
        //    }

        //    MoveSpeedModifier += 0.3f;
        //    Debug.Log("Machi's current movement speed modifier: " + MoveSpeedModifier);
        //}
        #endregion

        yield return new WaitForSeconds(0.3f);
        MachineGunner_RemoveArmor();
        MoveSpeedModifier += 0.3f;
            //Debug.Log("Machi's current movement speed modifier: " + MoveSpeedModifier);
    }

    private void SecondSubweapon_ArmorShield()
    {
        while(ArmorPiecesReference.ActiveChildrenCount() > 0)
        {
            MachineGunner_RemoveArmor();

            StartCoroutine(DeployArmorShield());
        }
    }

    private IEnumerator DeployArmorShield()
    {
        yield return new WaitForSeconds(0.2f);

        if (!UniquePartsReference.GetChild(0).gameObject.activeSelf)
        {
            UniquePartsReference.GetChild(0).gameObject.SetActive(true);
            Debug.Log("Machi barrier activated.");
        }
        else
        {
            UniquePartsReference.GetChild(0).gameObject.GetComponent<BoxCollider>().size = new Vector3(UniquePartsReference.GetChild(0).gameObject.GetComponent<BoxCollider>().size.x + 2, UniquePartsReference.GetChild(0).gameObject.GetComponent<BoxCollider>().size.y + 0.5f, UniquePartsReference.GetChild(0).gameObject.GetComponent<BoxCollider>().size.z);

            Debug.Log("Barrier increased in size");
            UniquePartsReference.GetChild(0).GetComponent<DamageableObject>().ReceiveDamage(-3);
        }
    }

    private void MachineGunner_RemoveArmor()
    {
        if (ArmorPiecesReference.ActiveChildrenCount() > 0)
        {
            for (byte i = 0; i < ArmorPiecesReference.childCount; i++)
            {
                if (ArmorPiecesReference.GetChild(i).gameObject.activeSelf)
                {
                    ArmorPiecesReference.GetChild(i).gameObject.SetActive(false);
                    break;
                }
            }
        }
    }
    #endregion
}
