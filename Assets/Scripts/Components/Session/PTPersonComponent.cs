using UnityEngine;

    public class PTPersonComponent : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public delegate void PersonDeathDelegate();
        private PersonDeathDelegate personDeathTrigger;

        public void InitComponent(PersonDeathDelegate personDeathTrigger)
        {
            this.personDeathTrigger = personDeathTrigger;
        }

        public void Move(Vector3 vector)
        {
            transform.position += vector;
        }

       
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("FinishLine"))
            {
                personDeathTrigger?.Invoke();
                animator.SetBool("Death", true);
                //SessionCore.LoseSession(transform.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length);
            }
        }
    }