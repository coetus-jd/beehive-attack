using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bee.Enemies
{
    public class AI : MonoBehaviour
    {
        [Tooltip("Comb Object")]
        [SerializeField] private Transform m_Target;
        [Tooltip("Double array for the path")]
        [SerializeField] private TransformArray[] m_Position;
        [Tooltip("Enemy speed")]
        [SerializeField] private float m_SpeedMove;
        [Tooltip("Choose the way you want")]
        [SerializeField] private int m_ChoosenWay = 0;

        private bool m_Retreat;

        /// <summary>
        /// Index for the waypoint for Enemy walk
        /// </summary>
        private int m_WayIndex = 0;
        void Start()
        {
            //Enemy start in the first position of the waypoints.
            transform.position = m_Position[m_ChoosenWay].Array[m_WayIndex].transform.position;
            m_Retreat = false;
        }

        
        void Update()
        {
            Move();
            
        }

        private void FixedUpdate()
        {

        }

        private void Move()
        {

            if(m_Retreat == true)
            {
                m_WayIndex = 0;

                float randomWay = Random.Range(0, 2);
                if (randomWay == 1)
                    m_ChoosenWay += 1;
                else if (randomWay == 2)
                    m_ChoosenWay -= 1;

                if (m_ChoosenWay < 0)
                    m_ChoosenWay = m_Position.Length;



            }
            //Verify if it doesn't arrive in the final index
            else if (m_WayIndex <= m_Position[m_ChoosenWay].Array.Length - 1 && m_Retreat == false)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    m_Position[m_ChoosenWay].Array[m_WayIndex].transform.position,
                    m_SpeedMove * Time.deltaTime);

                if (transform.position == m_Position[m_ChoosenWay].Array[m_WayIndex].transform.position)
                {
                    m_WayIndex += 1;
                }
            }
            else if (m_WayIndex >= m_Position[m_ChoosenWay].Array.Length && m_Retreat == true)
            {

            }

            //else, it arrives in the final
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
