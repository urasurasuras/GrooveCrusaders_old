using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boosAnimationScript : MonoBehaviour
{
    [SerializeField] Animator bossAnimationController;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            bossAnimationController.SetTrigger("FightStart");
            Debug.Log("boss HIt!!");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bossAnimationController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
