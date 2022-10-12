using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerShoot : MonoBehaviour
{
    public enum Weapon
    {
        Hook,
        Rifle
    }
    [SerializeField] private MultiAimConstraint bodyAimConstraint;
    [SerializeField] private Animator animator;

    [SerializeField] private Transform bulletContainer;
    [SerializeField] private Transform aimTarget;
    [SerializeField] private Vector3 runAimingTargetPos;
    [SerializeField] private Vector3 shootAimingTargetPos;

    [SerializeField] private Hook hook;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float rateFire;

    private int maxFifleAmmo = 50;
    private int currentRifleAmmo = 0;


    public Weapon currentWeapon;
    private float fireTime = 0;
    public bool IsShooting { get; set; }

    private void Start()
    {
        currentWeapon = Weapon.Hook;
    }

    public void ShootHook()
    {
        if (hook.isActiveAndEnabled)
            return;
        IsShooting = true;
        hook.gameObject.SetActive(true);
        hook.gameObject.transform.position = shootPoint.position;
    }

    private void Update()
    {
        HandleInputs();

        if (IsShooting)
        {
            animator.SetBool("IsShooting", IsShooting);
            aimTarget.localPosition = Vector3.Lerp(aimTarget.localPosition, shootAimingTargetPos, Time.deltaTime * 30f);
            bodyAimConstraint.weight = 1;          
            animator.SetBool("IsRunning", false);
        }
        else
        {
            animator.SetBool("IsShooting", IsShooting);
            bodyAimConstraint.weight = 0;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentWeapon = Weapon.Hook;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            currentRifleAmmo = maxFifleAmmo;
            currentWeapon = Weapon.Rifle;
        }

        if (Input.GetMouseButton(0) && currentWeapon == Weapon.Rifle && currentRifleAmmo > 0)
        {
            IsShooting = true;

        }
        else if(Input.GetMouseButtonUp(0) && currentWeapon == Weapon.Rifle)
        {
            IsShooting = false;
        }

        if (IsShooting && currentWeapon == Weapon.Rifle)
        {
            if (fireTime < rateFire)
            {
                fireTime += Time.deltaTime;
            }
            else
            {
                //Shoot
                fireTime = 0;
                IsShooting = true;
                Instantiate(bullet, shootPoint.position, Quaternion.identity,bulletContainer);
                currentRifleAmmo--;
                if(currentRifleAmmo == 0)
                {
                    currentWeapon = Weapon.Hook;
                    IsShooting = false;
                }
            }
        }
    }

    public void ShootRifle()
    {

    }

    public Hook GetHook() => hook;

    private void HandleInputs()
    {
        //Can shoot only when hook is disabled and you clicked
        if (currentWeapon == PlayerShoot.Weapon.Hook && Input.GetMouseButtonDown(0) && !GetHook().isActiveAndEnabled)
        {
            StartCoroutine(Shoot());
        }
    }
    public IEnumerator Shoot()
    {
        bool completed = false;

        ShootHook();
        animator.SetBool("IsShooting", IsShooting);
        bodyAimConstraint.weight = 1;


        while (!completed)
        {
            aimTarget.localPosition = Vector3.Lerp(aimTarget.localPosition, shootAimingTargetPos, Time.deltaTime * 30f);
            if (Vector3.Distance(aimTarget.localPosition, shootAimingTargetPos) > 0.05f)
            {
                yield return null;
            }
            else
            {
                completed = true;
            }
        }

        IsShooting = false;
        animator.SetBool("IsShooting", IsShooting);
        bodyAimConstraint.weight = 0;

    }
    public void AimToRun()
    {
        aimTarget.localPosition = Vector3.Lerp(aimTarget.localPosition, runAimingTargetPos, Time.deltaTime * 30f);
    }
}
