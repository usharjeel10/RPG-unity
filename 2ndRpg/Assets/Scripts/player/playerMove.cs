using RPG.core;
using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace RPG.Movement
{
    public class playerMove : MonoBehaviour ,IAction,ISaveable
    {
        private NavMeshAgent agent;
        private Animator _anim;
        private Health health;
        [SerializeField] private float maxSpeed=6.0f;
        void Start()
        {
            health=GetComponent<Health>();
            _anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            agent.enabled = !health.IsDead();
            calculateMoveAnimation();
        }
        public void startMoveAction(Vector3 destination, float SpeedFraction)
        {
            GetComponent<actionScheduler>().playAction(this);
            Move(destination,SpeedFraction);
        }
        public void Move(Vector3 destination,float speedFraction)
        {
            agent.destination = destination;
            agent.speed = maxSpeed*Mathf.Clamp01(speedFraction);
            Debug.Log(agent.speed);
            agent.isStopped = false;
        }
        public void cancel()
        {
            agent.isStopped= true;
        }
        public void calculateMoveAnimation()
        {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            _anim.SetFloat("playerMove", speed);
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector();
            GetComponent<NavMeshAgent>().enabled=true;
        }
    }
}
