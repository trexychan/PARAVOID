using System;
using UnityEngine;

public enum SecondaryMechanics {
       DOUBLE_JUMP,
       TIME_SHIFT,
       THIRD_MECHANIC
}

public class SecondaryMechanicUnlock : MonoBehaviour {

       public SecondaryMechanics unlockMechanic = SecondaryMechanics.DOUBLE_JUMP;

       void Start() {
              
       }

       private void OnTriggerEnter(Collider other) {
              if (other.gameObject.CompareTag("Player")) {
                     switch (unlockMechanic) {
                            case(SecondaryMechanics.DOUBLE_JUMP):
                                   other.gameObject.GetComponent<PlayerController>().EnableDoubleJump();
                                   break;
                            case(SecondaryMechanics.TIME_SHIFT):
                                   print("Time shift unlocked");
                                   break;
                            case SecondaryMechanics.THIRD_MECHANIC:
                                   print("Third mechanic unlocked");
                                   break;
                     }
                     Destroy(gameObject);
              }
       }
}