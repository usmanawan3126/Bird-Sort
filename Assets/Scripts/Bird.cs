using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [HideInInspector]
    public Animator birdAnim;

    private void OnEnable()
    {
        birdAnim = transform.GetChild(0)?.GetComponent<Animator>();
        Debug.Log("Get Bird Anim");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.y > 5.0f)
        {
            Destroy(gameObject);
        }
    }
    public void flipPos()
    {
        Debug.Log("Entered");
        if (this.gameObject.transform.position.x > 0)
        {
            if (gameObject.transform.localScale.x < 0)
            {
                this.gameObject.transform.localScale = new Vector2(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y);

            }
            else
            {
                this.gameObject.transform.localScale = new Vector2(-this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y);
                Debug.Log("Position change Less then 1 changed ");
            }
        }
        else if (gameObject.transform.position.x < 0)
        {
            if (gameObject.transform.localScale.x<0)
            {
                this.gameObject.transform.localScale = new Vector2(-this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y);
            }
            else
            {
                this.gameObject.transform.localScale = new Vector2(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y);

            }
            Debug.Log("Position changed");
        }
        //if (this.gameObject.transform.position.x == 0)
        //{
        //    Debug.Log("equals to zero");
        //}
    }
}
