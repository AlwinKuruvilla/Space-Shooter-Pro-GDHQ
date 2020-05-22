using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    private Animator _anim;

    // Start is called before the first frame update
    void Start() {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxis("Horizontal") < 0) {
            _anim.SetBool("TurnLeft", true);
            _anim.SetBool("TurnRight", false);
        }
        else if (Input.GetAxis("Horizontal") > 0) {
            _anim.SetBool("TurnLeft", false);
            _anim.SetBool("TurnRight", true);
        }
        else {
            _anim.SetBool("TurnLeft", false);
            _anim.SetBool("TurnRight", false);
        }
    }
}
