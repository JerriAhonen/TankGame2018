//using System;
//using System.Collections;
//using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.AI
{
    public class ShootState : AIStateBase
    {
        public float SqrShootingDistance
        {
            get { return Owner.ShootingDistance * Owner.ShootingDistance; }
        }

        public ShootState(EnemyUnit owner)
            : base(owner, AIStateType.Shoot)
        {
            AddTransition(AIStateType.Patrol);
            AddTransition(AIStateType.FollowTarget);
        }

        public override void Update()
        {
            if (!ChangeState())
            {
                Owner.Mover.Move(Owner.transform.forward);                  // Moving
                Owner.Mover.Turn(Owner.Target.transform.position);          // Turning
                Owner.Weapon.Shoot();                                       // Shooting
            }
        }

        private bool ChangeState()
        {
            Vector3 toPlayerVector = Owner.transform.position - Owner.Target.transform.position;
            float sqrDistanceToPlayer = toPlayerVector.sqrMagnitude;
            
            // If player dead, go to patrol state
            if (Owner.Target.gameObject.activeSelf == false)                // Check if player activeSelf false. If yes, player dead.
            {
                Owner.Target = null;                                        // Set the target to null
                return Owner.PerformTransition(AIStateType.Patrol);         // Go to Patrol state
            }
            
            // If not at shooting range, go to follow state.
            if (sqrDistanceToPlayer > SqrShootingDistance)
            {
                return Owner.PerformTransition(AIStateType.FollowTarget);   // Go to Follow state
            }

            // Otherwise return false.
            return false;
        }
    }
}